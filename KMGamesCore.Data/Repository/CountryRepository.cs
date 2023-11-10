using KMGamesCore.Data.DBContext;
using KMGamesCore.Data.Repository.Interfaces;
using KMGamesCore.Models.Models;

namespace KMGamesCore.Data.Repository
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        //----------PROPERTIES----------//

        private readonly ApplicationDBContext _dbContext;

        //----------CONSTRUCTOR----------//

        public CountryRepository(ApplicationDBContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;
        }

        //----------METHODS----------//

        public bool Exist(Country country)
        {
            return _dbContext.Countries.Any(c => c.Name == country.Name && c.CountryId != country.CountryId);
        }

        public bool Exist(int id)
        {
            return _dbContext.Countries.Any(c => c.CountryId == id);
        }

        public void Update(Country country)
        {
            _dbContext.Countries.Update(country);
        }
    }
}
