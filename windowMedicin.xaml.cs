using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for windowMedicin.xaml
    /// </summary>
    public partial class windowMedicin : Window
    {
        public windowMedicin()
        {
            InitializeComponent();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void refresh()
        {
            cbIDDossier.DataContext = MainWindow.bdHospital.DossierAdmissions.ToList();
            dgDossier.ItemsSource = MainWindow.bdHospital.DossierAdmissions.ToList();
        }

        private void btnDonnerConge_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DossierAdmission DA = ((DossierAdmission)cbIDDossier.SelectedItem);
                if(DA.DateAdmission > dkDateConge.SelectedDate)
                    throw new Exception ("La date de conge ne peut pas etre inferior de la date d'admission.");
                DA.DateConge = dkDateConge.SelectedDate;
                DA.ChirurgieProg = true;

                Lit LI = MainWindow.bdHospital.Lits.Where(l => l.NumeroLit == DA.NumeroLit).FirstOrDefault();
                LI.Occupe = false;

                MainWindow.bdHospital.SaveChanges();
                MessageBox.Show("Modification Fait! La date de congé a été fixé", "La date de congé avec Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                
                refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message , "Erreur Asignation date conge" ,MessageBoxButton.OK , MessageBoxImage.Information);
            }
        }

        private void cbIDDossier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DossierAdmission DA = ((DossierAdmission)cbIDDossier.SelectedItem);
            labDateAdminission.Content = DA.DateAdmission.Value.ToString("yyyy-MM-dd");
            labDateChirurgie.Content = string.IsNullOrEmpty(DA.DateChirurgie.ToString()) ? "Pas Date" : DA.DateChirurgie.Value.ToString("yyyy-MM-dd");
            labNSS.Content = DA.NSS;
            labLit.Content = DA.NumeroLit;
            DateTime dConge = string.IsNullOrEmpty(DA.DateConge.ToString()) ? DateTime.Today : DateTime.Parse(DA.DateConge.ToString());
            dkDateConge.SelectedDate = dConge;
        }

        private void windowMedecin_Loaded(object sender, RoutedEventArgs e)
        {
            refresh();
        }

       
    }
}
