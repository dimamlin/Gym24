using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
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
using System.Security.Cryptography;


namespace kp
{

    public partial class Registration : Window
    {
        public class avatar
        {
            public string imagepath_file;
        }

        avatar user_ava = new avatar();

        public Registration()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            bool correct = true;
                string sex = "";
                if (sexm_field.IsChecked.ToString() == "True" && sexf_field.IsChecked.ToString() == "False" && sexo_field.IsChecked.ToString() == "False")
                {
                    sex = "Male";
                    correct_sex.Content = "";
                }
                else
                {
                    if (sexm_field.IsChecked.ToString() == "False" && sexf_field.IsChecked.ToString() == "True" && sexo_field.IsChecked.ToString() == "False")
                    {
                        sex = "Female";
                        correct_sex.Content = "";
                    }
                    else
                    {
                        if (sexm_field.IsChecked.ToString() == "False" && sexf_field.IsChecked.ToString() == "False" && sexo_field.IsChecked.ToString() == "True")
                        {
                            sex = "Other";
                            correct_sex.Content = "";
                        }
                        else
                        {
                            if (sexm_field.IsChecked.ToString() == "False" && sexf_field.IsChecked.ToString() == "False" && sexo_field.IsChecked.ToString() == "False")
                            {
                                correct_sex.Content = "Choose sex";
                                correct = false;
                            }
                            else
                            {
                                if (sexm_field.IsChecked.ToString() == "True" && sexf_field.IsChecked.ToString() == "True" && sexo_field.IsChecked.ToString() == "False")
                                {
                                    correct_sex.Content = "Choose one sex";
                                    correct = false;
                                }
                                else
                                {
                                    if (sexm_field.IsChecked.ToString() == "False" && sexf_field.IsChecked.ToString() == "True" && sexo_field.IsChecked.ToString() == "True")
                                    {
                                        correct_sex.Content = "Choose one sex";
                                        correct = false;
                                    }
                                    else
                                    {
                                        if (sexm_field.IsChecked.ToString() == "True" && sexf_field.IsChecked.ToString() == "False" && sexo_field.IsChecked.ToString() == "True")
                                        {
                                            correct_sex.Content = "Choose one sex";
                                            correct = false;
                                        }
                                        else
                                        {
                                            if (sexm_field.IsChecked.ToString() == "True" && sexf_field.IsChecked.ToString() == "True" && sexo_field.IsChecked.ToString() == "True")
                                            {
                                                correct_sex.Content = "Choose one sex";
                                                correct = false;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                string data_birtgh = $"{day_birth.Text}" + " of " + $"{month_birth.Text}" + " in " + $"{year_birth.Text}";

                bool micro_correct = true;
                /////
                Regex regex = new Regex("^[A-Z][a-zA-Z]*$");
                Match match = regex.Match(surname_field.Text);
                correct_surname.Content = "Enter correct surname";
                if (!match.Success)
                {
                    correct = false;
                    micro_correct = false;
                }
                while (match.Success)
                {
                    match = match.NextMatch();
                    correct_surname.Content = "";
                }

                if (surname_field.Text != "" && micro_correct && surname_field.Text.Length < 50)
                {
                    correct_surname.Content = "";
                }
                else
                {
                    correct_surname.Content = "Enter correct surname";
                    correct = false;
                }
                /////
                micro_correct = true;
                Regex regex1 = new Regex("^[A-Z][a-zA-Z]*$");
                Match match1 = regex1.Match(name_field.Text);
                correct_name.Content = "Enter correct name";
                if (!match1.Success)
                {
                    correct = false;
                    micro_correct = false;
                }
                while (match1.Success)
                {
                    match1 = match1.NextMatch();
                    correct_name.Content = "";
                }

                if (name_field.Text != "" && micro_correct && name_field.Text.Length < 50)
                {
                    correct_name.Content = "";
                }
                else
                {
                    correct_name.Content = "Enter correct name";
                    correct = false;
                }
                /////
                micro_correct = true;
                Regex regex2 = new Regex("^[a-zA-Z0-9][a-zA-Z0-9]*$");
                Match match2 = regex2.Match(nickname_field.Text);
                correct_nickname.Content = "Enter correct nickname";
                if (!match2.Success)
                {
                    correct = false;
                    micro_correct = false;
                }
                while (match2.Success)
                {
                    match2 = match2.NextMatch();
                    correct_nickname.Content = "";
                }

                if (nickname_field.Text != "" && micro_correct && nickname_field.Text.Length < 50)
                {
                    correct_nickname.Content = "";
                }
                else
                {
                    correct_nickname.Content = "Enter correct nickname";
                    correct = false;
                }
                /////
                micro_correct = true;
                Regex regex11 = new Regex("^[0-9][0-9]*$");
                Match match11 = regex11.Match(day_birth.Text);
                correct_birthday.Content = "Enter correct birthday";
                if (!match11.Success)
                {
                    correct = false;
                    micro_correct = false;
                }
                while (match11.Success)
                {
                    match11 = match11.NextMatch();
                    correct_birthday.Content = "";
                }

                if (day_birth.Text != "" && micro_correct && day_birth.Text.Length < 50)
                {
                    correct_birthday.Content = "";
                }
                else
                {
                    correct_birthday.Content = "Enter correct birthday";
                    correct = false;
                }

                micro_correct = true;
                Regex regex12 = new Regex("^[0-9][0-9][0-9][0-9]*$");
                Match match12 = regex12.Match(year_birth.Text);
                correct_birthday.Content = "Enter correct birthday";
                if (!match12.Success)
                {
                    correct = false;
                    micro_correct = false;
                }
                while (match12.Success)
                {
                    match12 = match12.NextMatch();
                    correct_birthday.Content = "";
                }

                if (year_birth.Text != "" && micro_correct && year_birth.Text.Length < 50)
                {
                    correct_birthday.Content = "";
                }
                else
                {
                    correct_birthday.Content = "Enter correct birthday";
                    correct = false;
                }

                if (micro_correct)
                {
                    if (day_birth.Text == "" || Convert.ToInt32(day_birth.Text.ToString()) > 31 || Convert.ToInt32(day_birth.Text.ToString()) < 1 || month_birth.Text.ToString() == "" || Convert.ToInt32(year_birth.Text.ToString()) < 1875 || Convert.ToInt32(year_birth.Text.ToString()) > 2002)
                    {
                        correct_birthday.Content = "Enter correct birthday";
                        correct = false;
                    }
                    else
                        correct_birthday.Content = "";
                }

               /////

                micro_correct = true;
                Regex regex9 = new Regex(@"^[a-zA-Z0-9]+@[a-z]+\.[a-z]*$");
                Match match9 = regex9.Match(email_field.Text);
                correct_email.Content = "Enter correct email";
                if (!match9.Success)
                {
                    correct = false;
                    micro_correct = false;
                }
                while (match9.Success)
                {
                    match9 = match9.NextMatch();
                    correct_email.Content = "";
                }

                if (email_field.Text != "" && micro_correct && email_field.Text.Length < 50)
                {
                    correct_email.Content = "";
                }
                else
                {
                    correct_email.Content = "Enter correct email";
                    correct = false;
                }
                //////
                micro_correct = true;
                Regex regex6 = new Regex("^[a-zA-Z0-9][a-zA-Z0-9]*$");
                Match match6 = regex6.Match(login_field.Text);
                correct_login.Content = "Enter correct login";
                if (!match6.Success)
                {
                    correct = false;
                    micro_correct = false;
                }
                while (match6.Success)
                {
                    match6 = match6.NextMatch();
                    correct_login.Content = "";
                }

                if (login_field.Text != "" && micro_correct && login_field.Text.Length < 50)
                {
                    correct_login.Content = "";
                }
                else
                {
                    correct_login.Content = "Enter correct login";
                    correct = false;
                }
                //////

                micro_correct = true;
                Regex regex7 = new Regex("^[a-zA-Z0-9][a-zA-Z0-9]*$");
                Match match7 = regex7.Match(password_field.Password);
                correct_password.Content = "Enter correct password";
                if (!match7.Success)
                {
                    correct = false;
                    micro_correct = false;
                }
                while (match7.Success)
                {
                    match7 = match7.NextMatch();
                    correct_password.Content = "";
                }

                if (password_field.Password != "" && micro_correct && password_field.Password.Length < 50)
                {
                    correct_password.Content = "";
                }
                else
                {
                    correct_password.Content = "Enter correct password";
                    correct = false;
                }
                /////
                if (password_field.Password != confirm_field.Password || confirm_field.Password == "")
                {
                    correct_confirm.Content = "Passwords mismatch";
                    correct = false;
                }
                else
                    correct_confirm.Content = "";
                /////
                micro_correct = true;
                Regex regex8 = new Regex("^[0-9+][0-9]*$");
                Match match8 = regex8.Match(phone_field.Text);
                correct_phone.Content = "Enter correct phone";
                if (!match8.Success)
                {
                    correct = false;
                    micro_correct = false;
                }
                while (match8.Success)
                {
                    match8 = match8.NextMatch();
                    correct_phone.Content = "";
                }

                if (phone_field.Text != "" && micro_correct && phone_field.Text.Length < 50)
                {
                    correct_phone.Content = "";
                }
                else
                {
                    correct_phone.Content = "Enter correct phone";
                    correct = false;
                }
                /////

                if (correct)
                {
                    try
                    {
                        string GetHashString(string s)
                        {
                            user_ava.imagepath_file = @"D:\Лабораторные работы\ООП\4 семестр\КП\images\ava_o.png";

                            //переводим строку в байт-массим  
                            byte[] bytes = Encoding.Unicode.GetBytes(s);

                            //создаем объект для получения средст шифрования  
                            MD5CryptoServiceProvider CSP =
                                new MD5CryptoServiceProvider();

                            //вычисляем хеш-представление в байтах  
                            byte[] byteHash = CSP.ComputeHash(bytes);

                            string hash = string.Empty;

                            //формируем одну цельную строку из массива  
                            foreach (byte b in byteHash)
                                hash += string.Format("{0:x2}", b);

                            return hash;
                        }

                        string connectionString = @"server=DIMAMLIN-PC\QQQQ;database=kp;Integrated Security=true;";
                        string sqlExpression1 = $"INSERT INTO login_data (login, password) VALUES ('{login_field.Text}', '{GetHashString(password_field.Password)}')";
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            SqlCommand command = new SqlCommand(sqlExpression1, connection);
                            int number = command.ExecuteNonQuery();
                            Console.WriteLine("Добавлено объектов: {0}", number);
                            connection.Close();
                        }
                        string sqlExpression2 = $"INSERT INTO user_data (surname, name, nickname, birthday, email, sex, phone, photo) VALUES ('{surname_field.Text}', '{name_field.Text}','{nickname_field.Text}','{data_birtgh}','{email_field.Text}','{sex}','{phone_field.Text}','{user_ava.imagepath_file}')";
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            SqlCommand command = new SqlCommand(sqlExpression2, connection);
                            int number = command.ExecuteNonQuery();
                            Console.WriteLine("Добавлено объектов: {0}", number);
                            connection.Close();
                        }
                    }
                    catch (Exception er)
                    {
                        MessageBox.Show(er.Message);
                    }

                    MainWindow mainWindow = new MainWindow();
                    this.Close();
                    mainWindow.Show();
                }
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }  
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.youtube.com/channel/UCOoYQyEXsvcoh56hxqsRCYA");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://twitter.com/dimamlin");
        }

        private void get_photo_Click(object sender, RoutedEventArgs e) // установка аватара
        {
            try
            {
                string imagePath;
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image files|*.png;*.jpg";
                if (openFileDialog.ShowDialog() == true)
                {
                    imagePath = openFileDialog.FileName;
                    imageBrush.ImageSource = new BitmapImage(new Uri(imagePath, UriKind.Relative));
                    user_ava.imagepath_file = imagePath;
                }
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
                MessageBox.Show(err.Source);
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.facebook.com/gym24by/");
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.instagram.com/gym24by/?hl=ru");
        }

        private void Button_Click_5(object sender, RoutedEventArgs e) // выход
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            stay_home.Content = "Only for authoritated users";
        }
    }
}
