﻿using Kingscup.Commands;
using Kingscup.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Kingscup.ViewModels
{
    class RuleViewModel : ViewModelBase
    {
        public RuleViewModel(List<Card> list, List<string> Rules, ObservableCollection<Player> Player)
        {
            this.Rules = Rules;
            tempList = list;
            this.Player = Player;

            bool vorhanden = false;
            allCards = new List<Card>();
            foreach (var item in list)
            {
                foreach (var otherItem in allCards)
                {
                    if (otherItem.Wert == item.Wert)
                        vorhanden = true;
                }
                if (!vorhanden)
                {
                    allCards.Add(item);
                }
                vorhanden = false;
            }
            InitAmountPlayers();
            InitPlayers();
        }

        private void InitPlayers()
        {
            for (int i = 0; i < SelectedAmountPlayers; i++)
            {
                Player.Add(new Player("Player"));
            }
        }

        private void InitAmountPlayers()
        {
            AmountPlayers = new List<int>();

            AmountPlayers.Add(1);
            AmountPlayers.Add(2);
            AmountPlayers.Add(3);
            AmountPlayers.Add(4);
            AmountPlayers.Add(5);
            AmountPlayers.Add(6);
            AmountPlayers.Add(7);
            AmountPlayers.Add(8);
            AmountPlayers.Add(9);
            AmountPlayers.Add(10);
            AmountPlayers.Add(11);
            AmountPlayers.Add(12);

            SelectedAmountPlayers = 1;
        }

        private List<Card> _allCards { get; set; }
        public List<Card> allCards
        {
            get
            {
                return _allCards;
            }
            set
            {
                _allCards = value;
                OnPropertyChanged();
            }
        }
        public List<Card> tempList { get; set; }        
        private List<string> _Rules { get; set; }
        public List<string> Rules
        {
            get
            {
                return _Rules;
            }
            set
            {
                _Rules = value;
                OnPropertyChanged();
            }
        }        
        public List<int> AmountPlayers { get; set; }
        private int _SelectedAmountPlayers { get; set; } 
        public int SelectedAmountPlayers
        {
            get
            {
                return _SelectedAmountPlayers;
            }
            set
            {                
                _SelectedAmountPlayers = value;
                OnPropertyChanged();
            }
        }
        public ICommand ChangeSizeCommand => new DelegateCommand(x => ChangeSizeOfList());
        private void ChangeSizeOfList()
        {
            List<Player> list = Player.ToList();
            
            int cur = list.Count;
            if (SelectedAmountPlayers < cur)
                list.RemoveRange(SelectedAmountPlayers, cur - SelectedAmountPlayers);
            else if (SelectedAmountPlayers > cur)
            {
                for (int i = 0; i < (SelectedAmountPlayers - cur); i++)
                {
                    list.Add(new Player("Player"));
                }               
            }
            Player = ConvertToObservable.ToObservableCollection(list);
        }
        private ObservableCollection<Player> _Player { get; set; }
        public ObservableCollection<Player> Player {
            get
            {
                return _Player;
            }
            set
            {
                _Player = value;
                OnPropertyChanged();
            }
        }
        
    }
    public static class ConvertToObservable
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerableList)
        {
            if (enumerableList != null)
            {
                // Create an emtpy observable collection object
                var observableCollection = new ObservableCollection<T>();

                // Loop through all the records and add to observable collection object
                foreach (var item in enumerableList)
                {
                    observableCollection.Add(item);
                }

                // Return the populated observable collection
                return observableCollection;
            }
            return null;
        }
    }
}