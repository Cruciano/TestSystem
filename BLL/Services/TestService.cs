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
    public class TestService : ITestService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ICollection<TestDto>> GetAllOfThemeByIdAsync(int id)
        {
            List<TestDto> tests = new();
            IEnumerable<Test> testEntities = await _unitOfWork.TestRepository
                .GetByConditionAsync(test => test.ThemeId == id);

            foreach(var entity in testEntities)
            {
                tests.Add(entity.MapToDto());
            }

            foreach (var test in tests)
            {
                List<QuestionDto> questions = new();
                IEnumerable<Question> questionEntities = await _unitOfWork.QuestionRepository
                    .GetByConditionAsync(question => question.Id == test.Id);

                foreach (var entity in questionEntities)
                {
                    questions.Add(entity.MapToDto());
                }

                test.Questions = questions;
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
                questions.Add(entity.MapToDto());
            }

            test.Questions = questions;
            return test;
        }

        public async Task CreateAsync(TestDto dto)
        {
            await _unitOfWork.TestRepository.CreateAsync(dto.MapToEntity());
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(TestDto dto)
        {
            _unitOfWork.TestRepository.Update(dto.MapToEntity());
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Test testEntity = await _unitOfWork.TestRepository.ReadAsync(id);
            if (testEntity == null)
                throw new Exception("Nothing was found by this Id");

            _unitOfWork.TestRepository.Delete(testEntity);
            await _unitOfWork.SaveAsync();
        }
    }
}
