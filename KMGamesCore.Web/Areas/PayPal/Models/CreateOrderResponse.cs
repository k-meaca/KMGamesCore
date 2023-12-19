namespace KMGamesCore.Web.Areas.PayPal.Models
{
    public class CreateOrderResponse
    {
        public string id { get; set; }
        public string status { get; set; }
        public List<Link> links { get; set; }
    }
}
