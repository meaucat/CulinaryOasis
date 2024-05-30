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
using CulinaryOasis.Functions;

namespace CulinaryOasis.Pages
{
    /// <summary>
    /// Логика взаимодействия для ClientCartPage.xaml
    /// </summary>
    public partial class ClientCartPage : Page
    {
        public static OrderDetails selectedOrderDetails;
        public int _userId;
        public ClientCartPage(int userId)
        {
            InitializeComponent();
            CartLV.ItemsSource = DBConnection.culinaryOasis.OrderDetails.Where(x => x.OrderID == (DBConnection.culinaryOasis.Order.Where(i => i.UserID == GetUserId.UserId).ToList().FirstOrDefault().OrderID)).ToList();

            var orderId = DBConnection.culinaryOasis.Order
                .Where(i => i.UserID == GetUserId.UserId)
                .Select(o => o.OrderID)
                .FirstOrDefault();

            if (orderId != 0)
            {
                var totalPrice = DBConnection.culinaryOasis.OrderDetails
                    .Where(x => x.OrderID == orderId)
                    .Sum(x => x.Dish.Price * x.Quantity);

                SumTB.Text = totalPrice.ToString();
            }
            else
            {
                SumTB.Text = "0"; // Или другое значение по умолчанию
            }

            _userId = userId;

            Refresh();

            UpdateBalance();

            DataContext = this;
        }
        private void Refresh()
        {
            CartLV.ItemsSource = DBConnection.culinaryOasis.OrderDetails.Where(x => x.OrderID == (DBConnection.culinaryOasis.Order.Where(i => i.UserID == GetUserId.UserId).ToList().FirstOrDefault().OrderID)).ToList();
            var orderId = DBConnection.culinaryOasis.Order
                            .Where(i => i.UserID == GetUserId.UserId)
                            .Select(o => o.OrderID)
                            .FirstOrDefault();

            if (orderId != 0)
            {
                var totalPrice = DBConnection.culinaryOasis.OrderDetails
                    .Where(x => x.OrderID == orderId)
                    .Sum(x => x.Dish.Price * x.Quantity);

                SumTB.Text = totalPrice.ToString();
            }
            else
            {
                SumTB.Text = "0"; // Или другое значение по умолчанию
            }
        }

        private void AddMoneyBtn_Click(object sender, RoutedEventArgs e)
        {
            AddMoneyWindow addMoneyWindow = new AddMoneyWindow(_userId);
            addMoneyWindow.Show();
            addMoneyWindow.Closed += AddMoneyWindow_Closed;
        }

        private void CartLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedOrderDetails = CartLV.SelectedItem as OrderDetails;
        }

        private void CartLV_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (selectedOrderDetails != null)
            {
                ShoppingCart shoppingCart = new ShoppingCart(DBConnection.culinaryOasis);
                shoppingCart.RemoveFromCart(selectedOrderDetails.OrderDetailsID);
                Refresh();
            }
            else
            {
                MessageBox.Show("Корзина пуста");
            }
        }
        

        public void UpdateBalance()
        {
            var user = DBConnection.culinaryOasis.User.Find(_userId);
            if (user != null)
            {
                BalanceTB.Text = $"{user.Balance} ₽";
            }
        }

        private void AddMoneyWindow_Closed(object sender, System.EventArgs e)
        {
            UpdateBalance();
        }

        private void BuyBtn_Click(object sender, RoutedEventArgs e)
        {
            var order = DBConnection.culinaryOasis.Order.First(o => o.UserID == _userId && o.Status == "Active");
            order.Status = "Pending";
            DBConnection.culinaryOasis.SaveChanges();

            UpdateBalance();
            var user = DBConnection.culinaryOasis.User.Find(_userId);
            if (int.TryParse(SumTB.Text, out int number))
            {
                if (user.Balance >= number)
                {
                    user.Balance -= number;

                    WaitingOrderWindow waitingOrderWindow = new WaitingOrderWindow(_userId);
                    waitingOrderWindow.Show();
                    waitingOrderWindow.Closed += WaitingOrderWindow_Closed;
                    NavigationService.Navigate(new ClientPage());
                }
                else
                {
                    ErrorTB.Text = "Ваш баланс меньше, чем стоимость заказа";
                }
            }
            else
            {
                ErrorTB.Text = "Ошибка";
            }
        }

        private void WaitingOrderWindow_Closed(object sender, System.EventArgs e)
        {
            UpdateBalance();
        }
    }
}
