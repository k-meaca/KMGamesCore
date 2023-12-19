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
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUsersRepository
    {
        //----------PROPERTIES----------//

        private readonly ApplicationDBContext _dBContext;

        //----------CONSTRUCTOR----------//

        public ApplicationUserRepository(ApplicationDBContext dBContext) : base(dBContext)
        {
            _dBContext = dBContext;
        }
    }
}
