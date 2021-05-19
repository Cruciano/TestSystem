using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IService<T> where T: class
    {
        public Task<T> GetByIdAsync(int id);
        public Task CreateAsync(T dto);
        public Task UpdateAsync(T dto);
        public Task DeleteAsync(int id);
    }
}
