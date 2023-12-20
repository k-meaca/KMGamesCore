using KMGamesCore.Data.DBContext;
using KMGamesCore.Data.Repository.Interfaces;
using KMGamesCore.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KMGamesCore.Data.Repository
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        //----------PROPERTIES----------//

        private readonly ApplicationDBContext _dbContext;

        //-----------CONSTRUCTOR----------//

        public ShoppingCartRepository(ApplicationDBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
        }

        //----------METHODS----------//

        public bool Exist(int cartId)
        {
            return _dbContext.ShoppingCarts.FirstOrDefault(s => s.ShoppingCartId == cartId) is null? false : true;
        }

        public void RemoveFromCart(int gameId, int cartId)
        {
            GameInCart gameInCart = new()
            {
                GameId = gameId,
                ShoppingCartId = cartId
            };

            _dbContext.GamesInCart.Remove(gameInCart);

            if (_dbContext.GamesInCart.Count(g => g.ShoppingCartId == cartId) == 1)
            {
                var cart = _dbContext.ShoppingCarts.FirstOrDefault(s => s.ShoppingCartId == cartId);

                _dbContext.ShoppingCarts.Remove(cart);

            }

        }



        public void Update(ShoppingCart shoppingCart)
        {
            _dbContext.ShoppingCarts.Update(shoppingCart);
        }

    }
}
