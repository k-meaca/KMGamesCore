using KMGamesCore.Data.Repository.Interfaces;
using KMGamesCore.Web.Models;
using KMGamesCore.Web.ViewModel.HomeVM;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KMGamesCore.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;


        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {

            var games = _unitOfWork.Games.GetAll().Take(3).ToList();

            HomeVm homeVm = new HomeVm()
            {
                CategoriesInfo = _unitOfWork.Categories.GetInfoCategories(),
                BestSeller = games
            };

            return View(homeVm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}