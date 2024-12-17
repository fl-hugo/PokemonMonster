using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Data.SqlClient;

namespace PokemonMonster.Views
{
    public partial class DatabaseConnectionView : Window
    {
        public string ConnectionString { get; private set; }

        public DatabaseConnectionView()
        {
            InitializeComponent();
        }

        private void ValidateConnectionString(object sender, RoutedEventArgs e)
        {
            ConnectionString = ConnectionStringTextBox.Text;

            if (string.IsNullOrWhiteSpace(ConnectionString))
            {
                MessageBox.Show("Le lien de connexion ne peut pas être vide.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    connection.Close();
                }

                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Connexion échouée : {ex.Message}", "Erreur de connexion", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
