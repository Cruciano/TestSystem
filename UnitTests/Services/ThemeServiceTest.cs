using System.Collections.Generic;
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
    public class ThemeServiceTest
    {
        private IThemeService _service;
        private AppDbContext _context;

        [SetUp]
        public void Setup()
        {
            _context = new AppDbContext(UnitTestsDb.GetDbOptions());
            _service = new ThemeService(new UnitOfWork(_context));
        }

        [Test]
        public async Task GetAllThemes_ReturnsAll()
        {
            var countItemsImDataBase = await _context.Themes.CountAsync();
            _service.GetAllAsync().Result.Count.Should().Be(countItemsImDataBase);
        }

        [Test]
        public async Task GetByIdTheme_ReturnsThemeDto()
        {
            var dto = await _service.GetByIdAsync(1);
            dto.Should().BeOfType<ThemeDto>();
        }


        [Test]
        public async Task CreateTheme_AddItemToDb()
        {
            var countItemsImDataBase = await _context.Themes.CountAsync();
            await _service.CreateAsync(new ThemeDto() { Id = 3, Title = "Theme_3", Tests = new List<TestDto>()});
            _context.Themes.CountAsync().Result.Should().Be(countItemsImDataBase + 1);
        }


        [Test]
        public async Task UpdateTheme_ChangeItemId1()
        {
            var itemBefore = await _context.Themes.AsNoTracking().FirstOrDefaultAsync(c => c.Id == 1);
            await _service.UpdateAsync(new ThemeDto() { Id = 1, Title = "Theme_Updeted", Tests = new List<TestDto>() });
            var itemAfter = await _context.Themes.AsNoTracking().FirstOrDefaultAsync(c => c.Id == 1);

            itemAfter.Id.Should().Be(itemBefore.Id);
            itemAfter.Title.Should().NotBe(itemBefore.Title);
        }


        [Test]
        public async Task DeleteTheme_DeleteItemId1()
        {
            var countItemsImDataBase = await _context.Themes.CountAsync();
            await _service.DeleteAsync(1);

            _context.Themes.CountAsync().Result.Should().Be(countItemsImDataBase - 1);
            _context.Themes.FindAsync(1).Result.Should().BeNull();
        }
    }
}
