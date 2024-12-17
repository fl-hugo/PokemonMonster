using System.Windows;
using System.Windows.Controls;
using PokemonMonster.Models;
using PokemonMonster.ViewModels;

namespace PokemonMonster.Views
{
    public partial class BattleView : Window
    {
        public BattleView(MonsterModel playerPokemon)
        {
            InitializeComponent();
            DataContext = new BattleViewModel(playerPokemon);
        }
        
        private void QuitGameClick(object sender, RoutedEventArgs e)
        {
            var monsterListView = new MonsterListView();
            Application.Current.MainWindow = monsterListView;
            monsterListView.Show();
            this.Close();
        }

        private void SpellClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is SpellModel spell)
            {
                
            }
        }
    }
}
