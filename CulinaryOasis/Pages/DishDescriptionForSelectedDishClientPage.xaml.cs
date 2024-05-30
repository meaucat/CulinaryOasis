using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для DishDescriptionForSelectedDishClientPage.xaml
    /// </summary>
    public partial class DishDescriptionForSelectedDishClientPage : Page
    {
        Dish dish;

        public DishDescriptionForSelectedDishClientPage(Dish selectedDish)
        {
            InitializeComponent();
            dish = selectedDish;

            InitializeDataInPage();
            DataContext = this;
        }

        private void InitializeDataInPage()
        {
            DishTB.Text = dish.Name;
            CategoryTB.Text = dish.Category.Name;
            DescriptionTB.Text = dish.ClientDescription;
            UserImage.Source = ByteArrayToImageConverter.ConvertByteArrayToImage(dish.Image);
        }




        private void AddCartBtn_Click(object sender, RoutedEventArgs e)
        {
            int quantity = 0;
            if (int.TryParse(QuantityTB.Text, out int number))
            {
                if (number >= 1 && number <= 10)
                {
                    quantity = number;

                    ShoppingCart shoppingCart = new ShoppingCart(DBConnection.culinaryOasis);

                    shoppingCart.AddToCart(GetUserId.UserId, dish.DishID, quantity);

                    ErrorTB.Foreground = System.Windows.Media.Brushes.Green;
                    ErrorTB.Text = "Вы добавили в корзину " + quantity + " шт";
                }
                else
                {
                    ErrorTB.Foreground = System.Windows.Media.Brushes.Red;
                    ErrorTB.Text = "Введите число от 1 до 10";
                }
            }
            else
            {
                ErrorTB.Text = "Введите число";
            }

        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ClientPage());
        }


    }
}
