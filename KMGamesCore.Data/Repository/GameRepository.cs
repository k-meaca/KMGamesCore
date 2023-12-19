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

        public void DeleteGame(Game game)
        {
            //_dbContext.GameCategories.RemoveRange(_dbContext.GameCategories.Where(gc => gc.GameId == game.GameId));
            //_dbContext.PlayersGames.RemoveRange(_dbContext.PlayersGames.Where(pg => pg.GameId == game.GameId));

            Delete(game);
        }

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
                                   .Include("PlayersGames")
                                   .Include("Developer");
                                   
        }

        public Game GetGameById(int id)
        {
            return _dbContext.Games.Include("GameCategories")
                                   .Include("PlayersGames")
                                   .Include("Developer")
                                   .FirstOrDefault(g => g.GameId == id);
        }

        public void Update(Game game)
        {
            //_dbContext.Entry(game).State = EntityState.Modified;

            _dbContext.GameCategories.RemoveRange(_dbContext.GameCategories.Where(gc => gc.GameId == game.GameId));
            _dbContext.PlayersGames.RemoveRange(_dbContext.PlayersGames.Where(pg => pg.GameId == game.GameId));

            _dbContext.Update(game);
        }

        public List<Game> GetGamesForCategory(int categoryId)
        {
            var games = GetAllGames().Where(g => g.GameCategories.Any(gc => gc.CategoryId == categoryId));

            return games.ToList();
        }

        public Game GetGameWithDetails(int? gameId)
        {

            Game game = GetGameById(gameId.Value);

            if(game is null)
            {
                throw new Exception();
            }

            foreach(var type in game.PlayersGames)
            {
                game.PlayerTypes.Add(_dbContext.PlayerTypes.FirstOrDefault(pt => pt.PlayerTypeId == type.PlayerTypeId));
            }

            foreach (var category in game.GameCategories)
            {
                game.Categories.Add(_dbContext.Categories.FirstOrDefault(gc => gc.CategoryId == category.CategoryId));
            }

            return game;
        }

        public List<Game> GetGamesRelated(Game game)
        {
            List<Game> gamesTypes = _dbContext.Games.Where(
                                                        g => g.PlayersGames.Any(pt =>
                                                                pt.PlayerTypeId == game.PlayerTypes.FirstOrDefault().PlayerTypeId
                                                                && pt.GameId != game.GameId))
                                                    .Take(3).ToList();

            List<Game> gamesCategories = _dbContext.Games.Where(
                                            g => g.GameCategories.Any(pg =>
                                                    pg.CategoryId == game.GameCategories.FirstOrDefault().CategoryId
                                                    && pg.GameId != game.GameId))
                                        .Skip(3).Take(3).ToList();

            return gamesTypes.Union(gamesCategories).ToList();

        }

        public bool ItsRelated(Game game) 
        {
            return _dbContext.SalesDetails.Any(sd => sd.GameId == game.GameId);
        }
    }
}
