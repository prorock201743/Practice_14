using System;
using System.Collections.Generic;
using System.Data;
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
using LibArray;
using Microsoft.Win32;

namespace Practice_13
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            authorisation.ShowDialog();
            if (authorisation.IsAuthorized == true)
            {
                if (File.Exists("config.ini"))
                {
                    using (StreamReader streamwrite = new StreamReader("config.ini"))
                    {
                        _myArray = new MyArray(int.Parse(streamwrite.ReadLine()), int.Parse(streamwrite.ReadLine()));
                        InitializeComponent();
                        dataGrid.ItemsSource = _myArray.ToDataTable().DefaultView;
                    }
                }
                else MessageBox.Show("Файл конфигурации отсутствует!");
            }
            else Close();
        }

        private MyArray _myArray;

        Authorisation authorisation = new Authorisation();

        private void FillArray_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(rowCount.Text) || String.IsNullOrEmpty(columnCount.Text))
            {
                MessageBox.Show("Введите размер матрицы!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                _myArray = new MyArray(int.Parse(rowCount.Text), int.Parse(columnCount.Text));
                _myArray.Fill();
                dataGrid.ItemsSource = _myArray.ToDataTable().DefaultView;

                sizeRow.Text = $"Строк {dataGrid.Items.Count}";
                sizeColumn.Text = $"Столбцов {dataGrid.Columns.Count}";
            }
        }

        private void SaveArray_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.ItemsSource == null)
            {
                MessageBox.Show("Заполните матрицу перед сохранением!", "Предупреждение!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Текстовый документ (*.txt)|*.txt|Все файлы (*.*)|*.*";
            saveFileDialog.DefaultExt = ".txt";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.Title = "Экспорт массива";

            if (saveFileDialog.ShowDialog() == true)
            {
                _myArray.Export(saveFileDialog.FileName);
            }
            dataGrid.ItemsSource = null;
            dataGridResult.ItemsSource = null;
        }


        private void OpenArray_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Текстовый документ (*.txt)|*.txt|Все файлы (*.*)|*.*";
            openFileDialog.DefaultExt = ".txt";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Title = "Импорт массива";

            if (openFileDialog.ShowDialog() == true)
            {
                _myArray.Import(openFileDialog.FileName);
            }
            dataGrid.ItemsSource = _myArray.ToDataTable().DefaultView;
        }

        private void Swap_Click(object sender, RoutedEventArgs e)
        {
            int tmp, Nmax = 0, Nmin = 0;
            int min = _myArray[0, 0];
            int max = _myArray[0, 0];
            for (int i = 0; i < _myArray.RowLength; i++)
            {
                for (int j = 0; j < _myArray.ColumnLength; j++)
                {
                    if (_myArray[i, j] > max)
                    {
                        max = _myArray[i, j];
                        Nmax = i;
                    }
                    if (_myArray[i, j] < min)
                    {
                        min = _myArray[i, j];
                        Nmin = i;
                    }
                }
            }
            for (int j = 0; j < _myArray.ColumnLength; j++)
            {
                tmp = _myArray[Nmax, j];
                _myArray[Nmax, j] = _myArray[Nmin, j];
                _myArray[Nmin, j] = tmp;
            }

            dataGridResult.ItemsSource = _myArray.ToDataTable().DefaultView;
        }

        private void SettingsArray_Click(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings();
            settings.ShowDialog();
        }

        private void Information_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(" Золотарев М. А.\n Группа: ИСП-34\n Вариант №13", "Разработчик", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Завершить работу программы?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes) Close();
        }

        private void IndexArray_Click(object sender, MouseEventArgs e)
        {
            selectedCell.Text = $"[{dataGrid.Items.IndexOf(dataGrid.CurrentItem) + 1};" +
                $"{dataGrid.CurrentColumn.DisplayIndex + 1}]";
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = null;
        }

        private void ClearRight_Click(object sender, RoutedEventArgs e)
        {
            dataGridResult.ItemsSource = null;
        }
    }
}
