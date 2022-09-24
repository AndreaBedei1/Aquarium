using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Acquario2_0
{
    public class ossigenatore
    {
        public int Ossigenazione
        {
            get => default(int);
            set
            {
            }
        }
        public int Costo { get; set; }
        public ossigenatore()
        {
            Costo = 50;
        }


        public int Ossigeno { get; set; }= 60;


        public int OssigenoPresente()
        {
            if(Ossigeno>0)
                Ossigeno--;
            return Ossigeno;
        }

        public void Ossigena()
        {
            Ossigeno = 60;
        }
    }
}