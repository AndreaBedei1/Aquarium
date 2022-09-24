using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Acquario2_0
{
    public class PesceGatto : Pesci
    {
        public PesceGatto(int xc, int yc, Canvas x) : base(xc, yc, x, "Pesce1.png",100,"Carpa Koy")
        {
            Valore = 100;
        }
        public PesceGatto()
        {

        }

        public void Inizio()
        {
            Spostamento();
        }

        public override string Saluta()
        {
            return "Ciao, sono una carpa bella bella!";
        }
    }
}