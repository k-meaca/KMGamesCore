using KMGamesCore.Models.Models;

namespace KMGamesCore.Web.Areas.PayPal.Models
{
    public class ShoppingCartPayPal
    {
        public int ShoppingCartId { get; set; }

        public string ApplicationUserId { get; set; }

        public List<Game> Games { get; set; }
    }
}
