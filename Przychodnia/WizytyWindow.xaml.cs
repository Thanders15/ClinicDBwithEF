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
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            Przychodnia.PrzychodniaDBDataSetVVV przychodniaDBDataSetVVV = ((Przychodnia.PrzychodniaDBDataSetVVV)(this.FindResource("przychodniaDBDataSetVVV")));
            // Załaduj dane do tabeli Wizyty. Możesz modyfikować ten kod w razie potrzeby.
            Przychodnia.PrzychodniaDBDataSetVVVTableAdapters.WizytyTableAdapter przychodniaDBDataSetVVVWizytyTableAdapter = new Przychodnia.PrzychodniaDBDataSetVVVTableAdapters.WizytyTableAdapter();
            przychodniaDBDataSetVVVWizytyTableAdapter.Fill(przychodniaDBDataSetVVV.Wizyty);
            System.Windows.Data.CollectionViewSource wizytyViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("wizytyViewSource")));
            wizytyViewSource.View.MoveCurrentToFirst();
        }
    }
}
