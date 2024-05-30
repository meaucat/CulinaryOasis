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
    /// Логика взаимодействия для EditStaffPage.xaml
    /// </summary>
    public partial class EditStaffPage : Page
    {
        public static User selectedUser;
        public EditStaffPage()
        {
            InitializeComponent();
            StaffLV.ItemsSource = DBConnection.culinaryOasis.User.Where(i => i.RoleID == 1 ||  i.RoleID == 2).ToList();
            DataContext = this;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddStaffPage());
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            DBConnection.culinaryOasis.User.Remove(selectedUser);
            DBConnection.culinaryOasis.SaveChanges();
            StaffLV.ItemsSource = DBConnection.culinaryOasis.User.Where(i => i.RoleID == 1 || i.RoleID == 2).ToList();
        }

        private void StaffLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedUser = StaffLV.SelectedItem as User;
        }

        private void StaffLV_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            selectedUser = StaffLV.SelectedItem as User;
            NavigationService.Navigate(new EditPersonPage(selectedUser));
        }
    }
}
