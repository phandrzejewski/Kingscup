

using Kingscup.Commands;
using Kingscup.Models;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Kingscup.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            setSave = Visibility.Collapsed;
        }
        public ViewModelBase _currentViewModel { get; set; }
        public ViewModelBase currentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }

        private Visibility _setSave { get; set; }
        public Visibility setSave
        {
            get
            {
                return _setSave;
            }
            set
            {
                _setSave = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand => new DelegateCommand(x => Save());
        private void Save()
        {
            int istGleich = 0;

            RuleViewModel rvm = (RuleViewModel)ListViewModel[1];
            GameViewModel gvm = (GameViewModel)ListViewModel[0];

            foreach (var item in gvm.allCards)
            {
                foreach (var otherItem in rvm.allCards)
                {
                    if (otherItem.Wert == item.Wert)
                    {
                        item.Rule = otherItem.Rule;
                    }
                }
            }
            for (int i = 0; i < rvm.allCards.Count; i++)
            {
                if (rvm.Rules[i] == rvm.allCards[i].Rule)
                {
                    istGleich++;
                }
            }
            if (istGleich != 8)
            {
                for (int i = 0; i < rvm.allCards.Count; i++)
                {
                    rvm.Rules[i] = rvm.allCards[i].Rule;
                }
                gvm.allCards.Clear();
                gvm.initializePics();
            }
            gvm.Player = rvm.Player;
            setSave = Visibility.Collapsed;


            currentViewModel = ListViewModel[0];
        }

        public ICommand SettingCommand => new DelegateCommand(x => Settings());
        private void Settings()
        {
            setSave = Visibility.Visible;
            currentViewModel = ListViewModel[1];
        }

        private List<ViewModelBase> _ListViewModels = new List<ViewModelBase>();
        public List<ViewModelBase> ListViewModel
        {
            get
            {
                return _ListViewModels;
            }
            set
            {
                _ListViewModels = value;
                OnPropertyChanged();
            }

        }
      public void OnWindowClosing(object sender, CancelEventArgs e)
      {
            if (MessageBox.Show("Willst du wirklich beenden?", "Achtung", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
         {
            e.Cancel = true;
         }
      }
   }
}