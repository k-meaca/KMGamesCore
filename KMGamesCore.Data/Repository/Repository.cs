using KMGamesCore.Data.DBContext;
using KMGamesCore.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KMGamesCore.Data.Repository
{
    public class Repository<T> : IRepository<T> where T: class
    {
        //----------PROPERTIES-----------//

        private readonly ApplicationDBContext _dbContext;

        internal DbSet<T> _dbSet;

        //----------CONSTRUCTOR----------//

        public Repository(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
            _dbSet = _dbContext.Set<T>();
        }

        //----------METHODS----------//

        public void Add(T item)
        {
            _dbSet.Add(item);
        }

        public void Delete(T item)
        {
            _dbSet.Remove(item);
        }

        public T Get(Expression<Func<T,bool>> filter)
        {
            return _dbSet.Where(filter).FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

    }
}
