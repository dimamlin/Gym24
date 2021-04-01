using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace kp
{
    /// <summary>
    /// Логика взаимодействия для Administrator.xaml
    /// </summary>
    public partial class Administrator : Window
    {
        int id_of_user;
        string connectionString = @"server=DIMAMLIN-PC\QQQQ;database=kp;Integrated Security=true;";
        public class user
        {
            public string training { get; set; }
            public string trainer { get; set; }
            public string date { get; set; }
            public string time { get; set; }


        }

        void LoadUsers()
        {
            try
            {

                int i = 0;

                int[] id_train = new int[30];
                string sqlExpression3 = $"SELECT train_id FROM user_trainings;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression3, connection);
                    SqlDataReader reader1 = command.ExecuteReader();
                    while (reader1.Read())
                    {
                        id_train[i] = Convert.ToInt32(reader1[0]);
                        i++;
                    }
                    reader1.Close();
                    connection.Close();
                }
                i = 0;
                int[] id_trainer = new int[30];
                string sqlExpression1 = $"SELECT trainer_id FROM user_trainings";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression1, connection);
                    SqlDataReader reader1 = command.ExecuteReader();
                    while (reader1.Read())
                    {
                        id_trainer[i] = Convert.ToInt32(reader1[0]);
                        i++;
                    }
                    reader1.Close();
                    connection.Close();
                }

                for (int j = 0; j < i; j++)
                {
                    user dataUser = new user();
                    string sqlExpression2 = $"SELECT train_name FROM trains where id = {j};";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(sqlExpression2, connection);
                        SqlDataReader reader1 = command.ExecuteReader();
                        while (reader1.Read())
                        {
                            dataUser.training = reader1[0].ToString();
                        }
                        reader1.Close();
                        connection.Close();
                    }
                    string sqlExpression4 = $"SELECT trainer FROM trainers where id = {j};";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(sqlExpression4, connection);
                        SqlDataReader reader1 = command.ExecuteReader();
                        while (reader1.Read())
                        {
                            dataUser.trainer = reader1[0].ToString();
                        }
                        reader1.Close();
                        connection.Close();
                    }
                    string sqlExpression5 = $"SELECT date FROM user_trainings where id = {j};";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(sqlExpression5, connection);
                        SqlDataReader reader1 = command.ExecuteReader();
                        while (reader1.Read())
                        {
                            dataUser.date = reader1[0].ToString();
                        }
                        reader1.Close();
                        connection.Close();
                    }
                    string sqlExpression6 = $"SELECT time FROM user_trainings where id = {j};";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(sqlExpression6, connection);
                        SqlDataReader reader1 = command.ExecuteReader();
                        while (reader1.Read())
                        {
                            dataUser.time = reader1[0].ToString();
                        }
                        reader1.Close();
                        connection.Close();
                    }
                    trainings_list.Items.Add(dataUser);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        public Administrator()
        {
            InitializeComponent();

            LoadUsers();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string connectionString = @"server=DIMAMLIN-PC\QQQQ;database=kp;Integrated Security=true;";
                string sqlExpression1 = $"INSERT INTO trainers (trainer) VALUES ('{trainer_name.Text}')";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression1, connection);
                    int number = command.ExecuteNonQuery();
                    Console.WriteLine("Добавлено объектов: {0}", number);
                    connection.Close();
                }
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win = new MainWindow();
            this.Close();
            win.Show();
        }
    }
}
