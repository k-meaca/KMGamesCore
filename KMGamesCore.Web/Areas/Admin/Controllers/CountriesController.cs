using KMGamesCore.Data.Repository.Interfaces;
using KMGamesCore.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace KMGamesCore.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CountriesController : Controller
    {
        //----------PROPERTIES----------//

        private readonly IUnitOfWork _unitOfWork;

        //----------CONSTRUCTOR----------//

        public CountriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //----------ACTIONS----------//

        public IActionResult Index()
        {
            var countries = _unitOfWork.Countries.GetAll();

            return View(countries);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Country country)
        {
            if (!ModelState.IsValid)
            {
                return View(country);
            }

            if (_unitOfWork.Countries.Exist(country))
            {
                ModelState.AddModelError(string.Empty, "There already exists a country with that name.");

                return View(country);
            }

            _unitOfWork.Countries.Add(country);
            _unitOfWork.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }

            Country country = _unitOfWork.Countries.Get(c => c.CountryId == id);

            if (country is null)
            {
                return NotFound();
            }

            return View(country);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Country country)
        {
            if (!ModelState.IsValid)
            {
                return View(country);
            }

            if (_unitOfWork.Countries.Exist(country))
            {
                ModelState.AddModelError(string.Empty, "There already exists a country with that name.");

                return View(country);
            }

            _unitOfWork.Countries.Update(country);
            _unitOfWork.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }

            Country country = _unitOfWork.Countries.Get(c => c.CountryId == id);

            if (country is null)
            {
                return NotFound();
            }

            return View(country);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Country country)
        {
            if (!_unitOfWork.Countries.Exist(country.CountryId))
            {
                return NotFound();
            }
            else
            {
                _unitOfWork.Countries.Delete(country);
                _unitOfWork.SaveChanges();

                return RedirectToAction("Index");
            }

        }
    }
}
