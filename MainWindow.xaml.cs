using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public static class UserData
        {
            public static string login { get; set; }
            public static string name { get; set; }
            public static string surname { get; set; }
            public static int roleID { get; set; }
            public static string password { get; set; }
            public static string phone { get; set; }
            public static int clientID { get; set; }
        }
        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            // Оставьте этот метод пустым, если вам не нужно выполнять какие-либо дополнительные действия при изменении текста в EquipmentTextBox.
        }

        public MainWindow()
        {
            InitializeComponent();

            // Check if a saved login exists
            if (Application.Current.Properties.Contains("SavedLogin"))
            {
                string savedLogin = Application.Current.Properties["SavedLogin"].ToString();
                LoginTextBox.Text = savedLogin;
                RememberCheckBox.IsChecked = true; // Check "Remember login" checkbox
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            UserData.login = LoginTextBox.Text;
            UserData.password = PasswordBox.Password;
            bool rememberLogin = RememberCheckBox.IsChecked ?? false;

            // Connection string to your database
            string connectionString = "Data Source=DESKTOP-UBFK7FM;Initial Catalog=Praktic;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Prepare SQL query to retrieve the password for the given login
                string query = "SELECT Password FROM [User] WHERE Login = @Login";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(new SqlParameter("@Login", SqlDbType.NVarChar, 50));
                    command.Parameters["@Login"].Value = UserData.login;

                    string storedPassword = command.ExecuteScalar() as string;

                    if (storedPassword != null && storedPassword == UserData.password)
                    {
                        if (rememberLogin)
                        {
                            // Save login in Properties
                            Application.Current.Properties["SavedLogin"] = UserData.login;
                        }

                        MainMenu mainMenu = new MainMenu();
                        mainMenu.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Incorrect login or password. Please try again.");
                    }
                }
            }
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            Registration registrationWindow = new Registration();
            registrationWindow.Show();
            this.Close();
        }
    }
}
