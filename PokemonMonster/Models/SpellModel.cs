using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonMonster.Models
{
    public class SpellModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Damage { get; set; }
        public string Description { get; set; }
        public List<MonsterModel> Monsters { get; set; } = new List<MonsterModel>();
    }
}
