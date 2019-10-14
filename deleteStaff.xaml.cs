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
using System.Windows.Shapes;

namespace HospitalNL
{
    /// <summary>
    /// Interaction logic for deleteStaff.xaml
    /// </summary>
    public partial class deleteStaff : Window
    {
        public deleteStaff()
        {
            InitializeComponent();
        }

        private void cbIdMedecin_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Medecin M = ((Medecin)cbIdMedecin.SelectedItem);
            labInfoNom.Content = M.Nom.ToString();
            labInfoPrenom.Content = M.Prenom.ToString();
            labInfoSpecialite.Content = M.Specialite.ToString();
        }

        private void refresh()
        {
            cbIdMedecin.DataContext = MainWindow.bdHospital.Medecins.ToList();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            refresh();
        }

        private void btnDeleteStaff_Click(object sender, RoutedEventArgs e)
        {
            Medecin M = ((Medecin)cbIdMedecin.SelectedItem);
            
            try
            {

                MessageBoxResult res = MessageBox.Show("Vous etes sur de supprimer le medecin ?", "Attention", MessageBoxButton.YesNo, MessageBoxImage.Question); 

                if (res == MessageBoxResult.Yes)
                {
                    MainWindow.bdHospital.Medecins.Remove(M);
                    MessageBox.Show("Modification Fait!", "Ajoute Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                    MainWindow.bdHospital.SaveChanges();
                    refresh();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
