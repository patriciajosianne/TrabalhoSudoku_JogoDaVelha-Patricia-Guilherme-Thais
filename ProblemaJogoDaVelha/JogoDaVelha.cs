using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlgoritmosGeneticos;

namespace ProblemaJogoDaVelha
{
    public class JogoDaVelha : IIndividuo
    {
        public JogoDaVelha()
        {
 
        }                  

        public object[] Cromossomos
        {
            get;
            set;
        }

        public int TamanhoCromossomo
        {
            get;
            set;
        }

        public float Fitness
        {
            get;
            set;
        }
    }
}
