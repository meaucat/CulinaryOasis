using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
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
using CulinaryOasis.DB;

namespace CulinaryOasis.Pages
{
    /// <summary>
    /// Логика взаимодействия для WaitingOrderWindow.xaml
    /// </summary>
    public partial class WaitingOrderWindow : Window
    {
        public int _userId;
        public WaitingOrderWindow(int userId)
        {
            InitializeComponent();
            _userId = userId;
        }

        private void OrderNotCompeted_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OrderSuccess_Click(object sender, RoutedEventArgs e)
        {
            // Получите OrderID текущего пользователя

            // Получите заказ текущего пользователя
            var order = DBConnection.culinaryOasis.Order.FirstOrDefault(o => o.UserID == _userId && o.Status == "Active");

            if (order != null)
            {
                // Удалите записи из таблицы OrderDetails для текущего пользователя
                var orderDetails = DBConnection.culinaryOasis.OrderDetails.Where(od => od.OrderID == order.OrderID);
                DBConnection.culinaryOasis.OrderDetails.RemoveRange(orderDetails);

                // Установите статус "Complete" для заказа текущего пользователя
                order.Status = "Complete";
                DBConnection.culinaryOasis.Order.Remove(order);

                // Сохраните изменения в базе данных
                DBConnection.culinaryOasis.SaveChanges();

                // Закройте окно WaitingOrderWindow
                Close();
            }
            else
            {
                MessageBox.Show("Заказ не найден или уже выполнен.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        void hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://t.me/meeaucat");
            Close();
        }

    }
}
