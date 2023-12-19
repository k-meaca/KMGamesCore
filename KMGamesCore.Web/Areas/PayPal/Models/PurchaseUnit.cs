namespace KMGamesCore.Web.Areas.PayPal.Models
{
    public class PurchaseUnit
    {
        public Amount amount { get; set; }
        public string reference_id { get; set; }

        public List<Item> items { get; set; }

    }
}
