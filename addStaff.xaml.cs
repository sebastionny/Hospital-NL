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
    /// Interaction logic for addStaff.xaml
    /// </summary>
    public partial class addStaff : Window
    {
        public addStaff()
        {
            InitializeComponent();
        }

        private void btnAddStaff_Click(object sender, RoutedEventArgs e)
        {
            Medecin M = new Medecin();

            M.Nom = txtNom.Text;
            M.Prenom = txtPrenom.Text;
            M.Specialite = txtSpecialite.Text;

            try
            {
                if (valida()) { 
                    MainWindow.bdHospital.Medecins.Add(M);
                    MainWindow.bdHospital.SaveChanges();
                    MessageBox.Show("Medecin ajouté", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                    txtNom.Text = String.Empty;
                    txtPrenom.Text = String.Empty;
                    txtSpecialite.Text = String.Empty;
                }else
                    MessageBox.Show("Tous les champs sont requis", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private bool valida()
        {
            bool ok = true;
            if (txtNom.Text == "" || txtPrenom.Text == "" || txtSpecialite.Text == "")
                ok = false;
            return ok;
        }
    }
}
