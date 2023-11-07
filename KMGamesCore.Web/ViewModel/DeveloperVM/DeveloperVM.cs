using KMGamesCore.Models.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KMGamesCore.Web.ViewModel.DeveloperVM
{
    public class DeveloperVM
    {
        public Developer Developer { get; set; }

        public IEnumerable<SelectListItem>? Countries { get; set; }

        public IEnumerable<SelectListItem>? Cities { get; set; }
    }
}
