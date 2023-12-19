using KMGamesCore.Models.Models;

namespace KMGamesCore.Web.ViewModel.BuyGameVm
{
    public class GameDetailVm
    {
        public Game Game { get; set; }

        public List<Game> GamesRelated { get; set; }
    }
}
