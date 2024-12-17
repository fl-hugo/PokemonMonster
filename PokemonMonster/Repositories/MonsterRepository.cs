using Microsoft.Data.SqlClient;
using PokemonMonster.Models;

namespace PokemonMonster.Repositories
{
    public class MonsterRepository : RepositoryBase
    {

        public IEnumerable<MonsterModel> GetAllMonster()
        {
            var monsterList = new List<MonsterModel>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT ID, Name, Health, ImageURL FROM Monster";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        monsterList.Add(new MonsterModel
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

            return monsterList;
        }

        public MonsterModel GetMonsterById(int id)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT ID, Name, Health, ImageURL FROM Monster WHERE ID = @id";
                command.Parameters.AddWithValue("@id", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var monster = new MonsterModel
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            MaxHp = reader.GetInt32(2),
                            Hp = reader.GetInt32(2),
                            ImageUrl = reader.GetString(3)
                        };

                        monster.Spells = GetSpellsByMonsterId(id).ToList();

                        return monster;
                    }
                }
            }

            return null;
        }

        public IEnumerable<SpellModel> GetSpellsByMonsterId(int monsterId)
        {
            var spells = new List<SpellModel>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                    SELECT S.ID, S.Name, S.Damage 
                    FROM Spell S
                    INNER JOIN MonsterSpell MS ON S.ID = MS.SpellID
                    WHERE MS.MonsterID = @MonsterId";

                command.Parameters.AddWithValue("@MonsterId", monsterId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        spells.Add(new SpellModel
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Damage = reader.GetInt32(2)
                        });
                    }
                }
            }

            return spells;
        }
    }
}
