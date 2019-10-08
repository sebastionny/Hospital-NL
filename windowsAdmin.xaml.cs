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
    /// Interaction logic for windowsAdmin.xaml
    /// </summary>
    public partial class windowsAdmin : Window
    {
        
        public windowsAdmin()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            refresh();
        }

        private void btnAddStaff_Click(object sender, RoutedEventArgs e)
        {
            addStaff addStaff = new addStaff();
            addStaff.ShowDialog();
        }

        private void btnUpdateStaff_Click(object sender, RoutedEventArgs e)
        {
            updateStaff updateStaff = new updateStaff();
            updateStaff.ShowDialog();
        }

        private void btnDeleteStaff_Click(object sender, RoutedEventArgs e)
        {
            deleteStaff deleteStaff = new deleteStaff();
            deleteStaff.ShowDialog();
        }

        public void refresh() => gridConsultation.ItemsSource = MainWindow.bdHospital.Medecin.ToList();

        private void Window_Activated(object sender, EventArgs e)
        {
            refresh();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
