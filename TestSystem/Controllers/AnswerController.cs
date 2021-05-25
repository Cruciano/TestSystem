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
    public class AnswerController : Controller
    {
        private IAnswerService _answerService;
        private IQuestionService _questionService;

        public AnswerController(IAnswerService answerService, IQuestionService questionService)
        {
            _answerService = answerService;
            _questionService = questionService;
        }

        public async Task<IActionResult> Index(int questionId)
        {
            ViewBag.Question = await _questionService.GetByIdAsync(questionId);
            return View(await _answerService.GetAllOfQuestionByIdAsync(questionId));
        }

        public async Task<IActionResult> Create(int questionId)
        {
            ViewBag.Question = await _questionService.GetByIdAsync(questionId);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AnswerDto answerDto)
        {
            if (ModelState.IsValid)
            {
                await _answerService.CreateAsync(answerDto);
                return RedirectToAction(nameof(Index), new { questionId = answerDto.QuestionId });
            }

            ViewBag.Question = await _questionService.GetByIdAsync(answerDto.QuestionId);
            return View(answerDto);
        }

        public async Task<IActionResult> Edit(int id, int questionId)
        {
            ViewBag.Question = await _questionService.GetByIdAsync(questionId);
            return View(await _answerService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AnswerDto answerDto)
        {
            if (ModelState.IsValid)
            {
                await _answerService.UpdateAsync(answerDto);
                return RedirectToAction(nameof(Index), new { questionId = answerDto.QuestionId });
            }

            ViewBag.Question = await _questionService.GetByIdAsync(answerDto.QuestionId);
            return View(answerDto);
        }

        public async Task<IActionResult> Delete(int id, int questionId)
        {
            ViewBag.Question = await _questionService.GetByIdAsync(questionId);
            return View(await _answerService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteVerified(int id)
        {
            AnswerDto answerDto = await _answerService.GetByIdAsync(id);
            await _answerService.DeleteAsync(id);
            return RedirectToAction(nameof(Index), new { questionId = answerDto.QuestionId });
        }
    }
}
