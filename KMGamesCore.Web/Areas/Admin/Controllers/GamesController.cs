using KMGamesCore.Data.Repository.Interfaces;
using KMGamesCore.Models.Models;
using KMGamesCore.Web.ViewModel.GameVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace KMGamesCore.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GamesController : Controller
    {

        //----------PROPERTIES----------//

        private readonly IUnitOfWork _unitOfWork;

        private readonly IWebHostEnvironment _webHostEnvironment;

        //----------CONSTRUCTOR----------//

        public GamesController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;

            _webHostEnvironment = webHostEnvironment;
        }

        //----------METHODS----------//

        private List<SelectListItem> SetSelection(IEnumerable<GameCategory> list)
        {
            var selected = _unitOfWork.Categories.GetAll().Select(c => new SelectListItem()
            {
                Value = c.CategoryId.ToString(),
                Text = c.Name
            }).ToList();

            foreach (var category in list)
            {
                selected.FirstOrDefault(s => s.Value == category.CategoryId.ToString()).Selected = true;
            }

            return selected;
        }

        private List<SelectListItem> SetSelection(IEnumerable<PlayerGame> list)
        {
            var selected = _unitOfWork.PlayerTypes.GetAll().Select(p => new SelectListItem()
            {
                Value = p.PlayerTypeId.ToString(),
                Text = p.Type
            }).ToList();

            foreach (var playerType in list)
            {
                selected.FirstOrDefault(s => s.Value == playerType.PlayerTypeId.ToString()).Selected = true;
            }

            return selected;
        }

        //----------ACTIONS----------//
        public IActionResult Index()
        {
            var games = _unitOfWork.Games.GetAll();

            foreach (var game in games)
            {
                game.Developer = _unitOfWork.Developers.Get(d => d.DeveloperId == game.DeveloperId);
            }

            return View(games);
        }

        [HttpGet]
        public IActionResult Create()
        {
            GameVM game = new GameVM()
            {
                Categories = _unitOfWork.Categories.GetAll()
                                                   .Select(c => new SelectListItem()
                                                   {
                                                       Value = c.CategoryId.ToString(),
                                                       Text = c.Name
                                                   }).ToList(),
                PlayerTypes = _unitOfWork.PlayerTypes.GetAll()
                                                     .Select(p => new SelectListItem()
                                                     {
                                                         Value = p.PlayerTypeId.ToString(),
                                                         Text = p.Type
                                                     }).ToList(),
                Developers = _unitOfWork.Developers.GetAll()
                                                   .Select(d => new SelectListItem()
                                                   {
                                                       Value = d.DeveloperId.ToString(),
                                                       Text = d.Name
                                                   }).ToList()
            };

            return View(game);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GameVM gameVm, IFormFile? file)
        {

            bool categoriesValid = gameVm.Categories.Any(c => c.Selected);
            bool typesValid = gameVm.PlayerTypes.Any(t => t.Selected);

            if (!ModelState.IsValid || !categoriesValid || !typesValid || file is null)
            {

                gameVm.Categories.ForEach(c =>
                {
                    c.Text = _unitOfWork.Categories.Get(cat => cat.CategoryId == int.Parse(c.Value)).Name;
                });

                gameVm.PlayerTypes.ForEach(t =>
                {
                    t.Text = _unitOfWork.PlayerTypes.Get(pt => pt.PlayerTypeId == int.Parse(t.Value)).Type;
                });

                gameVm.Developers = _unitOfWork.Developers.GetAll()
                                                   .Select(d => new SelectListItem()
                                                   {
                                                       Value = d.DeveloperId.ToString(),
                                                       Text = d.Name
                                                   }).ToList();

                if (!categoriesValid)
                {
                    ModelState.AddModelError("Categories", "You must peek at least one category.");
                }

                if (!typesValid)
                {
                    ModelState.AddModelError("PlayerTypes", "You must peek at least one player type.");
                }

                if (file is null)
                {
                    ModelState.AddModelError("Image", "You must upload an image from your files.");
                }

                return View(gameVm);
            }

            List<PlayerGame> types = gameVm.PlayerTypes.Where(t => t.Selected)
                                                       .Select(t => new PlayerGame()
                                                       {
                                                           PlayerTypeId = int.Parse(t.Value)
                                                       }).ToList();

            List<GameCategory> categories = gameVm.Categories.Where(c => c.Selected)
                                                             .Select(c => new GameCategory()
                                                             {
                                                                 CategoryId = int.Parse(c.Value)
                                                             }).ToList();

            Game game = new Game()
            {
                Title = gameVm.Title,
                ActualPrice = gameVm.ActualPrice,
                Release = gameVm.Release,
                DeveloperId = gameVm.DeveloperId,
                PlayersGames = types,
                GameCategories = categories
            };

            if (_unitOfWork.Games.Exist(game))
            {
                gameVm.Categories.ForEach(c =>
                {
                    c.Text = _unitOfWork.Categories.Get(cat => cat.CategoryId == int.Parse(c.Value)).Name;
                });

                gameVm.PlayerTypes.ForEach(t =>
                {
                    t.Text = _unitOfWork.PlayerTypes.Get(pt => pt.PlayerTypeId == int.Parse(t.Value)).Type;
                });

                gameVm.Developers = _unitOfWork.Developers.GetAll()
                                                   .Select(d => new SelectListItem()
                                                   {
                                                       Value = d.DeveloperId.ToString(),
                                                       Text = d.Name
                                                   }).ToList();

                ModelState.AddModelError(string.Empty, "There already exist a game with that name.");

                return View(gameVm);
            }

            var wwwRootPath = _webHostEnvironment.WebRootPath;
            var fileName = Guid.NewGuid().ToString();
            var extension = Path.GetExtension(file.FileName);
            var folder = Path.Combine(wwwRootPath, @"images\games\");

            using (var fileStream = new FileStream(Path.Combine(folder, fileName + extension), FileMode.Create))
            {
                file.CopyTo(fileStream);

            }

            game.Image = fileName + extension;

            try
            {
                using (var transaction = new TransactionScope())
                {
                    _unitOfWork.Games.Add(game);
                    _unitOfWork.SaveChanges();

                    transaction.Complete();
                }

                TempData["SUCCESS"] = "Game added successfully";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                gameVm.Categories.ForEach(c =>
                {
                    c.Text = _unitOfWork.Categories.Get(cat => cat.CategoryId == int.Parse(c.Value)).Name;
                });

                gameVm.PlayerTypes.ForEach(t =>
                {
                    t.Text = _unitOfWork.PlayerTypes.Get(pt => pt.PlayerTypeId == int.Parse(t.Value)).Type;
                });

                gameVm.Developers = _unitOfWork.Developers.GetAll()
                                                   .Select(d => new SelectListItem()
                                                   {
                                                       Value = d.DeveloperId.ToString(),
                                                       Text = d.Name
                                                   }).ToList();


                TempData["ERROR"] = ex.Message;

                return View(gameVm);
            }
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null || id <= 0)
            {
                return NotFound();
            }

            Game game = _unitOfWork.Games.GetGameById(id.Value);

            if (game is null)
            {
                return NotFound();
            }

            GameVM gameVm = new GameVM()
            {
                GameId = game.GameId,
                Title = game.Title,
                ActualPrice = game.ActualPrice,
                Release = game.Release,
                Image = @"\images\games\" + game.Image,
                DeveloperId = game.DeveloperId,
                Developers = _unitOfWork.Developers.GetAll().Select(d => new SelectListItem
                {
                    Value = d.DeveloperId.ToString(),
                    Text = d.Name
                }).ToList(),
                Categories = SetSelection(game.GameCategories),
                PlayerTypes = SetSelection(game.PlayersGames)
            };

            return View(gameVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(GameVM gameVm, IFormFile? file)
        {
            bool categoriesValid = gameVm.Categories.Any(c => c.Selected);
            bool typesValid = gameVm.PlayerTypes.Any(t => t.Selected);

            if (!ModelState.IsValid || !categoriesValid || !typesValid)
            {

                gameVm.Categories.ForEach(c =>
                {
                    c.Text = _unitOfWork.Categories.Get(cat => cat.CategoryId == int.Parse(c.Value)).Name;
                });

                gameVm.PlayerTypes.ForEach(t =>
                {
                    t.Text = _unitOfWork.PlayerTypes.Get(pt => pt.PlayerTypeId == int.Parse(t.Value)).Type;
                });

                gameVm.Developers = _unitOfWork.Developers.GetAll()
                                                   .Select(d => new SelectListItem()
                                                   {
                                                       Value = d.DeveloperId.ToString(),
                                                       Text = d.Name
                                                   }).ToList();

                if (!categoriesValid)
                {
                    ModelState.AddModelError("Categories", "You must peek at least one category.");
                }

                if (!typesValid)
                {
                    ModelState.AddModelError("PlayerTypes", "You must peek at least one player type.");
                }

                //if (file is null)
                //{
                //    ModelState.AddModelError("Image", "You must upload an image from your files.");
                //}

                return View(gameVm);
            }

            List<PlayerGame> types = gameVm.PlayerTypes.Where(t => t.Selected)
                                                       .Select(t => new PlayerGame()
                                                       {
                                                           PlayerTypeId = int.Parse(t.Value)
                                                       }).ToList();

            List<GameCategory> categories = gameVm.Categories.Where(c => c.Selected)
                                                             .Select(c => new GameCategory()
                                                             {
                                                                 CategoryId = int.Parse(c.Value)
                                                             }).ToList();

            Game game = _unitOfWork.Games.Get(g => g.GameId == gameVm.GameId);

            game.Title = gameVm.Title;
            game.ActualPrice = gameVm.ActualPrice;
            game.DeveloperId = gameVm.DeveloperId;
            game.Release = gameVm.Release;
            game.PlayersGames = types;
            game.GameCategories = categories;

            if (_unitOfWork.Games.Exist(game))
            {
                gameVm.Categories.ForEach(c =>
                {
                    c.Text = _unitOfWork.Categories.Get(cat => cat.CategoryId == int.Parse(c.Value)).Name;
                });

                gameVm.PlayerTypes.ForEach(t =>
                {
                    t.Text = _unitOfWork.PlayerTypes.Get(pt => pt.PlayerTypeId == int.Parse(t.Value)).Type;
                });

                gameVm.Developers = _unitOfWork.Developers.GetAll()
                                                   .Select(d => new SelectListItem()
                                                   {
                                                       Value = d.DeveloperId.ToString(),
                                                       Text = d.Name
                                                   }).ToList();

                ModelState.AddModelError(string.Empty, "There already exist a game with that name.");

                return View(gameVm);
            }

            if (file is not null)
            {
                var wwwRootPath = _webHostEnvironment.WebRootPath;
                var fileName = Guid.NewGuid().ToString();
                var extension = Path.GetExtension(file.FileName);
                var folder = Path.Combine(wwwRootPath, @"images\games\");

                System.IO.File.Delete(folder + game.Image);

                using (var fileStream = new FileStream(Path.Combine(folder, fileName + extension), FileMode.Create))
                {

                    file.CopyTo(fileStream);

                }

                game.Image = fileName + extension;
            }


            try
            {
                using (var transaction = new TransactionScope())
                {
                    _unitOfWork.Games.Update(game);
                    _unitOfWork.SaveChanges();

                    transaction.Complete();
                }

                TempData["SUCCESS"] = "Game edited successfully";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                gameVm.Categories.ForEach(c =>
                {
                    c.Text = _unitOfWork.Categories.Get(cat => cat.CategoryId == int.Parse(c.Value)).Name;
                });

                gameVm.PlayerTypes.ForEach(t =>
                {
                    t.Text = _unitOfWork.PlayerTypes.Get(pt => pt.PlayerTypeId == int.Parse(t.Value)).Type;
                });

                gameVm.Developers = _unitOfWork.Developers.GetAll()
                                                   .Select(d => new SelectListItem()
                                                   {
                                                       Value = d.DeveloperId.ToString(),
                                                       Text = d.Name
                                                   }).ToList();


                TempData["ERROR"] = ex.Message;

                return View(gameVm);
            }
        }
    
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null || id <= 0) 
            {
                return NotFound();
            }

            Game game = _unitOfWork.Games.GetGameById(id.Value);

            //game.Developer = _unitOfWork.Developers.Get(d => d.DeveloperId == game.DeveloperId);

            if(game is null)
            {
                return NotFound();
            }

            if(_unitOfWork.Games.ItsRelated(game))
            {
                TempData["ERROR"] = "Can't be deleted because it has related sales.";

                return RedirectToAction("Index");
            }

            return View(game);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Game game)
        {
            if (!_unitOfWork.Games.Exist(game.GameId))
            {
                return NotFound();
            }

            game = _unitOfWork.Games.GetGameById(game.GameId);

            try
            {
                using(var transaction = new TransactionScope())
                {
                    var wwwRootPath = _webHostEnvironment.WebRootPath;

                    var folder = Path.Combine(wwwRootPath, @"images\games\");

                    System.IO.File.Delete(folder + game.Image);

                    _unitOfWork.Games.Delete(game);

                    _unitOfWork.SaveChanges();

                    transaction.Complete();
                }

                TempData["WARNING"] = "Game was deleted";

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["ERROR"] = ex.Message;

                return View(game);
            }
        }


    }

}