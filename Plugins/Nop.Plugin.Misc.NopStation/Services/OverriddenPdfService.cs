using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Tax;
using Nop.Core.Domain.Vendors;
using Nop.Core.Infrastructure;
using Nop.Core;
using Nop.Services.Attributes;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Directory;
using Nop.Services.Helpers;
using Nop.Services.Html;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Orders;
using Nop.Services.Payments;
using Nop.Services.Shipping;
using Nop.Services.Stores;
using Nop.Services.Vendors;
using Nop.Core.Domain.Stores;
using Nop.Services.Common.Pdf;
using QuestPDF.Helpers;
using QuestPDF.Fluent;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Orders;

namespace Nop.Plugin.Misc.NopStation.Services;

public class OverriddenPdfService : PdfService
{
    public OverriddenPdfService(AddressSettings addressSettings,
        CatalogSettings catalogSettings,
        CurrencySettings currencySettings,
        IAddressService addressService,
        IAttributeFormatter<AddressAttribute, AddressAttributeValue> addressAttributeFormatter,
        ICountryService countryService,
        ICurrencyService currencyService,
        IDateTimeHelper dateTimeHelper,
        IGiftCardService giftCardService,
        IHtmlFormatter htmlFormatter,
        ILanguageService languageService,
        ILocalizationService localizationService,
        IMeasureService measureService,
        INopFileProvider fileProvider,
        IOrderService orderService,
        IPaymentPluginManager paymentPluginManager,
        IPaymentService paymentService,
        IPictureService pictureService,
        IPriceFormatter priceFormatter,
        IProductService productService,
        IRewardPointService rewardPointService,
        ISettingService settingService,
        IShipmentService shipmentService,
        IStateProvinceService stateProvinceService,
        IStoreContext storeContext,
        IStoreService storeService,
        IVendorService vendorService,
        IWorkContext workContext,
        MeasureSettings measureSettings,
        TaxSettings taxSettings,
        VendorSettings vendorSettings) : base(
            addressSettings,
            catalogSettings,
            currencySettings,
            addressService,
            addressAttributeFormatter,
            countryService,
            currencyService,
            dateTimeHelper,
            giftCardService,
            htmlFormatter,
            languageService,
            localizationService,
            measureService,
            fileProvider,
            orderService,
            paymentPluginManager,
            paymentService,
            pictureService,
            priceFormatter,
            productService,
            rewardPointService,
            settingService,
            shipmentService,
            stateProvinceService,
            storeContext,
            storeService,
            vendorService,
            workContext,
            measureSettings,
            taxSettings,
            vendorSettings
        )
    {

    }

    public override async Task PrintOrderToPdfAsync(Stream stream, Order order, Language language = null, Store store = null, Vendor vendor = null)
    {
        ArgumentNullException.ThrowIfNull(order);

        //store info
        store ??= await _storeContext.GetCurrentStoreAsync();

        var orderStore = order.StoreId == 0 || order.StoreId == store?.Id ?
            store : await _storeService.GetStoreByIdAsync(order.StoreId);

        //language info
        language ??= await _languageService.GetLanguageByIdAsync(order.CustomerLanguageId);

        if (language?.Published != true)
            language = await _workContext.GetWorkingLanguageAsync();

        //by default _pdfSettings contains settings for the current active store
        //and we need PdfSettings for the store which was used to place an order
        //so let's load it based on a store of the current order
        var pdfSettingsByStore = await _settingService.LoadSettingAsync<PdfSettings>(orderStore.Id);

        byte[] logo = null;
        var logoPicture = await _pictureService.GetPictureByIdAsync(pdfSettingsByStore.LogoPictureId);
        if (logoPicture != null)
        {
            logo = await _pictureService.LoadPictureBinaryAsync(logoPicture);

            if (logoPicture.MimeType == MimeTypes.ImageSvg)
            {
                await using var logoStream = new MemoryStream(logo);
                logo = await _pictureService.ConvertSvgToPngAsync(logoStream);
            }
        }

        var date = await _dateTimeHelper.ConvertToUserTimeAsync(order.CreatedOnUtc, DateTimeKind.Utc);

        //a vendor should have access only to products
        var orderItems = await _orderService.GetOrderItemsAsync(order.Id, vendorId: vendor?.Id ?? 0);

        var column1Lines = string.IsNullOrEmpty(pdfSettingsByStore.InvoiceFooterTextColumn1) ?
            new List<string>()
            : pdfSettingsByStore.InvoiceFooterTextColumn1
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

        var column2Lines = string.IsNullOrEmpty(pdfSettingsByStore.InvoiceFooterTextColumn2) ?
            new List<string>()
            : pdfSettingsByStore.InvoiceFooterTextColumn2
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

        var source = new InvoiceSource()
        {
            //StoreUrl = orderStore.Url?.Trim('/'),
            StoreUrl = "www.masudparvez.com",
            Language = language,
            FontFamily = pdfSettingsByStore.FontFamily,
            OrderDateUser = date,
            LogoData = logo,
            OrderNumberText = order.CustomOrderNumber,
            PageSize = pdfSettingsByStore.LetterPageSizeEnabled ? PageSizes.Letter : PageSizes.A4,
            BillingAddress = await GetBillingAddressAsync(vendor, language, order),
            ShippingAddress = await GetShippingAddressAsync(language, order),
            Products = await GetOrderProductItemsAsync(order, orderItems, language),
            ShowSkuInProductList = _catalogSettings.ShowSkuOnProductDetailsPage,
            ShowVendorInProductList = _vendorSettings.ShowVendorOnOrderDetailsPage,
            CheckoutAttributes = vendor is null ? _htmlFormatter.ConvertHtmlToPlainText(order.CheckoutAttributeDescription, true, true) : string.Empty, //vendors cannot see checkout attributes
            Totals = vendor is null ? await GetTotalsAsync(language, order) : new(), //vendors cannot see totals
            OrderNotes = await GetOrderNotesAsync(pdfSettingsByStore, order, language),
            FooterTextColumn1 = column1Lines,
            FooterTextColumn2 = column2Lines
        };

        await using var pdfStream = new MemoryStream();
        new InvoiceDocument(source, _localizationService)
            .GeneratePdf(pdfStream);

        pdfStream.Position = 0;
        await pdfStream.CopyToAsync(stream);
    }

    protected override async Task<InvoiceTotals> GetTotalsAsync(Language lang, Order order)
    {
        var result = await base.GetTotalsAsync(lang, order);
        var orderTotalInCustomerCurrency = _currencyService.ConvertCurrency(order.OrderTotal, order.CurrencyRate);
        var orderTotalStr = await _priceFormatter.FormatPriceAsync(orderTotalInCustomerCurrency, true, order.CustomerCurrencyCode, false, lang.Id);
        result.OrderTotal = orderTotalStr;

        return result;
    }
}
