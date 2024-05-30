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
    /// Логика взаимодействия для EmployeePage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void OrderBtn_Click(object sender, RoutedEventArgs e)
        {
            MainMenuFrame.Navigate(new OrdersPage());
        }

        private void ReceiptBtn_Click(object sender, RoutedEventArgs e)
        {
            MainMenuFrame.Navigate(new DishesPage());
        }

        private void Regbt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthorizationPage());
        }
    }
}
