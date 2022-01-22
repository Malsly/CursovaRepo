using BL.Impl;
using Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class WordsController : Controller
    {
        private WordService wordsService = new WordService();


        public WordsController()
        {
        }

        [HttpGet("~/GetAllWords")]
        public ActionResult<Result<List<Word>>> GetAllWords()
        {
            return wordsService.GetAll();
        }

        [HttpGet("~/GetWord")]
        public ActionResult<Result<Word>> Get(int id)
        {
            return wordsService.Get(id);
        }

        [HttpPost("~/AddTWord")]
        public ActionResult<Result<Word>> Add(Word dto)
        {
            return wordsService.Add(dto);
        }

        [HttpDelete("~/DeleteWordById")]
        public ActionResult<Result<Word>> Delete(int id)
        {
            return wordsService.Delete(id);
        }
    }
}
