using KMGamesCore.Data.DBContext;
using KMGamesCore.Data.Repository.Interfaces;
using KMGamesCore.Models.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace KMGamesCore.Data.Repository
{
    public class GameRepository : Repository<Game>, IGameRepository
    {
        //----------PROPERTIES----------//

        private readonly ApplicationDBContext _dbContext;

        //----------CONSTRUCTOR----------//

        public GameRepository(ApplicationDBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
        }

        //----------METHODS----------//

        public bool Exist(Game game)
        {
            return _dbContext.Games.Any(g => g.Title == game.Title && g.GameId != game.GameId);
        }

        public bool Exist(int id)
        {
            return _dbContext.Games.Any(g => g.GameId == id);
        }

        public IEnumerable<Game> GetAllGames()
        {
            return _dbContext.Games.Include("GameCategories")
                                   .Include("PlayersGames");
                                   
        }

        public Game GetGameById(int id)
        {
            return _dbContext.Games.Include("GameCategories")
                                   .Include("PlayersGames")
                                   .FirstOrDefault(g => g.GameId == id);
        }

        public void Update(Game game)
        {
            _dbContext.Update(game);
        }
    }
}
