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
    public abstract class Pesci : IComparable<Pesci>
    {
        public abstract string Saluta();
        public int Valore { get; set; }
        public string Nome { get; set; }

        string _id; // Nome della stella in oggetto.
        int _xx, _yy; // Variabili di movimento della stella.

        public Image N { get; set; } // Creazione di una proprietà pubblica.
        Random _rand = new Random(); // Variabile per geneare numiri random.
        int _dir; // Variabile per numero random.
        bool _avanti, _su; // Variabili booleane per controllare il movimento della stella a seconda della posizione nello schermo.
        DispatcherTimer _dt = new DispatcherTimer(); // Variabile per il tempo, una specie di multithreding.
        public int Costo { get; set; }

        // Costruttore della classe.
        public Pesci()
        {
            Costo = 0;
        }

        string _nPesce;
        public Pesci(int xc, int yc, Canvas x, string nPesce, int cs, string nome)
        {
            _dir = _rand.Next(1, 5); // Generazione diun numero random.
            _xx = xc; // Copiatura delle coordinate.
            _yy = yc; // Copiatura delle coordinate.
            BitmapImage immagine = new BitmapImage(new Uri("../../Pesci/" + nPesce, UriKind.Relative)); // Creazione di una variabile bitmat per il percorso dell'immagine della stella.
            N = new Image(); // Creazione della proprietaà immagine.
            N.Source = immagine; // Passaggio del percorcorso della stella.
            N.Width = 75; // Larghezza dell'immagine.
            N.Height = 75; // Altezza dell'immagine.

            Canvas.SetLeft(N, xc); // Posizionamento dell' immagine nel canvas per la coordinata x.
            Canvas.SetTop(N, yc); // Posizionamento dell' immagine nel canvas per la coordinata y.
            _id = "Pesce  n°"+(x.Children.Add(N)) +": "+ nome; // Creazione dell'id dell'immagine.
            Direzione(out _avanti, out _su); // Scelta della direzione in base alle coordinate.
            Costo += cs;
            _nPesce = nPesce;
            N.MouseLeftButtonDown += Saluta;
        }

        private void Saluta(object sender, MouseButtonEventArgs e)
        {
            PesceGatto pg = new PesceGatto();
            PesceRosso pr = new PesceRosso();
            PesceStrano ps = new PesceStrano();
            Messaggio m = new Messaggio();
            if(_nPesce=="Pesce1.png")
            {
                m.lblNome.Content = pg.Saluta();
            }
            if (_nPesce == "pesce3.png")
            {
                m.lblNome.Content = pr.Saluta();
            }
            if (_nPesce == "pesce2.png")
            {
                m.lblNome.Content = ps.Saluta();
            }
            m.ShowDialog();
        }

        // Metodo dell'inseguimento.
        public void Movimento()
        {
            // Condizione vera la modalita inseguimentonon è attiva, se è falsa vuol dire che è attiva.
            _dt.Tick -= timer_Tick; // Chiusura del threading precedente.
            _dt.Interval = TimeSpan.FromSeconds(0.01); // Settaggio dell'intervallo tra una operazione a un altra.
            _dt.Tick += timer_Tick; // Creazione del treading.
            _dt.Start(); // Partenza del processo continuo.
        }

        // Scelta della direzione iniziale.
        public void Direzione(out bool avanti, out bool su)
        {
            avanti = true; // Variabile di lavoro.
            su = true; // Variabile di lavoro.


            // Condizioni per le quali le stelle all'inizio non vanno tutte nella stessa direzione.
            if (_dir % 4 == 0)
            {
                avanti = true;
                su = true;
            }
            else if (_dir % 3 == 0)
            {
                avanti = false;
                su = true;
            }
            else if (_dir % 2 == 0)
            {
                avanti = true;
                su = false;
            }
            else if (_dir % 1 == 0)
            {
                avanti = false;
                su = false;
            }
        }

        // Movimento e controllo che non escano dai bordi.
        public void Spostamento()
        {
            Canvas.SetLeft(N, _xx); // Posizionamento nel canvas dell'immagine nelle coordinate x desiderate.
            Canvas.SetTop(N, _yy); // Posizionamento nel canvas dell'immagine nelle coordinate y desiderate.


            // Controllo che le stelle non escano dalla finestra e che rimbalzino correttamente 
            if (_xx > 850)
            {
                _avanti = false;
            }
            if (_xx < 0)
            {
                _avanti = true;
            }
            // Spostamento avanti.
            if (_avanti)
            {
                _xx += 1;
                N.RenderTransform = new ScaleTransform(1, 1, N.Width / 2, N.Height / 2);
            }
            // Spostamento indietro.
            else
            {
                _xx -= 1;
                N.RenderTransform = new ScaleTransform(-1, 1, N.Width / 2, N.Height / 2);
            }
            if (_yy > 360)
            {
                _su = false;
            }
            if (_yy < -30)
            {
                _su = true;
            }
            // Spostamento giù.
            if (_su)
            {
                _yy += 1;
            }
            // Spostamento su.
            else
            {
                _yy -= 1;
            }
        }

        // Metodo che eseguo ripetutamente nel dispaccertimer.
        void timer_Tick(object sender, EventArgs e)
        {
            Spostamento(); // Invoca il metodo del movimento.
        }

        // Ovverride di ToString.
        public override string ToString()
        {
            return _id; // Passagio del nome della stella.  
        }

        public int CompareTo(Pesci p)
        {
            return string.Compare(Nome, p.Nome);
        }
    }
}