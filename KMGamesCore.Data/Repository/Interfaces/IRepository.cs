using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KMGamesCore.Data.Repository.Interfaces
{
    public interface IRepository <T> where T: class
    {
        void Add(T item);

        void Delete(T item);

        T Get(Expression<Func<T,bool>> filter);

        IEnumerable<T> GetAll();

        
    }
}
