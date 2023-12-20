using KMGamesCore.Data.Repository.Interfaces;
using KMGamesCore.Models.Models;
using KMGamesCore.Web.ViewModel.BuyGameVm;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using System.Drawing;
using System.Security.Claims;

namespace KMGamesCore.Web.Areas.Customer.Controllers
{

    [Area("Customer")]
    public class BuyGamesController : Controller
    {

        //----------PROPERTIES----------//

        private readonly IUnitOfWork _unitOfWork;

        //----------CONSTRUCTOR----------//

        public BuyGamesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //----------ACTIONS----------//

        public IActionResult Index(int? categoryId = null)
        {

            GameIndexVm games = new();

            if(categoryId == null)
            {
                games.Games = _unitOfWork.Games.GetAllGames().ToList();
            }
            else
            {
                games.Games = _unitOfWork.Games.GetGamesForCategory(categoryId.Value);
                games.Category = _unitOfWork.Categories.Get(c => c.CategoryId == categoryId).Name;
            }

            return View(games);
        }

        [HttpGet]
        public IActionResult Details(int? gameId)
        {
            if(gameId is null || gameId <= 0)
            {
                return NotFound(gameId.ToString());
            }

            try
            {

                Game game = _unitOfWork.Games.GetGameWithDetails(gameId.Value);

                GameDetailVm gameDetails = new()
                {
                    Game = game,

                    GamesRelated = _unitOfWork.Games.GetGamesRelated(game)
                };

                gameDetails.Game.Image = @"/images/games/" + game.Image;

                return View(gameDetails);

            }
            catch (Exception)
            {
                return NotFound(gameId.ToString);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Details(int gameId)
        {
            if (gameId <= 0)
            {
                return NotFound(gameId.ToString());
            }

            GameInCart gameInCart = new GameInCart()
            {
                GameId = gameId,
                Game = _unitOfWork.Games.GetGameById(gameId)
            };

            ShoppingCart cart;

            string userId = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;

            if(await _unitOfWork.ShoppingCarts.Exist(c=> c.ApplicationUserId == userId))
            {
                cart = _unitOfWork.ShoppingCarts.Get(s => s.ApplicationUserId == userId, "GamesInCart");
            
                cart.GamesInCart.Add(gameInCart);

                _unitOfWork.ShoppingCarts.Update(cart);
            }
            else
            {
                cart = new()
                {
                    ApplicationUserId = userId,
                    GamesInCart = new List<GameInCart>()
                };

                cart.GamesInCart.Add(gameInCart);

                _unitOfWork.ShoppingCarts.Add(cart);
            }

            _unitOfWork.SaveChanges();

            return  RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ShowCart(string userId)
        {
            if(string.IsNullOrEmpty(userId))
            {
                return BadRequest();
            }

            ShoppingCart shoppingCart = _unitOfWork.ShoppingCarts.Get(s => s.ApplicationUserId == userId, "GamesInCart");

            ShoppingCartVm shoppingCartVm = null;

            if(shoppingCart is not null)
            {
                shoppingCartVm = new() 
                {
                    ShoppingCartId = shoppingCart.ShoppingCartId,
                    ApplicationUserId = userId
                };

                foreach(var gameIncart in shoppingCart.GamesInCart)
                {
                    shoppingCartVm.Games.Add(_unitOfWork.Games.GetGameById(gameIncart.GameId));
                }
            }


            return View(shoppingCartVm);
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int? gameId, int? cartId)
        {
            if (gameId is null or <= 0 || cartId is null or <= 0)
            {
                return NotFound();
            }

            try
            {
                _unitOfWork.BeginTransaction();

                 _unitOfWork.ShoppingCarts.RemoveFromCart(gameId.Value, cartId.Value);

                _unitOfWork.SaveChanges();

                _unitOfWork.CommitChanges();
                
                if(_unitOfWork.ShoppingCarts.Exist(cartId.Value))
                {

                    return RedirectToAction("ShowCart", "BuyGames", new { userId = User.Claims.First().Value });
                }
                else
                {
                    return RedirectToAction("Index", "BuyGames");
                }
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackChanges();

                return BadRequest(ex.Message);
            }
        }

    }
}
