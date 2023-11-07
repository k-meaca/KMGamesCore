using KMGamesCore.Data.DBContext;
using KMGamesCore.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KMGamesCore.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        //----------PROPERTIES----------//

        private readonly ApplicationDBContext? _dbContext = null;

        public ICountryRepository Countries { get; private set; }
        public ICityRepository Cities { get; private set; }
        public IDeveloperRepository Developers { get; private set; }

        //----------CONSTRUCTOR----------//

        public UnitOfWork(ApplicationDBContext dbContext) 
        {
            _dbContext = dbContext;
            Countries = new CountryRepository(dbContext);
            Cities = new CityRepository(dbContext);
            Developers = new DeveloperRepository(dbContext);
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
