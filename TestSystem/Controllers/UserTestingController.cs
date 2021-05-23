using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using BLL.Interfaces;
using BLL.DTO;
using DAL.Entities;

namespace TestSystem.Controllers
{
    [Authorize(Roles = "User")]
    public class UserTestingController : Controller
    {
        private UserManager<User> _userManager;
        private IUserTestingService _userTestingService;
        private IResultService _resultService;

        public UserTestingController(UserManager<User> userManager, IUserTestingService userTestingService, IResultService resultService)
        {
            _userManager = userManager;
            _userTestingService = userTestingService;
            _resultService = resultService;
        }


        public async Task<IActionResult> Index()
        {
            ICollection<TestDto> model = await _userTestingService.GetAllTestsAsync();

            return View(model);
        }

        public async Task<IActionResult> PassTest(int testId)
        {
            TestDto test = await _userTestingService.GetByIdAsync(testId);
            test = _userTestingService.ShuffleTest(test);

            ViewBag.test = test;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PassTest(TestDto test, List<List<bool>> answers)
        {
            User user = await _userManager.GetUserAsync(HttpContext.User);
            await _userTestingService.CommitTest(test, answers, user);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> TestResults(int testId)
        {
            User user = await _userManager.GetUserAsync(HttpContext.User);
            return View(await _resultService.GetAllByTestIdAsync(testId, user.FirstName));
        }
    }
}
