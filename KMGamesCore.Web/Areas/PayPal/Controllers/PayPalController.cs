using KMGamesCore.Data.Repository.Interfaces;
using KMGamesCore.Models.Models;
using KMGamesCore.Web.Areas.PayPal.Models;
using KMGamesCore.Web.ViewModel.BuyGameVm;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Security.Claims;


namespace KMGamesCore.Web.Areas.PayPal.Controllers
{

    [Area("PayPal")]
    public class PayPalController : Controller
    {

        //----------PROPERTIES----------//

        private readonly IUnitOfWork _unitOfWork;
        private readonly PayPalClient _payPalClient;
        //private ShoppingCartPayPal _shoppingCart;

        //----------CONSTRUCTOR----------//

        public PayPalController(IUnitOfWork unitOfWork, PayPalClient payPalClient)
        {
            _unitOfWork = unitOfWork;
            _payPalClient = payPalClient;
        }

        //----------ACTIONS----------//

        public async Task<IActionResult> Index(int? cartId)
        {
            
            // USED TO GET PAYPAL CHECKOUT JAVASCRIPT SDK

            ViewBag.ClientId = _payPalClient.ClientId;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Order(CancellationToken cancellationToken)
        {
            try
            {

                var userId = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;

                var applicationUser = _unitOfWork.ApplicationUsers.Get(a => a.Id == userId);

                applicationUser.Country = _unitOfWork.Countries.Get(c => c.CountryId == applicationUser.CountryId);

                ShoppingCart cart = _unitOfWork.ShoppingCarts.Get(c => c.ApplicationUserId == userId, "GamesInCart");

                List<Game> games = new List<Game>();

                foreach (var gameInCart in cart.GamesInCart)
                {
                    games.Add(_unitOfWork.Games.Get(g => g.GameId == gameInCart.GameId));
                }

                // SET THE TRANSACTION PRICE AND CURRENCY

                var price = games.Sum(g => g.ActualPrice).ToString().Replace(',', '.');

                var currency = "USD";

                // "reference" IS THE TRANSACTION KEY

                var reference = "REF" + cart.ShoppingCartId.ToString();

                //var countries = _unitOfWork.CountriesCode.GetAll();

                var countryCode = _unitOfWork.CountriesCode.Get(cc => cc.Country == applicationUser.Country.Name.ToUpper());

                var code = countryCode.Code;

                PayPal.Models.PayPal paypal = new()
                {
                    name = new()
                    {
                        given_name = applicationUser.FirstName,
                        surname = applicationUser.LastName
                    },
                    email_address = applicationUser.Email,
                    address = new()
                    {
                        country_code = code,
                        address_line_1 = applicationUser.StreetAddress,
                        admin_area_1 = _unitOfWork.Cities.Get(c => c.CityId == applicationUser.CityId).Name,
                        postal_code = applicationUser.ZipCode
                    },
                    phone = new()
                    {
                        phone_number = new()
                        {
                            national_number = applicationUser.PhoneNumber
                        }
                    }
                    
                };


                PaymentSource paymentSource = new()
                {
                    paypal = paypal
                };


                List<Item> items = new();

                foreach(var game in games)
                {
                    items.Add(new Item()
                    {
                        name = game.Title,
                        quantity = "1",
                        unit_amount = new()
                        {
                            currency_code = currency,
                            value = game.ActualPrice.ToString().Replace(",", ".")
                        }
                    });
                }

                PurchaseUnit purchaseUnit = new()
                {
                    amount = new()
                    {
                        currency_code = currency,
                        value = price,
                        breakdown = new()
                        {
                            item_total = new()
                            {
                                currency_code = currency,
                                value = price
                            }
                        }
                    },
                    reference_id = reference,
                    items = items
                };

                var response = await _payPalClient.CreateOrder(purchaseUnit, paymentSource);

                return Ok(response);
            }
            catch(Exception ex)
            {
                var error = new
                {
                    ex.GetBaseException().Message
                };

                return BadRequest(error);
            }
        }

        public async Task<IActionResult> Capture(string orderId, CancellationToken cancellationToken)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                var response = await _payPalClient.CaptureOrder(orderId);

                var reference = response.purchase_units[0].reference_id;

                var userId = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;

                var cart = _unitOfWork.ShoppingCarts.Get(c => c.ApplicationUserId == userId, "GamesInCart");

                var games = new List<Game>();

                var details = new List<SaleDetail>();

                var purchasedGames = new List<PurchasedGame>();

                foreach(var gameInCart in cart.GamesInCart)
                {
                    var game = _unitOfWork.Games.Get(g => g.GameId == gameInCart.GameId);
                    games.Add(game);
                    details.Add(new SaleDetail()
                    {
                        GameId = game.GameId,
                        GamePrice = game.ActualPrice
                    });
                    purchasedGames.Add(new PurchasedGame()
                    {
                        GameId = game.GameId,
                        Purchased = DateTime.Now
                    });
                }

                Sale sale = new Sale()
                {
                    ApplicationUserId = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value,
                    PayPalId = response.id,
                    Total = games.Sum(g => g.ActualPrice),
                    Date = DateTime.Now,
                    SalesDetails = details
                };

                _unitOfWork.Sales.Add(sale);

                _unitOfWork.SaveChanges();

                var applicationUser = _unitOfWork.ApplicationUsers.Get(a => a.Id == userId);

                _unitOfWork.ApplicationUsers.AddGamesTo(applicationUser, purchasedGames);

                _unitOfWork.SaveChanges();

                _unitOfWork.ShoppingCarts.Delete(cart);

                _unitOfWork.SaveChanges();

                _unitOfWork.CommitChanges();

                return Ok(response);
            }
            catch(SqlException sqlEx)
            {
                _unitOfWork.RollbackChanges();

                return BadRequest(sqlEx.Message);
            }
            catch(Exception ex)
            {
                _unitOfWork.RollbackChanges();

                var error = new
                {
                    ex.GetBaseException().Message
                };

                return BadRequest(error);
            }
        }

        public IActionResult Success()
        {
            return View();
        }

    }
}
