using KMGamesCore.Models.Models;

namespace KMGamesCore.Web.ViewModel.BuyGameVm
{
    public class GameIndexVm
    {
        public List<Game> Games { get; set; }

        public string Category { get; set; } = string.Empty;

    }
}
