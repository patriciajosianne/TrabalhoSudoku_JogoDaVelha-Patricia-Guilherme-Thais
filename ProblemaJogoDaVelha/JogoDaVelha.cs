using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProblemaJogoDaVelha
{
    public class JogoDaVelha
    {
        public static String[,] tabuleiro = new String[3, 3];
        public static String mensagem;

        static void Main(string[] args)
        {
            String jogada;
            imprimeTabuleiro();
            while (!jogoAcabou())
            {
                if (!jogoAcabou())
                {
                    do
                    {
                        jogada = Console.ReadLine();
                    } while ((verificaJogadaInValida(jogada, true)) || (verificaJogadaJaRealizada(jogada, true)));
                    realizaJogada(jogada, "X");
                    imprimeTabuleiro();
                    if (!jogoAcabou())
                    {
                        realizaJogadaPc();
                        imprimeTabuleiro();
                    }
                }
            }
            Console.WriteLine(mensagem);
            Console.WriteLine("O Jogo acabou!");
            Console.ReadLine();
        }

        public static void imprimeTabuleiro()
        {
            Console.Clear();
            Console.WriteLine("Modelo:\n");
            Console.WriteLine(" 1 | 2 | 3 ");
            Console.WriteLine("-----------");
            Console.WriteLine(" 4 | 5 | 6 ");
            Console.WriteLine("-----------");
            Console.WriteLine(" 7 | 8 | 9 ");
            
            Console.WriteLine("\nJogo:\n");
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if ((j == 1) && (tabuleiro[i, j]) == null)
                        Console.Write(" |   |");
                    else if ((j == 1) && (tabuleiro[i, j]) != null)
                        Console.Write(" | " + tabuleiro[i, j] + " |");
                    else if (tabuleiro[i, j] == null)
                        Console.Write("  ");
                    else if(tabuleiro[i,j] != null)
                        Console.Write(" "+tabuleiro[i,j]);
                }
                if(i!=2)
                    Console.WriteLine("\n-----------");
            }
            Console.Write("\n\n");
        }

        public static void realizaJogada(String jogada, String valor)
        {
            if (jogada == "1")
                tabuleiro[0, 0] = valor;
            else if (jogada == "2")
                tabuleiro[0, 1] = valor;
            else if (jogada == "3")
                tabuleiro[0, 2] = valor;
            else if (jogada == "4")
                tabuleiro[1, 0] = valor;
            else if (jogada == "5")
                tabuleiro[1, 1] = valor;
            else if (jogada == "6")
                tabuleiro[1, 2] = valor;
            else if (jogada == "7")
                tabuleiro[2, 0] = valor;
            else if (jogada == "8")
                tabuleiro[2, 1] = valor;
            else if (jogada == "9")
                tabuleiro[2, 2] = valor;
        }

        public static bool verificaJogadaInValida(String jogada, bool mostraMensagem)
        {
            List<String> jogadasPossiveis = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            if (jogadasPossiveis.Contains(jogada))
            {
                return false;
            }
            else
            {
                if (mostraMensagem)
                    Console.WriteLine("Jogada Inválida, faça uma jogada válida.");
                return true;
            }
        }

        public static bool verificaJogadaJaRealizada(String jogada, bool mostraMensagem)
        {
            if ((jogada == "1") && (tabuleiro[0, 0] == null))
                return false;
            else if ((jogada == "2") && (tabuleiro[0, 1] == null))
                return false;
            else if ((jogada == "3") && (tabuleiro[0, 2] == null))
                return false;
            else if ((jogada == "4") && (tabuleiro[1, 0] == null))
                return false;
            else if ((jogada == "5") && (tabuleiro[1, 1] == null))
                return false;
            else if ((jogada == "6") && (tabuleiro[1, 2] == null))
                return false;
            else if ((jogada == "7") && (tabuleiro[2, 0] == null))
                return false;
            else if ((jogada == "8") && (tabuleiro[2, 1] == null))
                return false;
            else if ((jogada == "9") && (tabuleiro[2, 2] == null))
                return false;
            else 
            {
                if (mostraMensagem)
                    Console.WriteLine("Jogada já realizada, escolha uma outra jogada.");
                return true;
            }
        }

        public static bool jogoAcabou()
        {
            String ganhador = verificaGanhador();

            if (ganhador != null)
            {
                if(ganhador == "X")
                    mensagem = "Parabéns!!! Você ganhou...";
                else
                    mensagem = "Você perdeu...";
                return true;
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (tabuleiro[i, j] == null)
                        return false;
                }
            }
            mensagem = "O jogo empatou.";
            return true;
        }

        public static void realizaJogadaPc()
        {
            List<String> listJogadas = new List<string>();
            listJogadas = verficaPossibilidadesGanhar("O");
            if (listJogadas.Count > 0)
            {
                realizaJogada(listJogadas[0], "O");
                return;    
            }

            listJogadas = verficaPossibilidadesGanhar("X");
            if (listJogadas.Count > 0)
            {
                realizaJogada(listJogadas[0], "O");
                return;
            }

            if(tabuleiro[1,1] == null)
            {
                realizaJogada("5", "O");
                return;
            }

            if (tabuleiro[0, 0] == null)
            {
                realizaJogada("1", "O");
                return;
            }

            if (tabuleiro[0, 2] == null)
            {
                realizaJogada("3", "O");
                return;
            }

            if (tabuleiro[2, 0] == null)
            {
                realizaJogada("7", "O");
                return;
            }

            if (tabuleiro[2, 2] == null)
            {
                realizaJogada("9", "O");
                return;
            }

            if (tabuleiro[0, 1] == null)
            {
                realizaJogada("2", "O");
                return;
            }

            if (tabuleiro[1, 0] == null)
            {
                realizaJogada("4", "O");
                return;
            }

            if (tabuleiro[1, 2] == null)
            {
                realizaJogada("6", "O");
                return;
            }

            if (tabuleiro[2, 1] == null)
            {
                realizaJogada("8", "O");
                return;
            }

            /*Random rnd = new Random();
            int p;
            do
            {
                p = rnd.Next(10);
            }while((p==0) || (verificaJogadaJaRealizada(p.ToString(), false)));
            realizaJogada(p.ToString(), "O");*/
        }

        public static List<String> verficaPossibilidadesGanhar(String jogador)
        {
            List<String> possibilidades = new List<string>();

            //verifica se tem possibilidade de ganhar na primeira linha
            if (((tabuleiro[0, 0] == null) && (tabuleiro[0, 1] == jogador) && (tabuleiro[0, 2] == jogador)) ||
                ((tabuleiro[0, 0] == jogador) && (tabuleiro[0, 1] == null) && (tabuleiro[0, 2] == jogador)) ||
                ((tabuleiro[0, 0] == jogador) && (tabuleiro[0, 1] == jogador) && (tabuleiro[0, 2] == null)))
            {
                if (tabuleiro[0, 0] == null)
                    possibilidades.Add("1");
                if (tabuleiro[0, 1] == null)
                    possibilidades.Add("2");
                if (tabuleiro[0, 2] == null)
                    possibilidades.Add("3");
            }

            //verifica se tem possibilidade de ganhar na segunda linha
            if (((tabuleiro[1, 0] == null) && (tabuleiro[1, 1] == jogador) && (tabuleiro[1, 2] == jogador)) ||
                ((tabuleiro[1, 0] == jogador) && (tabuleiro[1, 1] == null) && (tabuleiro[1, 2] == jogador)) ||
                ((tabuleiro[1, 0] == jogador) && (tabuleiro[1, 1] == jogador) && (tabuleiro[1, 2] == null)))
            {
                if (tabuleiro[1, 0] == null)
                    possibilidades.Add("4");
                if (tabuleiro[1, 1] == null)
                    possibilidades.Add("5");
                if (tabuleiro[1, 2] == null)
                    possibilidades.Add("6");
            }

            //verifica se tem possibilidade de ganhar na terceira linha
            if (((tabuleiro[2, 0] == null) && (tabuleiro[2, 1] == jogador) && (tabuleiro[2, 2] == jogador)) ||
                ((tabuleiro[2, 0] == jogador) && (tabuleiro[2, 1] == null) && (tabuleiro[2, 2] == jogador)) ||
                ((tabuleiro[2, 0] == jogador) && (tabuleiro[2, 1] == jogador) && (tabuleiro[2, 2] == null)))
            {
                if (tabuleiro[2, 0] == null)
                    possibilidades.Add("7");
                if (tabuleiro[2, 1] == null)
                    possibilidades.Add("8");
                if (tabuleiro[2, 2] == null)
                    possibilidades.Add("9");
            }


            //verifica se tem possibilidade de ganhar na primeira coluna
            if (((tabuleiro[0, 0] == null) && (tabuleiro[1, 0] == jogador) && (tabuleiro[2, 0] == jogador)) ||
                ((tabuleiro[0, 0] == jogador) && (tabuleiro[1, 0] == null) && (tabuleiro[2, 0] == jogador)) ||
                ((tabuleiro[0, 0] == jogador) && (tabuleiro[1, 0] == jogador) && (tabuleiro[2, 0] == null)))
            {
                if (tabuleiro[0, 0] == null)
                    possibilidades.Add("1");
                if (tabuleiro[1, 0] == null)
                    possibilidades.Add("4");
                if (tabuleiro[2, 0] == null)
                    possibilidades.Add("7");
            }

            //verifica se tem possibilidade de ganhar na segunda coluna
            if (((tabuleiro[0, 1] == null) && (tabuleiro[1, 1] == jogador) && (tabuleiro[2, 1] == jogador)) ||
                ((tabuleiro[0, 1] == jogador) && (tabuleiro[1, 1] == null) && (tabuleiro[2, 1] == jogador)) ||
                ((tabuleiro[0, 1] == jogador) && (tabuleiro[1, 1] == jogador) && (tabuleiro[2, 1] == null)))
            {
                if (tabuleiro[0, 1] == null)
                    possibilidades.Add("2");
                if (tabuleiro[1, 1] == null)
                    possibilidades.Add("5");
                if (tabuleiro[2, 1] == null)
                    possibilidades.Add("8");
            }

            //verifica se tem possibilidade de ganhar na terceira coluna
            if (((tabuleiro[0, 2] == null) && (tabuleiro[1, 2] == jogador) && (tabuleiro[2, 2] == jogador)) ||
                ((tabuleiro[0, 2] == jogador) && (tabuleiro[1, 2] == null) && (tabuleiro[2, 2] == jogador)) ||
                ((tabuleiro[0, 2] == jogador) && (tabuleiro[1, 2] == jogador) && (tabuleiro[2, 2] == null)))
            {
                if (tabuleiro[0, 2] == null)
                    possibilidades.Add("3");
                if (tabuleiro[1, 2] == null)
                    possibilidades.Add("6");
                if (tabuleiro[2, 2] == null)
                    possibilidades.Add("9");
            }

            //verifica se tem possibilidade de ganhar na diagonal principal
            if (((tabuleiro[0, 0] == null) && (tabuleiro[1, 1] == jogador) && (tabuleiro[2, 2] == jogador)) ||
                ((tabuleiro[0, 0] == jogador) && (tabuleiro[1, 1] == null) && (tabuleiro[2, 2] == jogador)) ||
                ((tabuleiro[0, 0] == jogador) && (tabuleiro[1, 1] == jogador) && (tabuleiro[2, 2] == null)))
            {
                if (tabuleiro[0, 0] == null)
                    possibilidades.Add("1");
                if (tabuleiro[1, 1] == null)
                    possibilidades.Add("5");
                if (tabuleiro[2, 2] == null)
                    possibilidades.Add("9");
            }

            //verifica se tem possibilidade de ganhar na diagonal secundária
            if (((tabuleiro[0, 2] == null) && (tabuleiro[1, 1] == jogador) && (tabuleiro[2, 0] == jogador)) ||
                ((tabuleiro[0, 2] == jogador) && (tabuleiro[1, 1] == null) && (tabuleiro[2, 0] == jogador)) ||
                ((tabuleiro[0, 2] == jogador) && (tabuleiro[1, 1] == jogador) && (tabuleiro[2, 0] == null)))
            {
                if (tabuleiro[0, 2] == null)
                    possibilidades.Add("3");
                if (tabuleiro[1, 1] == null)
                    possibilidades.Add("5");
                if (tabuleiro[2, 0] == null)
                    possibilidades.Add("7");
            }

            return possibilidades;
        }

        public static String verificaGanhador()
        {
            String ganhador = null;

            //verifica se alguém ganhou na primeira linha
            if (((tabuleiro[0, 0] == "X") && (tabuleiro[0, 1] == "X") && (tabuleiro[0, 2] == "X")) ||
                ((tabuleiro[0, 0] == "O") && (tabuleiro[0, 1] == "O") && (tabuleiro[0, 2] == "O")))
            {
                if (tabuleiro[0, 0] == "X")
                    ganhador = "X";
                else
                    ganhador = "O";
                return ganhador;
            }

            //verifica se alguém ganhou na segunda linha
            if (((tabuleiro[1, 0] == "X") && (tabuleiro[1, 1] == "X") && (tabuleiro[1, 2] == "X")) ||
                ((tabuleiro[1, 0] == "O") && (tabuleiro[1, 1] == "O") && (tabuleiro[1, 2] == "O")))
            {
                if (tabuleiro[1, 0] == "X")
                    ganhador = "X";
                else
                    ganhador = "O";
                return ganhador;
            }

            //verifica se alguém ganhou na terceira linha
            if (((tabuleiro[2, 0] == "X") && (tabuleiro[2, 1] == "X") && (tabuleiro[2, 2] == "X")) ||
                ((tabuleiro[2, 0] == "O") && (tabuleiro[2, 1] == "O") && (tabuleiro[2, 2] == "O")))
            {
                if (tabuleiro[2, 0] == "X")
                    ganhador = "X";
                else
                    ganhador = "O";
                return ganhador;
            }

            //verifica se alguém ganhou na primeira coluna
            if (((tabuleiro[0, 0] == "X") && (tabuleiro[1, 0] == "X") && (tabuleiro[2, 0] == "X")) ||
                ((tabuleiro[0, 0] == "O") && (tabuleiro[1, 0] == "O") && (tabuleiro[2, 0] == "O")))
            {
                if (tabuleiro[0, 0] == "X")
                    ganhador = "X";
                else
                    ganhador = "O";
                return ganhador;
            }

            //verifica se alguém ganhou na segunda coluna
            if (((tabuleiro[0, 1] == "X") && (tabuleiro[1, 1] == "X") && (tabuleiro[2, 1] == "X")) ||
                ((tabuleiro[0, 1] == "O") && (tabuleiro[1, 1] == "O") && (tabuleiro[2, 1] == "O")))
            {
                if (tabuleiro[0, 1] == "X")
                    ganhador = "X";
                else
                    ganhador = "O";
                return ganhador;
            }

            //verifica se alguém ganhou na terceira coluna
            if (((tabuleiro[0, 2] == "X") && (tabuleiro[1, 2] == "X") && (tabuleiro[2, 2] == "X")) ||
                ((tabuleiro[0, 2] == "O") && (tabuleiro[1, 2] == "O") && (tabuleiro[2, 2] == "O")))
            {
                if (tabuleiro[0, 2] == "X")
                    ganhador = "X";
                else
                    ganhador = "O";
                return ganhador;
            }

            //verifica se alguém ganhou na diagonal principal
            if (((tabuleiro[0, 0] == "X") && (tabuleiro[1, 1] == "X") && (tabuleiro[2, 2] == "X")) ||
                ((tabuleiro[0, 0] == "O") && (tabuleiro[1, 1] == "O") && (tabuleiro[2, 2] == "O")))
            {
                if (tabuleiro[0, 0] == "X")
                    ganhador = "X";
                else
                    ganhador = "O";
                return ganhador;
            }

            //verifica se alguém ganhou na diagonal secundária
            if (((tabuleiro[0, 2] == "X") && (tabuleiro[1, 1] == "X") && (tabuleiro[2, 0] == "X")) ||
                ((tabuleiro[0, 2] == "O") && (tabuleiro[1, 1] == "O") && (tabuleiro[2, 0] == "O")))
            {
                if (tabuleiro[0, 2] == "X")
                    ganhador = "X";
                else
                    ganhador = "O";
                return ganhador;
            }

            return ganhador;
        }
    }
}
