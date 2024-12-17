using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Microsoft.Data.SqlClient;

namespace PokemonMonster.Repositories
{
    public abstract class RepositoryBase
    {
        private string _connectionString;

        public RepositoryBase()
        {
            _connectionString = "Server = localhost\\SQLEXPRESS; Database = ExerciceMonster; Trusted_Connection = True; TrustServerCertificate = True;" ;
            ExecuteSqlAtStartup();

            //---------------------------------------Database connection from Window (failed)-----------------------------------------
            //_connectionString = App.UserConnectionString;
            //if (string.IsNullOrWhiteSpace(_connectionString))
            //{
            //    throw new InvalidOperationException("La chaîne de connexion à la base de données est invalide.");
            //}
        }

        protected SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        private void ExecuteSqlAtStartup()
        {
            string querySQL = @"
                -- Création de la base de données 
                CREATE DATABASE ExerciceMonster; 
                GO 
                -- Utilisation de la base de données 
                USE ExerciceMonster; 
                GO 
                -- Suppression des tables existantes (si elles existent) 
                IF OBJECT_ID('Login', 'U') IS NOT NULL DROP TABLE Login; 
                IF OBJECT_ID('Player', 'U') IS NOT NULL DROP TABLE Player; 
                IF OBJECT_ID('Monster', 'U') IS NOT NULL DROP TABLE Monster; 
                IF OBJECT_ID('Spell', 'U') IS NOT NULL DROP TABLE Spell; 
                IF OBJECT_ID('PlayerMonster', 'U') IS NOT NULL DROP TABLE PlayerMonster; 
                IF OBJECT_ID('MonsterSpell', 'U') IS NOT NULL DROP TABLE MonsterSpell;
                -- Table Login 
                CREATE TABLE Login ( 
                ID INT PRIMARY KEY IDENTITY(1,1), 
                Username NVARCHAR(50) NOT NULL, 
                PasswordHash NVARCHAR(255) NOT NULL 
                ); 
                -- Table Player 
                CREATE TABLE Player ( 
                ID INT PRIMARY KEY IDENTITY(1,1), 
                Name NVARCHAR(50) NOT NULL, 
                LoginID INT, 
                FOREIGN KEY (LoginID) REFERENCES Login(ID) 
                ); 
                -- Table Monster 
                CREATE TABLE Monster ( 
                ID INT PRIMARY KEY IDENTITY(1,1), 
                Name NVARCHAR(50) NOT NULL, 
                Health INT NOT NULL,
                ImageURL VARCHAR(255)
                ); 
                -- Table Spell 
                CREATE TABLE Spell ( 
                ID INT PRIMARY KEY IDENTITY(1,1), 
                Name NVARCHAR(50) NOT NULL, 
                Damage INT NOT NULL,
                Description NVARCHAR(MAX) 
                ); 
                -- Table PlayerMonster (relation Player <-> Monster) 
                CREATE TABLE PlayerMonster ( 
                PlayerID INT NOT NULL, 
                MonsterID INT NOT NULL, 
                PRIMARY KEY (PlayerID, MonsterID), 
                FOREIGN KEY (PlayerID) REFERENCES Player(ID), 
                FOREIGN KEY (MonsterID) REFERENCES Monster(ID) 
                ); 
                -- Table MonsterSpell (relation Monster <-> Spell) 
                CREATE TABLE MonsterSpell ( 
                MonsterID INT NOT NULL, 
                SpellID INT NOT NULL, 
                PRIMARY KEY (MonsterID, SpellID), 
                FOREIGN KEY (MonsterID) REFERENCES Monster(ID), 
                FOREIGN KEY (SpellID) REFERENCES Spell(ID) 
                );
                --Insert to Spell
                INSERT INTO Spell (Name, Damage, Description) VALUES
                ('Lance-Flammes', 90, 'Un jet de flamme est lancé pour brûler l’ennemi.'),
                ('Surf', 90, 'Une énorme vague submerge tout sur son passage.'),
                ('Machouille', 80, 'L’ennemi est mordu avec des crocs sombres.'),
                ('Tonnerre', 90, 'Un puissant éclair frappe l’ennemi.'),
                ('Vibraqua', 60, 'Une onde sonore d’eau frappe l’ennemi.'),
                ('Lame de Roc', 100, 'Des pierres sont projetées sur l’ennemi.'),
                ('Dracochoc', 85, 'Un choc de puissance draconique frappe l’ennemi.'),
                ('Psyko', 90, 'Une puissante onde psychique frappe l’ennemi.'),
                ('Canon Graine', 80, 'Le lanceur tire deux à cinq balles de graines d’affilée sur l’ennemi.'),
                ('Poing-Éclair', 75, 'Un coup de poing électrifié frappe l’ennemi.'),
                ('Blizzard', 110, 'Un vent glacé frappe l’ennemi.'),
                ('Damoclès', 120, 'Une charge puissante qui inflige des dégâts de recul au lanceur.'),
                ('Éboulement', 75, 'De grosses pierres tombent sur l’ennemi.'),
                ('Vent Violent', 110, 'Un vent violent frappe l’ennemi.'),
                ('Feu Follet', 0, 'L’ennemi est maudit par des flammes mystérieuses.'),
                ('Hydrocanon', 110, 'Un puissant jet d’eau frappe l’ennemi.'),
                ('Giga-Sangsue', 75, 'Une attaque qui vole de l’énergie pour soigner le lanceur.'),
                ('Séisme', 100, 'Un tremblement de terre frappe tous les Pokémon autour.'),
                ('Tranche', 70, 'Un coup tranchant porté à l’ennemi. Taux de critique élevé.'),
                ('Ball’Ombre', 80, 'Une boule d’ombre est projetée sur l’ennemi.'),
                ('Mégafouet', 120, 'Un puissant coup de fouet frappe l’ennemi.'),
                ('Coup d’Boule', 70, 'Un coup puissant porté avec la tête.'),
                ('Dracoqueue', 60, 'Un coup de queue frappe l’ennemi et le force à changer.'),
                ('Plaquage', 85, 'Le lanceur charge et écrase l’ennemi.'),
                ('Onde Boréale', 65, 'Un souffle glacé frappe l’ennemi.'),
                ('Laser Glace', 90, 'Un rayon glacé frappe l’ennemi.'),
                ('Déflagration', 110, 'Un torrent de flammes frappe l’ennemi.'),
                ('Queue de Fer', 100, 'Le lanceur fouette l’ennemi avec sa queue.'),
                ('Coup-Croix', 100, 'Deux bras croisés frappent l’ennemi.'),
                ('Sabotage', 65, 'Le lanceur attaque et détruit l’objet tenu par l’ennemi.'),
                ('Tranche-Nuit', 70, 'Un coup d’ombre porté à l’ennemi.'),
                ('Éclair Fou', 90, 'Le lanceur charge l’ennemi avec son corps électrique.'),
                ('Choc Psy', 80, 'Une attaque psychique qui inflige des dégâts physiques.'),
                ('Ultralaser', 150, 'Un laser destructeur frappe l’ennemi.'),
                ('Pied Voltige', 130, 'Un coup de pied sauté frappe l’ennemi.'),
                ('Vent Arrière', 0, 'Le lanceur augmente la Vitesse de l’équipe pendant quatre tours.'),
                ('Pouvoir Lunaire', 95, 'Une force mystérieuse frappe l’ennemi.'),
                ('Coup Bas', 70, 'Une attaque qui touche en priorité si l’ennemi prépare une attaque.'),
                ('Ténèbres', 60, 'Un rayon sinistre frappe l’ennemi.'),
                ('Double Pied', 30, 'Le lanceur donne deux coups de pied successifs à l’ennemi.'),
                ('Crocs Givre', 65, 'Une morsure glaciale frappe l’ennemi.'),
                ('Poing de Feu', 75, 'Un coup de poing embrasé frappe l’ennemi. Peut aussi le brûler.'),
                ('Triplattaque', 80, 'Une attaque qui peut paralyser, brûler ou geler l’ennemi.'),
                ('Force Cachée', 70, 'Une attaque aux effets variables en fonction du terrain.'),
                ('Mur Lumière', 0, 'Un écran lumineux réduit les dégâts spéciaux reçus pendant cinq tours.'),
                ('Poing Ombre', 60, 'Un coup de poing ombragé frappe l’ennemi.'),
                ('Fatal-Foudre', 110, 'Un éclair destructeur frappe l’ennemi.'),
                ('Colère', 120, 'Le lanceur attaque furieusement l’ennemi.');
                --Insert to Monster
                INSERT INTO Monster (Name, Health, ImageURL) VALUES
                ('Bulbasaur', 45, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/001.png'),
                ('Ivysaur', 60, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/002.png'),
                ('Venusaur', 80, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/003.png'),
                ('Charmander', 39, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/004.png'),
                ('Charmeleon', 58, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/005.png'),
                ('Charizard', 78, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/006.png'),
                ('Squirtle', 44, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/007.png'),
                ('Wartortle', 59, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/008.png'),
                ('Blastoise', 79, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/009.png'),
                ('Caterpie', 45, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/010.png'),
                ('Metapod', 50, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/011.png'),
                ('Butterfree', 60, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/012.png'),
                ('Weedle', 40, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/013.png'),
                ('Kakuna', 45, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/014.png'),
                ('Beedrill', 65, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/015.png'),
                ('Pidgey', 40, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/016.png'),
                ('Pidgeotto', 63, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/017.png'),
                ('Pidgeot', 83, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/018.png'),
                ('Rattata', 30, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/019.png'),
                ('Raticate', 55, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/020.png'),
                ('Spearow', 40, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/021.png'),
                ('Fearow', 65, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/022.png'),
                ('Ekans', 35, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/023.png'),
                ('Arbok', 60, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/024.png'),
                ('Pikachu', 35, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/025.png'),
                ('Raichu', 60, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/026.png'),
                ('Sandshrew', 50, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/027.png'),
                ('Sandslash', 75, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/028.png'),
                ('Nidoran♀', 55, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/029.png'),
                ('Nidorina', 70, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/030.png'),
                ('Nidoqueen', 90, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/031.png'),
                ('Nidoran♂', 46, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/032.png'),
                ('Nidorino', 61, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/033.png'),
                ('Nidoking', 81, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/034.png'),
                ('Clefairy', 70, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/035.png'),
                ('Clefable', 95, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/036.png'),
                ('Vulpix', 38, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/037.png'),
                ('Ninetales', 73, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/038.png'),
                ('Jigglypuff', 115, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/039.png'),
                ('Wigglytuff', 140, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/040.png'),
                ('Zubat', 40, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/041.png'),
                ('Golbat', 75, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/042.png'),
                ('Oddish', 45, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/043.png'),
                ('Gloom', 60, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/044.png'),
                ('Vileplume', 75, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/045.png'),
                ('Paras', 35, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/046.png'),
                ('Parasect', 60, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/047.png'),
                ('Venonat', 60, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/048.png'),
                ('Venomoth', 70, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/049.png'),
                ('Diglett', 10, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/050.png'),
                ('Dugtrio', 35, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/051.png'),
                ('Meowth', 40, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/052.png'),
                ('Persian', 65, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/053.png'),
                ('Psyduck', 50, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/054.png'),
                ('Golduck', 80, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/055.png'),
                ('Mankey', 40, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/056.png'),
                ('Primeape', 65, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/057.png'),
                ('Growlithe', 55, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/058.png'),
                ('Arcanine', 90, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/059.png'),
                ('Poliwag', 40, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/060.png'),
                ('Poliwhirl', 65, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/061.png'),
                ('Poliwrath', 90, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/062.png'),
                ('Abra', 25, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/063.png'),
                ('Kadabra', 40, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/064.png'),
                ('Alakazam', 55, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/065.png'),
                ('Machop', 70, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/066.png'),
                ('Machoke', 80, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/067.png'),
                ('Machamp', 90, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/068.png'),
                ('Bellsprout', 50, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/069.png'),
                ('Weepinbell', 65, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/070.png'),
                ('Victreebel', 80, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/071.png'),
                ('Tentacool', 40, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/072.png'),
                ('Tentacruel', 80, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/073.png'),
                ('Geodude', 40, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/074.png'),
                ('Graveler', 55, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/075.png'),
                ('Golem', 80, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/076.png'),
                ('Ponyta', 50, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/077.png'),
                ('Rapidash', 65, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/078.png'),
                ('Slowpoke', 90, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/079.png'),
                ('Slowbro', 95, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/080.png'),
                ('Magnemite', 25, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/081.png'),
                ('Magneton', 50, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/082.png'),
                ('Farfetch’d', 52, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/083.png'),
                ('Doduo', 35, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/084.png'),
                ('Dodrio', 60, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/085.png'),
                ('Seel', 65, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/086.png'),
                ('Dewgong', 90, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/087.png'),
                ('Grimer', 80, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/088.png'),
                ('Muk', 105, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/089.png'),
                ('Shellder', 30, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/090.png'),
                ('Cloyster', 50, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/091.png'),
                ('Gastly', 30, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/092.png'),
                ('Haunter', 45, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/093.png'),
                ('Gengar', 60, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/094.png'),
                ('Onix', 35, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/095.png'),
                ('Drowzee', 60, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/096.png'),
                ('Hypno', 85, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/097.png'),
                ('Krabby', 30, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/098.png'),
                ('Kingler', 55, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/099.png'),
                ('Voltorb', 40, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/100.png'),
                ('Electrode', 60, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/101.png'),
                ('Exeggcute', 60, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/102.png'),
                ('Exeggutor', 95, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/103.png'),
                ('Cubone', 50, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/104.png'),
                ('Marowak', 60, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/105.png'),
                ('Hitmonlee', 50, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/106.png'),
                ('Hitmonchan', 50, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/107.png'),
                ('Lickitung', 90, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/108.png'),
                ('Koffing', 40, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/109.png'),
                ('Weezing', 65, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/110.png'),
                ('Rhyhorn', 80, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/111.png'),
                ('Rhydon', 105, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/112.png'),
                ('Chansey', 250, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/113.png'),
                ('Tangela', 65, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/114.png'),
                ('Kangaskhan', 105, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/115.png'),
                ('Horsea', 30, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/116.png'),
                ('Seadra', 55, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/117.png'),
                ('Goldeen', 45, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/118.png'),
                ('Seaking', 80, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/119.png'),
                ('Staryu', 30, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/120.png'),
                ('Starmie', 60, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/121.png'),
                ('Mr. Mime', 40, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/122.png'),
                ('Scyther', 70, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/123.png'),
                ('Jynx', 65, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/124.png'),
                ('Electabuzz', 65, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/125.png'),
                ('Magmar', 65, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/126.png'),
                ('Pinsir', 65, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/127.png'),
                ('Tauros', 75, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/128.png'),
                ('Magikarp', 20, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/129.png'),
                ('Gyarados', 95, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/130.png'),
                ('Lapras', 130, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/131.png'),
                ('Ditto', 48, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/132.png'),
                ('Eevee', 55, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/133.png'),
                ('Vaporeon', 130, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/134.png'),
                ('Jolteon', 65, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/135.png'),
                ('Flareon', 65, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/136.png'),
                ('Porygon', 65, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/137.png'),
                ('Omanyte', 35, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/138.png'),
                ('Omastar', 70, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/139.png'),
                ('Kabuto', 30, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/140.png'),
                ('Kabutops', 60, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/141.png'),
                ('Aerodactyl', 80, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/142.png'),
                ('Snorlax', 160, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/143.png'),
                ('Articuno', 90, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/144.png'),
                ('Zapdos', 90, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/145.png'),
                ('Moltres', 90, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/146.png'),
                ('Dratini', 41, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/147.png'),
                ('Dragonair', 61, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/148.png'),
                ('Dragonite', 91, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/149.png'),
                ('Mewtwo', 106, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/150.png'),
                ('Mew', 100, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/151.png');
                --Insert to MonsterSpell
                WITH RandomSpells AS (
                    SELECT
                        m.ID AS MonsterID,
                        s.ID AS SpellID,
                        ROW_NUMBER() OVER (PARTITION BY m.ID ORDER BY NEWID()) AS RowNum
                    FROM Monster m
                    CROSS JOIN Spell s
                )
                INSERT INTO MonsterSpell (MonsterID, SpellID)
                SELECT
                    MonsterID,
                    SpellID
                FROM RandomSpells
                WHERE RowNum <= 4;
            ";

            using (SqlConnection connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(querySQL, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de l'exécution du SQL : " + ex.Message);
                }
            }
        }
    }
}
