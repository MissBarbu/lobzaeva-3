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
using LibMas;
using Lib_7;
using Microsoft.Win32;

namespace lobzaeva_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private int[,] matr;
        private void Fill_Click(object sender, RoutedEventArgs e)
        {
            if (!Int32.TryParse(inputM.Text, out int m)) return;
            if (!Int32.TryParse(inputN.Text, out int n)) return;
            rezultNumbers.Clear();
            matr = new int[m, n];
            MatrOperations.FillRandomValues(matr, -10, 10);
            dataGrid.ItemsSource = VisualArray.ToDataTable(matr).DefaultView;
        }
        private void Solve_Click(object sender, RoutedEventArgs e)
        {
            if (matr == null) return;
            string rezult = LibClass.MinItems(matr);
            rezultNumbers.Text = rezult;
        }
        private void TextBoxesClear(object sender, RoutedEventArgs e)
        {
            if (matr == null) return;
            rezultNumbers.Clear();
            MatrOperations.ClearMatrix(matr);
            dataGrid.ItemsSource = VisualArray.ToDataTable(matr).DefaultView;
        }

        private void SaveMas(object sender, RoutedEventArgs e)
        {
            if (matr == null)
            {
                MessageBox.Show("Таблица пуста", "Ошибка");
                return;
            }
            SaveFileDialog save = new SaveFileDialog();
            save.DefaultExt = ".txt";
            save.Filter = "Все файлы (*.*)|*.*|Текстовые файлы|*.txt";
            save.FilterIndex = 2;
            save.Title = "Сохранение таблицы";
            if (save.ShowDialog() == true)
            {
                MatrOperations.SaveMatrix(save.FileName, matr);
            }
        }
        private void openMas(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Все файлы (*.*)|*.*|Текстовые файлы|*.txt";
            open.FilterIndex = 2;
            open.Title = "Открытие таблицы";
            if (open.ShowDialog() == true)
            {
                if (open.FileName != string.Empty)
                {
                    MatrOperations.OpenMatrix(open.FileName, out matr);
                    inputM.Text = matr.GetLength(0).ToString();
                    inputN.Text = matr.GetLength(1).ToString();
                    dataGrid.ItemsSource = VisualArray.ToDataTable(matr).DefaultView;
                    Solve_Click(null, null);
                }
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Разработчик - Лобзаева Мария ИСП-31 \n\nВариант 7:\nДана матрица размера M × N. В каждой строке матрицы найти минимальный элемент.", "О программе", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void inputNumber_TextChanged(object sender, TextCompositionEventArgs e)
        {
            if (!Int32.TryParse(e.Text, out _))
            {
                e.Handled = true; // отклоняем ввод
                return;
            }
            TextBoxesClear(null, null);
        }

        private void dataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (matr == null) return;
            int columnIndex = e.Column.DisplayIndex;
            int rowIndex = e.Row.GetIndex();
            //Заносим полученное значение в массив
            matr[rowIndex, columnIndex] = Convert.ToInt32(((TextBox)e.EditingElement).Text);
            Solve_Click(null, null);
        }
    }
}
