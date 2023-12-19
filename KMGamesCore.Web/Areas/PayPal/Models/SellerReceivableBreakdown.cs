namespace KMGamesCore.Web.Areas.PayPal.Models
{
    public class SellerReceivableBreakdown
    {
        public string status { get; set; }
        public List<Link> dispute_categories { get; set; }
    }
}
