using KMGamesCore.Data.DBContext;
using KMGamesCore.Data.Repository.Interfaces;
using KMGamesCore.Models.Models;
using Microsoft.EntityFrameworkCore;
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

        public Dictionary<Category, (string, int)> GetInfoCategories()
        {
            var categories = _dbContext.Categories.ToList();

            Dictionary<Category, (string Image, int Items)> categoriesAndgames = new();

            var games = _dbContext.Games.Include("GameCategories").ToList();

            foreach (var category in categories)
            {

                var game = games.FirstOrDefault(g =>
                                g.GameCategories.Any(g => g.Category.CategoryId == category.CategoryId));

                if (game is not null)
                {
                    int indexGame = games.FindIndex(g => g.GameId == game.GameId);

                    games.RemoveRange(indexGame, 1);

                    string image = @"/images/games/" + game.Image;

                    int items = _dbContext.GameCategories.Where(gc => gc.CategoryId == category.CategoryId).Count();

                    categoriesAndgames.Add(category, (image, items));
                }
            };

            return categoriesAndgames;
        }

    }
}
