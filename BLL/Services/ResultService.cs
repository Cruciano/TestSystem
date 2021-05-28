using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.DTO;
using BLL.Mappers;
using DAL.Interfaces;
using DAL.Entities;

namespace BLL.Services
{
    public class ResultService : IResultService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ResultService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ICollection<ResultDto>> GetAllAsync()
        {
            List<ResultDto> results = new();

            IEnumerable<Result> resultEntities = await _unitOfWork.ResultRepository.GetAllAsync();

            foreach (var entity in resultEntities)
            {
                ResultDto result = entity.MapToDto();
                User user = await _unitOfWork.UserRepository.ReadAsync(result.UserId);
                result.UserName = user.FirstName;
                results.Add(result);
            }

            return results;
        }

        public async Task<ICollection<ResultDto>> GetAllByTestIdAndUserAsync(int testId, string userName)
        {
            List<ResultDto> results = new();

            IEnumerable<Result> resultEntities = await _unitOfWork.ResultRepository
                .GetByConditionAsync(result => result.TestId == testId && result.user.FirstName == userName);

            foreach (var entity in resultEntities)
            {
                results.Add(entity.MapToDto());
            }

            return results;
        }
    }
}
