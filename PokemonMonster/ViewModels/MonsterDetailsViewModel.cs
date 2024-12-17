using PokemonMonster.Models;
using PokemonMonster.Repositories;

namespace PokemonMonster.ViewModels
{
    public class MonsterDetailsViewModel : ViewModelBase
    {
        private readonly MonsterRepository _monsterRepository;

        public MonsterModel SelectedMonster { get; set; }

        public MonsterDetailsViewModel(int monsterId)
        {
            _monsterRepository = new MonsterRepository();
            LoadMonsterDetails(monsterId);
        }

        private void LoadMonsterDetails(int monsterId)
        {
            SelectedMonster = _monsterRepository.GetMonsterById(monsterId);
        }
    }
}
