using KMGamesCore.Data.DBContext;
using KMGamesCore.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KMGamesCore.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        //----------PROPERTIES----------//

        private readonly ApplicationDBContext? _dbContext = null;

        public ICategoryRepository Categories { get; private set; }
        public ICityRepository Cities { get; private set; }
        public ICountryRepository Countries { get; private set; }
        public IDeveloperRepository Developers { get; private set; }
        public IGameRepository Games { get; private set; }
        public IPlayerTypeRepository PlayerTypes { get; private set; }

        //----------CONSTRUCTOR----------//

        public UnitOfWork(ApplicationDBContext dbContext) 
        {
            _dbContext = dbContext;

            Categories = new CategoryRepository(dbContext);
            Cities = new CityRepository(dbContext);
            Countries = new CountryRepository(dbContext);
            Developers = new DeveloperRepository(dbContext);
            Games = new GameRepository(dbContext);
            PlayerTypes = new PlayerTypeRepository(dbContext);
        }

        //----------METHODS----------//

        //----PRIVATE----//



        //----PUBLIC----//
        public void SaveChanges()
        {
            _dbContext?.SaveChanges();
        }
    }
}
