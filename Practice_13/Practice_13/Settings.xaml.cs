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
using System.Windows.Shapes;

namespace Practice_13
{
    /// <summary>
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void SetTableSize_Click(object sender, RoutedEventArgs e)
        {
            using (StreamWriter streamwrite = new StreamWriter("config.ini"))
            {
                streamwrite.WriteLine(column.Text);
                streamwrite.WriteLine(row.Text);
            }
            Close();
        }
    }
}
