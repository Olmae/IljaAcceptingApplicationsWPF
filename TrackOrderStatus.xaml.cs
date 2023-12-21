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
    /// Логика взаимодействия для TrackOrderStatus.xaml
    /// </summary>
    public partial class TrackOrderStatus : Window
    {
        public TrackOrderStatus()
        {
            InitializeComponent();
        }

        private void TrackStatusButton_Click(object sender, RoutedEventArgs e)
        {
            // Здесь будет код для отслеживания заявок
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
            this.Close();
        }
    }
}
