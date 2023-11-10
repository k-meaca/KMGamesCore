using KMGamesCore.Data.DBContext;
using KMGamesCore.Data.Repository.Interfaces;
using KMGamesCore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGamesCore.Data.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        //----------PROPERTIES----------//

        private readonly ApplicationDBContext _dbContext;

        //----------CONSTRUCTOR----------//

        public CategoryRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        //----------METHODS----------//

        public bool Exist(Category category)
        {
            return _dbContext.Categories.Any(c => c.Name == category.Name && c.CategoryId != category.CategoryId);
        }

        public bool Exist(int id)
        {
            return _dbContext.Categories.Any(c => c.CategoryId == id);
        }

        public void Update(Category category)
        {
            _dbContext.Update(category);
        }
    }
}
