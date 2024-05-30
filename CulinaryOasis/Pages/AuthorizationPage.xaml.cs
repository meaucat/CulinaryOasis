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
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public static User user;
        public static List<User> users { get; set; }
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private void EnterBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string login = LoginTb.Text.Trim().ToLower();
                string password = PasswordTb.Password.Trim().ToLower();
                user = DBConnection.culinaryOasis.User.FirstOrDefault(i => i.Username == login && i.Password == password);
                if (user != null && user.RoleID == 1)
                {
                    NavigationService.Navigate(new MainPage());
                }
                else if (user != null && user.RoleID == 2)
                {
                    NavigationService.Navigate(new AdminPage());
                }
                else if (user != null && user.RoleID == 3)
                {
                    NavigationService.Navigate(new ClientMainPage(user.UserID));
                    GetUserId.UserId = user.UserID;
                }
                else
                {
                    ErrorTextBlock.Text = "Не удалось войти, проверьте введенные вами данные";
                }
            }
            catch
            {
                ErrorTextBlock.Text = "Произошла ошибка, попробуйте снова";
            }
        }

    }
}
