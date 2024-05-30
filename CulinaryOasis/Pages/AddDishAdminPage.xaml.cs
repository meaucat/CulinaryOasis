using CulinaryOasis.DB;
using Microsoft.Win32;
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

namespace CulinaryOasis.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddDishAdminPage.xaml
    /// </summary>
    public partial class AddDishAdminPage : Page
    {
        private byte[] imageBytes;
        Dish dish = new Dish();
        public AddDishAdminPage()
        {
            InitializeComponent();
            CategoryCb.ItemsSource = DBConnection.culinaryOasis.Category.ToList();
        }

        private void LoadImageButton_Click(object sender, RoutedEventArgs e)
        {
            // Создаем экземпляр OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg";

            // Открываем диалоговое окно
            if (openFileDialog.ShowDialog() == true)
            {
                // Загружаем и отображаем изображение
                var bitmap = new BitmapImage(new Uri(openFileDialog.FileName));
                UserImage.Source = bitmap;

                imageBytes = File.ReadAllBytes(openFileDialog.FileName);
            }

        }

        private void AddDishBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CategoryCb.SelectedItem != null && NameTb != null && DescriptionTb != null && PriceTb != null && imageBytes != null)
                {
                    dish.Category = CategoryCb.SelectedItem as Category;
                    dish.Name = NameTb.Text;
                    dish.Description = DescriptionTb.Text;
                    dish.Image = imageBytes;
                    dish.ClientDescription = LongDescriptionTB.Text;
                    if (int.TryParse(PriceTb.Text, out int number))
                    {
                        dish.Price = number;
                        DBConnection.culinaryOasis.Dish.Add(dish);
                        DBConnection.culinaryOasis.SaveChanges();
                        NavigationService.Navigate(new AddCookingStagePage(dish));
                    }
                    else
                    {
                        PriceErrorTb.Text = "Введите действительное число";
                    }

                }
                else
                {
                    ErrorTB.Text = "Заполните все поля!";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                ErrorTB.Text = "Ошибка. Попробуйте уменьшить количество символов";
            }
        }
    }
}
