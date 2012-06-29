using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlgoritmosGeneticos;

namespace ProblemaSudoku
{
    public class Sudoku : IIndividuo
    {

        public Sudoku()
        {
 
        }

        public Sudoku(int t)
        {
            TamanhoCromossomo = t;
            Cromossomos = new object[t];
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
