using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.DTO;
using BLL.Mappers;
using BLL.Algoritms;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class UserTestingService : IUserTestingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserTestingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ICollection<TestDto>> GetAllTestsAsync()
        {
            List<TestDto> tests = new();
            IEnumerable<Test> testEntities = await _unitOfWork.TestRepository.GetAllAsync();

            foreach(var item in testEntities)
            {
                item.Theme = await _unitOfWork.ThemeRepository.ReadAsync(item.ThemeId);
                TestDto test = item.MapToDto();

                List<QuestionDto> questions = new();
                IEnumerable<Question> questionEntities = await _unitOfWork.QuestionRepository
                    .GetByConditionAsync(question => question.TestId == item.Id);

                foreach (var entity in questionEntities)
                {


                    questions.Add(entity.MapToDto());
                }

                test.Questions = questions;
                tests.Add(test);
            }

            return tests;
        }

        public async Task<TestDto> GetByIdAsync(int id)
        {
            Test testEntity = await _unitOfWork.TestRepository.ReadAsync(id);
            if (testEntity == null)
                throw new Exception("Nothing was found by this Id");

            testEntity.Theme = await _unitOfWork.ThemeRepository.ReadAsync(testEntity.ThemeId);

            TestDto test = testEntity.MapToDto();

            List<QuestionDto> questions = new();
            IEnumerable<Question> questionEntities = await _unitOfWork.QuestionRepository
                .GetByConditionAsync(question => question.TestId == testEntity.Id);

            foreach (var entity in questionEntities)
            {
                QuestionDto question = entity.MapToDto();

                List<AnswerDto> answers = new();
                IEnumerable<Answer> answerEntities = await _unitOfWork.AnswerRepository
                    .GetByConditionAsync(answer => answer.QuestionId == entity.Id);

                foreach (var item in answerEntities)
                {
                    answers.Add(item.MapToDto());
                }

                question.Answers = answers;
                questions.Add(question);
            }

            test.Questions = questions;
            return test;
        }

        public async Task CommitTest(TestDto test, List<List<bool>> answers, User user)
        {
            ResultDto result = new()
            {
                UserName = user.FirstName,
                UserId = user.Id,
                DateTime = DateTime.Now,
                TestTitle = test.Title,
                TestId = test.Id
            };

            int totalScore = 0;

            for(int i = 0; i < test.Questions.Count; i++)
            {
                bool IsCorrect = true;
                for (int j = 0; j < test.Questions.ElementAt(i).Answers.Count; j++)
                {
                    AnswerDto answer = test.Questions.ElementAt(i).Answers.ElementAt(j);
                    if (!(answer.IsCorrect == answers[i][j]))
                    {
                        IsCorrect = false;
                    }
                }

                if (IsCorrect)
                {
                    totalScore += test.Questions.ElementAt(i).Score;
                }
            }

            result.Score = totalScore;
            await _unitOfWork.ResultRepository.CreateAsync(result.MapToEntity());
            await _unitOfWork.SaveAsync();
        }

        public TestDto ShuffleTest(TestDto testDto)
        {
            foreach(var item in testDto.Questions)
            {
                List<AnswerDto> answers = item.Answers.ToList();
                answers.Shuffle<AnswerDto>();
                item.Answers = answers;
            }

            List<QuestionDto> questions = testDto.Questions.ToList();
            questions.Shuffle();
            testDto.Questions = questions;

            return testDto;
        }
    }
}
