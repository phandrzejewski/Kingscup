using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Kingscup.Models
{
    public class Card : INotifyPropertyChanged
    {
        public int Wert { get; set; }
        public string Name { get; set; }
        private string _Rule { get; set; }
        public string Rule
        {
            get { return _Rule; }
            set
            {
                _Rule = value;
                OnPropertyChanged();
            }
        }

        public Card(int Wert, string Name, string Rule)
        {
            this.Wert = Wert;
            this.Name = Name;
            this.Rule = Rule;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
