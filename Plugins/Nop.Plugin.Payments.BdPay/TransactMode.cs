namespace Nop.Plugin.Payments.BdPay;

public enum TransactMode
{
    Pending = 0,

    Authorize = 1,

    AuthorizeAndCapture = 2
}
