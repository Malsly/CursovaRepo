using Aspose.Cells;
using BL.Impl;
using Entities;
using Entities.Enum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ViewModel;
using ViewModels;

namespace Library
{
    public partial class MainWindow : Window
    {
        ICrosswordViewModel viewModel;
        private List<Button> _buttons;
        private string location;

        public MainWindow()
        {
            bool useDatabase = MessageBox.Show("You want use database?",
                    "Choose data model",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes;

            if (useDatabase)
            {
                DataContext = new CrosswordOnDbViewModel();
                viewModel = (DataContext as CrosswordOnDbViewModel);
                location = "";
            }
            else
            {
                DataContext = new CrosswordOnFilesViewModel();
                viewModel = (DataContext as CrosswordOnFilesViewModel);
                location = @"..\..\..\..\src\";
            }

            InitializeComponent();
            InitBoard();
        }


        private void InitBoard() 
        { 
            _buttons = new List<Button> { button4, button5, button6 };

            for (var i = 0; i < viewModel.Board.N; i++)
            {
                for (var j = 0; j < viewModel.Board.M; j++)
                {
                    var b = new Button { Background = _buttons[0].Background, Content = "" };

                    Grid.SetRow(b, i);
                    Grid.SetColumn(b, j);
                    grid1.Children.Add(b);


                }
            }
        }

        private void Button2Click(object sender, RoutedEventArgs e)
        {
            var word = newWordTextBox.Text.Trim();
            if (word.Length != 0)
            {

                if (viewModel.Words.Contains(word))
                {
                    MessageBox.Show("This word already exists in the crossword", "Alert!!", MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                    return;
                }

                viewModel.Words.Add(word);
                listView1.Items.Add(word);
            }

            newWordTextBox.Text = "";
            newWordTextBox.Focus();
        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            ClearBoard();
            ActualizeData();
            newWordTextBox.Focus();
        }

        void ActualizeData()
        {
            var count = viewModel.Board.N * viewModel.Board.M;
            var board = viewModel.Board.GetBoard;
            var p = 0;

            for (var i = 0; i < viewModel.Board.N; i++)
            {
                for (var j = 0; j < viewModel.Board.M; j++)
                {
                    var letter = board[i, j] == '*' ? ' ' : board[i, j];
                    if (letter != ' ') count--;
                    ((Button)grid1.Children[p]).FontSize = 15;
                    ((Button)grid1.Children[p]).Content = letter.ToString();
                    p++;
                }
            }
        }

        void ClearBoard()
        {
            var p = 0;
            for (var i = 0; i < viewModel.Board.N; i++)
            {
                for (var j = 0; j < viewModel.Board.M; j++)
                {
                    ((Button)grid1.Children[p]).Content = "";
                    ((Button)grid1.Children[p]).Background = _buttons[0].Background;
                    p++;
                }
            }
        }

        private void NewButton_Click_1(object sender, RoutedEventArgs e)
        {
            viewModel.ClearLists();
            listView1.Items.Clear();
            newWordTextBox.Text = "";
            ClearBoard();
            newWordTextBox.Focus();
        }

        private void NewWordTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Button2Click(null, null);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            Result<ThemeAndWords> result = viewModel.GetThemes(location + cmb.SelectedItem.ToString());
            if (result.Status == Status.Success)
            {
                foreach (Word word in result.Value.Words)
                {
                    listView1.Items.Add(word.Value);
                    viewModel.Words.Add(word.Value);
                }
            }
            else
            {
                MessageBox.Show(result.Message, result.Message, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ComboBox_Initialized(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            List<string> themes = viewModel.GetAllThemes();
            foreach (string theme in themes)
            {
                cmb.Items.Add(theme);
            }
        }
    }
}

