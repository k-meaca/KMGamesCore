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

        ICategoryRepository Categories { get; }
        ICityRepository Cities { get; }
        ICountryRepository Countries { get; }
        IDeveloperRepository Developers { get; }
        IGameRepository Games { get; }
        IPlayerTypeRepository PlayerTypes { get; }


        //----------METHODS----------//

        void SaveChanges();
    }
}
