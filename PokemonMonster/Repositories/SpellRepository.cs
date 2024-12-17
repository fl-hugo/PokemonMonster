using Microsoft.Data.SqlClient;
using PokemonMonster.Models;

namespace PokemonMonster.Repositories
{
    public class SpellRepository : RepositoryBase
    {

        public IEnumerable<SpellModel> GetAllSpell()
        {
            var monsterList = new List<SpellModel>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT ID, Name, Damage, Description FROM Spell";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        monsterList.Add(new SpellModel
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Damage = reader.GetInt32(2),
                            Description = reader.GetString(3)
                        });
                    }
                }
            }

            return monsterList;
        }

        public SpellModel GetSpellById(int id)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT ID, Name, Damage, Description FROM Spell WHERE ID = @id";
                command.Parameters.AddWithValue("@id", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var spell = new SpellModel
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Damage = reader.GetInt32(2),
                            Description = reader.GetString(3)
                        };

                        spell.Monsters = GetMonstersBySpellId(id).ToList();

                        return spell;
                    }
                }
            }
            return null;
        }

        public IEnumerable<MonsterModel> GetMonstersBySpellId(int spellId)
        {
            var monsters = new List<MonsterModel>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                    SELECT M.ID, M.Name, M.Health, M.ImageURL 
                    FROM Monster M
                    INNER JOIN MonsterSpell MS ON M.ID = MS.MonsterID
                    WHERE MS.SpellID = @SpellId";

                command.Parameters.AddWithValue("@SpellId", spellId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        monsters.Add(new MonsterModel
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            MaxHp = reader.GetInt32(2),
                            Hp = reader.GetInt32(2),
                            ImageUrl = reader.GetString(3)
                        });
                    }
                }
            }
            return monsters;
        }
    }
}
