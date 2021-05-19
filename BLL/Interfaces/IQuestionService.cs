using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IQuestionService : IService<QuestionDto>
    {
        public Task<ICollection<QuestionDto>> GetAllOfTestByIdAsync(int id);
    }
}
