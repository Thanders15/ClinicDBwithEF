﻿using System;
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
                    if(txtName.Text == "" || txtSurname.Text == "" || txtTitle.Text == "" || txtSpecialization.Text == "")
                    {
                        MessageBox.Show("Wartości nie mogą być puste");
                    }
                    else
                    {
                        db.Lekarze.Add(newDoktor);
                        db.SaveChanges();
                    }
                LoadDoctors();
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
        private void DeleteDoctor(object sender, RoutedEventArgs e)
        {
            PrzychodniaDBEntities db = new PrzychodniaDBEntities();

            try
            {
            int Id = (gridDoctors.SelectedItem as Lekarze).Id;
            var delete = db.Lekarze.Where(m => m.Id == Id).Single();
            db.Lekarze.Remove(delete);
            }
            catch(System.NullReferenceException)
            {
                MessageBox.Show("Musisz wybrać lekarza");
            }
            try
            {
            db.SaveChanges();
            }
            catch(System.Data.Entity.Infrastructure.DbUpdateException)
            {
                MessageBox.Show("Nie możesz usunąć lekarza, bo jego ID jest przypisane w innej tabeli");
            }

            gridDoctors.ItemsSource = db.Lekarze.ToList();
            LoadDoctors();
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
            // Załaduj dane do tabeli Lekarze. Możesz modyfikować ten kod w razie potrzeby.
            Przychodnia.PrzychodniaDBDataSetVVVTableAdapters.LekarzeTableAdapter przychodniaDBDataSetVVVLekarzeTableAdapter = new Przychodnia.PrzychodniaDBDataSetVVVTableAdapters.LekarzeTableAdapter();
            przychodniaDBDataSetVVVLekarzeTableAdapter.Fill(przychodniaDBDataSetVVV.Lekarze);
            System.Windows.Data.CollectionViewSource lekarzeViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("lekarzeViewSource")));
            lekarzeViewSource.View.MoveCurrentToFirst();
        }
        private void UpdateDoctor(object sender, RoutedEventArgs e)
        {
            PrzychodniaDBEntities db = new PrzychodniaDBEntities();
            try
            {
                int Id = (gridDoctors.SelectedItem as Lekarze).Id;
                var swapDoctor = db.Lekarze.Where(m => m.Id == Id).Single();
                swapDoctor.Imie = txtName.Text;
                swapDoctor.Nazwisko = txtSurname.Text;
                swapDoctor.StopienNaukowy = txtTitle.Text;
                swapDoctor.Specjalizacja = txtSpecialization.Text;
                db.SaveChanges();
            }
            catch (Exception)
            {
             MessageBox.Show("Musisz wybrać lekarza");
            }
            LoadDoctors();
        }
    }
}
