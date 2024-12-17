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
    class SpellListViewModel : ViewModelBase
    {
        private readonly SpellRepository spellRepository;

        public ObservableCollection<SpellModel> SpellList { get; set; }

        public SpellListViewModel()
        {
            spellRepository = new SpellRepository();
            SpellList = new ObservableCollection<SpellModel>(spellRepository.GetAllSpell());
        }
    }
}
