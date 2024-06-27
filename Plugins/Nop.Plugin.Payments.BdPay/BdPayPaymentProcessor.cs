using LinqToDB.Common.Internal.Cache;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Payments;
using Nop.Plugin.Payments.BdPay.Components;
using Nop.Plugin.Payments.BdPay.Domain;
using Nop.Plugin.Payments.BdPay.Models;
using Nop.Plugin.Payments.BdPay.Services;
using Nop.Plugin.Payments.BdPay.Validators;
using Nop.Services.Configuration;
using Nop.Services.Customers;
using Nop.Services.Localization;
using Nop.Services.Orders;
using Nop.Services.Payments;
using Nop.Services.Plugins;

namespace Nop.Plugin.Payments.BdPay;

public class BdPayPaymentProcessor : BasePlugin, IPaymentMethod
{
    protected readonly ILocalizationService _localizationService;
    protected readonly IOrderTotalCalculationService _orderTotalCalculationService;
    protected readonly ISettingService _settingService;
    protected readonly IWebHelper _webHelper;
    protected readonly BdPayPaymentSettings _bdPayPaymentSettings;
    private readonly IPaymentInfoServices _paymentInfoServices;
    private readonly IMemoryCache _memoryCache;
    private readonly IOrderService _orderService;
    private readonly IStaticCacheManager _staticCacheManager;
    private readonly IWorkContext _workContext;
    private readonly ICustomerService _customerService;

    public BdPayPaymentProcessor(ILocalizationService localizationService,
        IOrderTotalCalculationService orderTotalCalculationService,
        ISettingService settingService,
        IWebHelper webHelper,
        BdPayPaymentSettings bdPayPaymentSettings,
        IPaymentInfoServices paymentInfoServices,
        IMemoryCache memoryCache,
        IStaticCacheManager staticCacheManager,
        IWorkContext workContext,
        IOrderService orderService,
        ICustomerService customerService)
    {
        _localizationService = localizationService;
        _bdPayPaymentSettings = bdPayPaymentSettings;
        _settingService = settingService;
        _webHelper = webHelper;
        _orderTotalCalculationService = orderTotalCalculationService;
        _paymentInfoServices = paymentInfoServices;
        _memoryCache = memoryCache;
        _orderService = orderService;
        _staticCacheManager = staticCacheManager;
        _workContext = workContext;
        _customerService = customerService;
    }

    public bool SupportCapture => true;

    public bool SupportPartiallyRefund => false;

    public bool SupportRefund => false;

    public bool SupportVoid => false;

    public RecurringPaymentType RecurringPaymentType => RecurringPaymentType.Manual;

    public PaymentMethodType PaymentMethodType => PaymentMethodType.Standard;

    public bool SkipPaymentInfo => false;

    public Task<CancelRecurringPaymentResult> CancelRecurringPaymentAsync(CancelRecurringPaymentRequest cancelPaymentRequest)
    {
        return Task.FromResult(new CancelRecurringPaymentResult());
    }


    public Task<bool> CanRePostProcessPaymentAsync(Order order)
    {
        ArgumentNullException.ThrowIfNull(order);

        return Task.FromResult(false);
    }


    // It is used in the public store to validate customer input. It returns a list of warnings.
    public Task<IList<string>> ValidatePaymentFormAsync(IFormCollection form)
    {
        var warnings = new List<string>();

        // Instantiate the validator (assume it takes a localization service for localization purposes)
        var validator = new PaymentInfoValidator(_localizationService);

        // Capture form data into the PaymentInfoModel for validation
        var model = new PaymentInfoModel
        {
            MobileNumber = form[nameof(PaymentInfoModel.MobileNumber)].ToString(),
            AccountType = form[nameof(PaymentInfoModel.AccountType)].ToString(),
            TransactionId = form[nameof(PaymentInfoModel.TransactionId)].ToString(),
            CustomerId = 0,
            OrderId = 0
        };

        // Perform validation on the model
        var validationResult = validator.Validate(model);

        // If validation fails, collect the error messages
        if (!validationResult.IsValid)
        {
            warnings.AddRange(validationResult.Errors.Select(error => error.ErrorMessage));
            return Task.FromResult<IList<string>>(warnings);  // Return warnings if validation fails
        }

        return Task.FromResult<IList<string>>(warnings);
    }

