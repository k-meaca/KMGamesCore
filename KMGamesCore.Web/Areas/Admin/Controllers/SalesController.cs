using KMGamesCore.Data.Repository.Interfaces;
using KMGamesCore.Models.Models;
using KMGamesCore.Web.ViewModel.SaleVM;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace KMGamesCore.Web.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class SalesController : Controller
    {
        //----------PROPERTIES----------//

        private readonly IUnitOfWork _unitOfWork;

        //----------CONSTRUCTOR----------//

        public SalesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //----------ACTIONS----------//

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Details(int? saleId)
        {
            if(saleId is null or <= 0)
            {
                return NotFound();
            }

            var sale = _unitOfWork.Sales.Get(s => s.SaleId == saleId, "SalesDetails");

            sale.ApplicationUser = _unitOfWork.ApplicationUsers.Get(a => a.Id == sale.ApplicationUserId);

            //var games = new List<Game>();

            //foreach(var detail in sale.SalesDetails)
            //{
            //    var game = _unitOfWork.Games.GetGameById(detail.GameId);

            //    game.Developer = _unitOfWork.Developers.Get(d => d.DeveloperId == game.DeveloperId);

            //    games.Add(game);
            //}

            //var saleVm = new SaleDetailVM()
            //{
            //    Sale = sale,
            //    Games = games
            //};

            return View(sale);
        }

        #region API CALL

        public IActionResult GetSales()
        {
            var sales = _unitOfWork.Sales.GetAll();


            foreach(var sale in sales)
            {
                sale.ApplicationUser = _unitOfWork.ApplicationUsers.Get(a => a.Id == sale.ApplicationUserId);
            }

            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
            };

            return Json(new { data = sales }, options);
        }

        #endregion
    }
}
