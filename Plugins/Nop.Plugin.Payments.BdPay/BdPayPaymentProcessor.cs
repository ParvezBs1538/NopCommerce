using FluentValidation;
using Microsoft.AspNetCore.Http;
using Nop.Core;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Payments;
using Nop.Plugin.Payments.BdPay.Components;
using Nop.Plugin.Payments.BdPay.Domain;
using Nop.Plugin.Payments.BdPay.Models;
using Nop.Plugin.Payments.BdPay.Services;
using Nop.Plugin.Payments.BdPay.Validators;
using Nop.Services.Configuration;
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

    public BdPayPaymentProcessor(ILocalizationService localizationService,
        IOrderTotalCalculationService orderTotalCalculationService,
        ISettingService settingService,
        IWebHelper webHelper,
        BdPayPaymentSettings bdPayPaymentSettings,
        IPaymentInfoServices paymentInfoServices)
    {
        _localizationService = localizationService;
        _bdPayPaymentSettings = bdPayPaymentSettings;
        _settingService = settingService;
        _webHelper = webHelper;
        _orderTotalCalculationService = orderTotalCalculationService;
        _paymentInfoServices = paymentInfoServices;
    }

    public bool SupportCapture => false;

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

    public Task<CapturePaymentResult> CaptureAsync(CapturePaymentRequest capturePaymentRequest)
    {
        return Task.FromResult(new CapturePaymentResult { Errors = new[] { "Capture method not supported" } });
    }

    public async Task<decimal> GetAdditionalHandlingFeeAsync(IList<ShoppingCartItem> cart)
    {
        return await _orderTotalCalculationService.CalculatePaymentAdditionalFeeAsync(cart,
            _bdPayPaymentSettings.AdditionalFee, _bdPayPaymentSettings.AdditionalFeePercentage);
    }

    public Task<ProcessPaymentRequest> GetPaymentInfoAsync(IFormCollection form)
    {
        var request = new ProcessPaymentRequest();
        request.CustomValues["Account type"] = form[nameof(PaymentInfoModel.AccountType)].ToString();
        request.CustomValues["Number"] = form[nameof(PaymentInfoModel.MobileNumber)].ToString();
        request.CustomValues["Txn ID"] = form[nameof(PaymentInfoModel.TransactionId)].ToString();

        return Task.FromResult(request);
    }

    public async Task<string> GetPaymentMethodDescriptionAsync()
    {
        return await _localizationService.GetResourceAsync("Plugins.Payments.BdPay.PaymentMethodDescription");
    }

    public Type GetPublicViewComponent()
    {
        return typeof(BdPayViewComponent);
    }
    
    public Task<bool> HidePaymentMethodAsync(IList<ShoppingCartItem> cart)
    {
        return Task.FromResult(false);
    }

    public Task<ProcessPaymentResult> ProcessPaymentAsync(ProcessPaymentRequest processPaymentRequest)
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
    public Task PostProcessPaymentAsync(PostProcessPaymentRequest postProcessPaymentRequest)
    {
        return Task.CompletedTask;
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

    public async Task<IList<string>> ValidatePaymentFormAsync(IFormCollection form)
    {
        var warnings = new List<string>();

        // Instantiate the validator (assume it takes a localization service for localization purposes)
        var validator = new PaymentInfoValidator(_localizationService);

        // Capture form data into the PaymentInfoModel for validation
        var model = new PaymentInfoModel
        {
            MobileNumber = form[nameof(PaymentInfoModel.MobileNumber)].ToString(),
            AccountType = form[nameof(PaymentInfoModel.AccountType)].ToString(),
            TransactionId = form[nameof(PaymentInfoModel.TransactionId)].ToString()
        };

        // Perform validation on the model
        var validationResult = validator.Validate(model);

        // If validation fails, collect the error messages
        if (!validationResult.IsValid)
        {
            warnings.AddRange(validationResult.Errors.Select(error => error.ErrorMessage));
            return warnings;  // Return warnings if validation fails
        }

        // If validation succeeds, transfer data to the domain class
        var domain = new PaymentInfo
        {
            //MobileNumber = model.MobileNumber,
            //AccountType = model.AccountType,
            //TransactionId = model.TransactionId

            MobileNumber = form[nameof(PaymentInfo.MobileNumber)].ToString(),
            AccountType = form[nameof(PaymentInfo.AccountType)].ToString(),
            TransactionId = form[nameof(PaymentInfo.TransactionId)].ToString()
        };

        // Insert the valid domain object into the database
        await _paymentInfoServices.InsertPaymentInfoAsync(domain);

        // Return any warnings (empty if validation succeeded)
        return warnings;
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
