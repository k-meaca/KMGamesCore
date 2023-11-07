using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGamesCore.Data.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        //----------PROPERTIES----------//

        ICountryRepository Countries { get; }
        ICityRepository Cities { get; }
        IDeveloperRepository Developers { get; }

        //----------METHODS----------//

        void SaveChanges();
    }
}
