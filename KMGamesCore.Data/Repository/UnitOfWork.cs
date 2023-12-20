using KMGamesCore.Data.DBContext;
using KMGamesCore.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KMGamesCore.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        //----------PROPERTIES----------//

        private readonly ApplicationDBContext? _dbContext = null;

        public IApplicationUsersRepository ApplicationUsers {  get; private set; }
        public ICategoryRepository Categories { get; private set; }
        public ICityRepository Cities { get; private set; }
        public ICountryRepository Countries { get; private set; }

        public ICountryCodeRepository CountriesCode { get; private set; }
        public IDeveloperRepository Developers { get; private set; }
        public IGameRepository Games { get; private set; }
        public IPlayerTypeRepository PlayerTypes { get; private set; }
        public ISalesRepository Sales { get; private set; }
        public IShoppingCartRepository ShoppingCarts { get; private set; }

        //----------CONSTRUCTOR----------//

        public UnitOfWork(ApplicationDBContext dbContext) 
        {
            _dbContext = dbContext;

            ApplicationUsers = new ApplicationUserRepository(dbContext);
            Categories = new CategoryRepository(dbContext);
            Cities = new CityRepository(dbContext);
            Countries = new CountryRepository(dbContext);
            CountriesCode = new CountryCodeRepository(dbContext);
            Developers = new DeveloperRepository(dbContext);
            Games = new GameRepository(dbContext);
            PlayerTypes = new PlayerTypeRepository(dbContext);
            Sales = new SaleRepository(dbContext);
            ShoppingCarts = new ShoppingCartRepository(dbContext);
        }

        //----------METHODS----------//

        //----PRIVATE----//



        //----PUBLIC----//

        public void BeginTransaction() 
        { 
            _dbContext.Database.BeginTransaction();
        }

        public void CommitChanges()
        {
            _dbContext.Database.CommitTransaction();
        }

        public void RollbackChanges()
        {
            _dbContext.Database.RollbackTransaction();
        }


        public void SaveChanges()
        {
            _dbContext?.SaveChanges();
        }
    }
}
