using BL.Impl;
using Entities;
using Entities.Enum;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Collections.Generic;
using WebApi.Controllers;

namespace TestProj
{
    public class CrosswordFileManagerTests
    {
        private CrosswordFileManager crosswordContext;


        [SetUp]
        public void SetupBeforeEachTest()
        {
            crosswordContext = new CrosswordFileManager(new List<ThemeAndWords>());
        }

        [Test]
        public void AddThemeAndWords()
        {
            ThemeAndWords themeAndWords = new ThemeAndWords() { Theme = "test", Words = new List<Word>() { new Word() { Value = "test" }, new Word() { Value = "test1" }, new Word() { Value = "test3" } } };
            Result<ThemeAndWords> result = crosswordContext.AddThemeAndWords(themeAndWords);
            Assert.AreEqual(result.Status, Status.Success);
        }

        [Test]
        public void AddThemeAndWordsNullTheme()
        {
            ThemeAndWords themeAndWords = new ThemeAndWords() { Theme = null, Words = new List<Word>() { new Word() { Value = "test" }, new Word() { Value = "test1" }, new Word() { Value = "test3" } } };
            Result<ThemeAndWords> result = crosswordContext.AddThemeAndWords(themeAndWords);
            Assert.AreEqual(result.Status, Status.Error);
        }

        [Test]
        public void AddThemeAndWordsNullWords()
        {
            ThemeAndWords themeAndWords = new ThemeAndWords() { Theme = "test", Words = null };
            Result<ThemeAndWords> result = crosswordContext.AddThemeAndWords(themeAndWords);
            Assert.AreEqual(result.Status, Status.Error);
        }

        [Test]
        public void AddThemeAndWordsNullWordsAndTheme()
        {
            ThemeAndWords themeAndWords = new ThemeAndWords() { Theme = null, Words = null };
            Result<ThemeAndWords> result = crosswordContext.AddThemeAndWords(themeAndWords);
            Assert.AreEqual(result.Status, Status.Error);
        }

        [Test]
        public void AddThemeAndWordsEmptyWords()
        {
            ThemeAndWords themeAndWords = new ThemeAndWords() { Theme = "test", Words = new List<Word>() { } };
            Result<ThemeAndWords> result = crosswordContext.AddThemeAndWords(themeAndWords);
            Assert.AreEqual(result.Status, Status.Error);
        }

        [Test]
        public void AddThemeAndWordsEmptyTheme()
        {
            ThemeAndWords themeAndWords = new ThemeAndWords() { Theme = "", Words = new List<Word>() { new Word() { Value = "test" }, new Word() { Value = "test1" }, new Word() { Value = "test3" } } };
            Result<ThemeAndWords> result = crosswordContext.AddThemeAndWords(themeAndWords);
            Assert.AreEqual(result.Status, Status.Error);
        }

        [Test]
        public void AddThemeAndWordsEmptyThemeAndWords()
        {
            ThemeAndWords themeAndWords = new ThemeAndWords() { Theme = "", Words = new List<Word>() { } };
            Result<ThemeAndWords> result = crosswordContext.AddThemeAndWords(themeAndWords);
            Assert.AreEqual(result.Status, Status.Error);
        }

        [Test]
        public void GetTheme()
        {
            AddThemeAndWords();
            Result<ThemeAndWords> result = crosswordContext.GetTheme("test");
            Assert.AreEqual(result.Status, Status.Success);
        }

        [Test]
        public void GetThemeNullTheme()
        {
            AddThemeAndWords();
            Result<ThemeAndWords> result = crosswordContext.GetTheme(null);
            Assert.AreEqual(result.Status, Status.Error);
        }

        [Test]
        public void GetThemeEmptyTheme()
        {
            AddThemeAndWords();
            Result<ThemeAndWords> result = crosswordContext.GetTheme("");
            Assert.AreEqual(result.Status, Status.Error);
        }

        [Test]
        public void GetThemeNotExistTheme()
        {
            Result<ThemeAndWords> result = crosswordContext.GetTheme("test");
            Assert.AreEqual(result.Status, Status.Error);
        }

        [Test]
        public void RemoveTheme()
        {
            AddThemeAndWords();
            Result<ThemeAndWords> result = crosswordContext.RemoveTheme("test");
            Assert.AreEqual(result.Status, Status.Success);
        }

        [Test]
        public void RemoveThemeNullTheme()
        {
            AddThemeAndWords();
            Result<ThemeAndWords> result = crosswordContext.RemoveTheme(null);
            Assert.AreEqual(result.Status, Status.Error);
        }

        [Test]
        public void RemoveThemeEmptyTheme()
        {
            AddThemeAndWords();
            Result<ThemeAndWords> result = crosswordContext.RemoveTheme("");
            Assert.AreEqual(result.Status, Status.Error);
        }

        [Test]
        public void RemoveThemeNotExistTheme()
        {
            Result<ThemeAndWords> result = crosswordContext.RemoveTheme("test");
            Assert.AreEqual(result.Status, Status.Warning);
        }

        [Test]
        public void AddThemeFile()
        {
            ThemeAndWords themeAndWords = new ThemeAndWords() { Theme = "../../../../src/test", Words = new List<Word>() { new Word() { Value = "test" }, new Word() { Value = "test1" }, new Word() { Value = "test3" } } };
            Result<ThemeAndWords> result = crosswordContext.AddThemeFile(themeAndWords);
            Assert.AreEqual(result.Status, Status.Success);
            crosswordContext.RemoveThemeFile("../../../../src/test");
        }

