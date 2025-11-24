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

namespace Submission_of_Applications_peshin.Pages
{
    /// <summary>
    /// Логика взаимодействия для Parents.xaml
    /// </summary>
    public partial class Parents : Page
    {
        public Parents()
        {
            InitializeComponent();
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateData())
            {
                MessageBox.Show("Заявление успешно отправлено!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private bool ValidateData()
        {
            if (!ValidateParentData("Мать", txtMotherName, dpMotherBirth, txtMotherBirthPlace,
                txtMotherPosition, txtMotherAddress))
                return false;
            bool fatherDataFilled = !string.IsNullOrWhiteSpace(txtFatherName.Text) ||
                                   dpFatherBirth.SelectedDate != null ||
                                   !string.IsNullOrWhiteSpace(txtFatherBirthPlace.Text) ||
                                   !string.IsNullOrWhiteSpace(txtFatherPosition.Text) ||
                                   !string.IsNullOrWhiteSpace(txtFatherAddress.Text);

            if (fatherDataFilled)
            {
                if (!ValidateParentData("Отец", txtFatherName, dpFatherBirth, txtFatherBirthPlace,
                    txtFatherPosition, txtFatherAddress))
                    return false;
            }
            if (!ValidatePhoneNumbers("Мать", txtMotherHomePhone, txtMotherMobile))
                return false;

            if (fatherDataFilled && !ValidatePhoneNumbers("Отец", txtFatherHomePhone, txtFatherMobile))
                return false;

            return true;
        }

        private bool ValidateParentData(string parentType, TextBox name, DatePicker birthDate,
            TextBox birthPlace, TextBox position, TextBox address)
        {
            if (string.IsNullOrWhiteSpace(name.Text))
            {
                MessageBox.Show($"Введите ФИО {parentType.ToLower()}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                name.Focus();
                return false;
            }

            string namePattern = @"^[а-яА-ЯёЁa-zA-Z\-\s]+$";
            if (!Regex.IsMatch(name.Text, namePattern))
            {
                MessageBox.Show($"ФИО {parentType.ToLower()} должно содержать только буквы, дефисы и пробелы", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                name.Focus();
                return false;
            }
            if (birthDate.SelectedDate == null)
            {
                MessageBox.Show($"Выберите дату рождения {parentType.ToLower()}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                birthDate.Focus();
                return false;
            }

            if (birthDate.SelectedDate > DateTime.Now.AddYears(-14))
            {
                MessageBox.Show($"Проверьте корректность даты рождения {parentType.ToLower()}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                birthDate.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(birthPlace.Text))
            {
                MessageBox.Show($"Введите место рождения {parentType.ToLower()}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                birthPlace.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(position.Text))
            {
                MessageBox.Show($"Введите должность {parentType.ToLower()}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                position.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(address.Text))
            {
                MessageBox.Show($"Введите место жительства {parentType.ToLower()}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                address.Focus();
                return false;
            }

            return true;
        }

        private bool ValidatePhoneNumbers(string parentType, TextBox homePhone, TextBox mobile)
        {
            if (!string.IsNullOrWhiteSpace(homePhone.Text))
            {
                string phonePattern = @"^[\+]?[0-9\s\-\(\)]{6,15}$";
                if (!Regex.IsMatch(homePhone.Text.Replace(" ", ""), phonePattern))
                {
                    MessageBox.Show($"Введите корректный домашний телефон {parentType.ToLower()}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    homePhone.Focus();
                    return false;
                }
            }
            if (!string.IsNullOrWhiteSpace(mobile.Text))
            {
                string mobilePattern = @"^[\+]?[7-8]?[0-9\s\-\(\)]{10,15}$";
                if (!Regex.IsMatch(mobile.Text.Replace(" ", ""), mobilePattern))
                {
                    MessageBox.Show($"Введите корректный мобильный телефон {parentType.ToLower()}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    mobile.Focus();
                    return false;
                }
            }

            return true;
        }
        private void PhoneTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text[0]) && e.Text != "+" && e.Text != " " && e.Text != "-" && e.Text != "(" && e.Text != ")")
            {
                e.Handled = true;
            }
        }

        private void NameTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!char.IsLetter(e.Text[0]) && e.Text != "-" && e.Text != " ")
            {
                e.Handled = true;
            }
        }
        private void txtMotherName_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            NameTextBox_PreviewTextInput(sender, e);
        }

        private void txtFatherName_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            NameTextBox_PreviewTextInput(sender, e);
        }

        private void txtMotherHomePhone_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            PhoneTextBox_PreviewTextInput(sender, e);
        }

        private void txtMotherMobile_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            PhoneTextBox_PreviewTextInput(sender, e);
        }

        private void txtFatherHomePhone_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            PhoneTextBox_PreviewTextInput(sender, e);
        }

        private void txtFatherMobile_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            PhoneTextBox_PreviewTextInput(sender, e);
        }

    }
}
