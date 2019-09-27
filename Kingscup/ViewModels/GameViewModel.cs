using Kingscup.Commands;
using Kingscup.Models;
using Kingscup.Views;
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
   class GameViewModel : ViewModelBase
   {
      public GameViewModel(List<Card> list, List<string> Rules, ObservableCollection<Player> Player)
      {
         allCards = list;
         this.Player = Player;
         this.Rules = Rules;
         initializePics();
      }
      
      private ObservableCollection<Player> _Player { get; set; }
      public ObservableCollection<Player> Player
      {
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
      private Player _CurrentPlayer { get; set; }
      public Player CurrentPlayer
      {
         get
         {
            return _CurrentPlayer;
         }
         set
         {
            _CurrentPlayer = value;
            OnPropertyChanged();
         }
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
      private List<string> _Rules { get; set; }
      public List<string> Rules
      {
         get { return _Rules; }
         set
         {
            _Rules = value;
            OnPropertyChanged();
         }
      }
      private string _OutputText { get; set; }
      public string OutputText
      {
         get
         {
            return _OutputText;
         }
         set
         {
            _OutputText = value;
            OnPropertyChanged();
         }
      }
      private Card _currentCard { get; set; }
      public Card currentCard
      {
         get
         {
            return _currentCard;
         }
         set
         {
            _currentCard = value;
            OnPropertyChanged();
         }
      }
      public int CountKings { get; set; }
      private string _DrawnKings { get; set; }
      public string DrawnKings
      {
         get
         {
            return _DrawnKings;
         }
         set
         {
            _DrawnKings = value;
            OnPropertyChanged();
         }
      }
      private int _currentNumber { get; set; }
      public int currentNumber
      {
         get
         {
            return _currentNumber;
         }
         set
         {
            _currentNumber = value;
            OnPropertyChanged();
         }
      }
      private int _Border { get; set; }
      public int Border
      {
         get
         {
            return _Border;
         }
         set
         {
            _Border = value;
            OnPropertyChanged();
         }
      }
      private int _CurrentPlayerNumber { get; set; }
      public int CurrentPlayerNumber
      {
         get
         {
            return _CurrentPlayerNumber;
         }
         set
         {
            _CurrentPlayerNumber = value;
            OnPropertyChanged();
         }
      }
      public ICommand PreviewMouseDownICommand => new DelegateCommand(x => getRandomCard());
      public string getLetterForNumber(int number)
      {
         string letter;
         if (number == 0)
         {
            letter = "C";
         }
         else if (number == 1)
         {
            letter = "H";
         }
         else if (number == 2)
         {
            letter = "P";
         }
         else
         {
            letter = "K";
         }
         return letter;
      }
      public MessageBoxView msg;
      public void getRandomCard()
      {
        
         if (CurrentPlayerNumber >= Player.Count)
         {
            CurrentPlayerNumber = 0;
         }

         CurrentPlayer = Player[CurrentPlayerNumber];
        
         if (allCards.Count == 0)
         {
            initializePics();
            Border = 0;
         }
         else
         {
            if (Border != 1)
               Border = 1;

            Random rnd = new Random();
            int zufall = rnd.Next(0, allCards.Count);
            currentCard = allCards[zufall];

            allCards.Remove(currentCard);
            currentNumber = allCards.Count;
            if (currentNumber != 1)
            {
               OutputText = "Noch " + currentNumber + " Karten im Deck";
            }
            else
            {
               OutputText = "Noch " + currentNumber + " Karte im Deck";
            }

            if (currentCard.Wert == 13)
            {
               CountKings++;
               DrawnKings = "Gezogene Könige: " + CountKings;
            }
            if (CountKings == 4)
            {
               currentCard.Rule = "";
               msg = new MessageBoxView(CurrentPlayer.PlayerName + "\nhat den letzten König gezogen!\nViel Spaß beim Saufen!\nProst");
               msg.ShowDialog();

               allCards.Clear();
               initializePics();
            }
            CurrentPlayerNumber++;
         }
      }
      public void initializePics()
      {
         CountKings = 0;
         Border = 0;
         Random rnd = new Random();
         allCards.Add(new Card(0, "pack://application:,,,/Kingscup;component/Cardbacks/Card" + rnd.Next(1, 8) + ".gif", ""));

         currentNumber = 32;
         currentCard = allCards[0];
         OutputText = "Noch " + currentNumber + " Karten im Deck";
         DrawnKings = "Gezogene Könige: " + CountKings;
         allCards.Remove(currentCard);
         for (var i = 7; i < 15; i++)
         {
            for (var j = 0; j < 4; j++)
            {
               this.allCards.Add(new Card(i, "pack://application:,,,/Kingscup;component/Cards/" + i + getLetterForNumber(j) + ".png", Rules[i - 7]));
            }
         }
      }
   }
}
