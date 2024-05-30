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

namespace CulinaryOasis.Pages
{
    /// <summary>
    /// Логика взаимодействия для ClientMainPage.xaml
    /// </summary>
    public partial class ClientMainPage : Page
    {
        private int _userId;
        public ClientMainPage(int userId)
        {
            InitializeComponent();
            _userId = userId;
        }

        private void OrderBtn_Click(object sender, RoutedEventArgs e)
        {
            MainMenuFrame.Navigate(new ClientCartPage(_userId));
        }

        private void ReceiptBtn_Click(object sender, RoutedEventArgs e)
        {
            MainMenuFrame.Navigate(new ClientPage());
        }

        private void Regbt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthorizationPage());
        }
    }
}
