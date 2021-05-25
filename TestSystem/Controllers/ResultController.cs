using Microsoft.AspNetCore.Http;
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
   // [Authorize(Roles = "Administrator")]
    public class ResultController : Controller
    {
        private IResultService _resultService;

        public ResultController(IResultService resultService)
        {
            _resultService = resultService;
        }

        public async Task<ActionResult> Index()
        {

            return View(await _resultService.GetAllAsync());
        }       
    }
}
