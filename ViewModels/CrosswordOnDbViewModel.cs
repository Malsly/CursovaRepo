using Aspose.Cells;
using BL.Impl;
using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ViewModels
{
    public class CrosswordOnDbViewModel : INotifyPropertyChanged, ICrosswordViewModel
    {
        private List<string> horizontalWords = new List<string>();
        private List<string> verticalWords = new List<string>();
        private List<string> notUsedWords = new List<string>();
        private List<string> words = new List<string>();
        private List<string> order;

        private string excelFileExportName;
        private Word wordToAdd;

        private CrosswordAlgorithm board = new CrosswordAlgorithm(15, 15);
        private ThemeAndWordsService themeAndWordsService = new ThemeAndWordsService();
        private WordService wordsService = new WordService();
        private CrosswordFileManager crosswordFileManager = new CrosswordFileManager(new List<ThemeAndWords>());

        public string ExcelFileExportName
        {
            get { return excelFileExportName; }
            set
            {
                excelFileExportName = value;
                OnPropertyChanged("ExcelFileExportName");
            }
        }

        public Word WordToAdd
        {
            get { return wordToAdd; }
            set
            {
                wordToAdd = value;
                OnPropertyChanged("WordToAdd");
            }
        }

        public List<string> HorizontalWords
        {
            get { return horizontalWords; }
            set
            {
                horizontalWords = value;
                OnPropertyChanged("WordToAdd");
            }
        }

        public List<string> VerticalWords
        {
            get { return verticalWords; }
            set
            {
                verticalWords = value;
                OnPropertyChanged("WordToAdd");
            }
        }

        public List<string> NotUsedWords
        {
            get { return notUsedWords; }
            set
            {
                notUsedWords = value;
                OnPropertyChanged("WordToAdd");
            }
        }

        public List<string> Words
        {
            get { return words; }
            set
            {
                words = value;
                OnPropertyChanged("WordToAdd");
            }
        }

        public CrosswordAlgorithm Board
        {
            get { return board; }
        }

        protected RelayCommand exportCommand;

        public RelayCommand ExportCommand
        {
            get
            {
                return exportCommand ??
                    (exportCommand = new RelayCommand(obj =>
                    {
                        Export();
                    }));
            }
        }

        private RelayCommand generateCommand;

        public RelayCommand GenerateCommand
        {
            get
            {
                return generateCommand ??
                    (generateCommand = new RelayCommand(obj =>
                    {
                        Generate();
                    }));
            }
        }

        public List<string> GetAllThemes()
        {
            List<string> themesNames = new List<string>();

            foreach (ThemeAndWords theme in themeAndWordsService.GetAll().Value) 
            {
                themesNames.Add(theme.Theme);
            }

            return themesNames;
        }

        public Result<ThemeAndWords> GetThemes(string theme)
        {
            return new Result<ThemeAndWords>()
            {
                Value = themeAndWordsService.GetAll().Value.Find(t => t.Theme == theme),
                Status = Entities.Enum.Status.Success
            };
        }

        public void ClearLists()
        {
            horizontalWords.Clear();
            verticalWords.Clear();
            notUsedWords.Clear();
            words.Clear();
        }

        static int Comparer(string a, string b)
        {
            var temp = a.Length.CompareTo(b.Length);
            return temp == 0 ? a.CompareTo(b) : temp;
        }

        public void Export()
        {
            crosswordFileManager.Export(board, excelFileExportName);
        }

        public void Generate()
        {
            words.Sort(Comparer);
            words.Reverse();
            order = words;

            horizontalWords.Clear();
            verticalWords.Clear();
            notUsedWords.Clear();
            board.Reset();

            foreach (var word in order)
            {
                switch (board.AddWord(word))
                {
                    case 0:
                        horizontalWords.Add(word);
                        break;
                    case 1:
                        verticalWords.Add(word);
                        break;
                    default:
                        notUsedWords.Add(word);
                        break;

                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public void Test()
        {
            CrosswordFileManager crosswordContextOld = new CrosswordFileManager(new List<ThemeAndWords>());
            

            //List<ThemeAndWords> themeAndWords = new List<ThemeAndWords>();
            //themeAndWords.Add(crosswordContextOld.GetThemeFromFile("Animals").Value);
            //themeAndWords.Add(crosswordContextOld.GetThemeFromFile("Famous painters").Value);
            //themeAndWords.Add(crosswordContextOld.GetThemeFromFile("Presidents of the United States").Value);
            //foreach (ThemeAndWords theme in themeAndWords)
            //{
            //    themeAndWordsService.Add(theme);
            //}

            var bbb = wordsService.GetAll();

            var aaa = themeAndWordsService.GetAll();

            //foreach (ThemeAndWords theme in aaa.Value)
            //{
            //    themeAndWordsService.Delete(theme);
            //}

            //foreach (Word word in bbb.Value)
            //{
            //    wordsService.Delete(word);
            //}

            //var ccc = themeAndWordsService.GetAll();
            //var ttt = wordsService.GetAll();

        }
    }
}
