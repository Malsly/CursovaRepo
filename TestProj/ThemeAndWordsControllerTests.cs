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
    public class ThemeAndWordsControllerTests
    {
        ThemeAndWordsController themeAndWordsController;


        [SetUp]
        public void SetupBeforeEachTest()
        {
            themeAndWordsController = new ThemeAndWordsController();
        }

        [Test]
        public void ThemeAndWordsControllerGetAllThemes()
        {
            ActionResult<Result<List<ThemeAndWords>>> result = themeAndWordsController.GetAllThemes();
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Value.Status, Status.Success);
        }

        [Test]
        public void ThemeAndWordsControllerGet()
        {
            ThemeAndWords dto = new ThemeAndWords() { Theme = "test", Words = new List<Word>() };
            themeAndWordsController.Add(dto);
            ActionResult<Result<ThemeAndWords>> result = themeAndWordsController.Get(themeAndWordsController.GetAllThemes().Value.Value.Find(t => t.Theme == "test").Id);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Value.Status, Status.Success);
            themeAndWordsController.Delete(themeAndWordsController.GetAllThemes().Value.Value.Find(t => t.Theme == "test").Id);
        }

        [Test]
        public void ThemeAndWordsControllerAdd()
        {
            ThemeAndWords dto = new ThemeAndWords() { Theme = "test", Words = new List<Word>() };
            ActionResult<Result<ThemeAndWords>> result = themeAndWordsController.Add(dto);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Value.Status, Status.Success);
            themeAndWordsController.Delete(themeAndWordsController.GetAllThemes().Value.Value.Find(t => t.Theme == "test").Id);
        }

        [Test]
        public void ThemeAndWordsControllerDelete()
        {
            ThemeAndWords dto = new ThemeAndWords() { Theme = "test", Words = new List<Word>() };
            themeAndWordsController.Add(dto);
            ActionResult<Result<ThemeAndWords>> result = themeAndWordsController.Delete(themeAndWordsController.GetAllThemes().Value.Value.Find(t => t.Theme == "test").Id);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Value.Status, Status.Success);
        }
    }
}
