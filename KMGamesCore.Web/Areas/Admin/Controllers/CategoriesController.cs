using KMGamesCore.Data.Repository.Interfaces;
using KMGamesCore.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace KMGamesCore.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        //----------PROPERTIES-----------//

        private readonly IUnitOfWork _unitOfWork;
        
        //----------CONSTRUCTOR----------//

        public CategoriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //----------ACTIONS----------//

        public IActionResult Index()
        {
            var categories = _unitOfWork.Categories.GetAll();

            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            if (_unitOfWork.Categories.Exist(category))
            {
                ModelState.AddModelError(string.Empty, "There already exist that category.");

                return View(category);
            }

            _unitOfWork.Categories.Add(category);
            _unitOfWork.SaveChanges();

            TempData["SUCCESS"] = "Category added successfully";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id is null || id == 0)
            {
                return NotFound();
            }

            Category category = _unitOfWork.Categories.Get(c => c.CategoryId == id);

            if(category is null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            if (_unitOfWork.Categories.Exist(category))
            {
                ModelState.AddModelError(string.Empty, "There already exist a category with that name.");

                return View(category);
            }

            _unitOfWork.Categories.Update(category);
            _unitOfWork.SaveChanges();

            TempData["SUCCESS"] = "Category edited successfully";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }

            Category category = _unitOfWork.Categories.Get(c => c.CategoryId == id);

            if (category is null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category category)
        {
            if (! _unitOfWork.Categories.Exist(category.CategoryId))
            {
                return NotFound();
            }

            _unitOfWork.Categories.Delete(category);
            _unitOfWork.SaveChanges();

            TempData["WARNING"] = "Category was deleted";

            return RedirectToAction("Index");
        }
    }
}
