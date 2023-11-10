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
    public class PlayerTypeRepository : Repository<PlayerType>, IPlayerTypeRepository
    {
        //----------PROPERTIES----------//

        private readonly ApplicationDBContext _dbContext;

        //----------CONSTRUCTOR----------//

        public PlayerTypeRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        //----------METHODS----------//
        public bool Exist(PlayerType type)
        {
            return _dbContext.PlayerTypes.Any(t => t.Type == type.Type && t.PlayerTypeId != type.PlayerTypeId);
        }

        public bool Exist(int id)
        {
            return _dbContext.PlayerTypes.Any(t=> t.PlayerTypeId == id);
        }

        public void Update(PlayerType type)
        {
            _dbContext.Update(type);
        }
    }
}
