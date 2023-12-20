using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGamesCore.Data.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        //----------PROPERTIES----------//

        IApplicationUsersRepository ApplicationUsers {  get; }
        ICategoryRepository Categories { get; }
        ICityRepository Cities { get; }
        ICountryRepository Countries { get; }

        ICountryCodeRepository CountriesCode { get; }
        IDeveloperRepository Developers { get; }
        IGameRepository Games { get; }
        IPlayerTypeRepository PlayerTypes { get; }
        ISalesRepository Sales { get; }
        IShoppingCartRepository ShoppingCarts { get; }


        //----------METHODS----------//

        void BeginTransaction();

        void CommitChanges();

        void RollbackChanges();

        void SaveChanges();
    }
}
