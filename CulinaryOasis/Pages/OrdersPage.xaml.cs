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
using CulinaryOasis.DB;

namespace CulinaryOasis.Pages
{
    /// <summary>
    /// Логика взаимодействия для OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        public static Order selectedOrder;
        public OrdersPage()
        {
            InitializeComponent();
            OrdersLV.ItemsSource = DBConnection.culinaryOasis.Order.ToList();
            DataContext = this;
        }

        private void OrdersLV_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            selectedOrder = OrdersLV.SelectedItem as Order;
            if (selectedOrder != null)
            {
                NavigationService.Navigate(new DetailsOrderPage(selectedOrder));
            }
        }
    }
}
