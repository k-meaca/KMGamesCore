namespace KMGamesCore.Web.Areas.PayPal.Models
{
    public class Item
    {
        public string name { get; set; }
        public string quantity { get; set; }

        public UnitAmount unit_amount { get; set; }
    }
}
