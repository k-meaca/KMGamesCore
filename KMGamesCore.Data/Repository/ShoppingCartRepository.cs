using KMGamesCore.Data.DBContext;
using KMGamesCore.Data.Repository.Interfaces;
using KMGamesCore.Models.Models;
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

        public void RemoveFromCart(int gameId, int cartId)
        {
            GameInCart gameInCart = new()
            {
                GameId = gameId,
                ShoppingCartId = cartId
            };

            _dbContext.GamesInCart.Remove(gameInCart);
        }


        public void Update(ShoppingCart shoppingCart)
        {
            _dbContext.ShoppingCarts.Update(shoppingCart);
        }

    }
}
