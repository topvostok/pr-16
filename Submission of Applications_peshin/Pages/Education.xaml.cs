using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using Microsoft.Win32;

namespace Submission_of_Applications_peshin.Pages
{
    /// <summary>
    /// Логика взаимодействия для Education.xaml
    /// </summary>
    public partial class Education : Page
    {
        public Education()
        {
            InitializeComponent();
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateData())
            {
                NavigationService.Navigate(new Status());
            }
        }

        private void SelectFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Документы (*.png;*.jpg;*.jpeg;*.pdf)|*.png;*.jpg;*.jpeg;*.pdf|Все файлы (*.*)|*.*";
            openFileDialog.Title = "Выберите сканы документа";
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == true)
            {
                var fileInfo = new System.IO.FileInfo(openFileDialog.FileName);
                if (fileInfo.Length > 5 * 1024 * 1024)
                {
                    MessageBox.Show("Размер файла не должен превышать 5 МБ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                txtFilePath.Text = openFileDialog.FileName;
            }
        }

        private bool ValidateData()
        {
            if (!rbClass9.IsChecked.Value && !rbClass11.IsChecked.Value &&
                !rbSPO.IsChecked.Value && !rbVPO.IsChecked.Value)
            {
                MessageBox.Show("Выберите базу образования", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!rbAttestat.IsChecked.Value && !rbDiplom.IsChecked.Value)
            {
                MessageBox.Show("Выберите тип документа об образовании", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDocumentNumber.Text))
            {
                MessageBox.Show("Введите серию и номер документа об образовании", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                txtDocumentNumber.Focus();
                return false;
            }

            string documentPattern = @"^[0-9\s\-]{5,20}$";
            if (!Regex.IsMatch(txtDocumentNumber.Text, documentPattern))
            {
                MessageBox.Show("Введите корректные серию и номер документа\nТолько цифры, пробелы и дефисы", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                txtDocumentNumber.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtAverageScore.Text))
            {
                MessageBox.Show("Введите средний балл", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                txtAverageScore.Focus();
                return false;
            }

            if (!double.TryParse(txtAverageScore.Text.Replace(".", ","), out double score))
            {
                MessageBox.Show("Средний балл должен быть числом", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                txtAverageScore.Focus();
                return false;
            }

            if (score < 0 || score > 5)
            {
                MessageBox.Show("Средний балл должен быть в диапазоне от 0 до 5", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                txtAverageScore.Focus();
                return false;
            }

            if (txtAverageScore.Text.Contains(".") || txtAverageScore.Text.Contains(","))
            {
                string[] parts = txtAverageScore.Text.Contains(".") ?
                    txtAverageScore.Text.Split('.') : txtAverageScore.Text.Split(',');
                if (parts.Length > 1 && parts[1].Length > 2)
                {
                    MessageBox.Show("Средний балл должен быть указан с точностью до сотых", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtAverageScore.Focus();
                    return false;
                }
            }

            return true;
        }
        private void txtAverageScore_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string newText = textBox.Text + e.Text;

            if (!char.IsDigit(e.Text[0]) && e.Text != "," && e.Text != ".")
            {
                e.Handled = true;
                return;
            }

            if ((e.Text == "," || e.Text == ".") && (textBox.Text.Contains(",") || textBox.Text.Contains(".")))
            {
                e.Handled = true;
                return;
            }

            if (newText.Contains(".") || newText.Contains(","))
            {
                string[] parts = newText.Contains(".") ? newText.Split('.') : newText.Split(',');
                if (parts.Length > 1 && parts[1].Length > 2)
                {
                    e.Handled = true;
                }
            }
        }

        private void txtDocumentNumber_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text[0]) && e.Text != " " && e.Text != "-")
            {
                e.Handled = true;
            }
        }

        private void txtAverageScore_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtAverageScore.Text.Contains("."))
            {
                txtAverageScore.Text = txtAverageScore.Text.Replace(".", ",");
            }
        }
    }
}