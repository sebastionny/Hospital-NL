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
    /// Interaction logic for windowPrepose.xaml
    /// </summary>
    public partial class windowPrepose : Window
    {
        public windowPrepose()
        {
            InitializeComponent();
           
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnUpdate.IsEnabled = false;
            cbNSSDossier.ItemsSource = MainWindow.bdHospital.Patient.ToList();
            cbIDMedecinDossier.ItemsSource = MainWindow.bdHospital.Medecin.ToList();
        }

        private void btnRecheche_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Patient pa = MainWindow.bdHospital.Patient.Where(p => p.Telephone == txtRecherchePatient.Text).FirstOrDefault();

                if (pa == null)
                    throw new Exception("Patient non trouve sur la base de donnee");
                MessageBox.Show("Voila! On a trouve le patient.","Recherche avec succes", MessageBoxButton.OK);
                btnUpdate.IsEnabled = true;
                btnAjouterPatiet.IsEnabled = false;
                dateNaissancePatient.SelectedDate = pa.DateNaissance;
                txtNomPatient.Text = pa.Nom;
                txtPrenomPatient.Text = pa.Prenom;
                txtAdressePatient.Text = pa.Adresse;
                txtProvincePatient.Text = pa.Province;
                txtVillePatient.Text = pa.Ville;
                txtCodePostalPatient.Text = pa.CodePostal;
                txtTelePatient.Text = pa.Telephone;
                cbAssurancePatient.ItemsSource = MainWindow.bdHospital.CompagnieAssurance.ToList();
                cbRefPatient.ItemsSource = MainWindow.bdHospital.Parent.ToList();
                cbAssurancePatient.SelectedItem = pa.CompagnieAssurance;
                cbRefPatient.SelectedItem = pa.RefParent;
   
                Parent parent = MainWindow.bdHospital.Parent.Where(obj => obj.RefParent == (int)pa.RefParent).FirstOrDefault();
                if (parent == null)
                    throw new Exception("Le parent du patient n'exite pas sur la base de donnee");
                txtNomParent.Text = parent.Nom;
                txtPrenomParent.Text = parent.Prenom;
                txtAdresseParent.Text = parent.Adresse;
                txtTeleParent.Text = parent.Telephone;
                txtProvinceParent.Text = parent.Province;
                txtVilleParent.Text = parent.Ville;
                txtCodeParent.Text = parent.CodePostal;
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Oups, on a de problemes!", MessageBoxButton.OK, MessageBoxImage.Information);
                btnUpdate.IsEnabled = false;
                btnAjouterPatiet.IsEnabled = true;
                clearAll();
            }
            

        }

        private void clearAll()
        {
            foreach (Control ctl in containerGroupe.Children)
            {
                if (ctl.GetType() == typeof(CheckBox))
                    ((CheckBox)ctl).IsChecked = false;
                if (ctl.GetType() == typeof(TextBox))
                    ((TextBox)ctl).Text = String.Empty;
                if (ctl.GetType() == typeof(DatePicker))
                    ((DatePicker)ctl).SelectedDate = null;
                if (ctl.GetType() == typeof(ComboBox))
                    ((ComboBox)ctl).SelectedIndex = -1;

            }
        }

        private void btnEmptyBox_Click(object sender, RoutedEventArgs e)
        {
            clearAll();
            btnUpdate.IsEnabled = false;
        }

        
    }
}
