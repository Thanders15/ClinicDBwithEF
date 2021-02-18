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

namespace Przychodnia
{
    /// <summary>
    /// Logika interakcji dla klasy PacjenciWindow.xaml
    /// </summary>
    public partial class PacjenciWindow : Window
    {
        public PacjenciWindow()
        {
            InitializeComponent();
            LoadPatients();
            PrzychodniaDBEntities db = new PrzychodniaDBEntities();
        }
        private void AddingPatient(object sender, RoutedEventArgs e)
        {
            PrzychodniaDBEntities db = new PrzychodniaDBEntities();
            {
                try
                {
                    Pacjenci newPatient = new Pacjenci()
                    {
                        Imie = txtName.Text,
                        Nazwisko = txtSurname.Text,
                        Pesel = Int64.Parse(txtPesel.Text),
                        Adres = txtAdres.Text,
                    };
                    if (txtName.Text == "" || txtSurname.Text == "" || Int64.Parse(txtPesel.Text) < 1010100000 || txtAdres.Text == "")
                    {
                        MessageBox.Show("Wartości nie mogą być puste lub są za małe");
                    }
                    else
                    {
                        db.Pacjenci.Add(newPatient);
                        db.SaveChanges();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Podałeś niepoprawne wartości");
                }                
                LoadPatients();
            }
        }
        private void LoadPatients()
        {
            PrzychodniaDBEntities db = new PrzychodniaDBEntities();
            this.pacjenciDataGrid.ItemsSource = db.Pacjenci.ToList();
        }
        private void RefreshPatients(object sender, RoutedEventArgs e)
        {
            PrzychodniaDBEntities db = new PrzychodniaDBEntities();
            var data = from r in db.Pacjenci select r;
            this.pacjenciDataGrid.ItemsSource = data.ToList();
        }
        private void DeletePatient(object sender, RoutedEventArgs e)
        {
            PrzychodniaDBEntities db = new PrzychodniaDBEntities();

            try {
                int Id = (pacjenciDataGrid.SelectedItem as Pacjenci).Id;
                var deletePatient = db.Pacjenci.Where(m => m.Id == Id).Single();
                db.Pacjenci.Remove(deletePatient);
            }catch(System.NullReferenceException)
            {
                MessageBox.Show("Musisz wybrać pacjenta");
            }
            try
            {
                db.SaveChanges();
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException)
            {
                MessageBox.Show("Nie możesz usunąć pacjenta, bo jego ID jest przypisane w innej tabeli");
            }
            pacjenciDataGrid.ItemsSource = db.Pacjenci.ToList();
            LoadPatients();
        }
        private void backToMainMenu(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Przychodnia.PrzychodniaDBDataSetVVV przychodniaDBDataSetVVV = ((Przychodnia.PrzychodniaDBDataSetVVV)(this.FindResource("przychodniaDBDataSetVVV")));
            // Załaduj dane do tabeli Pacjenci. Możesz modyfikować ten kod w razie potrzeby.
            Przychodnia.PrzychodniaDBDataSetVVVTableAdapters.PacjenciTableAdapter przychodniaDBDataSetVVVPacjenciTableAdapter = new Przychodnia.PrzychodniaDBDataSetVVVTableAdapters.PacjenciTableAdapter();
            przychodniaDBDataSetVVVPacjenciTableAdapter.Fill(przychodniaDBDataSetVVV.Pacjenci);
            System.Windows.Data.CollectionViewSource pacjenciViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("pacjenciViewSource")));
            pacjenciViewSource.View.MoveCurrentToFirst();
        }
        private void UpdatePatient(object sender, RoutedEventArgs e)
        {
            PrzychodniaDBEntities db = new PrzychodniaDBEntities();
            try
            {
                int Id = (pacjenciDataGrid.SelectedItem as Pacjenci).Id;
                var swapPatient = db.Pacjenci.Where(m => m.Id == Id).Single();
                swapPatient.Imie = txtName.Text;
                swapPatient.Nazwisko = txtSurname.Text;
                swapPatient.Pesel = int.Parse(txtPesel.Text);
                swapPatient.Adres = txtAdres.Text;
                db.SaveChanges();
            }
            catch (Exception){
                MessageBox.Show("Musisz wybrać pacjenta lub podałeś nieprawidłowe wartości");
            }
            LoadPatients();
        }
    }
}