        [Test]
        public void AddThemeFileAlreadyExist()
        {
            ThemeAndWords themeAndWords = new ThemeAndWords() { Theme = "../../../../src/test", Words = new List<Word>() { new Word() { Value = "test" }, new Word() { Value = "test1" }, new Word() { Value = "test3" } } };
            Result<ThemeAndWords> result = crosswordContext.AddThemeFile(themeAndWords);
            Result<ThemeAndWords> result1 = crosswordContext.AddThemeFile(themeAndWords);
            Assert.AreEqual(result1.Status, Status.Error);
            crosswordContext.RemoveThemeFile("../../../../src/test");
        }

        [Test]
        public void AddThemeFileNullTheme()
        {
            ThemeAndWords themeAndWords = new ThemeAndWords() { Theme = null, Words = new List<Word>() { new Word() { Value = "test" }, new Word() { Value = "test1" }, new Word() { Value = "test3" } } };
            Result<ThemeAndWords> result = crosswordContext.AddThemeFile(themeAndWords);
            Assert.AreEqual(result.Status, Status.Error);
        }

        [Test]
        public void AddThemeFileNullWords()
        {
            ThemeAndWords themeAndWords = new ThemeAndWords() { Theme = "test", Words = null };
            Result<ThemeAndWords> result = crosswordContext.AddThemeFile(themeAndWords);
            Assert.AreEqual(result.Status, Status.Error);
        }

        [Test]
        public void AddThemeFileEmptyTheme()
        {
            ThemeAndWords themeAndWords = new ThemeAndWords() { Theme = "", Words = new List<Word>() { new Word() { Value = "test" }, new Word() { Value = "test1" }, new Word() { Value = "test3" } } };
            Result<ThemeAndWords> result = crosswordContext.AddThemeFile(themeAndWords);
            Assert.AreEqual(result.Status, Status.Error);
        }

        [Test]
        public void RemoveThemeFile()
        {
            ThemeAndWords themeAndWords = new ThemeAndWords() { Theme = "../../../../src/test", Words = new List<Word>() { new Word() { Value = "test" }, new Word() { Value = "test1" }, new Word() { Value = "test3" } } };
            crosswordContext.AddThemeFile(themeAndWords);
            Result<ThemeAndWords> result = crosswordContext.RemoveThemeFile("../../../../src/test");
            Assert.AreEqual(result.Status, Status.Success);
        }

        [Test]
        public void RemoveThemeFileNotExist()
        {
            Result<ThemeAndWords> result = crosswordContext.RemoveThemeFile("../../../../src/test");
            Assert.AreEqual(result.Status, Status.Warning);
        }

        [Test]
        public void RemoveThemeFileNullTheme()
        {
            Result<ThemeAndWords> result = crosswordContext.RemoveThemeFile(null);
            Assert.AreEqual(result.Status, Status.Error);
        }

        [Test]
        public void GetThemeFromFile()
        {
            ThemeAndWords themeAndWords = new ThemeAndWords() { Theme = "../../../../src/test", Words = new List<Word>() { new Word() { Value = "test" }, new Word() { Value = "test1" }, new Word() { Value = "test3" } } };
            Result<ThemeAndWords> result = crosswordContext.AddThemeFile(themeAndWords);
            Result<ThemeAndWords> result1 = crosswordContext.GetThemeFromFile("../../../../src/test");
            Assert.AreEqual(result1.Status, Status.Success);
            crosswordContext.RemoveThemeFile("../../../../src/test");
        }

        [Test]
        public void GetThemeFromFileNotExist()
        {
            Result<ThemeAndWords> result1 = crosswordContext.GetThemeFromFile("test");
            Assert.AreEqual(result1.Status, Status.Error);
        }

        [Test]
        public void GetThemeFromFileNullTheme()
        {
            Result<ThemeAndWords> result1 = crosswordContext.GetThemeFromFile(null);
            Assert.AreEqual(result1.Status, Status.Error);
        }

        [Test]
        public void GetAndAddThemeFromFile()
        {
            ThemeAndWords themeAndWords = new ThemeAndWords() { Theme = "../../../../src/test", Words = new List<Word>() { new Word() { Value = "test" }, new Word() { Value = "test1" }, new Word() { Value = "test3" } } };
            crosswordContext.AddThemeFile(themeAndWords);
            Result<ThemeAndWords> result1 = crosswordContext.GetAndAddThemeFromFile("../../../../src/test");
            Assert.AreEqual(result1.Status, Status.Success);
            crosswordContext.RemoveThemeFile("../../../../src/test");
        }

        [Test]
        public void GetAndAddThemeFromFileNotExist()
        {
            Result<ThemeAndWords> result1 = crosswordContext.GetAndAddThemeFromFile("test");
            Assert.AreEqual(result1.Status, Status.Error);
        }

        [Test]
        public void GetAndAddThemeFromFileThemeNull()
        {
            Result<ThemeAndWords> result1 = crosswordContext.GetAndAddThemeFromFile(null);
            Assert.AreEqual(result1.Status, Status.Error);
        }

        [Test]
        public void GetAndAddThemeFromFileThemeEmpty()
        {
            Result<ThemeAndWords> result1 = crosswordContext.GetAndAddThemeFromFile("");
            Assert.AreEqual(result1.Status, Status.Error);
        }
    }
}