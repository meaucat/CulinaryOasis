using CulinaryOasis.DB;
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
    /// Логика взаимодействия для EditPersonPage.xaml
    /// </summary>
    public partial class EditPersonPage : Page
    {
        User user = new User();
        public EditPersonPage(User selectedUser)
        {
            InitializeComponent();
            RoleCb.ItemsSource = DBConnection.culinaryOasis.Role.Where(i => i.RoleID == 2 || i.RoleID == 1).ToList();
            user = selectedUser;
            RoleCb.SelectedItem = user.Role;
            NameTb.Text = user.Name;
            UsernameTb.Text = user.Username;
            SurnameTb.Text = user.Surname;
            PasswordTb.Text = user.Password;
            AgeTb.Text = user.Age.ToString();
            DataContext = this;
        }

        private void AddBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (RoleCb.SelectedItem != null && NameTb != null && SurnameTb != null && UsernameTb != null && PasswordTb != null && AgeTb != null)
                {
                    user.Role = RoleCb.SelectedItem as Role;
                    user.Name = NameTb.Text;
                    user.Surname = SurnameTb.Text;
                    user.Username = UsernameTb.Text;
                    user.Password = PasswordTb.Text;
                    if (int.TryParse(AgeTb.Text, out int number))
                    {
                        if (number >= 18 && number <= 65)
                        {
                            user.Age = number;
                            DBConnection.culinaryOasis.SaveChanges();
                            NavigationService.Navigate(new EditStaffPage());
                        }
                        else
                        {
                            AgeErrorTB.Text = "Сотрудник не подходит по возрасту";
                        }
                    }
                    else
                    {
                        AgeErrorTB.Text = "Введите число";
                    }

                }
                else
                {
                    ErrorTB.Foreground = System.Windows.Media.Brushes.Red;
                    ErrorTB.Text = "Заполните все поля!";
                }
            }
            catch (Exception ex)
            {
                ErrorTB.Foreground = System.Windows.Media.Brushes.Red;
                ErrorTB.Text = "Ошибка. Попробуйте уменьшить количество символов, либо введите другой Username";
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
