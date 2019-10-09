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
            cbIdMedecin.ItemsSource = MainWindow.bdHospital.Medecins.ToList();
            cbDepartement.ItemsSource = MainWindow.bdHospital.Departements.ToList();
            cbNSSPatient.ItemsSource = MainWindow.bdHospital.Patients.ToList();
            cbLocation.ItemsSource = MainWindow.bdHospital.Locations.ToList();
            cbAssurancePatient.ItemsSource = MainWindow.bdHospital.CompagnieAssurances.ToList();
            cbDepartement.SelectedIndex = -1;
            cbAssurancePatient.SelectedIndex = -1;
            dateAdmissionDossier.SelectedDate = DateTime.Today;
        }

        private void btnRecheche_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Patient pa = MainWindow.bdHospital.Patients.Where(p => p.Telephone == txtRecherchePatient.Text).FirstOrDefault();

                if (pa == null)
                    throw new Exception("Patient non trouve sur la base de donnee");
                MessageBox.Show("Voila! On a trouve le patient.", "Recherche avec succes", MessageBoxButton.OK);
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
                cbAssurancePatient.ItemsSource = MainWindow.bdHospital.CompagnieAssurances.ToList();
                cbAssurancePatient.SelectedItem = pa;
                cbRefPatient.ItemsSource = MainWindow.bdHospital.Parents.ToList();
                cbRefPatient.SelectedItem = pa;


                cbNSSPatient.SelectedItem = pa;


                Parent parent = MainWindow.bdHospital.Parents.Where(obj => obj.RefParent == (int)pa.RefParent).FirstOrDefault();
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
                    ((ComboBox)ctl).ItemsSource = null;

            }

            btnUpdate.IsEnabled = false;
            cbIdMedecin.ItemsSource = MainWindow.bdHospital.Medecins.ToList();
            cbDepartement.ItemsSource = MainWindow.bdHospital.Departements.ToList();
            cbNSSPatient.ItemsSource = MainWindow.bdHospital.Patients.ToList();
            cbLocation.ItemsSource = MainWindow.bdHospital.Locations.ToList();
            cbAssurancePatient.ItemsSource = MainWindow.bdHospital.CompagnieAssurances.ToList();
            cbDepartement.SelectedIndex = -1;
            cbAssurancePatient.SelectedIndex = -1;
            dateAdmissionDossier.SelectedDate = DateTime.Today;
        }

        private void btnEmptyBox_Click(object sender, RoutedEventArgs e)
        {
            clearAll();
            btnUpdate.IsEnabled = false;
            btnAjouterPatiet.IsEnabled = true;
        }

        private void cbTypeLit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TypeLit tli = cbTypeLit.SelectedItem as TypeLit;
            Departement dep = cbDepartement.SelectedItem as Departement;
            try {
                cbNumeroLit.ItemsSource = MainWindow.bdHospital.Lits.Where(lit => lit.NumeroType == tli.NumeroType & lit.IdDepartement == dep.IdDepartement & lit.Occupe == false).ToList();
            }
            catch (Exception) {
                cbNumeroLit.ItemsSource = null;
            }
        }

        private List<Lit> goodLit()
        {
            TypeLit tli = cbTypeLit.SelectedItem as TypeLit;
            Departement dep = cbDepartement.SelectedItem as Departement;
            List<Lit> listLit = MainWindow.bdHospital.Lits.Where(lit => lit.NumeroType == tli.NumeroType & lit.IdDepartement == dep.IdDepartement).ToList();
            return listLit;
        }


        private void cbDepartement_DropDownClosed(object sender, EventArgs e)
        {
            Patient p = cbNSSPatient.SelectedItem as Patient;
            if (p.DateNaissance.Value.AddYears(16) > DateTime.Today)
                cbDepartement.SelectedIndex = 1;

            Departement dep = cbDepartement.SelectedItem as Departement;
            cbTypeLit.ItemsSource = MainWindow.bdHospital.TypeLits.ToList();

        }

        private void cbDepartement_DropDownOpened(object sender, EventArgs e)
        {
            cbTypeLit.ItemsSource = null;
            cbNumeroLit.ItemsSource = null;
        }

        private void btnAjouterPatiet_Click(object sender, RoutedEventArgs e)
        {
            CompagnieAssurance assu = cbAssurancePatient.SelectedItem as CompagnieAssurance;

            Parent par = new Parent {
                Nom = txtNomParent.Text,
                Prenom = txtPrenomParent.Text,
                Adresse = txtAdresseParent.Text,
                Province = txtProvinceParent.Text,
                CodePostal = txtCodeParent.Text,
                Ville = txtVilleParent.Text,
                Telephone = txtTeleParent.Text
            };

            try
            {
                if (validaParent())
                {
                    MainWindow.bdHospital.Parents.Add(par);
                    MainWindow.bdHospital.SaveChanges();
                    MessageBox.Show("OK Parent ajouté", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    throw new Exception("SECTION PARENT : Les champs Nom et Telephone sont requis.");
                //MessageBox.Show("Les champs Nom et Telephone sont requis", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }


            Parent Par = MainWindow.bdHospital.Parents.ToList().Last();

            try
            {
                if (validaPatient())
                {
                    Patient p = new Patient
                    {
                        DateNaissance = dateNaissancePatient.SelectedDate,
                        Nom = txtNomPatient.Text,
                        Prenom = txtPrenomPatient.Text,
                        Adresse = txtAdressePatient.Text,
                        Province = txtProvincePatient.Text,
                        Ville = txtVillePatient.Text,
                        CodePostal = txtCodePostalPatient.Text,
                        Telephone = txtTelePatient.Text,
                        IdCompagnie = assu.IdCompagnie,
                        RefParent = Par.RefParent,
                    };

                    MainWindow.bdHospital.Patients.Add(p);
                    MainWindow.bdHospital.SaveChanges();
                    MessageBox.Show("OK Patient ajouté", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                    clearAll();
                }
                else
                    throw new Exception("SECTION PATIENT: Les champs Nom, Prenom, Telephone, Date naissance et Assurance sont requis");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private bool validaParent()
        {
            bool ok = true;
            if (txtNomParent.Text == "" || txtTeleParent.Text == "")
                ok = false;
            return ok;
        }

        private bool validaPatient()
        {
            bool ok = true;
            if (txtNomPatient.Text == "" || txtPrenomPatient.Text == "" || txtTelePatient.Text == "" ||
                dateNaissancePatient.SelectedDate == null || cbAssurancePatient.SelectedItem == null)
                ok = false;
            return ok;
        }

        private void txtTelePatient_KeyDown(object sender, KeyEventArgs e)
        {
            justeNumbers(e);
        }

        public static void justeNumbers(KeyEventArgs e)
        {
            if (char.IsNumber((char)e.Key))
            {
                MessageBox.Show("Il faut que soit juste de numeros", "Information", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtTeleParent_KeyDown(object sender, KeyEventArgs e)
        {
            justeNumbers(e);
        }
    }
}
