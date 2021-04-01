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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace kp
{


    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();

            DataTable dt_user1 = Select("SELECT * FROM login_data ORDER BY  id");
            for (int i = 0; i < dt_user1.Rows.Count; i++)
            {
                if (dt_user1.Rows[i][3].ToString() == "+")
                {
                    UserApp userApp = new UserApp();
                    this.Close();
                    userApp.Show();
                    break;
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            this.Close();
            registration.Show();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                string GetHashString(string s)
                {
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

                if (login.Text == "admin" && password.Password == "admin")
                {
                    Administrator window = new Administrator();
                    window.Show();
                    this.Close();
                }
                else
                {
                    bool authorixation = false;
                    DataTable dt_user = Select("SELECT * FROM login_data ORDER BY  id");
                    for (int i = 0; i < dt_user.Rows.Count; i++)
                    {
                        string logins = dt_user.Rows[i][1].ToString();
                        string passwords = dt_user.Rows[i][2].ToString();
                        logins = logins.Trim();
                        passwords = passwords.Trim();
                        if (login.Text == logins && GetHashString(password.Password) == passwords)
                        {

                            string connectionString = @"server=DIMAMLIN-PC\QQQQ;database=kp;Integrated Security=true;";
                            string sqlExpression1 = $"UPDATE login_data SET isenabled='+' where login = '{logins}'";
                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {
                                connection.Open();
                                SqlCommand command = new SqlCommand(sqlExpression1, connection);
                                int number = command.ExecuteNonQuery();
                                Console.WriteLine("Добавлено объектов: {0}", number);
                                connection.Close();
                            }

                            UserApp userApp = new UserApp();
                            this.Close();
                            userApp.Show();
                            authorixation = true;
                            break;
                        }
                    }
                    if (!authorixation) authorization.Content = "Incorrect login or password!";
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }
}
