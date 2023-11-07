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
    public class CityRepository : Repository<City>, ICityRepository
    {
        //----------PROPERTIES----------//

        private readonly ApplicationDBContext _dbContext;

        //----------CONSTRUCTOR----------//

        public CityRepository(ApplicationDBContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;
        }

        //----------METHODS----------//

        public bool Exist(City city)
        {
            return _dbContext.Cities.Any(c => (c.Name == city.Name && c.CountryId == city.CountryId) &&
                                        c.CityId != city.CityId);
        }

        public bool Exist(int id)
        {
            return _dbSet.Any(c => c.CityId == id);
        }

        public void Update(City city)
        {

            _dbContext.Cities.Update(city);
            
        }
    }
}
