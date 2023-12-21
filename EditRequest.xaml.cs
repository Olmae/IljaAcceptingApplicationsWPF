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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для EditRequest.xaml
    /// </summary>
    public partial class EditRequest : Window
    {
        public EditRequest()
        {
            InitializeComponent();
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
            this.Close();
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string requestNumber = RequestNumberTextBox.Text;
            // Теперь у вас есть значение номера заявки, которое можно использовать.
        }
    }

}
