using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Mappers;
using BLL.DTO;
using DAL.Interfaces;
using DAL.Entities;


namespace BLL.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public QuestionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ICollection<QuestionDto>> GetAllOfTestByIdAsync(int id)
        {
            List<QuestionDto> questions = new();
            IEnumerable<Question> questionEntities = await _unitOfWork.QuestionRepository
                .GetByConditionAsync(question => question.TestId == id);

            foreach (var entity in questionEntities)
            {
                questions.Add(entity.MapToDto());
            }

            foreach (var question in questions)
            {
                List<AnswerDto> answers = new();
                IEnumerable<Answer> answerEntities = await _unitOfWork.AnswerRepository
                    .GetByConditionAsync(answer => answer.Id == question.Id);

                foreach (var entity in answerEntities)
                {
                    answers.Add(entity.MapToDto());
                }

                question.Answers = answers;
            }

            return questions;
        }


        public async Task<QuestionDto> GetByIdAsync(int id)
        {
            Question questionEntity = await _unitOfWork.QuestionRepository.ReadAsync(id);
            if (questionEntity == null)
                throw new Exception("Nothing was found by this Id");

            questionEntity.Test = await _unitOfWork.TestRepository.ReadAsync(questionEntity.TestId);

            QuestionDto question = questionEntity.MapToDto();

            List<AnswerDto> questions = new();
            IEnumerable<Answer> answerEntities = await _unitOfWork.AnswerRepository
                .GetByConditionAsync(answer => answer.QuestionId == questionEntity.Id);

            foreach (var entity in answerEntities)
            {
                questions.Add(entity.MapToDto());
            }

            question.Answers = questions;
            return question;
        }

        public async Task CreateAsync(QuestionDto dto)
        {
            await _unitOfWork.QuestionRepository.CreateAsync(dto.MapToEntity());
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(QuestionDto dto)
        {
            _unitOfWork.QuestionRepository.Update(dto.MapToEntity());
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Question questionEntity = await _unitOfWork.QuestionRepository.ReadAsync(id);
            if (questionEntity == null)
                throw new Exception("Nothing was found by this Id");

            _unitOfWork.QuestionRepository.Delete(questionEntity);
            await _unitOfWork.SaveAsync();
        }
    }
}
