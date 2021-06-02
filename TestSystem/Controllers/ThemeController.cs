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
    public class ThemeController : Controller
    {
        private IThemeService _themeService;

        public ThemeController(IThemeService themeService)
        {
            _themeService = themeService;
        }

        public async Task<IActionResult> Index()
        {
            return  View(await _themeService.GetAllAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ThemeDto themeDto)
        {
            if (ModelState.IsValid)
            {
                await _themeService.CreateAsync(themeDto);
                return RedirectToAction(nameof(Index));
            }
            return View(themeDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            return View(await _themeService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ThemeDto themeDto)
        {
            if (ModelState.IsValid)
            {
                await _themeService.UpdateAsync(themeDto);
                return RedirectToAction(nameof(Index));
            }
            return View(themeDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _themeService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteVerified(int id)
        {
            await _themeService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
