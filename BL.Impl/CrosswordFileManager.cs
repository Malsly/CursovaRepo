using Aspose.Cells;
using Entities;
using Entities.Enum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Impl
{
    public class CrosswordFileManager
    {
        public List<ThemeAndWords> ThemeAndWordsList { get; }

        public CrosswordFileManager(List<ThemeAndWords> themeAndWordsList) 
        {
            ThemeAndWordsList = themeAndWordsList;
        }

        public Result<ThemeAndWords> AddThemeAndWords(ThemeAndWords themeAndWords) 
        {
            if (!string.IsNullOrWhiteSpace(themeAndWords.Theme) && themeAndWords.Words != null && themeAndWords.Words.Any()) 
            {
                ThemeAndWordsList.Add(themeAndWords);
                return new Result<ThemeAndWords> { Status = Status.Success };
            }
            return new Result<ThemeAndWords> { Status = Status.Error, Message = "Empty list or no theme", Value = themeAndWords };
        }

        public Result<ThemeAndWords> GetTheme(string themeName)
        {
            if (!string.IsNullOrWhiteSpace(themeName))
            {
                ThemeAndWords themeAndWords = ThemeAndWordsList.FirstOrDefault(themeAndWords => themeAndWords.Theme == themeName);
                if (themeAndWords == null)
                    return new Result<ThemeAndWords> { Status = Status.Error, Message = "Theme not exist"};
                return new Result<ThemeAndWords> { Status = Status.Success, Value = themeAndWords };
            }
            return new Result<ThemeAndWords> { Status = Status.Error, Message = "Empty theme name" };
        }

        public Result<ThemeAndWords> RemoveTheme(string themeName)
        {
            if (!string.IsNullOrWhiteSpace(themeName))
            {
                ThemeAndWords themeAndWordsToDelete = ThemeAndWordsList.FirstOrDefault(themeAndWords => themeAndWords.Theme == themeName);
                if (themeAndWordsToDelete == null)
                    return new Result<ThemeAndWords> { Status = Status.Warning, Message = "Theme not exist" };
                ThemeAndWordsList.Remove(themeAndWordsToDelete);
                return new Result<ThemeAndWords> { Status = Status.Success };
            }
            return new Result<ThemeAndWords> { Status = Status.Error, Message = "Empty theme name" };
        }

        public Result<ThemeAndWords> AddThemeFile(ThemeAndWords themeAndWords)
        {
            if (!string.IsNullOrWhiteSpace(themeAndWords.Theme) && themeAndWords.Words != null && themeAndWords.Words.Any()) 
            {
                string fileName = @"..\src\" + themeAndWords.Theme + ".txt";
                if (!File.Exists(fileName))
                {
                    using (StreamWriter writer = File.CreateText(fileName))
                    {
                        foreach (Word word in themeAndWords.Words) 
                        {
                            writer.WriteLine(word.Value);
                        }
                    }
                    return new Result<ThemeAndWords> { Status = Status.Success, Value = themeAndWords };
                }
                return new Result<ThemeAndWords> { Status = Status.Error, Message = "File already exist" };
            }
            return new Result<ThemeAndWords> { Status = Status.Error, Message = "Empty theme name or word list" };
        }

        public Result<ThemeAndWords> RemoveThemeFile(string themeName)
        {
            if (!string.IsNullOrWhiteSpace(themeName))
            {
                string fileName = @"..\src\" + themeName + ".txt";
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                    return new Result<ThemeAndWords> { Status = Status.Success };
                }
                return new Result<ThemeAndWords> { Status = Status.Warning, Message = "File not exist" };
            }
            return new Result<ThemeAndWords> { Status = Status.Error, Message = "Empty theme name" };
        }

        public Result<ThemeAndWords> GetThemeFromFile(string themeName) 
        {
            if (!string.IsNullOrWhiteSpace(themeName)) 
            {
                string fileName = @"..\src\" + themeName + ".txt";
                if (!File.Exists(fileName))
                    return new Result<ThemeAndWords> { Status = Status.Error, Message = "File not exist" };
                var reader = new StreamReader(fileName, true);
                List<Word> words = new List<Word>();

                while (!reader.EndOfStream)
                {
                    words.Add(new Word() { Value = reader.ReadLine().Trim() } );
                }
                if (words.Any())
                {
                    ThemeAndWords themeAndWords = new ThemeAndWords() { Theme = fileName, Words = words };
                    reader.Close();
                    return new Result<ThemeAndWords>() { Status = Status.Success, Value = themeAndWords };
                }
                return new Result<ThemeAndWords>() { Status = Status.Error, Message = "File not contain words" };
            }
            return new Result<ThemeAndWords>() { Status = Status.Error, Message = "Filename is null or empty" };
        }

        public Result<ThemeAndWords> GetAndAddThemeFromFile(string themeName)
        {
            if (!string.IsNullOrWhiteSpace(themeName))
            {
                string fileName = @"..\src\" + themeName + ".txt";
                if (!File.Exists(fileName))
                    return new Result<ThemeAndWords> { Status = Status.Error, Message = "File not exist" };
                var reader = new StreamReader(fileName, true);
                List<Word> words = new List<Word>();

                while (!reader.EndOfStream)
                {
                    words.Add(new Word() { Value = reader.ReadLine().Trim() });;
                }
                if (words.Any())
                {
                    ThemeAndWords themeAndWords = new ThemeAndWords() { Theme = themeName, Words = words };
                    AddThemeAndWords(themeAndWords);
                    reader.Close();
                    return new Result<ThemeAndWords>() { Status = Status.Success, Value = themeAndWords };
                }
                return new Result<ThemeAndWords>() { Status = Status.Error, Message = "File not contain words" };
            }
            return new Result<ThemeAndWords>() { Status = Status.Error, Message = "Filename is null or empty" };
        }

        public Result<IEnumerable<string>> GetAllThemeNames() 
        {
            List<string> fileNames = new List<string>();
            DirectoryInfo d = new DirectoryInfo(@"..\src\");
            foreach (FileInfo file in d.GetFiles("*.txt"))
            {
                fileNames.Add(file.Name.Substring(0, file.Name.LastIndexOf('.')));
            }
            return new Result<IEnumerable<string>>() { Status = Status.Success, Value = fileNames};
        }

        public void Export(CrosswordAlgorithm _board, string excelFileExportName)
        {
            var board = _board.GetBoard;
            Workbook workbook = new Workbook();
            Worksheet worksheet = workbook.Worksheets[0];
            for (int i = 0; i < 15; i++)
                for (int j = 0; j < 15; j++)
                {
                    worksheet.Cells[i, j].Value = board[i, j];
                }
            workbook.Save(@"..\..\..\..\src\" + excelFileExportName + ".xlsx");
        }
    }
}
