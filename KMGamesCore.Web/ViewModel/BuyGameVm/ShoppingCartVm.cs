using KMGamesCore.Models.Models;

namespace KMGamesCore.Web.ViewModel.BuyGameVm
{
    public class ShoppingCartVm
    {
        public int ShoppingCartId { get; set; }

        public string ApplicationUserId { get; set; }

        public List<Game> Games { get; set; } = new List<Game>();
    }
}
