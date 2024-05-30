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
    /// Логика взаимодействия для DetailsOrderPage.xaml
    /// </summary>
    public partial class DetailsOrderPage : Page
    {
        Order selectedOrder;
        public DetailsOrderPage(Order order)
        {
            InitializeComponent();
            selectedOrder = order;
            PopulateOrderListView();
        }

        private void PopulateOrderListView()
        {
            // Используйте выбранный заказ для фильтрации данных в базе данных и заполнения ListView
            var orderDetails = DBConnection.culinaryOasis.OrderDetails.Where(od => od.OrderID == selectedOrder.OrderID).ToList();
            OrderLV.ItemsSource = orderDetails;
        }
    }
}
