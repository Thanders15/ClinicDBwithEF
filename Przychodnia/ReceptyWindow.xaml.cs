using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
    /// Logika interakcji dla klasy ReceptyWindow.xaml
    /// </summary>
    public partial class ReceptyWindow : Window
    {
        public ReceptyWindow()
        {
            InitializeComponent();
            LoadPrescriptions();
        }
        private void AddingPrescription(object sender, RoutedEventArgs e)
        {
            PrzychodniaDBEntities db = new PrzychodniaDBEntities();
            {
                try
                {
                    Recepty newPrescription = new Recepty()
                    {
                        Pacjent_ID = int.Parse(txtPacjentId.Text),
                        NazwaRecepty = txtReceptaNazwa.Text,
                        NumerRecepty = int.Parse(txtNumerRecepty.Text),
                    };
                    if(int.Parse(txtNumerRecepty.Text) < 0 || txtReceptaNazwa.Text == "")
                    {
                        MessageBox.Show("Numer lub nazwa recepty jest niepoprawna");
                    }
                    else
                    {
                        db.Recepty.Add(newPrescription);
                        db.SaveChanges();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Id pacjenta nie istnieje w bazie danych lub podałeś nieprawidłową wartość");
                }
                LoadPrescriptions();
            }
        }
        private void LoadPrescriptions()
        {
            PrzychodniaDBEntities db = new PrzychodniaDBEntities();
            this.receptyDataGrid.ItemsSource = db.Recepty.ToList();
        }
        private void RefreshPrescriptions(object sender, RoutedEventArgs e)
        {
            PrzychodniaDBEntities db = new PrzychodniaDBEntities();
            var data = from r in db.Recepty select r;
            this.receptyDataGrid.ItemsSource = data.ToList();
        }
        private void DeletePrescription(object sender, RoutedEventArgs e)
        {
            PrzychodniaDBEntities db = new PrzychodniaDBEntities();
            try
            {
                int Id = (receptyDataGrid.SelectedItem as Recepty).Id;
                var deletePrescription = db.Recepty.Where(m => m.Id == Id).Single();
                db.Recepty.Remove(deletePrescription);
            }
            catch (System.NullReferenceException)
            {
                MessageBox.Show("Musisz wybrać recepte");
            }
            db.SaveChanges();
            receptyDataGrid.ItemsSource = db.Recepty.ToList();
            LoadPrescriptions();
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
            System.Windows.Data.CollectionViewSource pacjenciViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("receptyViewSource")));
            pacjenciViewSource.View.MoveCurrentToFirst();
        }
        private void UpdatePrescription(object sender, RoutedEventArgs e)
        {
            PrzychodniaDBEntities db = new PrzychodniaDBEntities();
            try
            {
                int Id = (receptyDataGrid.SelectedItem as Recepty).Id;
                var swapPrescription = db.Recepty.Where(m => m.Id == Id).Single();
                swapPrescription.Pacjent_ID = int.Parse(txtPacjentId.Text);
                swapPrescription.NumerRecepty = int.Parse(txtNumerRecepty.Text);
                swapPrescription.NazwaRecepty = txtReceptaNazwa.Text;
                db.SaveChanges();
            }
            catch (Exception)
            {
                MessageBox.Show("Musisz wybrać receptę lub podałeś nieprawidłowe wartości");
            }
            LoadPrescriptions();
        }
    }
}