using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    public partial class RequestList : Window
    {
        public RequestList()
        {
            InitializeComponent();
            PopulateDataGrid();
        }

        public class Request
        {
            public int ID { get; set; }
            public int DeviceID { get; set; }
            public int ExecutorID { get; set; }
            public DateTime StartDate { get; set; }
            public string Comment { get; set; }
            public string Description { get; set; }
        }

        private void AddRequestButton_Click(object sender, RoutedEventArgs e)
        {
            AddRequest addRequest = new AddRequest();
            addRequest.Show();
            this.Close();
        }

        private void EditRequestButton_Click(object sender, RoutedEventArgs e)
        {
            EditRequest editRequest = new EditRequest();
            editRequest.Show();
            this.Close();
        }

        private void DeleteRequestButton_Click(object sender, RoutedEventArgs e)
        {
            // Проверка наличия выделенной строки
            if (dataGrid.SelectedItem != null)
            {
                // Запрос подтверждения удаления
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить эту заявку?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    // Удаление выбранной заявки из списка
                    var selectedRequest = (Request)dataGrid.SelectedItem;
                    dataGrid.Items.Remove(selectedRequest);

                    // Здесь можно добавить код для удаления заявки из базы данных
                    DeleteRequestFromDatabase(selectedRequest.ID);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите заявку для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
            this.Close();
        }

        private void PopulateDataGrid()
        {
            // Создаем строку подключения к базе данных
            string connectionString = "Data Source=DESKTOP-UBFK7FM;Initial Catalog=Praktic;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Подготавливаем SQL-запрос для выборки данных из таблицы Orders
                string query = "SELECT ID, DeviceID, ExecutorID, StartDate, Comment, Description FROM Orders";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Request request = new Request
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            DeviceID = Convert.ToInt32(reader["DeviceID"]),
                            ExecutorID = Convert.ToInt32(reader["ExecutorID"]),
                            StartDate = Convert.ToDateTime(reader["StartDate"]),
                            Comment = Convert.ToString(reader["Comment"]),
                            Description = Convert.ToString(reader["Description"])
                        };

                        dataGrid.Items.Add(request);
                    }
                }
            }
        }

        private void DeleteRequestFromDatabase(int requestId)
        {
            // Создаем строку подключения к базе данных
            string connectionString = "Data Source=DESKTOP-UBFK7FM;Initial Catalog=Praktic;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Подготавливаем SQL-запрос для удаления строки с указанным ID из таблицы Orders
                string query = "DELETE FROM Orders WHERE ID = @RequestId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Добавляем параметр для предотвращения SQL-инъекций
                    command.Parameters.Add(new SqlParameter("@RequestId", SqlDbType.Int)).Value = requestId;

                    // Выполняем SQL-запрос
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Заявка удалена из базы данных.");
                    }
                    else
                    {
                        MessageBox.Show("Ошибка удаления заявки из базы данных.");
                    }
                }
            }
        }
    }
}
