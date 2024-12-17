using System.Windows;
using PokemonMonster.Models;
using System.Windows.Controls;
using Microsoft.Win32;

namespace PokemonMonster.Views
{
    public partial class MonsterListView : Window
    {
        public MonsterListView()
        {
            InitializeComponent();
        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is MonsterModel monster)
            {
                var detailsView = new MonsterDetailsView(monster.Id);
                detailsView.Show();
            }
        }

        private void PlayClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is MonsterModel monster)
            {
                var battleView = new BattleView(monster);
                Application.Current.MainWindow = battleView;
                battleView.Show();
                this.Close();
            }
        }

        private void NavigateToSpellListClick(object sender, RoutedEventArgs e)
        {
            var spellListView = new SpellListView();
            Application.Current.MainWindow = spellListView;
            spellListView.Show();
            this.Close();
        }
    }
}
