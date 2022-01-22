using Entities.Abs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace DTObjects
{
    public class PrintedEditionOrderDTO : IEntity, INotifyPropertyChanged
    {
        public int Id { get; set; }
        private BookDTO printedEdition;
        private DateTime startDate;
        private DateTime endDate;

        public BookDTO PrintedEdition
        {
            get { return printedEdition; }
            set
            {
                printedEdition = value;
                OnPropertyChanged("Name");
            }
        }
        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                startDate = value;
                OnPropertyChanged("Name");
            }
        }
        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                endDate = value;
                OnPropertyChanged("Name");
            }
        }

        public int? BookDebtId { get; set; }
        public int? TakenBookId { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
