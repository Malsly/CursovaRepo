using System;
using System.Collections.Generic;
using System.Text;
using Entities.Abs;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace DTObjects
{
    public class BookDTO: IPrintedEdition, INotifyPropertyChanged
    {
        public int? PrintedEditionOrderID { get; set; }
        private string name;
        private ObservableCollection<AuthorDTO> authors;
        private float rate;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public ObservableCollection<AuthorDTO> Authors
        {
            get { return authors; }
            set
            {
                authors = value;
                OnPropertyChanged("Authors");
            }
        }

        public float Rate
        {
            get { return rate; }
            set
            {
                rate = value;
                OnPropertyChanged("Rate");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
