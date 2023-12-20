namespace KMGamesCore.Web.Areas.PayPal.Models
{
    public class Phone
    {
        public string phone_type { get; } = "MOBILE";

        public PhoneNumber phone_number { get; set; }
    }
}
