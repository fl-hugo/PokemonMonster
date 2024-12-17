using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using PokemonMonster.Models;
using PokemonMonster.Repositories;
using PokemonMonster.Views;

namespace PokemonMonster.ViewModels
{
    public class BattleViewModel : ViewModelBase
    {
        public MonsterModel PlayerPokemon { get; set; }
        public MonsterModel OpponentPokemon { get; set; }
        public string CombatMessages { get; set; }
        private readonly MonsterRepository _monsterRepository;
        public ICommand CastSpellCommand { get; }
        private List<MonsterModel> _availableOpponents;
        private Random _random;
        private int MonsterCounter = 0;

        public int PlayerHpPercent => PlayerPokemon != null && PlayerPokemon.MaxHp > 0
            ? (int)((double)PlayerPokemon.Hp / PlayerPokemon.MaxHp * 100)
            : 0;
        public int OpponentHpPercent => OpponentPokemon != null && OpponentPokemon.MaxHp > 0
            ? (int)((double)OpponentPokemon.Hp / OpponentPokemon.MaxHp * 100)
            : 0;

        public BattleViewModel(MonsterModel playerPokemon)
        {   
            _monsterRepository = new MonsterRepository();
            PlayerPokemon = _monsterRepository.GetMonsterById(playerPokemon.Id);

            _availableOpponents = _monsterRepository.GetAllMonster().ToList();
            _random = new Random();

            LoadNewOpponent();

            CastSpellCommand = new RelayCommand<SpellModel>(CastSpell);
        }

        private async void CastSpell(SpellModel spell)
        {
            if (OpponentPokemon != null && spell != null)
            {
                CombatMessages = $"{PlayerPokemon.Name} lance {spell.Name} ! Il inflige {spell.Damage} dégâts.";
                OnPropertyChanged(nameof(CombatMessages));
                await Task.Delay(1500); 

                //Ceci est une tentative d'animer la barre de vie du pokemon
                int totalDamage = spell.Damage;
                while (totalDamage > 0 && OpponentPokemon.Hp > 0)
                {
                    OpponentPokemon.Hp -= 1;
                    totalDamage -= 1;

                    OnPropertyChanged(nameof(OpponentPokemon));
                    OnPropertyChanged(nameof(OpponentHpPercent));
                }

                if (OpponentPokemon.Hp < 0)
                    OpponentPokemon.Hp = 0;

                OnPropertyChanged(nameof(OpponentPokemon));

                if (OpponentPokemon.Hp == 0) 
                {
                    CombatMessages = $"{OpponentPokemon.Name} ne peut plus continuer le combat ! ";
                    OnPropertyChanged(nameof(CombatMessages));
                    await Task.Delay(1500); 

                    LoadNewOpponent();
                    OnPropertyChanged(nameof(OpponentPokemon));
                }
                else
                {
                    OnPropertyChanged(nameof(OpponentPokemon)); 
                    await Task.Delay(1500);
                    OpponentTurn();
                }
            }
        }

        private async void OpponentTurn()
        {
            var randomSpell = OpponentPokemon.Spells[_random.Next(OpponentPokemon.Spells.Count)];

            CombatMessages = $"{OpponentPokemon.Name} lance {randomSpell.Name} ! Il inflige {randomSpell.Damage} dégâts.";
            OnPropertyChanged(nameof(CombatMessages));
            await Task.Delay(1500);

            //Ceci est une tentative d'animer la barre de vie du pokemon
            int totalDamage = randomSpell.Damage;
            while (totalDamage > 0 && PlayerPokemon.Hp > 0)
            {
                PlayerPokemon.Hp -= 1;
                totalDamage -= 1;

                OnPropertyChanged(nameof(PlayerPokemon));
                OnPropertyChanged(nameof(PlayerHpPercent));
            }

            if (PlayerPokemon.Hp < 0)
                PlayerPokemon.Hp = 0;

            OnPropertyChanged(nameof(PlayerPokemon)); 
            if (PlayerPokemon.Hp == 0)
            {
                CombatMessages = $"{PlayerPokemon.Name} ne peut plus continuer, {OpponentPokemon.Name} remporte le combat ! ";
                OnPropertyChanged(nameof(CombatMessages));
                await Task.Delay(1500);

                MessageBox.Show($"{PlayerPokemon.Name} est K.O.! L'adversaire a gagné.", "Défaite", MessageBoxButton.OK, MessageBoxImage.Information);
                await Task.Delay(1500);
                Application.Current.Dispatcher.Invoke(() =>
                {
                    var monsterListView = new MonsterListView();
                    monsterListView.DataContext = new MonsterListViewModel();
                    Application.Current.MainWindow = monsterListView;
                    monsterListView.Show();
                });
            }
        }


        private async void LoadNewOpponent()
        {
            if (_availableOpponents.Count == 0) return;

            MonsterCounter += 1;

            var newOpponent = _availableOpponents[_random.Next(_availableOpponents.Count)];
            var newOpponentModel = _monsterRepository.GetMonsterById(newOpponent.Id);

            OpponentPokemon = newOpponentModel;

            CombatMessages = $"Un nouvel adversaire apparaît : {OpponentPokemon.Name} !";
            OnPropertyChanged(nameof(CombatMessages));

            if (MonsterCounter > 3) 
            { 
                await Task.Delay(2000);

                var boostType = _random.Next(2);
                if (boostType == 0) 
                {
                    var spellBoostFactor = 1 + (_random.Next(1, 3) * 0.1); 
                    foreach (var spell in OpponentPokemon.Spells)
                    {
                        var originalDamage = spell.Damage;
                        spell.Damage = (int)(originalDamage * spellBoostFactor);
                        CombatMessages = $"{OpponentPokemon.Name} veut venger ses camarades, il obtient un boost de dommages !";
                        OnPropertyChanged(nameof(CombatMessages));
                    }
                }
                else if (boostType == 1)
                {
                    var hpBoostFactor = 1 + (_random.Next(5, 15) * 0.01);
                    OpponentPokemon.MaxHp = (int)(OpponentPokemon.MaxHp * hpBoostFactor);
                    OpponentPokemon.Hp = OpponentPokemon.MaxHp;

                    CombatMessages = $"{OpponentPokemon.Name} a récupéré un boost de vie !";
                    OnPropertyChanged(nameof(CombatMessages));
                }
            }

            OnPropertyChanged(nameof(OpponentPokemon));
        }

    }
}
