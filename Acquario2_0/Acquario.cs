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
using System.Collections;

namespace Acquario2_0
{
    public class Acquario : IEnumerable<string>
    {
        
        private Random randx = new Random(); // Variabile per generare un numero random.
        public Depuratore Depuratore { get; set; } = new Depuratore();

        public ossigenatore ossigenatore { get; set; } = new ossigenatore();

        public List<Pesci> Pesce { get; set; } = new List<Pesci>();

        public List<int> Giorni { get; set; } = new List<int>();

        public int Valore { get; set; }

        public void aggiungi(ComboBox cmb, Canvas cnv,int ind, string nome )
        {
            if (nome == "")
            {
                throw new Exception("Non è stato inserito il nome");
            }
            // Se la scelta selezionata è diversa dalla sezione vuota entra.
            if (cmb.SelectedItem != null)
            {
                int x = randx.Next(1, 800); // Generazione della coordinata x random.
                int y = randx.Next(1, 330); // Generazione della coordinata y random.

                switch (ind)
                {
                    case 0:
                        ind = 75;
                        break;
                    case 1:
                        ind = 90;
                        break;
                    case 2:
                        ind = 105;
                        break;
                    case 3:
                        ind = 115;
                        break;
                }

                if (cmb.SelectedIndex == 2)
                {
                    Pesce.Add(new PesceRosso(x, y, cnv)); // Creazione di un elemento dentro alla lista.
                    Giorni.Add(0);

                }

                if (cmb.SelectedIndex == 0)
                {
                    Pesce.Add(new PesceGatto(x, y, cnv)); // Creazione di un elemento dentro alla lista.
                    Giorni.Add(0);
                    
                }

                if (cmb.SelectedIndex == 1)
                {
                    Pesce.Add(new PesceStrano(x, y, cnv)); // Creazione di un elemento dentro alla lista.
                    Giorni.Add(0);
                }
                Pesce[Pesce.Count - 1].N.Width = ind;
                Pesce[Pesce.Count - 1].N.Height = ind;
                Pesce[Pesce.Count - 1].Nome = nome;

            }

            // COn questo for si fa partire il moto di tutte le stelle.
            for (int i = 0; i < Pesce.Count; i++)
                Pesce[i].Movimento(); // Richiamato il metodo di movimento per ogni singola stella e gli viene passo un booleano che decide se le stelle si devono inseguire.

        }

        DispatcherTimer _dt = new DispatcherTimer(); // Variabile per il tempo, una specie di multithreding.
        DispatcherTimer _dt2 = new DispatcherTimer(); // Variabile per il tempo, una specie di multithreding.
        DispatcherTimer _dtg = new DispatcherTimer();
        public int ossigenoPres { get; set; } = 60;
        public int Giorno { get; set; } = 0;
        public int phPres { get; set; }
        void Calc(object sender, EventArgs e)
        {
            if(Pesce.Count!=0)
                ossigenoPres= ossigenatore.OssigenoPresente();
        }

        public void Calc2(object sender, EventArgs e)
        {
           if(Pesce.Count!=0)
                phPres = Depuratore.PhVariazione();
        }

        public void CalcG(object sender, EventArgs e)
        {
            Giorno++;
            for(int i = 0; i < Pesce.Count; i++)
                Giorni[i] += 1;
            if (Pesce.Count != 0)
            {

                for (int i = 0; i < Pesce.Count; i++)
                {
                    if (Giorni[i] % 2 == 0 && Giorni[i] < 50 && Pesce[Pesce.Count-1].N.Width<115)
                    {

                        Pesce[i].N.Width += 2;
                        Pesce[i].N.Height += 2;
                        Pesce[i].Valore += (Pesce[i].Valore / 100) * 5;
                    }
                }
                int valore = 0;
                for (int i = 0; i < Pesce.Count; i++)
                {
                    valore += Pesce[i].Valore;
                }

                Valore = valore;
            } 

        }


        public void ThreadPartenza()
        {


            _dt.Interval = TimeSpan.FromSeconds(1); // Settaggio dell'intervallo tra una operazione a un altra.
            _dt.Tick += Calc; // Creazione del treading.
            _dt.Start(); // Partenza del processo continuo.

            _dt2.Interval = TimeSpan.FromSeconds(10); // Settaggio dell'intervallo tra una operazione a un altra.
            _dt2.Tick += Calc2; // Creazione del treading.
            _dt2.Start(); // Partenza del processo continuo.


            _dtg.Interval = TimeSpan.FromSeconds(1); // Settaggio dell'intervallo tra una operazione a un altra.
            _dtg.Tick += CalcG; // Creazione del treading.
            _dtg.Start(); // Partenza del processo continuo.

        }

        public void Elimin(ComboBox cmb,Canvas cnv)
        {
            if (cmb.SelectedItem != null)
            {
                cnv.Children.Remove(((Pesci)cmb.SelectedItem).N); // Eliminazione dalla stella dal canvas.
                Pesce.Remove((Pesci)cmb.SelectedItem); // RImozione dalla lista dell'oggetto.
                cmb.Items.Refresh(); // Viene aggiornata la combobox.
            }
        }

        public void pulizia()
        {
            _dt.Stop();
            _dt.Tick -= Calc;
            ossigenatore.Ossigena();
            _dt = new DispatcherTimer();
            _dt.Interval = TimeSpan.FromSeconds(1);
            ossigenoPres= ossigenatore.Ossigeno;
            _dt.Tick += Calc; // Creazione del treading.
            _dt.Start(); // Partenza del processo continuo.
        }

        public void Stabilizza()
        {
            _dt2.Stop();
            _dt2.Tick -= Calc2;
            Depuratore.STabilizza();
            _dt2 = new DispatcherTimer();
            _dt2.Interval = TimeSpan.FromSeconds(10);
            phPres = Depuratore.Ph;
            _dt2.Tick += Calc2; // Creazione del treading.
            _dt2.Start(); // Partenza del processo continuo.

        }

        public int Costo()
        {
            int costoTot = 0;
            for(int i = 0; i < Pesce.Count; i++)
            {
                costoTot += Pesce[i].Costo;
            }
            costoTot += Depuratore.Costo + ossigenatore.Costo;
            return costoTot;
        }

        public IEnumerator<string> GetEnumerator()
        {
            for(int i = 0; i < Pesce.Count; i++)
            {
                yield return Pesce[i].ToString();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        public string Messaggio()
        {
        
            return Pesce[Pesce.Count - 1].Saluta();

        }

        public void ListaPesci()
        {
            string n = "";
            for (int i = 0; i < Pesce.Count; i++)
            {
                n += Pesce[i].Nome + "\n";

            }
            MessageBox.Show(n, "Lista pesci", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void ListaPesciOrdinata()
        {
            Pesci[] p = new Pesci[Pesce.Count];
            Pesce.CopyTo(p);
            Array.Sort<Pesci>(p);
            
            string n = "";
            for (int i = 0; i < p.Length; i++)
            {
                n += p[i].Nome + "\n";

            }
            MessageBox.Show(n, "Lista pesci", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}