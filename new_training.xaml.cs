using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
    /// Логика взаимодействия для new_training.xaml
    /// </summary>
    public partial class new_training : Window
    {
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

        int id_of_user;
        string connectionString = @"server=DIMAMLIN-PC\QQQQ;database=kp;Integrated Security=true;";
        public new_training()
        {
            InitializeComponent();

            try
            {
               

                DataTable dt_user1 = Select("SELECT id FROM login_data where isenabled = '+'");
                for (int i = 0; i < dt_user1.Rows.Count; i++)
                {
                    id_of_user = Convert.ToInt32(dt_user1.Rows[i][0].ToString());
                }

                int[] trains_user = new int[100]; // добавление в лист вью раннее выбранных тренировок пользователя
                int j = 0;
                string sqlExpression2 = $"SELECT train_id FROM user_train WHERE user_id='{id_of_user}' ;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression2, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        trains_user[j] = Convert.ToInt32(reader[0]);
                        j++;
                    }
                    reader.Close();
                }
                while (j >= 0)
                {
                    string sqlExpression3 = $"SELECT train_name FROM trains WHERE id = '{trains_user[j]}';";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(sqlExpression3, connection);
                        SqlDataReader reader1 = command.ExecuteReader();
                        while (reader1.Read())
                        {
                            your_trains.Items.Add(reader1[0].ToString());
                        }
                        reader1.Close();
                    }
                    j--;
                }

                string sqlExpression4 = $"SELECT trainer FROM trainers;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression4, connection);
                    SqlDataReader reader4 = command.ExecuteReader();
                    while (reader4.Read())
                    {
                        trainers.Items.Add(reader4[0].ToString());
                    }
                    reader4.Close();
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

        private void exit_bitton_Click(object sender, RoutedEventArgs e)
        {
            UserApp win = new UserApp();
            this.Close();
            win.Show();
        }

        private void add_training_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool correct = true;
                bool micro_correct = true;

                Regex regex = new Regex(@"\d{2}");
                Match match = regex.Match(day_field.Text);
                day_correct.Content = "Enter correct day";
                if (!match.Success)
                {
                    correct = false;
                    micro_correct = false;
                }
                while (match.Success)
                {
                    match = match.NextMatch();
                    day_correct.Content = "";
                }

                if (day_field.Text != "" && micro_correct && day_field.Text.Length == 2)
                {
                    if (Convert.ToInt32(day_field.Text) > 0 && Convert.ToInt32(day_field.Text) < 32)
                    {
                        day_correct.Content = "";
                    }
                    else
                    {
                        day_correct.Content = "Enter correct day";
                        correct = false;
                    }
                }
                else
                {
                    day_correct.Content = "Enter correct day";
                    correct = false;
                }


                if (month_field.Text == "")
                {
                    month_correct.Content = "Choose correct month";
                    correct = false;
                }
                else
                    month_correct.Content = "";

                micro_correct = true;
                Regex regex1 = new Regex(@"\d{2}");
                Match match1 = regex1.Match(hours_field.Text);
                time_correct.Content = "Enter correct time";
                if (!match1.Success)
                {
                    correct = false;
                    micro_correct = false;
                }
                while (match1.Success)
                {
                    match1 = match1.NextMatch();
                    time_correct.Content = "";
                }

                if (hours_field.Text != "" && micro_correct && hours_field.Text.Length == 2)
                {
                    if (Convert.ToInt32(hours_field.Text) >= 0 && Convert.ToInt32(hours_field.Text) < 24)
                    {
                        time_correct.Content = "";
                    }
                    else
                    {
                        time_correct.Content = "Enter correct time";
                        correct = false;
                    }
                }
                else
                {
                    time_correct.Content = "Enter correct time";
                    correct = false;
                    micro_correct = false;
                }

                
                Regex regex2 = new Regex(@"\d{2}");
                Match match2 = regex2.Match(minutes_field.Text);
                time_correct.Content = "Enter correct time";
                if (!match2.Success)
                {
                    correct = false;
                    micro_correct = false;
                }
                while (match2.Success)
                {
                    match2 = match2.NextMatch();
                    time_correct.Content = "";
                }

                if (minutes_field.Text != "" && micro_correct && minutes_field.Text.Length == 2)
                {
                    if (Convert.ToInt32(minutes_field.Text) >= 0 && Convert.ToInt32(minutes_field.Text) < 60)
                    {
                        time_correct.Content = "";
                    }
                    else
                    {
                        time_correct.Content = "Enter correct time";
                        correct = false;
                    }
                }
                else
                {
                    time_correct.Content = "Enter correct time";
                    correct = false;
                }

                if(Convert.ToInt32(your_trains.SelectedIndex.ToString()) == -1)
                {
                    correct = false;
                    correct_plan.Content = "Choose one plan";
                }
                else
                    correct_plan.Content = "";

                if (Convert.ToInt32(trainers.SelectedIndex.ToString()) == -1 && is_trainer.IsChecked == false)
                {
                    correct = false;
                    trainer_correct.Content = "Choose one trainer or check \"without trainer\"";
                }
                else
                    trainer_correct.Content = "";

                if (correct)
                {
                        int choose_train_id;
                        int choose_trainer_id;
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
                        }

                        choose = trainers.SelectedItem.ToString();
                        string sqlExpression1 = $"SELECT id FROM trainers WHERE trainer LIKE '{choose}%'";
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            SqlCommand command = new SqlCommand(sqlExpression1, connection);
                            SqlDataReader reader1 = command.ExecuteReader();
                            reader1.Read();
                            choose_trainer_id = Convert.ToInt32(reader1[0]);
                            reader1.Close();
                            connection.Close();
                        }

                        if (is_trainer.IsChecked == true)
                            choose_trainer_id = 0;

                        string date = $"{day_field.Text} of {month_field.Text}";

                        string time = hours_field.Text + ":" + minutes_field.Text;

                        string sqlExpression3 = $"INSERT INTO user_trainings (user_id, train_id, trainer_id, date, time) VALUES ('{id_of_user}', '{choose_train_id}', '{choose_trainer_id}', '{date}', '{time}' ); ";
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            SqlCommand command = new SqlCommand(sqlExpression3, connection);
                            int number = command.ExecuteNonQuery();
                            Console.WriteLine("Добавлено объектов: {0}", number);
                            connection.Close();
                        }

                    string email = "";
                    string sqlExpression6 = $"SELECT email FROM user_data WHERE id = '{id_of_user}';";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {

                        connection.Open();
                        SqlCommand command = new SqlCommand(sqlExpression6, connection);
                        SqlDataReader reader1 = command.ExecuteReader();
                        while (reader1.Read())
                        {
                            email = reader1[0].ToString();
                            email = email.Trim();
                        }
                        reader1.Close();
                        connection.Close();
                    }

                    // отправитель - устанавливаем адрес и отображаемое в письме имя
                    MailAddress from = new MailAddress("gym24.kp@gmail.com", "Gym24");
                    // кому отправляем
                    MailAddress to = new MailAddress($"{email}");
                    // создаем объект сообщения
                    MailMessage m = new MailMessage(from, to);
                    // тема письма
                    m.Subject = "Training is near!";
                    // текст письма
                    m.Body = "<h2>Do not forget about your training!!!</h2>";
                    // письмо представляет код html
                    m.IsBodyHtml = true;
                    // адрес smtp-сервера и порт, с которого будем отправлять письмо
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                    // логин и пароль
                    smtp.Credentials = new NetworkCredential("gym24.kp@gmail.com", "31012001mss");
                    smtp.EnableSsl = true;
                    smtp.Send(m);
                    Console.Read();

                    UserApp win = new UserApp();
                        this.Close();
                        win.Show();
                }
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void search_training_button_Click(object sender, RoutedEventArgs e) // поиск занятия
        {
            try
            {
                string[] trains = new string[100];
                int[] trains_user = new int[100];
                int j = 0;
                string sqlExpression2 = $"SELECT train_id FROM user_train WHERE user_id='{id_of_user}' ;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression2, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        trains_user[j] = Convert.ToInt32(reader[0]);
                        j++;
                    }
                    reader.Close();
                }
                int i = 0;
                int count = 0;
                while (j >= 0)
                { 
                    string sqlExpression3 = $"SELECT train_name FROM trains WHERE id = '{trains_user[j]}';";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(sqlExpression3, connection);
                        SqlDataReader reader1 = command.ExecuteReader();
                        while (reader1.Read())
                        {
                            trains[i] = reader1[0].ToString();
                            count++;
                            i++;
                        }
                        reader1.Close();
                    }
                    j--;
                }

                your_trains.Items.Clear();

                int counter = 0;
                for (i = 0; i < count; i++)
                {
                    Regex regex = new Regex($@"{search_training_field.Text}(\w*)", RegexOptions.IgnoreCase);
                    MatchCollection matches = regex.Matches(trains[i]);
                    if (matches.Count > 0)
                    {
                        your_trains.Items.Add(trains[i]);
                        counter++;
                    }
                }
                if (counter == 0)
                    your_trains.Items.Add("No results");
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void search_trainer_button_Click(object sender, RoutedEventArgs e)  // поиск тренера
        {
            try
            {
                string[] trainers_array = new string[100]; 
                int j = 0;
                int count = 0;
                string sqlExpression2 = $"SELECT trainer FROM trainers ;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression2, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        trainers_array[j] = reader[0].ToString();
                        j++;
                        count++;
                    }
                    reader.Close();
                }

                trainers.Items.Clear();

                int counter = 0;
                for (j = 0; j < count; j++)
                {
                    Regex regex = new Regex($@"{search_trainer_field.Text}(\w*)", RegexOptions.IgnoreCase);
                    MatchCollection matches = regex.Matches(trainers_array[j]);
                    if (matches.Count > 0)
                    {
                        trainers.Items.Add(trainers_array[j]);
                        counter++;
                    }
                }
                if (counter == 0)
                    trainers.Items.Add("No results");
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }
}
