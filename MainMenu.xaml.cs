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
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            InitializeComponent();
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

        private void TrackOrderStatusButton_Click(object sender, RoutedEventArgs e)
        {
            TrackOrderStatus trackOrderStatus = new TrackOrderStatus();
            trackOrderStatus.Show();
            this.Close();
        }

        private void RequestListButton_Click(object sender, RoutedEventArgs e)
        {
            RequestList requestList = new RequestList();
            requestList.Show();
            this.Close();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }


    }
}
