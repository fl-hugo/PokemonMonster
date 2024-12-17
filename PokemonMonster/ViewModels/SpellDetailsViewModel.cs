using System.Collections.ObjectModel;
using PokemonMonster.Models;
using PokemonMonster.Repositories;

namespace PokemonMonster.ViewModels
{
    public class SpellDetailsViewModel : ViewModelBase
    {
        private readonly SpellRepository _spellRepository;

        public SpellModel SelectedSpell { get; set; }

        public ObservableCollection<MonsterModel> MonstersWithSpell { get; set; }

        public SpellDetailsViewModel(int spellId)
        {
            _spellRepository = new SpellRepository();
            LoadSpellDetails(spellId);
        }

        private void LoadSpellDetails(int spellId)
        {
            SelectedSpell = _spellRepository.GetSpellById(spellId);
            MonstersWithSpell = new ObservableCollection<MonsterModel>(_spellRepository.GetMonstersBySpellId(SelectedSpell.Id));
        }
    }
}
