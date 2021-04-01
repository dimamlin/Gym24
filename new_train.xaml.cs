using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для new_train.xaml
    /// </summary>
    public partial class new_train : Window
    {
        int id_of_user;
        string connectionString = @"server=DIMAMLIN-PC\QQQQ;database=kp;Integrated Security=true;";
        public new_train()
        {
            

            try
            {
                DataTable dt_user1 = Select("SELECT id FROM login_data where isenabled = '+'");
                for (int i = 0; i < dt_user1.Rows.Count; i++)
                {
                    id_of_user = Convert.ToInt32(dt_user1.Rows[i][0].ToString());
                }


            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }


            InitializeComponent();
            try
            {
                string connectionString = @"server=DIMAMLIN-PC\QQQQ;database=kp;Integrated Security=true;";
                string sqlExpression1 = $"SELECT train_name FROM trains;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression1, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        train_list.Items.Add(reader[0].ToString());
                    }
                    reader.Close();
                }

                int[] trains_user = new int[100]; // добавление в лист вью раннее выбранных тренировок пользователя
                int i = 0;
                string sqlExpression2 = $"SELECT train_id FROM user_train WHERE user_id='{id_of_user}' ;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression2, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        trains_user[i] = Convert.ToInt32(reader[0]);
                        i++;
                    }
                    reader.Close();
                }
                while (i >= 0)
                {
                    string sqlExpression3 = $"SELECT train_name FROM trains WHERE id = '{trains_user[i]}';";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(sqlExpression3, connection);
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            your_trains.Items.Add(reader[0].ToString());
                        }
                        reader.Close();
                    }
                    i--;
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        public DataTable Select(string selectSQL) // функция подключения к базе данных и обработка запросов
        {
            DataTable dataTable = new DataTable("dataBase");                // создаём таблицу в приложении
                                                                            // подключаемся к базе данных
            SqlConnection sqlConnection = new SqlConnection(@"server=DIMAMLIN-PC\QQQQ;database=kp;Integrated Security=true;");
            sqlConnection.Open();                                           // открываем базу данных
            SqlCommand sqlCommand = sqlConnection.CreateCommand();          // создаём команду
            sqlCommand.CommandText = selectSQL;                             // присваиваем команде текст
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand); // создаём обработчик
            sqlDataAdapter.Fill(dataTable);                                 // возращаем таблицу с результатом
            return dataTable;
        }
       

        private void Button_Click(object sender, RoutedEventArgs e) // add training
        {
            try
            {
                string connectionString = @"server=DIMAMLIN-PC\QQQQ;database=kp;Integrated Security=true;";
                bool correct = true;

                if (Convert.ToInt32(train_list.SelectedIndex.ToString()) >= 0)
                {
                    int choose_train_id;
                    string choose = train_list.SelectedItem.ToString();
                    string sqlExpression = $"SELECT id FROM trains WHERE train_name LIKE '{choose}%'";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(sqlExpression, connection);
                        SqlDataReader reader = command.ExecuteReader();
                        reader.Read();
                        choose_train_id = Convert.ToInt32(reader[0]);
                        reader.Close();
                        connection.Close();

                        int[] id_user_train = new int[1000];
                        int j = 0;
                        string sqlExpression1 = $"SELECT user_id FROM user_train WHERE train_id = '{choose_train_id}'";
                        connection.Open();
                        SqlCommand command1 = new SqlCommand(sqlExpression1, connection);
                        SqlDataReader reader1 = command1.ExecuteReader();
                        while (reader1.Read())
                        {
                            id_user_train[j] = Convert.ToInt32(reader1[0]);
                            j++;
                        }
                        reader1.Close();
                        connection.Close();
                        while(j >= 0)
                        {
                            if(id_user_train[j] == id_of_user)
                            {
                                correct = false;
                            }
                            j--;
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Choose the training!");
                }

                if (Convert.ToInt32(train_list.SelectedIndex.ToString()) >= 0)
                {
                    if (correct)
                    {
                        string sqlExpression1 = $"INSERT INTO user_train (user_id, train_id) VALUES ('{id_of_user}', '{Convert.ToInt32(train_list.SelectedIndex.ToString()) + 1}')";
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            SqlCommand command = new SqlCommand(sqlExpression1, connection);
                            int number = command.ExecuteNonQuery();
                            Console.WriteLine("Добавлено объектов: {0}", number);
                            connection.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("You already have this training!");
                    }
                }
                else
                {
                    MessageBox.Show("Choose the training!");
                }

                your_trains.Items.Clear();


                int[] trains_user = new int[100];
                int i = 0;
                string sqlExpression2 = $"SELECT train_id FROM user_train WHERE user_id='{id_of_user}' ;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression2, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        trains_user[i] = Convert.ToInt32(reader[0]);
                        i++;
                    }
                    reader.Close();
                }
                while (i >= 0)
                {
                    string sqlExpression3 = $"SELECT train_name FROM trains WHERE id = '{trains_user[i]}';";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(sqlExpression3, connection);
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            your_trains.Items.Add( reader[0].ToString());
                        }
                        reader.Close();
                    }
                    i--;
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void your_trains_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            UserApp app = new UserApp();
            app.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Message_window mes = new Message_window();
            mes.Show();
        }

        private void delete_training_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                if (Convert.ToInt32(your_trains.SelectedIndex.ToString()) >= 0)
                {
                    int choose_train_id;
                    string choose = your_trains.SelectedItem.ToString();
                    string sqlExpression = $"SELECT id FROM trains WHERE train_name LIKE '{choose}%'";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(sqlExpression, connection);
                        SqlDataReader reader = command.ExecuteReader();                     
                        reader.Read();
                        choose_train_id = Convert.ToInt32(reader[0]);
                        reader.Close();
                        connection.Close();

                        string sqlExpression1 = $"DELETE FROM user_train WHERE train_id = '{choose_train_id}' and user_id = '{id_of_user}' ";
                        connection.Open();
                        SqlCommand command1 = new SqlCommand(sqlExpression1, connection);
                        int number = command1.ExecuteNonQuery();
                        Console.WriteLine("Добавлено объектов: {0}", number);
                        connection.Close();
                    }

                }
                else
                {
                    MessageBox.Show("Choose the training!");
                }

                your_trains.Items.Clear();
                int[] trains_user = new int[100]; // добавление в лист вью раннее выбранных тренировок пользователя
                int i = 0;
                string sqlExpression2 = $"SELECT train_id FROM user_train WHERE user_id='{id_of_user}' ;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression2, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        trains_user[i] = Convert.ToInt32(reader[0]);
                        i++;
                    }
                    reader.Close();
                }
                while (i >= 0)
                {
                    string sqlExpression3 = $"SELECT train_name FROM trains WHERE id = '{trains_user[i]}';";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(sqlExpression3, connection);
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            your_trains.Items.Add(reader[0].ToString());
                        }
                        reader.Close();
                    }
                    i--;
                }
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void search_training_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] trains= new string[100];
                int j = 0;
                int count = 0;
                string sqlExpression2 = $"SELECT train_name FROM trains ;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression2, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        trains[j] = reader[0].ToString();
                        j++;
                        count++;
                    }
                    reader.Close();
                }
                

                train_list.Items.Clear();

                int counter = 0;
                for (j = 0; j < count; j++)
                {
                    Regex regex = new Regex($@"{search_training_field.Text}(\w*)", RegexOptions.IgnoreCase);
                    MatchCollection matches = regex.Matches(trains[j]);
                    if (matches.Count > 0)
                    {
                        train_list.Items.Add(trains[j]);
                        counter++;
                    }
                }
                if (counter == 0)
                    your_trains.Items.Add("No results");
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }
}
