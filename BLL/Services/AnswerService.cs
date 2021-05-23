using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Mappers;
using BLL.DTO;
using DAL.Interfaces;
using DAL.Entities;


namespace BLL.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnswerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ICollection<AnswerDto>> GetAllOfQuestionByIdAsync(int id)
        {
            List<AnswerDto> answers = new();
            IEnumerable<Answer> answerEntities = await _unitOfWork.AnswerRepository.GetByConditionAsync(answer => answer.QuestionId == id);

            foreach (var entity in answerEntities)
            {
                answers.Add(entity.MapToDto());
            }

            return answers;
        }


        public async Task<AnswerDto> GetByIdAsync(int id)
        {
            Answer answerEntity = await _unitOfWork.AnswerRepository.ReadAsync(id);
            if (answerEntity == null)
                throw new Exception("Nothing was found by this Id");

            answerEntity.Question = await _unitOfWork.QuestionRepository.ReadAsync(answerEntity.QuestionId);

            return answerEntity.MapToDto();
        }

        public async Task CreateAsync(AnswerDto dto)
        {
            await _unitOfWork.AnswerRepository.CreateAsync(dto.MapToEntity());
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(AnswerDto dto)
        {
            _unitOfWork.AnswerRepository.Update(dto.MapToEntity());
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Answer answerEntity = await _unitOfWork.AnswerRepository.ReadAsync(id);
            if (answerEntity == null)
                throw new Exception("Nothing was found by this Id");

            _unitOfWork.AnswerRepository.Delete(answerEntity);
            await _unitOfWork.SaveAsync();
        }
    }
}