    // It is used in the public store to parse customer input.
    public async Task<ProcessPaymentRequest> GetPaymentInfoAsync(IFormCollection form)
    {
        var request = new ProcessPaymentRequest();
        request.CustomValues["Account type"] = form[nameof(PaymentInfoModel.AccountType)].ToString();
        request.CustomValues["Number"] = form[nameof(PaymentInfoModel.MobileNumber)].ToString();
        request.CustomValues["Txn ID"] = form[nameof(PaymentInfoModel.TransactionId)].ToString();

        return request;
    }

    public async Task<CapturePaymentResult> CaptureAsync(CapturePaymentRequest capturePaymentRequest)
    {
        var result = new CapturePaymentResult();

        try
        {
            // Retrieve the order from the request
            var order = capturePaymentRequest.Order;
            if (order == null)
            {
                result.AddError("Order cannot be loaded");
                return result;
            }

            // Ensure the order is in a state that can be captured
            if (order.PaymentStatus != PaymentStatus.Authorized)
            {
                result.AddError("Cannot capture a payment that is not authorized");
                return result;
            }

            // Update payment status to paid
            order.PaymentStatus = PaymentStatus.Paid;

            // Update order status to complete
            order.OrderStatus = OrderStatus.Complete;

            // Save changes to the order
            await _orderService.UpdateOrderAsync(order);

            var paymentInfo = await _paymentInfoServices.GetPaymentInfoByOrderIdAsync(order.Id);
            await _paymentInfoServices.DeletePaymentInfoAsync(paymentInfo);

            // Set new payment status in the result
            result.NewPaymentStatus = PaymentStatus.Paid;
        }
        catch (Exception ex)
        {
            result.AddError($"An error occurred while capturing the payment: {ex.Message}");
        }

        return result;
    }


    public async Task<decimal> GetAdditionalHandlingFeeAsync(IList<ShoppingCartItem> cart)
    {
        return await _orderTotalCalculationService.CalculatePaymentAdditionalFeeAsync(cart,
            _bdPayPaymentSettings.AdditionalFee, _bdPayPaymentSettings.AdditionalFeePercentage);
    }


// gets payment method description which will be displayed on checkout pages in the public store
    public async Task<string> GetPaymentMethodDescriptionAsync()
    {
        return await _localizationService.GetResourceAsync("Plugins.Payments.BdPay.PaymentMethodDescription");
    }

    public Type GetPublicViewComponent()
    {
        return typeof(BdPayViewComponent);
    }

// If you doesn't want to access a certain country customer. 
    public Task<bool> HidePaymentMethodAsync(IList<ShoppingCartItem> cart)
    {
        return Task.FromResult(false);
    }

// This methon is use before customer places an order.
    public async Task<ProcessPaymentResult> ProcessPaymentAsync(ProcessPaymentRequest processPaymentRequest)
    {
        var result = new ProcessPaymentResult
        {
            AllowStoringCreditCardNumber = true
        };
        switch (_bdPayPaymentSettings.TransactMode)
        {
            case TransactMode.Pending:
                result.NewPaymentStatus = PaymentStatus.Pending;
                break;
            case TransactMode.Authorize:
                result.NewPaymentStatus = PaymentStatus.Authorized;
                break;
            case TransactMode.AuthorizeAndCapture:
                result.NewPaymentStatus = PaymentStatus.Paid;
                break;
            default:
                result.AddError("Not supported transaction type");
                break;
        }

        var customer = await _workContext.GetCurrentCustomerAsync();
        var customVal = processPaymentRequest.CustomValues;

        var domain = new PaymentInfo
        {
            MobileNumber = customVal["Number"].ToString(), 
            AccountType = customVal["Account type"].ToString(),
            TransactionId = customVal["Txn ID"].ToString(),
            CustomerId = customer.Id,
            OrderId = 0,
        };

        _memoryCache.Set("Model", domain, TimeSpan.FromMinutes(5));

        return result;
    }

