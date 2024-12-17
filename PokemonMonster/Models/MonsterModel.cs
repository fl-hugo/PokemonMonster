using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonMonster.Models
{
    public class MonsterModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MaxHp { get; set; }
        public int Hp { get; set; }
        public string ImageUrl { get; set; }
        public List<SpellModel> Spells { get; set; } = new List<SpellModel>();
    }
}
