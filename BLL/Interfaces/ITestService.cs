using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface ITestService : IService<TestDto>
    {
        public Task<ICollection<TestDto>> GetAllOfThemeByIdAsync(int id);
    }
}
