using CulinaryOasis.DB;
using CulinaryOasis.Functions;
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

namespace CulinaryOasis.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddMoneyWindow.xaml
    /// </summary>
    public partial class AddMoneyWindow : Window
    {
        private int _userId;
        public AddMoneyWindow(int userId)
        {
            InitializeComponent();
            _userId = userId;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddBalanceBtn_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(BalanceTB.Text, out int amount) && amount > 0)
            {
                var user = DBConnection.culinaryOasis.User.FirstOrDefault(u => u.UserID == _userId);
                if (user != null)
                {
                    user.Balance += amount; // Обновить баланс пользователя
                    DBConnection.culinaryOasis.SaveChanges();
                    MessageBox.Show("Баланс успешно пополнен!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Пользователь не найден.");
                }
            }
            else
            {
                MessageBox.Show("Неверная сумма. Введите число.");
            }
        }
    }
}
