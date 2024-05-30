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
    /// Логика взаимодействия для DishesPage.xaml
    /// </summary>
    public partial class DishesPage : Page
    {
        public static List<Category> categories { get; set; }
        public static List<Dish> dishes { get; set; }
        public static Dish dish { get; set; }
        public DishesPage()
        {
            InitializeComponent();
            categories = DBConnection.culinaryOasis.Category.ToList();
            dishes = DBConnection.culinaryOasis.Dish.ToList();
            DataContext = this;
            Refresh();
        }

        private void Refresh()
        {
            DishesLV.ItemsSource = DBConnection.culinaryOasis.Dish.ToList();
        }

        private void CategoryCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var a = CategoryCB.SelectedItem as Category;
            DishesLV.ItemsSource = DBConnection.culinaryOasis.Dish.Where(i => i.CategoryID == a.CategoryID).ToList();
        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchTB.Text.Length > 0)
            {
                DishesLV.ItemsSource = DBConnection.culinaryOasis.Dish.Where(i => i.Name.ToLower().StartsWith(SearchTB.Text.Trim().ToLower())).ToList();
            }
            else
            {
                Refresh();
            }
        }

        private void DishesLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DishesLV.SelectedItem is Dish)
            {
                DBConnection.selectedDish = DishesLV.SelectedItem as Dish;
                NavigationService.Navigate(new RecipeForSelectedDishPage(DishesLV.SelectedItem as Dish));
            }
        }
    }
}
