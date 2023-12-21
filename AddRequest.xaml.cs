using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    public partial class AddRequest : Window
    {
        private bool unsavedData = false;

        public AddRequest()
        {
            InitializeComponent();
            // Добавляем обработчики событий для TextBox
            DescriptionTextBox.TextChanged += TextBox_TextChanged;
            EquipmentTextBox.TextChanged += TextBox_TextChanged;
            ClientTextBox.TextChanged += TextBox_TextChanged;
            EquipmentTextBox.TextChanged += TextBox_TextChanged_1; // Добавляем обработчик для нового TextBox
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (unsavedData)
            {
                MessageBoxResult result = MessageBox.Show("Уверены, что хотите выйти? Прогресс заполнения заявки будет потерян.", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    MainMenu mainMenu = new MainMenu();
                    mainMenu.Show();
                    this.Close();
                }
            }
            else
            {
                MainMenu mainMenu = new MainMenu();
                mainMenu.Show();
                this.Close();
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            unsavedData = true;
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            unsavedData = true;
        }

        private void AddRequestButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем данные из полей ввода
            string equipment = EquipmentTextBox.Text;
            string clientInfo = ClientTextBox.Text;
            string description = DescriptionTextBox.Text;

            // Создаем строку подключения к базе данных
            string connectionString = "Data Source=DESKTOP-UBFK7FM;Initial Catalog=Praktic;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Генерируем случайное значение для ID заявки
                Random random = new Random();
                int requestId = random.Next(1, 1000); // Пример диапазона значений для ID

                // Подготавливаем SQL-запрос для вставки данных в таблицу Orders
                string query = "INSERT INTO Orders (ID, DeviceID, ExecutorID, Description, StartDate, StatusID, DefectID, Comment, Price) " +
                               "VALUES (@ID, @DeviceID, @ExecutorID, @Description, @StartDate, @StatusID, @DefectID, @Comment, @Price)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Установка параметров для SQL-запроса
                    command.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = requestId;
                    command.Parameters.Add(new SqlParameter("@DeviceID", SqlDbType.Int)).Value = random.Next(1, 100);
                    command.Parameters.Add(new SqlParameter("@ExecutorID", SqlDbType.Int)).Value = random.Next(1, 100);
                    command.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 255)).Value = description;
                    command.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.Date)).Value = DateTime.Now;
                    command.Parameters.Add(new SqlParameter("@StatusID", SqlDbType.Int)).Value = random.Next(1, 100);
                    command.Parameters.Add(new SqlParameter("@DefectID", SqlDbType.Int)).Value = random.Next(1, 100);
                    command.Parameters.Add(new SqlParameter("@Comment", SqlDbType.VarChar, 255)).Value = clientInfo;
                    command.Parameters.Add(new SqlParameter("@Price", SqlDbType.Decimal)).Value = 0.0;

                    // Выполняем SQL-запрос
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Заявка успешно добавлена.");
                        unsavedData = false;
                        // Очищаем поля после успешной вставки данных
                        EquipmentTextBox.Text = "";
                        ClientTextBox.Text = "";
                        DescriptionTextBox.Text = "";
                        MainMenu mainMenu = new MainMenu();
                        mainMenu.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при добавлении заявки. Попробуйте еще раз.");
                    }
                }
            }
        }
    }
}