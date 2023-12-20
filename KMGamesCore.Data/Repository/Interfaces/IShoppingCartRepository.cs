using KMGamesCore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGamesCore.Data.Repository.Interfaces
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        void RemoveFromCart(int gameId, int cartId);

        bool Exist(int cartId);

        void Update(ShoppingCart shoppingCart);
    }
}
