using KMGamesCore.Data.DBContext;
using KMGamesCore.Data.Repository.Interfaces;
using KMGamesCore.Models.Models;

namespace KMGamesCore.Data.Repository
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        //----------PROPERTIES----------//


        //----------CONSTRUCTOR----------//

        public CountryRepository(ApplicationDBContext dbContext) : base(dbContext) { }

        //----------METHODS----------//

        public bool Exist(Country country)
        {
            return _dbSet.Any(c => c.Name == country.Name && c.CountryId != country.CountryId);
        }

        public bool Exist(int id)
        {
            return _dbSet.Any(c => c.CountryId == id);
        }

        public void Update(Country country)
        {
            _dbSet.Update(country);
        }
    }
}
