using BL.Impl;
using Entities;
using Entities.Enum;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Collections.Generic;
using WebApi.Controllers;

namespace TestProj
{
    public class CrosswordFilesControllerTests
    {
        CrosswordFilesController crosswordController;


        [SetUp]
        public void SetupBeforeEachTest()
        {
            crosswordController = new CrosswordFilesController();
        }

        [Test]
        public void CrosswordFilesControllerAddTheme()
        {
            ThemeAndWords themeAndWords = new ThemeAndWords() { Theme = "../../../../src/test", Words = new List<Word>() { new Word() { Value = "test" }, new Word() { Value = "test1" }, new Word() { Value = "test3" } } };
            ActionResult<Result<ThemeAndWords>> result = crosswordController.AddThemeFileManager(themeAndWords);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value.Value);
            Assert.AreEqual(result.Value.Status, Status.Success);
        }

        [Test]
        public void CrosswordFilesControllerRemoveTheme()
        {
            string themeName = "../../../../src/test";
            ActionResult<Result<ThemeAndWords>> result = crosswordController.RemoveThemeFileFileManager(themeName);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Value.Status, Status.Success);
        }
    }
}