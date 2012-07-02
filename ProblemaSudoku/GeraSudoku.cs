using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlgoritmosGeneticos;

namespace ProblemaSudoku
{
    public class GeraSudoku : AlgoritmoGenetico
    {

        public GeraSudoku(int tamanhoInd)
        {
            Solucao = new Sudoku(tamanhoInd);
            Solucao.Fitness = -30;
        }

        public override IIndividuo RealizarCruzamento(IIndividuo a, IIndividuo b)
        {
            IIndividuo novo = new Sudoku(TamanhoIndividuo);
            int modulo = TamanhoIndividuo % 2;
            int metade = (TamanhoIndividuo - modulo) / 2;

            for (int indice = 0; indice < metade; indice++)
                novo.Cromossomos[indice] = a.Cromossomos[indice];

            for (int indice = metade; indice < TamanhoIndividuo; indice++)
                novo.Cromossomos[indice] = b.Cromossomos[indice];

            return novo;
        }

        public override void RealizarMutacao(IIndividuo a)
        {
            Random rnd = new Random();
            int p;
            do
            {
                p = rnd.Next(10);
            } while (p == 0);
            a.Cromossomos[rnd.Next(TamanhoIndividuo - 1)] = p;
        }

        //Caso não tenha um desempenho bom, refazer método para gerar população inicial de forma mais inteligente
        public override void GerarPopulacaoInicial()
        {
            /*Random rnd = new Random();
            for (int individuo = 0; individuo < TamanhoPopulacao; individuo++)
            {
                IIndividuo ind = new Sudoku(TamanhoIndividuo);
                for (int cromossomo = 0; cromossomo < TamanhoIndividuo; cromossomo++)
                {
                    int p;
                    do
                    {
                        p = rnd.Next(10);
                    } while (p == 0);
                    ind.Cromossomos[cromossomo] = p;
                }
                Populacao.Add(ind);
            }*/

            Random rnd = new Random();
            for (int individuo = 0; individuo < TamanhoPopulacao; individuo++)
            {
                IIndividuo ind = new Sudoku(TamanhoIndividuo);
                for (int cromossomo = 0; cromossomo < TamanhoIndividuo; cromossomo++)
                {
                    int p;
                    do
                    {
                        do
                        {
                            p = rnd.Next(10);
                        } while (p == 0);
                    } while (verificaRepetidoLinha(p, cromossomo, ind));
                    ind.Cromossomos[cromossomo] = p;
                }
                Populacao.Add(ind);
            }
        }

        public override bool CriterioParada()
        {
            return ((Solucao.Fitness == 27)||(Geracoes == 200000));
        }

        public override float CalculaFitness(IIndividuo ind)
        {
            int posSudoku = 0;
            float fitness = 0;
            //Percorrendo cada quadro do sudoku
            for (int qtdQuadrado = 1; qtdQuadrado < 10; qtdQuadrado++)
            {
                int[] vetorValidacao = new int[10];
                //zerando vetor que será utilizado para calcular o fitness do indivíduo
                for (int i = 0; i < 10; i++)
                {
                    vetorValidacao[i] = 0;
                }

                //percorrendo elementos do quadrado
                int pos = posSudoku;
                for (int elemento = 1; elemento < 10; elemento++)
                {
                    //verifica se o número não é repetido
                    if (vetorValidacao[(int)ind.Cromossomos[pos]] == 0)
                        vetorValidacao[(int)ind.Cromossomos[pos]]++;
                    else//em caso do número for repetido
                    {
                        fitness--;
                        break;
                    }

                    //se chegar a este ponto, é pq o quadrado não possui números repetidos
                    if (elemento == 9)
                        fitness++;                

                    //se tiver percorrido uma linha completa do quadro, pula para a próxima
                    if ((elemento) % 3 == 0)
                        pos = pos + 7;
                    else
                        pos++;
                }

                if (qtdQuadrado % 3 == 0)
                    posSudoku = posSudoku + 21;
                else
                    posSudoku = posSudoku + 3;

            }


            //percorrendo cada linha do sudoku
            posSudoku = 0;
            for (int linha = 1; linha < 10; linha++)
            {
                int[] vetorValidacao = new int[10];
                //zerando vetor que será utilizado para calcular o fitness do indivíduo
                for (int i = 0; i < 10; i++)
                {
                    vetorValidacao[i] = 0;
                }

                //percorrendo os elementos da linha
                int pos = posSudoku;
                for (int elemento = 0; elemento < 9; elemento++)
                {
                    if (vetorValidacao[(int)ind.Cromossomos[pos]] == 0)
                        vetorValidacao[(int)ind.Cromossomos[pos]]++;
                    else
                    {
                        fitness--;
                        break;
                    }
                    pos++;
                    if (elemento == 8)
                        fitness++;
                }
                posSudoku = posSudoku + 9;

            }

            //Percorrendo cada coluna do sudoku
            posSudoku = 0;
            for (int coluna = 0; coluna < 9; coluna++)
            {
                int[] vetorValidacao = new int[10];
                //zerando vetor que será utilizado para calcular o fitness do indivíduo
                for (int i = 0; i < 10; i++)
                {
                    vetorValidacao[i] = 0;
                }

                //percorrendo os elementos da coluna
                int pos = coluna;
                for (int elemento = 0; elemento < 9; elemento++)
                {
                    if (vetorValidacao[(int)ind.Cromossomos[pos]] == 0)
                        vetorValidacao[(int)ind.Cromossomos[pos]]++;
                    else
                    {
                        fitness--;
                        break;
                    }
                    posSudoku = posSudoku + 9;

                    if (elemento == 8)
                        fitness++;
                }
            }
            return fitness;
        }

        public bool verificaRepetidoLinha(int elemento, int posIndividuo, IIndividuo ind)
        {
            if((posIndividuo % 9 != 0))
            {
                int cont = posIndividuo-1;
                bool continua = true;
                int number;
                while (continua)
                {
                    number = (int)ind.Cromossomos[cont];
                    if (number == elemento)
                    {
                        return true;

                    }
                    if (cont % 9 == 0)
                    {
                        continua = false;
                        return false;
                    }
                    cont--;

                }
            }
            return false;
        }

        public bool verificaRepetidoColuna(int elemento, int posIndividuo, IIndividuo ind)
        {
            if ((posIndividuo > 8))
            {
                int cont = posIndividuo - 9;
                bool continua = true;
                int number;
                while (continua)
                {
                    number = (int)ind.Cromossomos[cont];
                    if (number == elemento)
                    {
                        return true;

                    }
                    if (cont % 9 < 9)
                    {
                        continua = false;
                        return false;
                    }
                    cont = cont - 9;

                }
            }
            return false;
        }
    }


}
