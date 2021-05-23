using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTO;
using DAL.Entities;

namespace BLL.Interfaces
{
    public interface IUserTestingService
    {
        Task<ICollection<TestDto>> GetAllTestsAsync();
        Task<TestDto> GetByIdAsync(int id);
        TestDto ShuffleTest(TestDto testDto);
        Task CommitTest(TestDto test, List<List<bool>> answers, User user);
    }
}
