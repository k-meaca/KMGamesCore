using KMGamesCore.Data.Repository.Interfaces;
using KMGamesCore.Models.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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

        public IActionResult IndexDTable()
        {
            return View();
        }

        #region CREATE

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

            TempData["SUCCESS"] = "Country added successfully";

            return RedirectToAction("Index");
        }

        #endregion

        #region EDIT

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

            TempData["SUCCESS"] = "Country edited successfully";

            return RedirectToAction("Index");
        }

        #endregion

        #region DELETE

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

                TempData["WARNING"] = "Country was deleted";

                return RedirectToAction("Index");
            }
        }



        #endregion

        #region API CALL

        [HttpGet]
        public IActionResult GetCountries()
        {
            var countries = _unitOfWork.Countries.GetAll();

            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            return Json( new { data = countries }, options);
        }

        [HttpDelete]
        public IActionResult DeleteCountry(int? id)
        {
            if (id is null || id <= 0)
            {
                return Json(new { success = false, message = "Not found" });
            }

            Country country = _unitOfWork.Countries.Get(c => c.CountryId == id);

            if (country is null)
            {
                return Json(new { success = false, message = "Not found" });
            }

            try
            {
                _unitOfWork.Countries.Delete(country);
                _unitOfWork.SaveChanges();

                return Json(new { success = true, message = "Country deleted successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

        }

        #endregion
    }
}
