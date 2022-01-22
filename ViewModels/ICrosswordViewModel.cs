using BL.Impl;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public interface ICrosswordViewModel
    {
        List<string> Words { get; set; }
        CrosswordAlgorithm Board { get; }

        Result<ThemeAndWords> GetThemes(string v);
        void ClearLists();
        List<string> GetAllThemes();
    }
}
