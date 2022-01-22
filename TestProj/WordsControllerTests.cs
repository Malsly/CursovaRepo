using API.Controllers;
using Entities;
using Entities.Enum;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestProj
{
    public class WordsControllerTests
    {

        WordsController wordsController;


        [SetUp]
        public void SetupBeforeEachTest()
        {
            wordsController = new WordsController();
        }

        [Test]
        public void WordsControllerGetAllWords()
        {
            ActionResult<Result<List<Word>>> result = wordsController.GetAllWords();
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Value.Status, Status.Success);
        }

        [Test]
        public void WordsControllerGet()
        {
            Word dto = new Word() { Value = "test", ThemeAndWordsID = 1 };
            wordsController.Add(dto);
            ActionResult<Result<Word>> result = wordsController.Get(wordsController.GetAllWords().Value.Value.Find(t => t.Value == "test").Id);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Value.Status, Status.Success);
            wordsController.Delete(wordsController.GetAllWords().Value.Value.Find(t => t.Value == "test").Id);
        }

        [Test]
        public void WordsControllerAdd()
        {
            Word dto = new Word() { Value = "test", ThemeAndWordsID = 1 };
            ActionResult<Result<Word>> result = wordsController.Add(dto);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Value.Status, Status.Success);
            wordsController.Delete(wordsController.GetAllWords().Value.Value.Find(t => t.Value == "test").Id);
        }

        [Test]
        public void WordsControllerDelete()
        {
            Word dto = new Word() { Value = "test", ThemeAndWordsID = 1 };
            wordsController.Add(dto);
            ActionResult<Result<Word>> result = wordsController.Delete(wordsController.GetAllWords().Value.Value.Find(t => t.Value == "test").Id);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Value.Status, Status.Success);
        }
    }
}
