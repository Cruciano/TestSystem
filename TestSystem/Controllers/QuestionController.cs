using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using BLL.Interfaces;
using BLL.DTO;

namespace TestSystem.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class QuestionController : Controller
    {
        private IQuestionService _questionService;
        private ITestService _testService;

        public QuestionController(IQuestionService questionService, ITestService testService)
        {
            _questionService = questionService;
            _testService = testService;
        }

        public async Task<IActionResult> Index(int testId)
        {
            ViewBag.Test = await _testService.GetByIdAsync(testId);
            return View(await _questionService.GetAllOfTestByIdAsync(testId));
        }

        public async Task<IActionResult> Create(int testId)
        {
            ViewBag.Test = await _testService.GetByIdAsync(testId);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(QuestionDto questionDto)
        {
            if (ModelState.IsValid)
            {
                await _questionService.CreateAsync(questionDto);
                return RedirectToAction(nameof(Index), new { testId = questionDto.TestId });
            }

            ViewBag.Test = await _testService.GetByIdAsync(questionDto.TestId);
            return View(questionDto);
        }

        public async Task<IActionResult> Edit(int id, int testId)
        {
            ViewBag.Test = await _testService.GetByIdAsync(testId);
            return View(await _questionService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(QuestionDto questionDto)
        {
            if (ModelState.IsValid)
            {
                await _questionService.UpdateAsync(questionDto);
                return RedirectToAction(nameof(Index), new { testId = questionDto.TestId });
            }

            ViewBag.Test = await _testService.GetByIdAsync(questionDto.TestId);
            return View(questionDto);
        }

        public async Task<IActionResult> Delete(int id, int testId)
        {
            ViewBag.Test = await _testService.GetByIdAsync(testId);
            return View(await _questionService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteVerified(int id)
        {
            QuestionDto questionDto = await _questionService.GetByIdAsync(id);
            await _questionService.DeleteAsync(id);
            return RedirectToAction(nameof(Index), new { testId = questionDto.TestId });
        }
    }
}