    public async Task PostProcessPaymentAsync(PostProcessPaymentRequest postProcessPaymentRequest)
    {
        var order = postProcessPaymentRequest.Order;
        var model = _memoryCache.Get<PaymentInfo>("Model");

        model.OrderId = order.Id;

        await _paymentInfoServices.InsertPaymentInfoAsync(model);
    }

    public Task<ProcessPaymentResult> ProcessRecurringPaymentAsync(ProcessPaymentRequest processPaymentRequest)
    {
        var result = new ProcessPaymentResult
        {
            AllowStoringCreditCardNumber = true
        };
        switch (_bdPayPaymentSettings.TransactMode)
        {
            case TransactMode.Pending:
                result.NewPaymentStatus = PaymentStatus.Pending;
                break;
            case TransactMode.Authorize:
                result.NewPaymentStatus = PaymentStatus.Authorized;
                break;
            case TransactMode.AuthorizeAndCapture:
                result.NewPaymentStatus = PaymentStatus.Paid;
                break;
            default:
                result.AddError("Not supported transaction type");
                break;
        }

        return Task.FromResult(result);
    }

    public Task<RefundPaymentResult> RefundAsync(RefundPaymentRequest refundPaymentRequest)
    {
        return Task.FromResult(new RefundPaymentResult { Errors = new[] { "Refund method not supported" } });
    }

    public Task<VoidPaymentResult> VoidAsync(VoidPaymentRequest voidPaymentRequest)
    {
        return Task.FromResult(new VoidPaymentResult { Errors = new[] { "Void method not supported" } });
    }

    public override string GetConfigurationPageUrl()
    {
        return $"{_webHelper.GetStoreLocation()}Admin/PaymentBdPay/Configure";
    }

    public override async Task InstallAsync()
    {
        //settings
        var settings = new BdPayPaymentSettings
        {
            TransactMode = TransactMode.Pending
        };
        await _settingService.SaveSettingAsync(settings);

        //locales
        await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
        {
            ["Plugins.Payments.BdPay.Instructions"] = "This payment method stores credit card information in database (it's not sent to any third-party processor). In order to store credit card information, you must be PCI compliant.",
            ["Plugins.Payments.BdPay.Fields.AdditionalFee"] = "Additional fee",
            ["Plugins.Payments.BdPay.Fields.AdditionalFee.Hint"] = "Enter additional fee to charge your customers.",
            ["Plugins.Payments.BdPay.Fields.AdditionalFeePercentage"] = "Additional fee. Use percentage",
            ["Plugins.Payments.BdPay.Fields.AdditionalFeePercentage.Hint"] = "Determines whether to apply a percentage additional fee to the order total. If not enabled, a fixed value is used.",
            ["Plugins.Payments.BdPay.Fields.TransactMode"] = "After checkout mark payment as",
            ["Plugins.Payments.BdPay.Fields.TransactMode.Hint"] = "Specify transaction mode.",
            ["Plugins.Payments.BdPay.PaymentMethodDescription"] = "Pay by credit / debit card",
            ["Payment.SelectAccount"] = "Select Account",
            ["Payment.MobileNumber"] = "Mobile Number",
            ["Payment.TransactionId"] = "Transaction ID"
        });

        await base.InstallAsync();
    }

    public override async Task UninstallAsync()
    {
        //settings
        await _settingService.DeleteSettingAsync<BdPayPaymentSettings>();

        //locales
        await _localizationService.DeleteLocaleResourcesAsync("Plugins.Payments.BdPay");

        await base.UninstallAsync();
    }
}
