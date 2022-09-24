using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Acquario2_0
{
    public class Depuratore
    {

        public int Ph { get; set; }
        public int Costo { get; set; }

        public Depuratore()
        {
            Costo = 200;
            Ph = 6;
        }


        public int PhVariazione()
        {
            if(Ph<10)
                Ph++;
            return Ph;
        }

        public void STabilizza()
        {
            Ph = 6;
        }
    }
}