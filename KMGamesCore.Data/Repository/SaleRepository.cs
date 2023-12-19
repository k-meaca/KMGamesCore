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
    public class SaleRepository : Repository<Sale>, ISalesRepository
    {
        //----------PROPERTIES----------//

        private readonly ApplicationDBContext _dbContext;

        //----------CONSTRUCTOR----------//
        public SaleRepository(ApplicationDBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
        }
    }
}
