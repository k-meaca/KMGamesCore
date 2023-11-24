using KMGamesCore.Data.Repository.Interfaces;
using KMGamesCore.Models.Models;
using KMGamesCore.Web.ViewModel.CityVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KMGamesCore.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CitiesController : Controller
    {
        //----------PROPERTIES----------//

        private readonly IUnitOfWork _unitOfWork;

        //----------CONTRUCTOR----------//

        public CitiesController(IUnitOfWork unitOfwork)
        {
            _unitOfWork = unitOfwork;
        }


        //----------ACTIONS----------//
        public IActionResult Index()
        {
            var cities = _unitOfWork.Cities.GetAll();
            var countries = _unitOfWork.Countries.GetAll();

            foreach (var city in cities)
            {
                city.Country = countries.FirstOrDefault(c => c.CountryId == city.CountryId);
            }

            return View(cities);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CityEditVm cityVm = new CityEditVm()
            {
                Countries = _unitOfWork.Countries.GetAll()
                                                 .Select(c =>
                                                 {
                                                     return new SelectListItem()
                                                     {
                                                         Value = c.CountryId.ToString(),
                                                         Text = c.Name
                                                     };
                                                 })

            };

            return View(cityVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CityEditVm cityVm)
        {
            if (!ModelState.IsValid)
            {
                cityVm.Countries = _unitOfWork.Countries.GetAll()
                                                 .Select(c =>
                                                 {
                                                     return new SelectListItem()
                                                     {
                                                         Value = c.CountryId.ToString(),
                                                         Text = c.Name
                                                     };
                                                 });

                return View(cityVm);
            }

            if (_unitOfWork.Cities.Exist(cityVm.City))
            {
                ModelState.AddModelError(string.Empty, "This city already exist in that country.");

                cityVm.Countries = _unitOfWork.Countries.GetAll()
                                 .Select(c =>
                                 {
                                     return new SelectListItem()
                                     {
                                         Value = c.CountryId.ToString(),
                                         Text = c.Name
                                     };
                                 });

                return View(cityVm);
            }

            _unitOfWork.Cities.Add(cityVm.City);
            _unitOfWork.SaveChanges();

            TempData["SUCCESS"] = "City added successfully";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {

            if (id is null)
            {
                return NotFound();
            }

            City city = _unitOfWork.Cities.Get(c => c.CityId == id);

            if (city is null)
            {
                return NotFound();
            }

            CityEditVm cityVm = new CityEditVm()
            {
                City = city,
                Countries = _unitOfWork.Countries.GetAll()
                                                 .Select(c =>
                                                 {
                                                     return new SelectListItem()
                                                     {
                                                         Value = c.CountryId.ToString(),
                                                         Text = c.Name
                                                     };
                                                 })
            };

            return View(cityVm);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(CityEditVm cityVm)
        {
            if (!ModelState.IsValid)
            {
                cityVm.Countries = _unitOfWork.Countries.GetAll()
                 .Select(c =>
                 {
                     return new SelectListItem()
                     {
                         Value = c.CountryId.ToString(),
                         Text = c.Name
                     };
                 });

                return View(cityVm);
            }

            if (_unitOfWork.Cities.Exist(cityVm.City))
            {
                ModelState.AddModelError(string.Empty, "This city already exist in that country.");

                cityVm.Countries = _unitOfWork.Countries.GetAll()
                                 .Select(c =>
                                 {
                                     return new SelectListItem()
                                     {
                                         Value = c.CountryId.ToString(),
                                         Text = c.Name
                                     };
                                 });

                return View(cityVm);
            }


            _unitOfWork.Cities.Update(cityVm.City);
            _unitOfWork.SaveChanges();

            TempData["SUCCESS"] = "City edited successfully";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }

            City city = _unitOfWork.Cities.Get(c => c.CityId == id);

            if (city is null)
            {
                return NotFound();
            }

            city.Country = _unitOfWork.Countries.Get(c => c.CountryId == city.CountryId);

            return View(city);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(City city)
        {
            if (!_unitOfWork.Cities.Exist(city.CityId))
            {
                return NotFound();
            }
            else
            {
                _unitOfWork.Cities.Delete(city);
                _unitOfWork.SaveChanges();

                TempData["WARNING"] = "City was deleted";

                return RedirectToAction("Index");
            }

        }
    }
}