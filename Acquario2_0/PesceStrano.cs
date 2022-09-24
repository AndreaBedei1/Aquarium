using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Acquario2_0
{
    public class PesceStrano : Pesci
    {
        public PesceStrano(int xc, int yc, Canvas x) : base(xc, yc, x, "pesce2.png",1000, "Carpa")
        {
            Valore = 1000;
        }
        public PesceStrano()
        {

        }

        public void Inizio()
        {
            Spostamento();
        }

        public override string Saluta() =>  "Ciao, sono una carpa Koy bella bella!";
    }
}