using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using BLL.Interfaces;
using BLL.Services;
using BLL.DTO;
using DAL.Entities;
using DAL.Context;
using DAL.UnitOfWork;

namespace UnitTests
{
    public class UserTestingServiceTest
    {
        private IUserTestingService _service;
        private AppDbContext _context;

        [SetUp]
        public void Setup()
        {
            _context = new AppDbContext(UnitTestsDb.GetDbOptions());
            _service = new UserTestingService(new UnitOfWork(_context));
        }

        [Test]
        public async Task GetAllTests_ReturnsAll()
        {
            var countItemsImDataBase = await _context.Tests.CountAsync();
            _service.GetAllTestsAsync().Result.Count.Should().Be(countItemsImDataBase);
        }


        [Test]
        public async Task GetByIdTest_ReturnsTestDto()
        {
            var dto = await _service.GetByIdAsync(1);
            dto.Should().BeOfType<TestDto>();
        }


        [Test]
        public async Task CommitTest_Test()
        {
            var countItemsImDataBase = await _context.Results.CountAsync();

            User user = new() { Id = 1, FirstName = "Name_1", LastName = "SurName_1" };
            TestDto test = new() { Id = 1, ThemeId = 1, Questions = new List<QuestionDto>(), Title = "Test_1" };
            QuestionDto question1 = new QuestionDto()
                { Id = 1, Answers = new List<AnswerDto>(), Score = 4, TaskText = "Question_1", TestId = 1 };
            question1.Answers.Add(new AnswerDto() { Id = 1, IsCorrect = true, Text = "Answer_1", QuestionId = 1 });

            QuestionDto question2 = new QuestionDto()
                { Id = 2, Answers = new List<AnswerDto>(), Score = 5, TaskText = "Question_2", TestId = 2 };
            question2.Answers.Add(new AnswerDto() { Id = 2, IsCorrect = false, Text = "Answer_2", QuestionId = 2 });

            test.Questions.Add(question1);
            test.Questions.Add(question2);

            List<List<bool>> answers = new();
            List<bool> ans1 = new();
            ans1.Add(true);
            List<bool> ans2 = new();
            ans1.Add(false);
            answers.Add(ans1);
            answers.Add(ans1);

            await _service.CommitTest(test, answers, user);
            _context.Results.CountAsync().Result.Should().Be(countItemsImDataBase + 1);
        }
    }
}
