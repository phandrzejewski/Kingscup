

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
            SetSave = Visibility.Collapsed;
        }
        public ViewModelBase _currentViewModel { get; set; }
        public ViewModelBase CurrentViewModel
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
        public Visibility SetSave
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

        public ICommand AbortCommand => new DelegateCommand(x => Abort());

        private RuleViewModel RuleViewModel { get; set; }
        private GameViewModel GameViewModel { get; set; }
        private List<string> CurrentRules { get; set; }
        private List<Card> CurrentCards { get; set; }
        private int CurrentCountPlayers { get; set; }
        private ObservableCollection<Player> CurrentPlayers { get; set; }

        private void Abort()
        {
            SetSave = Visibility.Collapsed;
            RuleViewModel.Rules = CurrentRules;
            RuleViewModel.SelectedAmountPlayers = CurrentCountPlayers;
            RuleViewModel.Player = CurrentPlayers;
            RuleViewModel.AllCards = CurrentCards;
            CurrentViewModel = ListViewModel[0];
        }

        private void Save()
        {
            int istGleich = 0;
           
            foreach (var item in GameViewModel.AllCards)
            {
                foreach (var otherItem in RuleViewModel.AllCards)
                {
                    if (otherItem.Wert == item.Wert)
                    {
                        item.Rule = otherItem.Rule;
                    }
                }
            }
            for (int i = 0; i < RuleViewModel.AllCards.Count; i++)
            {
                if (RuleViewModel.Rules[i] == RuleViewModel.AllCards[i].Rule)
                {
                    istGleich++;
                }
            }
            if (istGleich != 13)
            {
                for (int i = 0; i < RuleViewModel.AllCards.Count; i++)
                {
                    RuleViewModel.Rules[i] = RuleViewModel.AllCards[i].Rule;
                }
                GameViewModel.AllCards.Clear();
                GameViewModel.InitializePics();
            }
            GameViewModel.Player = RuleViewModel.Player;
            SetSave = Visibility.Collapsed;

            CurrentViewModel = ListViewModel[0];
        }

        public ICommand SettingCommand => new DelegateCommand(x => Settings());
        private void Settings()
        {
            SetSave = Visibility.Visible;

            RuleViewModel = (RuleViewModel)ListViewModel[1];
            GameViewModel = (GameViewModel)ListViewModel[0];
            CurrentRules = RuleViewModel.Rules;
            CurrentCountPlayers = RuleViewModel.SelectedAmountPlayers;
            CurrentPlayers = RuleViewModel.Player;
            CurrentCards = RuleViewModel.AllCards;
            CurrentViewModel = ListViewModel[1];
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
            if (MessageBox.Show("Willst du wirklich beenden?", "Willst du wirklich beenden?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            {
                e.Cancel = true;
            }            
        }
    }
}