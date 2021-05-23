using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IThemeService : IService<ThemeDto>
    {
        public Task<ICollection<ThemeDto>> GetAllAsync();
    }
}
