using KMGamesCore.Data.Repository.Interfaces;
using KMGamesCore.Models.Models;
using KMGamesCore.Web.ViewModel.DeveloperVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KMGamesCore.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DevelopersController : Controller
    {
        //----------PROPERTIES----------//

        private readonly IUnitOfWork _unitOfWork;

        //----------CONSTRUCTOR----------//

        public DevelopersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //----------METHODS----------//

        public JsonResult GetCities(int countryId)
        {
            return Json(_unitOfWork.Cities.GetAll().Where(c => c.CountryId == countryId)
                                                   .ToList()
            );

        }

        //----------ACTIONS----------//
        public IActionResult Index()
        {
            var developers = _unitOfWork.Developers.GetDevelopers();

            return View(developers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var countries = _unitOfWork.Countries.GetAll()
                                                 .Select(c =>
                                                 {
                                                     return new SelectListItem()
                                                     {
                                                         Value = c.CountryId.ToString(),
                                                         Text = c.Name
                                                     };
                                                 });

            DeveloperVM developerVm = new DeveloperVM()
            {
                Countries = countries,
                Cities = new List<SelectListItem>()
            };

            return View(developerVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DeveloperVM developerVm)
        {
            if (!ModelState.IsValid)
            {
                developerVm.Countries = _unitOfWork.Countries.GetAll()
                                                             .Select(c =>
                                                             {
                                                                 return new SelectListItem()
                                                                 {
                                                                     Value = c.CountryId.ToString(),
                                                                     Text = c.Name
                                                                 };
                                                             });

                if (!(developerVm.Developer.CountryId == 0))
                {
                    developerVm.Cities = _unitOfWork.Cities.GetAll()
                                                           .Where(c => c.CountryId == developerVm.Developer.CountryId)
                                                           .Select(c =>
                                                           {
                                                               return new SelectListItem()
                                                               {
                                                                   Value = c.CityId.ToString(),
                                                                   Text = c.Name
                                                               };
                                                           });
                }

                return View(developerVm);
            }

            if (_unitOfWork.Developers.Exist(developerVm.Developer))
            {
                developerVm.Countries = _unitOfWork.Countries.GetAll()
                                             .Select(c =>
                                             {
                                                 return new SelectListItem()
                                                 {
                                                     Value = c.CountryId.ToString(),
                                                     Text = c.Name
                                                 };
                                             });


                developerVm.Cities = _unitOfWork.Cities.GetAll()
                                                        .Where(c => c.CountryId == developerVm.Developer.CountryId)
                                                        .Select(c =>
                                                        {
                                                            return new SelectListItem()
                                                            {
                                                                Value = c.CityId.ToString(),
                                                                Text = c.Name
                                                            };
                                                        });


                ModelState.AddModelError(string.Empty, "There already exist a developer with that name.");

                return View(developerVm);
            }

            _unitOfWork.Developers.Add(developerVm.Developer);
            _unitOfWork.SaveChanges();

            TempData["SUCCESS"] = "Developer added successfully";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            Developer developer = _unitOfWork.Developers.Get(d => d.DeveloperId == id);

            if (developer is null)
            {
                return NotFound();
            }

            DeveloperVM developerVm = new DeveloperVM()
            {
                Developer = developer,
                Countries = _unitOfWork.Countries.GetAll()
                                                 .Select(c =>
                                                 {
                                                     return new SelectListItem()
                                                     {
                                                         Value = c.CountryId.ToString(),
                                                         Text = c.Name
                                                     };
                                                 }),
                Cities = _unitOfWork.Cities.GetAll()
                                           .Where(c => c.CountryId == developer.CountryId)
                                           .Select(c =>
                                           {
                                               return new SelectListItem()
                                               {
                                                   Value = c.CityId.ToString(),
                                                   Text = c.Name
                                               };
                                           })
            };

            return View(developerVm);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(DeveloperVM developerVm)
        {
            if (!ModelState.IsValid)
            {
                developerVm.Countries = _unitOfWork.Countries.GetAll()
                                                             .Select(c =>
                                                             {
                                                                 return new SelectListItem()
                                                                 {
                                                                     Value = c.CountryId.ToString(),
                                                                     Text = c.Name
                                                                 };
                                                             });

                if (!(developerVm.Developer.CountryId == 0))
                {
                    developerVm.Cities = _unitOfWork.Cities.GetAll()
                                                           .Where(c => c.CountryId == developerVm.Developer.CountryId)
                                                           .Select(c =>
                                                           {
                                                               return new SelectListItem()
                                                               {
                                                                   Value = c.CityId.ToString(),
                                                                   Text = c.Name
                                                               };
                                                           });
                }

                return View(developerVm);
            }

            if (_unitOfWork.Developers.Exist(developerVm.Developer))
            {
                developerVm.Countries = _unitOfWork.Countries.GetAll()
                                             .Select(c =>
                                             {
                                                 return new SelectListItem()
                                                 {
                                                     Value = c.CountryId.ToString(),
                                                     Text = c.Name
                                                 };
                                             });


                developerVm.Cities = _unitOfWork.Cities.GetAll()
                                                        .Where(c => c.CountryId == developerVm.Developer.CountryId)
                                                        .Select(c =>
                                                        {
                                                            return new SelectListItem()
                                                            {
                                                                Value = c.CityId.ToString(),
                                                                Text = c.Name
                                                            };
                                                        });


                ModelState.AddModelError(string.Empty, "There already exist a developer with that name.");

                return View(developerVm);
            }

            _unitOfWork.Developers.Update(developerVm.Developer);
            _unitOfWork.SaveChanges();

            TempData["SUCCESS"] = "Developer edited successfully";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            Developer developer = _unitOfWork.Developers.Get(d => d.DeveloperId == id);

            if (developer is null)
            {
                return NotFound();
            }

            if (_unitOfWork.Developers.ItsRelated(developer))
            {
                TempData["ERROR"] = "Can't be deleted because it has related games";

                return RedirectToAction("Index");
            }

            developer.Country = _unitOfWork.Countries.Get(c => c.CountryId == developer.CountryId);
            developer.City = _unitOfWork.Cities.Get(c => c.CityId == developer.CityId);

            return View(developer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Developer developer)
        {
            if (!_unitOfWork.Developers.Exist(developer.DeveloperId))
            {
                return NotFound();
            }

            _unitOfWork.Developers.Delete(developer);
            _unitOfWork.SaveChanges();

            TempData["WARNING"] = "Developer was deleted";

            return RedirectToAction("Index");
        }
    }
}
