using System;
using System.Collections.Generic;
using System.Data.Common;
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
    /// Логика взаимодействия для AddDish.xaml
    /// </summary>
    public partial class AddDish : Page
    {
        public static Dish selectedDish;
        public AddDish()
        {
            InitializeComponent();
            DishLV.ItemsSource = DBConnection.culinaryOasis.Dish.ToList();
            DataContext = this;
        }


        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddDishAdminPage());
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            DBConnection.culinaryOasis.Dish.Remove(selectedDish);
            DBConnection.culinaryOasis.SaveChanges();
            DishLV.ItemsSource = DBConnection.culinaryOasis.Dish.ToList();
        }

        private void DishLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedDish = DishLV.SelectedItem as Dish;
        }

    }
}
