using KMGamesCore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGamesCore.Data.Repository.Interfaces
{
    public interface IGameRepository : IRepository<Game>
    {

        bool Exist(Game game);

        bool Exist(int id);

        IEnumerable<Game> GetAllGames();

        Game GetGameById(int id);

        public List<Game> GetGamesForCategory(int categoryId);

        void Update(Game game);
    }
}
