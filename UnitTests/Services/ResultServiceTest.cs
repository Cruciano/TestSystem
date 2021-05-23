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
    public class ResultServiceTest
    {
        private IResultService _service;
        private AppDbContext _context;

        [SetUp]
        public void Setup()
        {
            _context = new AppDbContext(UnitTestsDb.GetDbOptions());
            _service = new ResultService(new UnitOfWork(_context));
        }

        [Test]
        public async Task GetAllResults_ReturnsAll()
        {
            var countItemsImDataBase = await _context.Results.CountAsync();
            _service.GetAllAsync().Result.Count.Should().Be(countItemsImDataBase);
        }



        [Test]
        public async Task GetAllOfTestResults_ReturnsAll()
        {
            var countItemsImDataBase = await _context.Results
                .Where(result => result.TestId == 1 && result.user.FirstName == "Name_1").CountAsync();
            _service.GetAllByTestIdAndUserAsync(1, "Name_1").Result.Count.Should().Be(countItemsImDataBase);
        }
    }
}

