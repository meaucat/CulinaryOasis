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
    /// Логика взаимодействия для AddCookingStagePage.xaml
    /// </summary>
    public partial class AddCookingStagePage : Page
    {
        int count = 0;
        public static Dish dish = new Dish();
        public static CookingStage cookingStage = new CookingStage();
        public AddCookingStagePage(Dish enterDish)
        {
            InitializeComponent();
            dish = enterDish;
            TextTB.Text = $"Добавьте шаги для приготовления {dish.Name}";
        }

        private void AddStageBtn_Click(object sender, RoutedEventArgs e)
        {
            if (StageTB != null && TimeTB != null)
            {
                if (int.TryParse(TimeTB.Text, out int number))
                {
                    cookingStage.DishID = dish.DishID;
                    cookingStage.ProcessDescription = StageTB.Text;
                    cookingStage.TimeInMinutes = number;
                    DBConnection.culinaryOasis.CookingStage.Add(cookingStage);
                    DBConnection.culinaryOasis.SaveChanges();
                    count += 1;

                    ErrorTB.Foreground = System.Windows.Media.Brushes.Green;
                    ErrorTB.Text = $"Вы записали шаг {count}!";
                    StageTB.Text = "";
                    TimeTB.Text = "";
                }
                else
                {
                    ErrorTB.Foreground = System.Windows.Media.Brushes.Red;
                    ErrorTB.Text = "Введите число";
                }

            }
            else
            {
                ErrorTB.Foreground = System.Windows.Media.Brushes.Red;
                ErrorTB.Text = "Введите данные";
            }
        }
    }
}
