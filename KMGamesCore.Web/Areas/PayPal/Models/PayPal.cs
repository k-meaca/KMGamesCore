namespace KMGamesCore.Web.Areas.PayPal.Models
{
    public class PayPal
    {
        public Name name { get; set; }
        public string email_address { get; set; }

        public Address address { get; set; }
    }
}
