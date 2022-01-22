using Aspose.Cells;
using BL.Impl;
using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using ViewModels;

namespace ViewModel
{
    public class CrosswordOnFilesViewModel : INotifyPropertyChanged, ICrosswordViewModel
    {
        private List<string> horizontalWords = new List<string>();
        private List<string> verticalWords = new List<string>();
        private List<string> notUsedWords = new List<string>();
        private List<string> words = new List<string>();
        private List<string> order;

        private string excelFileExportName;
        private Word wordToAdd;

        private CrosswordAlgorithm board = new CrosswordAlgorithm(15, 15);
        private CrosswordFileManager crosswordContext = new CrosswordFileManager(new List<ThemeAndWords>());

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

        public Result<ThemeAndWords> GetThemes(string theme) 
        {
            return crosswordContext.GetThemeFromFile(theme);
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
            crosswordContext.Export(board, excelFileExportName);
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

        public List<string> GetAllThemes()
        {
            List<string> themesNames = new List<string>();
            DirectoryInfo d = new DirectoryInfo(@"..\..\..\..\src");
            foreach (FileInfo file in d.GetFiles("*.txt"))
            {
                themesNames.Add(file.Name.Substring(0, file.Name.LastIndexOf('.')));
            }
            return themesNames;
        }
    }
}
