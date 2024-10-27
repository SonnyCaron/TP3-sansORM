using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
namespace TP3_sansORM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            string connectionString = "Server=\"SONNY\\SQLEXPRESS\";Database=TP3;Trusted_Connection=True;TrustServerCertificate=True;";
            using (SqlConnection connexion_BD = new SqlConnection(connectionString))
            {

                connexion_BD.Open();

                SqlCommand command = new SqlCommand("Select * from Personne", connexion_BD);
                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                    ListView.Items.Add(new Personne((int)dataReader["Id"], (string)dataReader["Nom"], (string)dataReader["Prenom"],
                        DateOnly.FromDateTime((DateTime)dataReader["DateNaissance"]), (string)dataReader["Metier"]));
                ListView.SelectedIndex = 0;
                /*
                command = new SqlCommand("INSERT INTO Personne VALUES(@dateNaissance, @Nom, @Prenom)", connexion_BD);

                command.Parameters.Add("@dateNaissance", System.Data.SqlDbType.Date);
                command.Parameters.Add("@Nom", System.Data.SqlDbType.NVarChar);
                command.Parameters.Add("@Prenom", System.Data.SqlDbType.NVarChar);

                command.Parameters["@dateNaissance"].Value = "1988-10-12";
                command.Parameters["@Nom"].Value = "Jackson";
                command.Parameters["@Prenom"].Value = "Michael";


                command.ExecuteNonQuery();


                command.Parameters.Clear();


                command.Dispose();

                command = new SqlCommand("Select * from Personne", connexion_BD);

                SqlDataReader dataReader = command.ExecuteReader();

                string output = "";
                while (dataReader.Read())
                    output += dataReader.GetValue(0).ToString() + '\t' + dataReader.GetValue(1) + '\t' + dataReader.GetValue(2) + '\t' + dataReader.GetValue(3) + '\n';
                */
            }

        }
        private void Supprimer_Click(object sender, RoutedEventArgs e)
        {
            Personne personne = (Personne)ListView.SelectedItem;
            string connectionString = "Server=\"SONNY\\SQLEXPRESS\";Database=TP3;Trusted_Connection=True;TrustServerCertificate=True;";
            using (SqlConnection connexion_BD = new SqlConnection(connectionString))
            {
                connexion_BD.Open();
                SqlCommand command = new SqlCommand("delete from Personne where Id = " + personne.Id, connexion_BD);
                command.ExecuteNonQuery();
                ListView.Items.Remove(personne);
            }
        }

        private void Modifier_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "Server=\"SONNY\\SQLEXPRESS\";Database=TP3;Trusted_Connection=True;TrustServerCertificate=True;";
            using (SqlConnection connexion_BD = new SqlConnection(connectionString))
            {
                connexion_BD.Open();
                int id = ((Personne)ListView.SelectedItem).Id;
                SqlCommand command = new SqlCommand($"update Personne set " +
                    $"Nom = '{BoxNom.Text}',Prenom = '{BoxPrenom.Text}',DateNaissance = '{DtNaissance.Text}',Metier = '{CbMetier.Text}' where Id = {id}", connexion_BD);
                SqlDataReader dataReader = command.ExecuteReader();
                //ListView.Items.Add(new Personne(id, BoxNom.Text, BoxPrenom.Text, DateOnly.Parse(DtNaissance.Text), CbMetier.Text));
                //ListView.SelectedItem = new Personne(id, BoxNom.Text, BoxPrenom.Text, DateOnly.Parse(DtNaissance.Text), CbMetier.Text);
                int index = ListView.SelectedIndex;
                ListView.Items[index] = new Personne(id, BoxNom.Text, BoxPrenom.Text, DateOnly.Parse(DtNaissance.Text), CbMetier.Text);
                ListView.SelectedIndex = index;
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListView.SelectedItem != null)
            {
                Personne personne = (Personne)ListView.SelectedItem;
                BoxNom.Text = personne.Nom;
                BoxPrenom.Text = personne.Prenom;
                DtNaissance.Text = personne.DateNaissance.ToString();
                CbMetier.Text = personne.Metier;
            }
        }

        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "Server=\"SONNY\\SQLEXPRESS\";Database=TP3;Trusted_Connection=True;TrustServerCertificate=True;";
            using (SqlConnection connexion_BD = new SqlConnection(connectionString))
            {
                connexion_BD.Open();
                SqlCommand command = new SqlCommand($"insert into Personne output inserted.Id values (" +
                    $"'{BoxNom.Text}','{BoxPrenom.Text}','{DtNaissance.Text}','{CbMetier.Text}')", connexion_BD);
                SqlDataReader dataReader = command.ExecuteReader();
                dataReader.Read();
                int id = dataReader.GetInt32(0);
                ListView.Items.Add(new Personne(id, BoxNom.Text, BoxPrenom.Text,DateOnly.Parse(DtNaissance.Text),CbMetier.Text));
                ListView.SelectedIndex = ListView.Items.Count -1;
            }
        }
    }
}