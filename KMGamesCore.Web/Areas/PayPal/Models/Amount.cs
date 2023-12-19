namespace KMGamesCore.Web.Areas.PayPal.Models
{
    public class Amount
    {
        public string currency_code { get; set; }
        public string value { get; set; }

        public Breakdown breakdown { get; set; }
    }
}
