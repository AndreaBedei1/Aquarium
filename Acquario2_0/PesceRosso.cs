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
    public class PesceRosso : Pesci
    {
        public PesceRosso(int xc, int yc, Canvas x) :base(xc,yc,x,"pesce3.png",250,"Rosso")
        {
            Valore = 250;
        }
        public PesceRosso()
        {

        }

        public override string Saluta()
        {
            return "Ciao, sono un pesce rosso bello bello!";
        }
        public void Inizio()
        {

            Spostamento();

        }

    }
}