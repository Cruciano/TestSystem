using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTO;


namespace BLL.Interfaces
{
    public interface IResultService
    {
        Task<ICollection<ResultDto>> GetAllAsync();
        Task<ICollection<ResultDto>> GetAllByTestIdAsync(int testId, string userName);
    }
}
