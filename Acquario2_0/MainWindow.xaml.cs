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
using System.Windows.Threading;
using System.Drawing;
using System.Windows.Interop;

namespace Acquario2_0
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Acquario aq = new Acquario();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Aggiungi_Pesci(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = (ComboBox)sender; // VAriabile per il controllo dell' elemento scelto.
            int indice = cmbAnni.SelectedIndex;
            aq.aggiungi(cmb, cnv,indice, txtNome.Text);
            cmbElimina.ItemsSource = aq.Pesce; // Passaggio dei dati.
            cmbElimina.Items.Refresh(); // Aggiornamento della combo box.
            cmbAggiungi.Items.Refresh(); // Viene aggiornata la combobox.
            Messaggio ac = new Messaggio();
            ac.lblNome.Content = aq.Messaggio();
            ac.Show();
        }
        DispatcherTimer _dt = new DispatcherTimer();

        private void Apertura(object sender, RoutedEventArgs e)
        {
            aq.phPres = 6;
            cmbAggiungi.Items.Add("Carpa"); // Aggiunta nella combobox.
            cmbAggiungi.Items.Add("Carpa Koy"); // Aggiunta nella combobox.
            cmbAggiungi.Items.Add("Pesce rosso"); // Aggiunta nella combobox.
            cmbAggiungi.Items.Refresh(); // Aggiornamento della combobox.

            cmbAnni.Items.Add(1); // Aggiunta nella combobox.
            cmbAnni.Items.Add(5); // Aggiunta nella combobox.
            cmbAnni.Items.Add(10); // Aggiunta nella combobox.
            cmbAnni.Items.Add(15);
            cmbAnni.Items.Refresh(); // Aggiornamento della combobox.
            cmbAnni.SelectedIndex=0;

            aq.ThreadPartenza();
            lblOssigeno.Content = aq.ossigenoPres;
            lblph.Content = aq.phPres;


            _dt.Interval = TimeSpan.FromSeconds(1); // Settaggio dell'intervallo tra una operazione a un altra.
            _dt.Tick += Aggiornamento; // Creazione del treading.
            _dt.Start(); // Partenza del processo continuo.





        }

        public void PesciTotali()
        {
            string n = "";
            foreach (string s in aq)
            {
                n += s + "\n";
            }
            MessageBox.Show(n, "Lista pesci", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        void Aggiornamento(object sender, EventArgs e)
        {
            lblOssigeno.Content = aq.ossigenoPres;
            lblph.Content = aq.phPres;
            lblCosto1.Content = aq.Costo();
            lblValore.Content = aq.Valore;
            if (aq.phPres == 8)
            {
                marrone.Visibility = Visibility.Visible;
            }
            if (aq.phPres == 10)
            {
                imgEmoji.Visibility = Visibility.Visible;
            }

            if (aq.ossigenoPres == 0)
            {
                cnv.Children.Clear();
                aq.Pesce.Clear();
                aq.Valore = 0;
            }
            lblGiorno.Content = aq.Giorno;
        }

        private void Elimina(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = (ComboBox)sender; // VAriabile per il controllo dell' elemento scelto.
            aq.Elimin(cmb,cnv);
        }

        private void btnPulizia_Click(object sender, RoutedEventArgs e)
        {
            aq.pulizia();
            lblOssigeno.Content = aq.ossigenoPres;
        }


        private void BtnStabilizza_Click(object sender, RoutedEventArgs e)
        {
            aq.Stabilizza();
            lblph.Content = aq.phPres;
            marrone.Visibility = Visibility.Hidden;
            imgEmoji.Visibility = Visibility.Hidden;

        }
        private void btnMSG_Click(object sender, RoutedEventArgs e)
        {
            PesciTotali();

        }


        private void Acquario2_Click(object sender, RoutedEventArgs e)
        {
            MainWindow ac = new MainWindow();
            ac.Show();
        }

        private void btnListaNomi_Click(object sender, RoutedEventArgs e)
        {
            aq.ListaPesci();            
        }

        private void btnNomiRiordinati_Click(object sender, RoutedEventArgs e)
        {
            aq.ListaPesciOrdinata();
        }
    }
}
