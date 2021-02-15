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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Przychodnia
{
    /// <summary>
    /// Logika interakcji dla klasy LekarzeWindow.xaml
    /// </summary>
    public partial class LekarzeWindow : Window
    {
        public LekarzeWindow()
        {
            InitializeComponent();
            LoadDoctors();
            PrzychodniaDBEntities db = new PrzychodniaDBEntities();
            var docs = from d in db.Lekarze
                       select new
                       {
                           doctorName = d.Imie,
                           doctorSurname = d.Nazwisko,
                           doctorTitle = d.StopienNaukowy,
                           doctorSpecjalization = d.Specjalizacja,
                       };
            foreach (var item in docs)
            {
                Console.WriteLine(item.doctorName);
                Console.WriteLine(item.doctorSurname);
                Console.WriteLine(item.doctorTitle);
                Console.WriteLine(item.doctorSpecjalization);
            }
            this.gridDoctors.ItemsSource = docs.ToList();
        }

       
        private void AddingDoctor(object sender, RoutedEventArgs e)
        {
            PrzychodniaDBEntities db = new PrzychodniaDBEntities();
            {
                Lekarze newDoktor = new Lekarze()
                {
                    Imie = txtName.Text,
                    Nazwisko = txtSurname.Text,
                    StopienNaukowy = txtTitle.Text,
                    Specjalizacja = txtSpecialization.Text,
                };
                db.Lekarze.AddOrUpdate(newDoktor);
                db.SaveChanges();
            }
        }

        private void LoadDoctors()
        {
            PrzychodniaDBEntities db = new PrzychodniaDBEntities();
            this.gridDoctors.ItemsSource = db.Lekarze.ToList();
        }
        private void RefreshDoctors(object sender, RoutedEventArgs e)
        {
            PrzychodniaDBEntities db = new PrzychodniaDBEntities();
            var data = from r in db.Lekarze select r;
            this.gridDoctors.ItemsSource = data.ToList();
        }
        private int updatingDoctorID = 0;
        private void ShowDoctors(object sender, SelectionChangedEventArgs e)
        {
            if (this.gridDoctors.SelectedIndex >= 0)
            {
                if (this.gridDoctors.SelectedItems.Count >= 0)
                {
                    if (this.gridDoctors.SelectedItems[0].GetType() == typeof(Lekarze))
                    {
                        Lekarze d = (Lekarze)this.gridDoctors.SelectedItems[0];
                        this.txtName.Text = d.Imie;
                        this.txtSurname.Text = d.Nazwisko;
                        this.txtTitle.Text = d.StopienNaukowy;
                        this.txtSpecialization.Text = d.Specjalizacja;

                        this.updatingDoctorID = d.Id;
                    }
                }
            }
        }
        private void DeleteDoctor(object sender, RoutedEventArgs e)
        {
            PrzychodniaDBEntities db = new PrzychodniaDBEntities();

            int Id = (gridDoctors.SelectedItem as Lekarze).Id;
            var delete = db.Lekarze.Where(m => m.Id == Id).Single();
            db.Lekarze.Remove(delete);
            db.SaveChanges();
            gridDoctors.ItemsSource = db.Lekarze.ToList();
        }
   
        private void backToMainMenu(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }
        
    }
}
