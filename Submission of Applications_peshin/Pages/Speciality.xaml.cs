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
    /// Логика взаимодействия для Speciality.xaml
    /// </summary>
    public partial class Speciality : Page
    {
        public Speciality()
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
            var checkBoxes = new[] { cbSpeciality1, cbSpeciality2, cbSpeciality3, cbSpeciality4, cbSpeciality5,
                                   cbSpeciality6, cbSpeciality7, cbSpeciality8, cbSpeciality9, cbSpeciality10,
                                   cbSpeciality11, cbSpeciality12, cbSpeciality13 };

            bool anySpecialitySelected = checkBoxes.Any(cb => cb.IsChecked == true);

            if (!anySpecialitySelected)
            {
                MessageBox.Show("Выберите хотя бы одну специальность", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!rbFirstTime.IsChecked.Value && !rbNotFirstTime.IsChecked.Value)
            {
                MessageBox.Show("Выберите, поступаете ли вы впервые", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (!cbAgreement1.IsChecked.Value)
            {
                MessageBox.Show("Необходимо ознакомиться и подтвердить согласие с документами образовательного учреждения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                cbAgreement1.Focus();
                return false;
            }

            if (!cbAgreement2.IsChecked.Value)
            {
                MessageBox.Show("Необходимо подтвердить ознакомление с датой представления оригиналов документов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                cbAgreement2.Focus();
                return false;
            }

            return true;
        }
    }
}

