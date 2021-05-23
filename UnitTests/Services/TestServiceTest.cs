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
    public class TestServiceTest
    {
        private ITestService _service;
        private AppDbContext _context;

        [SetUp]
        public void Setup()
        {
            _context = new AppDbContext(UnitTestsDb.GetDbOptions());
            _service = new TestService(new UnitOfWork(_context));
        }

        [Test]
        public async Task GetAllOfThemeTests_ReturnsAll()
        {
            var dtos = await _service.GetAllOfThemeByIdAsync(1);
            dtos.Count.Should().Be(1);
        }


        [Test]
        public async Task GetByIdTest_ReturnsTestDto()
        {
            var dto = await _service.GetByIdAsync(1);
            dto.Should().BeOfType<TestDto>();
        }


        [Test]
        public async Task CreateTest_AddItemToDb()
        {
            var countItemsImDataBase = await _context.Tests.CountAsync();
            await _service.CreateAsync(new TestDto() { Id = 3, ThemeId = 1, Questions = new List<QuestionDto>(), Title = "Test_1" });
            _context.Tests.CountAsync().Result.Should().Be(countItemsImDataBase + 1);
        }


        [Test]
        public async Task UpdateTest_ChangeItemId1()
        {
            var itemBefore = await _context.Tests.AsNoTracking().FirstOrDefaultAsync(c => c.Id == 1);
            await _service.UpdateAsync(new TestDto() { Id = 1, ThemeId = 1, Questions = new List<QuestionDto>(), Title = "Test_Update" });
            var itemAfter = await _context.Tests.AsNoTracking().FirstOrDefaultAsync(c => c.Id == 1);

            itemAfter.Id.Should().Be(itemBefore.Id);
            itemAfter.Title.Should().NotBe(itemBefore.Title);
        }


        [Test]
        public async Task DeleteTest_DeleteItemId1()
        {
            var countItemsImDataBase = await _context.Tests.CountAsync();
            await _service.DeleteAsync(1);

            _context.Tests.CountAsync().Result.Should().Be(countItemsImDataBase - 1);
            _context.Tests.FindAsync(1).Result.Should().BeNull();
        }
    }
}
