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
    /// Логика взаимодействия для Contacts.xaml
    /// </summary>
    public partial class Contacts : Page
    {
        public Contacts()
        {
            InitializeComponent();
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateData())
            {
                NavigationService.Navigate(new Passport());
            }
        }

        private bool ValidateData()
        {
            if (string.IsNullOrWhiteSpace(txtMobile.Text))
            {
                MessageBox.Show("Поле 'Мобильный номер' обязательно для заполнения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                txtMobile.Focus();
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtMobile.Text))
            {
                string mobilePattern = @"^[\+]?[7-8]?[0-9\s\-\(\)]{10,15}$";
                if (!Regex.IsMatch(txtMobile.Text.Replace(" ", ""), mobilePattern))
                {
                    MessageBox.Show("Введите корректный номер мобильного телефона\nПример: +7 912 345-67-89 или 89123456789", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtMobile.Focus();
                    return false;
                }
            }
            if (!string.IsNullOrWhiteSpace(txtHomePhone.Text))
            {
                string homePhonePattern = @"^[\+]?[0-9\s\-\(\)]{6,15}$";
                if (!Regex.IsMatch(txtHomePhone.Text.Replace(" ", ""), homePhonePattern))
                {
                    MessageBox.Show("Введите корректный номер домашнего телефона", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtHomePhone.Focus();
                    return false;
                }
            }
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Поле 'Электронный адрес' обязательно для заполнения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                txtEmail.Focus();
                return false;
            }

            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(txtEmail.Text, emailPattern))
            {
                MessageBox.Show("Введите корректный email адрес\nПример: example@mail.ru", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                txtEmail.Focus();
                return false;
            }

            return true;
        }
        private void txtMobile_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidatePhoneFormat(txtMobile);
        }

        private void txtHomePhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidatePhoneFormat(txtHomePhone);
        }

        private void ValidatePhoneFormat(TextBox textBox)
        {
            if (string.IsNullOrEmpty(textBox.Text)) return;

            string text = textBox.Text.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "");
            if (text.Length > 0 && !char.IsDigit(text[0]) && text[0] != '+')
            {
                textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
                textBox.CaretIndex = textBox.Text.Length;
            }
        }
    }
}
