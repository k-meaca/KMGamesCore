namespace KMGamesCore.Web.Areas.PayPal.Models
{
    public sealed class CreateOrderRequest
    {
        public string intent { get; set; }
        public List<PurchaseUnit> purchase_units { get; set; } = new();

        public PaymentSource payment_source { get; set; }
    }
}
