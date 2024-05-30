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
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        User newUser = new User();
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void Regbt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (LoginReg.Text.Length != 0 && PasswordReg.Password.Length != 0)
                {
                    if (PasswordReg.Password.Trim() == PasswordConfirmReg.Password.Trim())
                    {
                        newUser.Username = LoginReg.Text.Trim();
                        newUser.Password = PasswordReg.Password.Trim();
                        newUser.RoleID = 3;
                        newUser.Balance = 0;
                        DBConnection.culinaryOasis.User.Add(newUser);
                        DBConnection.culinaryOasis.SaveChanges();
                        InfoTextBlock.Foreground = System.Windows.Media.Brushes.Green;
                        InfoTextBlock.Text = "Вы успешно зарегистрировались!";
                    }
                    else
                    {
                        PasswordConfirmReg.Password = "";
                        InfoTextBlock.Foreground = System.Windows.Media.Brushes.Red;
                        InfoTextBlock.Text = "Неправильно введен пароль";
                    }
                }
                else
                {
                    InfoTextBlock.Foreground = System.Windows.Media.Brushes.Red;
                    InfoTextBlock.Text = "Ошибка, введите данные";
                }
            }
            catch
            {
                InfoTextBlock.Foreground = System.Windows.Media.Brushes.Red;
                InfoTextBlock.Text = "Введите другое имя пользователя или уменьшите количество символов";
            }
        }
    }
}
