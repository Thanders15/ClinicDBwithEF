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
    /// Logika interakcji dla klasy WizytyWindow.xaml
    /// </summary>
    public partial class WizytyWindow : Window
    {
        public WizytyWindow()
        {
            InitializeComponent();
            LoadVisits();
        }
        private void AddingVisit(object sender, RoutedEventArgs e)
        {
            PrzychodniaDBEntities db = new PrzychodniaDBEntities();
            {
                try
                {
                    Wizyty newVisit = new Wizyty()
                    {
                    Lekarz_ID = int.Parse(txtLekarzID.Text),
                    Pacjent_ID = int.Parse(txtPacjentId.Text),
                    DataWizyty = DateTime.Parse(txtData.Text),
                    NumerPokoju = int.Parse(txtNumerPokoju.Text),
                    };
                    if (int.Parse(txtNumerPokoju.Text) < 0)
                    {
                        MessageBox.Show("Wartość pokoju jest niepoprawna");
                    }
                    else
                    {
                        db.Wizyty.Add(newVisit);
                        db.SaveChanges();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Id pacjenta albo lekarza nie istnieje w bazie danych lub podałeś nieprawidłową wartość");
                }
                LoadVisits();
            }
        }
        private void LoadVisits()
        {
            PrzychodniaDBEntities db = new PrzychodniaDBEntities();
            this.wizytyDataGrid.ItemsSource = db.Wizyty.ToList();
        }
        private void RefreshVisits(object sender, RoutedEventArgs e)
        {
            PrzychodniaDBEntities db = new PrzychodniaDBEntities();
            var data = from r in db.Wizyty select r;
            this.wizytyDataGrid.ItemsSource = data.ToList();
        }
        private void DeleteVisit(object sender, RoutedEventArgs e)
        {
            PrzychodniaDBEntities db = new PrzychodniaDBEntities();
            try
            {
                int Id = (wizytyDataGrid.SelectedItem as Wizyty).Id;
                var deleteVisit = db.Wizyty.Where(m => m.Id == Id).Single();
                db.Wizyty.Remove(deleteVisit);
            }
            catch (System.NullReferenceException)
            {
                MessageBox.Show("Musisz wybrać wizyte");
            }
            db.SaveChanges();
            wizytyDataGrid.ItemsSource = db.Wizyty.ToList();
            LoadVisits();
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
            System.Windows.Data.CollectionViewSource pacjenciViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("wizytyViewSource")));
            pacjenciViewSource.View.MoveCurrentToFirst();
        }
        private void UpdateVisit(object sender, RoutedEventArgs e)
        {
            PrzychodniaDBEntities db = new PrzychodniaDBEntities();
            try
            {
                int Id = (wizytyDataGrid.SelectedItem as Wizyty).Id;
                var swapVisit = db.Wizyty.Where(m => m.Id == Id).Single();
                swapVisit.Lekarz_ID = int.Parse(txtLekarzID.Text);
                swapVisit.Pacjent_ID = int.Parse(txtPacjentId.Text);
                swapVisit.DataWizyty = DateTime.Parse(txtData.Text);
                swapVisit.NumerPokoju = int.Parse(txtNumerPokoju.Text);
                db.SaveChanges();
            }
            catch (Exception)
            {
                MessageBox.Show("Musisz wybrać wizytę lub podałeś nieprawidłowe wartości");
            }
            LoadVisits();
        }
    }
}
