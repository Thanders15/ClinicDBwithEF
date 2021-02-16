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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Przychodnia
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static DataGrid dataGrid;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void buttonDoctors(object sender, RoutedEventArgs e)
        {
            LekarzeWindow lw = new LekarzeWindow();
            lw.Show();
            this.Close();
        }
        private void buttonPatients(object sender, RoutedEventArgs e)
        {
            PacjenciWindow pw = new PacjenciWindow();
            pw.Show();
            this.Close();
        }
        private void buttonVisits(object sender, RoutedEventArgs e)
        {
            WizytyWindow ww = new WizytyWindow();
            ww.Show();
            this.Close();
        }
        private void buttonPrescriptions(object sender, RoutedEventArgs e)
        {
            ReceptyWindow rw = new ReceptyWindow();
            rw.Show();
            this.Close();
        }
    }
}
