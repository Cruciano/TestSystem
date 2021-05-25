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
    //[Authorize(Roles = "Administrator")]
    public class TestController : Controller
    {
        private ITestService _testService;
        private IThemeService _themeService;

        public TestController(ITestService testService, IThemeService themeService)
        {
            _testService = testService;
            _themeService = themeService;
        }

        public async Task<IActionResult> Index(int themeId)
        { 
            ViewBag.Theme = await _themeService.GetByIdAsync(themeId);
            return View(await _testService.GetAllOfThemeByIdAsync(themeId));
        }

        public async Task<IActionResult> Create(int themeId)
        {
            ViewBag.Theme = await _themeService.GetByIdAsync(themeId);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TestDto testDto)
        {
            if (ModelState.IsValid)
            {
                await _testService.CreateAsync(testDto);
                return RedirectToAction(nameof(Index), new { themeId = testDto.ThemeId });
            }

            ViewBag.Theme = await _themeService.GetByIdAsync(testDto.ThemeId);
            return View(testDto);
        }

        public async Task<IActionResult> Edit(int id, int themeId)
        {
            ViewBag.Theme = await _themeService.GetByIdAsync(themeId);
            return View(await _testService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TestDto testDto)
        {
            if (ModelState.IsValid)
            {
                await _testService.UpdateAsync(testDto);
                return RedirectToAction(nameof(Index), new { themeId = testDto.ThemeId });
            }

            ViewBag.Theme = await _themeService.GetByIdAsync(testDto.ThemeId);
            return View(testDto);
        }

        public async Task<IActionResult> Delete(int id, int themeId)
        {
            ViewBag.Theme = await _themeService.GetByIdAsync(themeId);
            return View(await _testService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteVerified(int id)
        {
            TestDto testDto = await _testService.GetByIdAsync(id);
            await _testService.DeleteAsync(id);
            return RedirectToAction(nameof(Index), new { themeId = testDto.ThemeId });
        }
    }
}
