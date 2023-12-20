using KMGamesCore.Data.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace KMGamesCore.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class PurchasedGamesController : Controller
    {
        //----------PROPERTIES----------//

        private readonly IUnitOfWork _unitOfWork;

        //----------CONSTRUCTOR----------//

        public PurchasedGamesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //----------ACTIONS----------//

        public IActionResult Index()
        {
            return View();
        }

        #region API CALL

        public IActionResult GetMyGames()
        {
            var userId = User.Claims.First().Value;

            var purchasedGames = _unitOfWork.Games.GetPurchasedGamesFor(userId);

            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
            };

            return Json(new { data = purchasedGames }, options);
        }

        #endregion

    }
}
