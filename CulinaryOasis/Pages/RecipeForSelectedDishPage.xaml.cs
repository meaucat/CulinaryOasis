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
using System.Xml.Schema;
using CulinaryOasis.DB;

namespace CulinaryOasis.Pages
{
    /// <summary>
    /// Логика взаимодействия для RecipeForSelectedDishPage.xaml
    /// </summary>
    public partial class RecipeForSelectedDishPage : Page
    {
        public static List<Dish> dishes { get; set; }
        public static List<CookingStage> cookingStages { get; set; }
        public static Category Category { get; set; }
        public static CookingStage CookingStage { get; set; }

        Dish contextDish;

        public RecipeForSelectedDishPage(Dish dish)
        {
            InitializeComponent();
            contextDish = dish;
            InitializeDataInPage();
            DataContext = this;
            Refresh();
        }

        private void Refresh()
        {
            CookingProcessLV.ItemsSource = DBConnection.culinaryOasis.CookingStage.Where(i => i.DishID == contextDish.DishID).ToList();
        }

        private void InitializeDataInPage()
        {
            cookingStages = DBConnection.culinaryOasis.CookingStage.ToList();
            dishes = DBConnection.culinaryOasis.Dish.ToList();
            DataContext = this;

            DishTB.Text = contextDish.Name;
            CategoryTB.Text = contextDish.Category.Name;
            DescriptionTB.Text = contextDish.Description;

            var time = DBConnection.culinaryOasis.CookingStage.Where(x => x.DishID == contextDish.DishID).Select(i => i.TimeInMinutes).ToList();
            int itogo = (int)time.Sum();
            CookingTimeTB.Text = $"{itogo} минут";

        }
    }
}
