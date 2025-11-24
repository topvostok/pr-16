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
    /// Логика взаимодействия для Passport.xaml
    /// </summary>
    public partial class Passport : Page
    {
        public Passport()
        {
            InitializeComponent();
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateData())
            {
                NavigationService.Navigate(new Parents());
            }
        }

        private void SelectFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Документы (*.png;*.jpg;*.jpeg;*.pdf)|*.png;*.jpg;*.jpeg;*.pdf|Все файлы (*.*)|*.*";
            openFileDialog.Title = "Выберите сканы страниц паспорта";
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filename in openFileDialog.FileNames)
                {
                    var fileInfo = new System.IO.FileInfo(filename);
                    if (fileInfo.Length > 5 * 1024 * 1024)
                    {
                        MessageBox.Show($"Файл {System.IO.Path.GetFileName(filename)} превышает размер 5 МБ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                txtPassportScan.Text = string.Join("; ", openFileDialog.FileNames);
            }
        }

        private bool ValidateData()
        {
            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Введите фамилию", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                txtLastName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Введите имя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                txtFirstName.Focus();
                return false;
            }

            string namePattern = @"^[а-яА-ЯёЁa-zA-Z\-\s]+$";
            if (!Regex.IsMatch(txtLastName.Text, namePattern))
            {
                MessageBox.Show("Фамилия должна содержать только буквы, дефисы и пробелы", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                txtLastName.Focus();
                return false;
            }

            if (!Regex.IsMatch(txtFirstName.Text, namePattern))
            {
                MessageBox.Show("Имя должно содержать только буквы, дефисы и пробелы", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                txtFirstName.Focus();
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtMiddleName.Text) && !Regex.IsMatch(txtMiddleName.Text, namePattern))
            {
                MessageBox.Show("Отчество должно содержать только буквы, дефисы и пробелы", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                txtMiddleName.Focus();
                return false;
            }

            if (dpBirthDate.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату рождения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                dpBirthDate.Focus();
                return false;
            }

            if (dpBirthDate.SelectedDate > DateTime.Now)
            {
                MessageBox.Show("Дата рождения не может быть в будущем", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                dpBirthDate.Focus();
                return false;
            }

            if (dpBirthDate.SelectedDate < DateTime.Now.AddYears(-100))
            {
                MessageBox.Show("Проверьте корректность даты рождения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                dpBirthDate.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCitizenship.Text))
            {
                MessageBox.Show("Введите гражданство", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                txtCitizenship.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtBirthPlace.Text))
            {
                MessageBox.Show("Введите место рождения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                txtBirthPlace.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPassportNumber.Text))
            {
                MessageBox.Show("Введите серию и номер паспорта", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                txtPassportNumber.Focus();
                return false;
            }

            string passportPattern = @"^\d{4}\s?\d{6}$";
            if (!Regex.IsMatch(txtPassportNumber.Text.Replace(" ", ""), passportPattern))
            {
                MessageBox.Show("Введите корректные серию и номер паспорта\nФормат: 1234 567890", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                txtPassportNumber.Focus();
                return false;
            }

            if (dpIssueDate.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату выдачи паспорта", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                dpIssueDate.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDepartmentCode.Text))
            {
                MessageBox.Show("Введите код подразделения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                txtDepartmentCode.Focus();
                return false;
            }

            string departmentCodePattern = @"^\d{3}-\d{3}$";
            if (!Regex.IsMatch(txtDepartmentCode.Text, departmentCodePattern))
            {
                MessageBox.Show("Введите корректный код подразделения\nФормат: 123-456", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                txtDepartmentCode.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtIssuedBy.Text))
            {
                MessageBox.Show("Введите кем выдан паспорт", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                txtIssuedBy.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtRegistrationAddress.Text))
            {
                MessageBox.Show("Введите адрес по прописке", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                txtRegistrationAddress.Focus();
                return false;
            }

            return true;
        }
        private void txtPassportNumber_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text[0]))
            {
                e.Handled = true;
            }
        }

        private void txtDepartmentCode_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (char.IsDigit(e.Text[0]) || (e.Text == "-" && !textBox.Text.Contains("-")))
            {
                if (textBox.Text.Length >= 7) e.Handled = true;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtName_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!char.IsLetter(e.Text[0]) && e.Text != "-" && e.Text != " ")
            {
                e.Handled = true;
            }
        }
        private void txtLastName_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            txtName_PreviewTextInput(sender, e);
        }

        private void txtFirstName_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            txtName_PreviewTextInput(sender, e);
        }

        private void txtMiddleName_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            txtName_PreviewTextInput(sender, e);
        }

        private void txtCitizenship_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            txtName_PreviewTextInput(sender, e);
        }

        private void txtBirthPlace_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!char.IsLetter(e.Text[0]) && !char.IsDigit(e.Text[0]) &&
                e.Text != "-" && e.Text != " " && e.Text != "," && e.Text != ".")
            {
                e.Handled = true;
            }
        }

        private void txtIssuedBy_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!char.IsLetter(e.Text[0]) && !char.IsDigit(e.Text[0]) &&
                e.Text != "-" && e.Text != " " && e.Text != "." && e.Text != ",")
            {
                e.Handled = true;
            }
        }
    }
}
