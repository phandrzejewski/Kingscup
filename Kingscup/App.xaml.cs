using Kingscup.Models;
using Kingscup.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Kingscup
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        MainWindow main = new Kingscup.MainWindow();
        MainViewModel mvm = new MainViewModel();
        RuleViewModel rvm;
        GameViewModel gvm;
        public List<string> regeln = new List<string>()
        {
            "Verteile 2 Schlücke",                                              //2
            "Trink 1",                                                          //3
            "Verteile 2, Trinke 2",                                             //4
            "Alle Männer trinken",                                              //5
            "Alle Damen trinken",                                               //6
            "Links trinken",                                                    //7
            "Trinkpartner",                                                     //8
            "Rechts trinken",                                                   //9
            "Alle Trinken",                                                     //10
            "Kategorie ausdenken - Wer zuerst etwas doppeltes sagt verliert!",  //Bube
            "Fragenmeister - Dem Fragenmeister bloß keine Fragen beantworten!", //Dame
            "Schluck in die Mitte geben",                                       //Koenig
            "Regel ausdenken"                                                   //Ass
        };

        public App()
        {
            var CardList = new List<Card>();
            ObservableCollection<Player> PlayerList = new ObservableCollection<Player>();

            gvm = new GameViewModel(CardList, regeln, PlayerList);
            rvm = new RuleViewModel(CardList, regeln, PlayerList);

            mvm.ListViewModel.Add(gvm);
            mvm.ListViewModel.Add(rvm);

            main.Closing += mvm.OnWindowClosing;

            mvm.CurrentViewModel = gvm;

            main.DataContext = mvm;
            main.Show();
        }
    }
}
