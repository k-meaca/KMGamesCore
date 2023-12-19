using KMGamesCore.Data.Repository.Interfaces;
using KMGamesCore.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace KMGamesCore.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PlayerTypesController : Controller
    {
        //----------PROPERTIES----------//

        private readonly IUnitOfWork _unitOfWork;

        //----------CONSTRUCTOR----------//

        public PlayerTypesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //----------ACTIONS----------//

        public IActionResult Index()
        {
            var types = _unitOfWork.PlayerTypes.GetAll();

            return View(types);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PlayerType playerType)
        {
            if (!ModelState.IsValid)
            {
                return View(playerType);
            }

            if (_unitOfWork.PlayerTypes.Exist(playerType))
            {
                ModelState.AddModelError(string.Empty, "There already exist a player type with that name.");

                return View(playerType);
            }

            _unitOfWork.PlayerTypes.Add(playerType);
            _unitOfWork.SaveChanges();

            TempData["SUCCESS"] = "Player Type added successfully";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id is null || id <= 0)
            {
                return NotFound();
            }

            PlayerType type = _unitOfWork.PlayerTypes.Get(t => t.PlayerTypeId == id);

            if(type is null)
            {
                return NotFound();
            }

            return View(type);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PlayerType playerType)
        {
            if (!ModelState.IsValid)
            {
                return View(playerType);
            }

            if (_unitOfWork.PlayerTypes.Exist(playerType))
            {
                ModelState.AddModelError(string.Empty, "There already exist a player type with that name.");

                return View(playerType);
            }

            _unitOfWork.PlayerTypes.Update(playerType);
            _unitOfWork.SaveChanges();

            TempData["SUCCESS"] = "Player Type edited successfully";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if(id is null || id <= 0)
            {
                return NotFound();
            }

            PlayerType type = _unitOfWork.PlayerTypes.Get(t => t.PlayerTypeId == id);

            if(type is null)
            {
                return NotFound();
            }

            if (_unitOfWork.PlayerTypes.ItsRelated(type))
            {
                TempData["Error"] = "Can't be deleted because it has related games";

                return RedirectToAction("Index");
            }

            return View(type);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete (PlayerType playerType)
        {
            if (!_unitOfWork.PlayerTypes.Exist(playerType.PlayerTypeId))
            {
                return NotFound();
            }

            _unitOfWork.PlayerTypes.Delete(playerType);
            _unitOfWork.SaveChanges();

            TempData["WARNING"] = "Player Type was deleted";

            return RedirectToAction("Index");
        }
    }
}
