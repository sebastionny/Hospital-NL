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

namespace HospitalNL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window

    {
        string[,] usersHospital = new string[3, 2]
                {{"admin","admin" },{"prepose" , "prepose" },{"medecin", "medecin"} };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            switch (ValidationError(txtUser.Text , txtPass.Password.ToString()))
            {
                case 0:
                    windowsAdmin wadmin = new windowsAdmin();
                    wadmin.ShowDialog();
                    break;

                case 1:
                    windowPrepose wpresose = new windowPrepose();
                    wpresose.ShowDialog();
                    break;

                case 2:
                    windowMedicin wmedecin = new windowMedicin();
                    wmedecin.ShowDialog();
                    break;

                default:
                    MessageBox.Show(" Utilizateuer ou le mot de passe sont incorrect!", "ERREUR", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }


        }

        private int ValidationError(string user, string pass)
        {
            int res = -1;
            if (string.Equals(user, usersHospital[0, 0], StringComparison.OrdinalIgnoreCase) && string.Equals(pass, usersHospital[0, 1], StringComparison.OrdinalIgnoreCase)) 
                res = 0;
            if (string.Equals(user, usersHospital[1, 0], StringComparison.OrdinalIgnoreCase) && string.Equals(pass, usersHospital[1, 1], StringComparison.OrdinalIgnoreCase)) 
                res = 1;
            if (string.Equals(user, usersHospital[2, 0], StringComparison.OrdinalIgnoreCase) && string.Equals(pass, usersHospital[2, 1], StringComparison.OrdinalIgnoreCase)) 
                res = 2;

            return res;
        }

        private void home_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
