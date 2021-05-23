using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using BLL.Interfaces;
using BLL.Services;
using BLL.DTO;
using DAL.Context;
using DAL.UnitOfWork;

namespace UnitTests
{
    public class QuestionServiceTest
    {
        private IQuestionService _service;
        private AppDbContext _context;

        [SetUp]
        public void Setup()
        {
            _context = new AppDbContext(UnitTestsDb.GetDbOptions());
            _service = new QuestionService(new UnitOfWork(_context));
        }


        [Test]
        public async Task GetAllOfTestQuestions_ReturnsAll()
        {
            var countItemsImDataBase = await _context.Questions.Where(question => question.TestId == 2).CountAsync();
            _service.GetAllOfTestByIdAsync(2).Result.Count.Should().Be(countItemsImDataBase);
        }


        [Test]
        public async Task GetByIdQuestion_ReturnsQuestionDto()
        {
            var dto = await _service.GetByIdAsync(1);
            dto.Should().BeOfType<QuestionDto>();
        }


        [Test]
        public async Task CreateQuestion_AddItemToDb()
        {
            var countItemsImDataBase = await _context.Questions.CountAsync();
            await _service.CreateAsync(new QuestionDto()
                { Id = 3, Answers = new List<AnswerDto>(), Score = 4, TaskText = "Question_3", TestId = 1 });

            _context.Questions.CountAsync().Result.Should().Be(countItemsImDataBase + 1);
        }


        [Test]
        public async Task UpdateTest_ChangeItemId1()
        {
            var itemBefore = await _context.Questions.AsNoTracking().FirstOrDefaultAsync(c => c.Id == 1);
            await _service.UpdateAsync(new QuestionDto()
                { Id = 1, Answers = new List<AnswerDto>(), Score = 4, TaskText = "Question_Update", TestId = 1 });

            var itemAfter = await _context.Questions.AsNoTracking().FirstOrDefaultAsync(c => c.Id == 1);

            itemAfter.Id.Should().Be(itemBefore.Id);
            itemAfter.TaskText.Should().NotBe(itemBefore.TaskText);
        }


        [Test]
        public async Task DeleteTest_DeleteItemId1()
        {
            var countItemsImDataBase = await _context.Questions.CountAsync();
            await _service.DeleteAsync(1);

            _context.Questions.CountAsync().Result.Should().Be(countItemsImDataBase - 1);
            _context.Questions.FindAsync(1).Result.Should().BeNull();
        }
    }
}
