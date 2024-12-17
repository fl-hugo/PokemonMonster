using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PokemonMonster.Models;

namespace PokemonMonster.Views
{
    public partial class SpellListView : Window
    {
        public SpellListView()
        {
            InitializeComponent();
        }

        private void NavigateToMonsterListClick(object sender, RoutedEventArgs e)
        {
            var monsterListView = new MonsterListView();
            Application.Current.MainWindow = monsterListView;
            monsterListView.Show();
            this.Close();
        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is SpellModel spell)
            {
                var detailsView = new SpellDetailsView(spell.Id);
                detailsView.Show();
            }
        }
    }
}
