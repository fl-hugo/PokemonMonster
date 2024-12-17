namespace PokemonMonster
{
    public class ConnectionManager
    {
        private static ConnectionManager _instance ;
        public static ConnectionManager Instance => _instance ??= new ConnectionManager();

        public string ConnectionString { get; private set; }

        private ConnectionManager() { }

        public void SetConnectionString(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}
