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
        public List<string> regeln = new List<string>() { "Links trinken", "Selbst trinken", "Rechts trinken", "Regel ausdenken", "Alle Männer trinken", "Alle Frauen trinken", "Schluck in die Mitte geben", "Alle Trinken" };

        public App()
        {
            var CardList = new List<Card>();
            ObservableCollection<Player> PlayerList = new ObservableCollection<Player>();

            gvm = new GameViewModel(CardList, regeln, PlayerList);
            rvm = new RuleViewModel(CardList, regeln, PlayerList);

            mvm.ListViewModel.Add(gvm);
            mvm.ListViewModel.Add(rvm);

            main.Closing += mvm.OnWindowClosing;

            mvm.currentViewModel = gvm;

            main.DataContext = mvm;
            main.Show();

        }

    }
}
