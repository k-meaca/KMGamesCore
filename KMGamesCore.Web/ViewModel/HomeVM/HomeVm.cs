using KMGamesCore.Models.Models;

namespace KMGamesCore.Web.ViewModel.HomeVM
{
    public class HomeVm
    {

        public List<Game> BestSeller { get; set; }

        public Dictionary<Category, (string Image,int Items)> CategoriesInfo { get; set; }

    }
}
