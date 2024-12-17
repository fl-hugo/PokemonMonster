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
using PokemonMonster.Repositories;
using PokemonMonster.ViewModels;

namespace PokemonMonster.Views
{
    public partial class MonsterDetailsView : Window
    {
        private readonly MonsterRepository monsterRepository;
        private MonsterModel monster;

        public MonsterDetailsView(int monsterId)
        {
            InitializeComponent();
            monsterRepository = new MonsterRepository();

            monster = monsterRepository.GetMonsterById(monsterId);
            DataContext = new MonsterDetailsViewModel(monsterId);
        }
    }
}
