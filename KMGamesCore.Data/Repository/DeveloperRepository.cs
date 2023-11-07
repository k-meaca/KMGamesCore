using KMGamesCore.Data.DBContext;
using KMGamesCore.Data.Repository.Interfaces;
using KMGamesCore.Models.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KMGamesCore.Data.Repository
{
    public class DeveloperRepository : Repository<Developer>, IDeveloperRepository
    {
        //----------PROPERTIES----------//

        private readonly ApplicationDBContext _dbContext;

        //----------CONSTRUCTOR----------//

        public DeveloperRepository(ApplicationDBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
        }

        //----------METHODS----------//

        public bool Exist(Developer developer)
        {
            return _dbContext.Developers.Any(d => d.Name == developer.Name && d.DeveloperId != developer.DeveloperId);
        }

        public bool Exist(int id)
        {
            return _dbContext.Developers.Any(d => d.DeveloperId == id);
        }

        public IEnumerable<Developer> GetDevelopers()
        {
            return _dbContext.Developers.Include("Country")
                                        .Include("City");
        }

        public void Update(Developer developer)
        {
            _dbContext.Developers.Update(developer);
        }

    }
}
