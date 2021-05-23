using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepository<T> where T: class
    {
        public Task<IEnumerable<T>> GetAllAsync();

        public Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> condition = null);

        public Task CreateAsync(T entity);

        public Task<T> ReadAsync(int id);

        public void Update(T entity);

        public void Delete(T entity);

        public void Delete(int id);
    }
}
