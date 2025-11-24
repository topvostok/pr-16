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
using Microsoft.Win32;

namespace Submission_of_Applications_peshin.Pages
{
    /// <summary>
    /// Логика взаимодействия для Status.xaml
    /// </summary>
    public partial class Status : Page
    {
        public Status()
        {
            InitializeComponent();
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateData())
            {
                NavigationService.Navigate(new Speciality());
            }
        }

        private void SelectFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Документы (*.png;*.jpg;*.jpeg;*.pdf)|*.png;*.jpg;*.jpeg;*.pdf|Все файлы (*.*)|*.*";
            openFileDialog.Title = "Выберите сканы военного билета или приписного удостоверения";
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == true)
            {
                var fileInfo = new System.IO.FileInfo(openFileDialog.FileName);
                if (fileInfo.Length > 5 * 1024 * 1024)
                {
                    MessageBox.Show("Размер файла не должен превышать 5 МБ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                txtMilitaryScan.Text = openFileDialog.FileName;
            }
        }

        private bool ValidateData()
        {
            if (rbHasMilitary.IsChecked == true)
            {
                if (string.IsNullOrWhiteSpace(txtMilitaryScan.Text))
                {
                    MessageBox.Show("При наличии военного билета необходимо прикрепить сканы", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }

            return true;
        }
    }
}
