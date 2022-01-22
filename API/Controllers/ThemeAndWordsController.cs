using BL.Impl;
using Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class ThemeAndWordsController : Controller
    {
        private ThemeAndWordsService themeAndWordsService = new ThemeAndWordsService();

        public ThemeAndWordsController()
        {
        }

        [HttpGet("~/GetAllThemes")]
        public ActionResult<Result<List<ThemeAndWords>>> GetAllThemes()
        {
            return themeAndWordsService.GetAll();
        }

        [HttpGet("~/GetTheme")]
        public ActionResult<Result<ThemeAndWords>> Get(int id)
        {
            return themeAndWordsService.Get(id);
        }

        [HttpPost("~/AddTheme")]
        public ActionResult<Result<ThemeAndWords>> Add(ThemeAndWords dto)
        {
            return themeAndWordsService.Add(dto);
        }

        [HttpDelete("~/DeleteThemeById")]
        public ActionResult<Result<ThemeAndWords>> Delete(int id)
        {
            return themeAndWordsService.Delete(id);
        }
    }
}
