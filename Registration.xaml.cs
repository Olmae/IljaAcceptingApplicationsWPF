using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text; // Получаем логин
            string password = PasswordBox.Password; // Получаем пароль
            string name = NameTextBox.Text;
            string surname = SurnameTextBox.Text;
            string phone = PhoneTextBox.Text;
            string role = (RoleComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(surname) || string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(role))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            int roleId = role == "Заказчик" ? 1 : 2; // 1 для заказчика, 2 для мастера

            // Создайте строку подключения к вашей базе данных
            string connectionString = "Data Source=DESKTOP-UBFK7FM;Initial Catalog=Praktic;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Подготовьте SQL-запрос для вставки нового пользователя в базу данных
                string query = "INSERT INTO [User] (Login, Password, Name, Surname, Phone, RoleID) " +
                               "VALUES (@Login, @Password, @Name, @Surname, @Phone, @RoleID)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(new SqlParameter("@Login", SqlDbType.NVarChar, 50)).Value = login;
                    command.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar, 50)).Value = password;
                    command.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 50)).Value = name;
                    command.Parameters.Add(new SqlParameter("@Surname", SqlDbType.NVarChar, 50)).Value = surname;
                    command.Parameters.Add(new SqlParameter("@Phone", SqlDbType.NVarChar, 15)).Value = phone;
                    command.Parameters.Add(new SqlParameter("@RoleID", SqlDbType.Int)).Value = roleId;

                    // Выполните SQL-запрос
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Регистрация прошла успешно.");
                        MainMenu mainMenu = new MainMenu();
                        mainMenu.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при регистрации. Попробуйте еще раз.");
                    }
                }
            }
        }
    }
}