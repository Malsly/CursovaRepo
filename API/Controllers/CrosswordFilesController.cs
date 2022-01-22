using BL.Impl;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CrosswordFilesController : ControllerBase
    {
        private CrosswordFileManager crosswordFileManager = new CrosswordFileManager(new List<ThemeAndWords>());

        public CrosswordFilesController()
        {
        }

        [HttpGet("~/GetAllThemeNamesFileManager")]
        public ActionResult<Result<IEnumerable<string>>> GetAllThemeNamesFileManager()
        {
            return crosswordFileManager.GetAllThemeNames();
        }

        [HttpGet("~/GetThemeFileManager")]
        public ActionResult<Result<ThemeAndWords>> GetThemeFileManager(string themeName)
        {
            return new ActionResult<Result<ThemeAndWords>>(crosswordFileManager.GetThemeFromFile(themeName));
        }

        [HttpPost("~/AddThemeFileManager")]
        public ActionResult<Result<ThemeAndWords>> AddThemeFileManager(ThemeAndWords themeAndWords)
        {
            return new ActionResult<Result<ThemeAndWords>>(crosswordFileManager.AddThemeFile(themeAndWords));
        }

        [HttpDelete("~/RemoveThemeFile")]
        public ActionResult<Result<ThemeAndWords>> RemoveThemeFileFileManager(string themeName)
        {
            return new ActionResult<Result<ThemeAndWords>>(crosswordFileManager.RemoveThemeFile(themeName));
        }

    }
}
