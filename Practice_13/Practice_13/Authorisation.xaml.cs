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
using System.Windows.Shapes;

namespace Practice_13
{
    /// <summary>
    /// Логика взаимодействия для Authorisation.xaml
    /// </summary>
    public partial class Authorisation : Window
    {
        public Authorisation()
        {
            InitializeComponent();
        }

        public bool IsAuthorized { get; set; }
       
        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            if (password.Password == "123")
            {
                IsAuthorized = true;
                Close();
            }
            else
            {
                MessageBox.Show("Неправильный пароль!", "Ошибка!", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                password.Focus();
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Owner.Close();
        }
    }
}
