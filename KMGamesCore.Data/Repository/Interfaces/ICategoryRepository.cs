using KMGamesCore.Models.Models;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGamesCore.Data.Repository.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        bool Exist(Category category);

        bool Exist(int id);

        void Update(Category category);
    }
}
