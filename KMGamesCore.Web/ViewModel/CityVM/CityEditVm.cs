using KMGamesCore.Models.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KMGamesCore.Web.ViewModel.CityVM
{
    public class CityEditVm
    {
        //----------PROPRETIES----------//

        public City? City { get; set; }

        public IEnumerable<SelectListItem>? Countries { get; set; }
    }
}
