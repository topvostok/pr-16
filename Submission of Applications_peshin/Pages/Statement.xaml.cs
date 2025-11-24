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

namespace Submission_of_Applications_peshin.Pages
{
    /// <summary>
    /// Логика взаимодействия для Statement.xaml
    /// </summary>
    public partial class Statement : Page
    {
        public Statement()
        {
            InitializeComponent();
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateData())
            {
                NavigationService.Navigate(new Education());
            }
        }

        private bool ValidateData()
        {
            if (!cbFullTime.IsChecked.Value && !cbPartTime.IsChecked.Value)
            {
                MessageBox.Show("Выберите форму обучения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!cbBudget.IsChecked.Value && !cbPaid.IsChecked.Value)
            {
                MessageBox.Show("Выберите тип финансирования", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtInstitution.Text))
            {
                MessageBox.Show("Заполните наименование образовательной организации", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                txtInstitution.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtGraduationYear.Text))
            {
                MessageBox.Show("Заполните год окончания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                txtGraduationYear.Focus();
                return false;
            }

            if (!int.TryParse(txtGraduationYear.Text, out int year))
            {
                MessageBox.Show("Год окончания должен быть числом", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                txtGraduationYear.Focus();
                return false;
            }

            int currentYear = DateTime.Now.Year;
            if (year < 1950 || year > currentYear)
            {
                MessageBox.Show($"Год окончания должен быть в диапазоне от 1950 до {currentYear}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                txtGraduationYear.Focus();
                return false;
            }

            return true;
        }
        private void txtGraduationYear_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text[0]))
            {
                e.Handled = true;
            }
            else
            {
                TextBox textBox = sender as TextBox;
                string newText = textBox.Text + e.Text;
                if (newText.Length > 4)
                {
                    e.Handled = true;
                }
            }
        }
    }
}