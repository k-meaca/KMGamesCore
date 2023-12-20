using KMGamesCore.Models.Models;

namespace KMGamesCore.Web.ViewModel.SaleVM
{
    public class SaleDetailVM
    {
        public Sale Sale { get; set; }

        public List<Game> Games { get; set; }
    }
}
