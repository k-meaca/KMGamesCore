using KMGamesCore.Data.Repository.Interfaces;
using KMGamesCore.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace KMGamesCore.Web.Areas.Customer.Controllers
{

    [Area("Customer")]
    public class BuyGamesController : Controller
    {

        //----------PROPERTIES----------//

        private readonly IUnitOfWork _unitOfWork;

        //----------CONSTRUCTOR----------//

        public BuyGamesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //----------ACTIONS----------//

        public IActionResult Index(int? categoryId = null)
        {

            List<Game> games;

            if(categoryId == null)
            {
                games = _unitOfWork.Games.GetAllGames().ToList();
            }
            else
            {
                games = _unitOfWork.Games.GetGamesForCategory(categoryId.Value);
            }

            return View(games);
        }
    }
}
