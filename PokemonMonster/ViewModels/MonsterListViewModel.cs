using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonMonster.Models;
using PokemonMonster.Repositories;

namespace PokemonMonster.ViewModels
{
    class MonsterListViewModel : ViewModelBase
    {
        private readonly MonsterRepository monsterRepository;

        public ObservableCollection<MonsterModel> MonsterList { get; set; }

        public MonsterListViewModel()
        {
            monsterRepository = new MonsterRepository();
            MonsterList = new ObservableCollection<MonsterModel>(monsterRepository.GetAllMonster());

        }
    }
}
