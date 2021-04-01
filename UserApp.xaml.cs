using System;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net;

namespace kp
{
    public partial class UserApp : Window
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
                string sqlExpression3 = $"SELECT train_id FROM user_trainings WHERE user_id = '{id_of_user}';";
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
                string sqlExpression1 = $"SELECT trainer_id FROM user_trainings WHERE user_id = '{id_of_user}';";
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
                    string sqlExpression2 = $"SELECT train_name FROM trains WHERE id = '{id_train[j]}';";
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
                    string sqlExpression4 = $"SELECT trainer FROM trainers WHERE id = '{id_trainer[j]}';";
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
                    string sqlExpression5 = $"SELECT date FROM user_trainings WHERE user_id = '{id_of_user}' and train_id = '{id_train[j]}' and trainer_id = '{id_trainer[j]}';";
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
                    string sqlExpression6 = $"SELECT time FROM user_trainings WHERE user_id = '{id_of_user}' and train_id = '{id_train[j]}' and trainer_id = '{id_trainer[j]}';";
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
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        public UserApp()
        {
            

            try
            {

                InitializeComponent();

                string connectionString = @"server=DIMAMLIN-PC\QQQQ;database=kp;Integrated Security=true;";


                string connectionString1 = @"server=DIMAMLIN-PC\QQQQ;database=kp;Integrated Security=true;"; // имя  и фамилия пользователя 
                string sqlExpression = $"SELECT id FROM login_data where isenabled = '+'";   
                using (SqlConnection connection = new SqlConnection(connectionString1))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    id_of_user = Convert.ToInt32(reader[0]);
                    reader.Close();
                    connection.Close();                    
                }

                string name_user;

                string sqlExpression1 = $"SELECT surname FROM user_data where id = '{id_of_user}'";
                using (SqlConnection connection = new SqlConnection(connectionString1))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression1, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    name_user = reader[0].ToString();
                    name_user =name_user.Trim();
                    reader.Close();
                    connection.Close();
                }

                string sqlExpression2 = $"SELECT name FROM user_data where id = '{id_of_user}'";
                using (SqlConnection connection = new SqlConnection(connectionString1))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression2, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    name_user += " " + reader[0].ToString();
                    name_user = name_user.Trim();
                    reader.Close();
                    connection.Close();
                }

                name_user_block.Text = name_user;

                string imagePath;
                string sqlExpression10 = $"SELECT photo FROM user_data where id = '{id_of_user}'";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression10, connection);
                    SqlDataReader reader10 = command.ExecuteReader();
                    reader10.Read();
                    imagePath = reader10[0].ToString().Trim();
                    reader10.Close();
                    connection.Close();
                }
                imageBrush.ImageSource = new BitmapImage(new Uri(imagePath, UriKind.Relative));

                LoadUsers();

                
                
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
        
       

        
        private void exit_Click_1(object sender, RoutedEventArgs e)
        {
            string connectionString = @"server=DIMAMLIN-PC\QQQQ;database=kp;Integrated Security=true;";
            string sqlExpression1 = $"UPDATE login_data SET isenabled='-'";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression1, connection);
                int number = command.ExecuteNonQuery();
                Console.WriteLine("Добавлено объектов: {0}", number);
                connection.Close();
            }
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
        }

        private void add_train_Click(object sender, RoutedEventArgs e)
        {
            new_train New_train = new new_train();
            this.Close();
            New_train.Show();
        }

        private void Add_training_Click(object sender, RoutedEventArgs e)
        {
            new_training win = new new_training();
            this.Close();
            win.Show();
        }

        private void edit_profile_Click(object sender, RoutedEventArgs e)
        {
            edit_profile win = new edit_profile();
            this.Close();
            win.Show();
        }

        private void Count_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool correct = true;
                bool micro_correct = true;

                Regex regex = new Regex(@"\d{3}");
                Match match = regex.Match(height_field.Text);
                height_correct.Content = "Enter correct height";
                if (!match.Success)
                {
                    correct = false;
                    micro_correct = false;
                }
                while (match.Success)
                {
                    match = match.NextMatch();
                    height_correct.Content = "";
                }

                if (height_field.Text != "" && micro_correct && height_field.Text.Length == 3)
                {
                    if (Convert.ToInt32(height_field.Text) > 140 && Convert.ToInt32(height_field.Text) < 230)
                    {
                        height_correct.Content = "";
                    }
                    else
                    {
                        height_correct.Content = "Enter correct height";
                        correct = false;
                    }
                }
                else
                {
                    height_correct.Content = "Enter correct height";
                    correct = false;
                }

                micro_correct = true;
                Regex regex1 = new Regex(@"\d{2}");
                Match match1 = regex1.Match(weight_field.Text);
                weight_correct.Content = "Enter correct weight";
                if (!match1.Success)
                {
                    correct = false;
                    micro_correct = false;
                }
                while (match1.Success)
                {
                    match1 = match1.NextMatch();
                    weight_correct.Content = "";
                }

                if (weight_field.Text != "" && micro_correct && weight_field.Text.Length == 2)
                {
                    if (Convert.ToInt32(weight_field.Text) > 40 && Convert.ToInt32(weight_field.Text) < 250)
                    {
                        weight_correct.Content = "";
                    }
                    else
                    {
                        weight_correct.Content = "Enter correct weight";
                        correct = false;
                    }
                }
                else
                {
                    weight_correct.Content = "Enter correct weight";
                    correct = false;
                }

                micro_correct = true;
                Regex regex2 = new Regex(@"\d{2}");
                Match match2 = regex2.Match(age_field.Text);
                age_correct.Content = "Enter correct age";
                if (!match2.Success)
                {
                    correct = false;
                    micro_correct = false;
                }
                while (match2.Success)
                {
                    match2 = match2.NextMatch();
                    age_correct.Content = "";
                }

                if (age_field.Text != "" && micro_correct && age_field.Text.Length == 2)
                {
                    if (Convert.ToInt32(age_field.Text) > 16 && Convert.ToInt32(age_field.Text) < 100)
                    {
                        age_correct.Content = "";
                    }
                    else
                    {
                        age_correct.Content = "Enter correct age";
                        correct = false;
                    }
                }
                else
                {
                    age_correct.Content = "Enter correct age";
                    correct = false;
                }

                if(sex_field.Text == "")
                {
                    sex_correct.Content = "Choose sex";
                    correct = false;
                }
                else
                    sex_correct.Content = "";

                if (correct)
                {
                    if (sex_field.Text == "Male")
                        calories_field.Text = (10 * Convert.ToInt32(weight_field.Text) + 6.25 * Convert.ToInt32(height_field.Text) + 5 * Convert.ToInt32(age_field.Text) + 5).ToString();
                    else
                        calories_field.Text = (10 * Convert.ToInt32(weight_field.Text) + 6.25 * Convert.ToInt32(height_field.Text) + 5 * Convert.ToInt32(age_field.Text) - 161).ToString();
                }
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void Refresh_button_Click(object sender, RoutedEventArgs e)
        {
            height_field.Text = "";
            weight_field.Text = "";
            age_field.Text = "";
            sex_field.Text = "";
            height_correct.Content = "";
            weight_correct.Content = "";
            age_correct.Content = "";
            sex_correct.Content = "";
            calories_field.Text = "";

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.gym24.by/");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.gym24.by/stoimost/");
        }
    }
}
