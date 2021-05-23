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
    public class AnswerServiceTest
    {
        private IAnswerService _service;
        private AppDbContext _context;

        [SetUp]
        public void Setup()
        {
            _context = new AppDbContext(UnitTestsDb.GetDbOptions());
            _service = new AnswerService(new UnitOfWork(_context));
        }

        [Test]
        public async Task GetAllOfQuestionAnswers_ReturnsAll()
        {
            var countItemsImDataBase = await _context.Answers.Where(answer => answer.QuestionId == 1).CountAsync();
            _service.GetAllOfQuestionByIdAsync(1).Result.Count.Should().Be(countItemsImDataBase);
        }


        [Test]
        public async Task GetByIdAnswer_ReturnsAnswerDto()
        {
            var dto = await _service.GetByIdAsync(1);
            dto.Should().BeOfType<AnswerDto>();
        }


        [Test]
        public async Task CreateAnswer_AddItemToDb()
        {
            var countItemsImDataBase = await _context.Answers.CountAsync();
            await _service.CreateAsync(new AnswerDto() { Id = 3, IsCorrect = false, Text = "Answer_1", QuestionId = 1 });
            _context.Answers.CountAsync().Result.Should().Be(countItemsImDataBase + 1);
        }


        [Test]
        public async Task UpdateAnswer_ChangeItemId1()
        {
            var itemBefore = await _context.Answers.AsNoTracking().FirstOrDefaultAsync(c => c.Id == 1);
            await _service.UpdateAsync(new AnswerDto() { Id = 1, IsCorrect = false, Text = "Answer_Update", QuestionId = 1 });
            var itemAfter = await _context.Answers.AsNoTracking().FirstOrDefaultAsync(c => c.Id == 1);

            itemAfter.Id.Should().Be(itemBefore.Id);
            itemAfter.Text.Should().NotBe(itemBefore.Text);
        }


        [Test]
        public async Task DeleteAnswer_DeleteItemId1()
        {
            var countItemsImDataBase = await _context.Answers.CountAsync();
            await _service.DeleteAsync(1);

            _context.Answers.CountAsync().Result.Should().Be(countItemsImDataBase - 1);
            _context.Answers.FindAsync(1).Result.Should().BeNull();
        }
    }
}
