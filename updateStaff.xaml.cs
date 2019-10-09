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
    /// Interaction logic for updateStaff.xaml
    /// </summary>
    public partial class updateStaff : Window
    {
        public updateStaff()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            refresh();
        }
        

        private void cbIdMedecin_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Medecin M = ((Medecin)cbIdMedecin.SelectedItem);
            txtNom.Text = M.Nom.ToString();
            txtPrenom.Text = M.Prenom.ToString();
            txtSpecialite.Text = M.Specialite.ToString();
        }

        private void refresh()
        {
            cbIdMedecin.DataContext = MainWindow.bdHospital.Medecins.ToList();
        }

        private void btnAddStaff_Click(object sender, RoutedEventArgs e)
        {
            Medecin M = cbIdMedecin.SelectedItem as Medecin;
            M.Nom = txtNom.Text;
            M.Prenom = txtPrenom.Text;
            M.Specialite = txtSpecialite.Text;

            try
            {
                MainWindow.bdHospital.SaveChanges();
                MessageBox.Show("Modification Fait!", "Ajoute Succes", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
