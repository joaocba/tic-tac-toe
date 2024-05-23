using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exe_aval_FINAL_v10
{
    internal class Program
    {
        static string[,] tabuleiro;
        static int tamanhoTabuleiro = 3; // Valor por defeito
        static int jogadas;
        static string nomeJogadorX, nomeJogadorO, jogadorAtual, jogadorPerdedor;
        static string tipoJogador; // Tipo de jogador com o valor de "X" ou "O"
        static bool jogoTerminado;
        static List<JogadorVencedor> lst_JogadorVencedores = new List<JogadorVencedor>();

        static void Main(string[] args)
        {
            Menu();
        }

        static void MostrarTabuleiro()
        {
            // Tabuleiro 3x3
            if (tamanhoTabuleiro == 3)
            {
                Console.Clear();
                Console.WriteLine("\n  1   2   3");
                Console.WriteLine(" ┌───┬───┬───┐");
                for (int i = 0; i < tabuleiro.GetLength(0); i++)
                {
                    Console.Write(i + 1);
                    for (int j = 0; j < tabuleiro.GetLength(1); j++)
                    {
                        string campo = tabuleiro[i, j].Trim();
                        Console.Write("│ ");

                        if (campo == "-")
                            Console.Write(" ");
                        else
                        {
                            if (campo == "X")
                                Console.ForegroundColor = ConsoleColor.Green;
                            else if (campo == "O")
                                Console.ForegroundColor = ConsoleColor.Red;

                            Console.Write(campo);
                            Console.ResetColor();
                        }

                        Console.Write(" ");
                    }
                    Console.WriteLine("│");

                    if (i < tabuleiro.GetLength(0) - 1)
                        Console.WriteLine(" ├───┼───┼───┤");
                }
                Console.WriteLine(" └───┴───┴───┘");
                Console.WriteLine("\n");
            }
            // Tabuleiro 5x5
            else if (tamanhoTabuleiro == 5)
            {
                Console.Clear();
                Console.WriteLine("\n  1   2   3   4   5");
                Console.WriteLine(" ┌───┬───┬───┬───┬───┐");
                for (int i = 0; i < tabuleiro.GetLength(0); i++)
                {
                    Console.Write(i + 1);
                    for (int j = 0; j < tabuleiro.GetLength(1); j++)
                    {
                        string campo = tabuleiro[i, j].Trim();
                        Console.Write("│ ");

                        if (campo == "-")
                            Console.Write(" ");
                        else
                        {
                            if (campo == "X")
                                Console.ForegroundColor = ConsoleColor.Green;
                            else if (campo == "O")
                                Console.ForegroundColor = ConsoleColor.Red;

                            Console.Write(campo);
                            Console.ResetColor();
                        }

                        Console.Write(" ");
                    }
                    Console.WriteLine("│");

                    if (i < tabuleiro.GetLength(0) - 1)
                        Console.WriteLine(" ├───┼───┼───┼───┼───┤");
                }
                Console.WriteLine(" └───┴───┴───┴───┴───┘");
                Console.WriteLine("\n");
            }
            // Tabuleiro 7x7
            else if (tamanhoTabuleiro == 7)
            {
                Console.Clear();
                Console.WriteLine("\n  1   2   3   4   5   6   7");
                Console.WriteLine(" ┌───┬───┬───┬───┬───┬───┬───┐");
                for (int i = 0; i < tabuleiro.GetLength(0); i++)
                {
                    Console.Write(i + 1);
                    for (int j = 0; j < tabuleiro.GetLength(1); j++)
                    {
                        string campo = tabuleiro[i, j].Trim();
                        Console.Write("│ ");

                        if (campo == "-")
                            Console.Write(" ");
                        else
                        {
                            if (campo == "X")
                                Console.ForegroundColor = ConsoleColor.Green;
                            else if (campo == "O")
                                Console.ForegroundColor = ConsoleColor.Red;

                            Console.Write(campo);
                            Console.ResetColor();
                        }

                        Console.Write(" ");
                    }
                    Console.WriteLine("│");

                    if (i < tabuleiro.GetLength(0) - 1)
                        Console.WriteLine(" ├───┼───┼───┼───┼───┼───┼───┤");
                }
                Console.WriteLine(" └───┴───┴───┴───┴───┴───┴───┘");
                Console.WriteLine("\n");
            }
        }

        static void ValidarJogada(int x, int y)
        {
            // Se a posiçao esta dentro dos limites do tabuleiro e se nao esta preenchida
            if (x >= 0 && x < tabuleiro.GetLength(0) && y >= 0 && y < tabuleiro.GetLength(1) && tabuleiro[x, y] == " - ")
            {
                FazerJogada(x, y); // Fazer a jogada para as posiçoes x e y
                VerificarVencedor(); // Verificar se o jogador ganhou

                if (!jogoTerminado)
                {
                    VerificarEmpate(); // Verificar se o jogo empatou
                    TrocarJogador(); // Trocar de jogador
                }
            }
            else
            {
                Console.WriteLine("Posição inválida ou preenchida. Tente novamente.");
                Console.ReadKey();
            }
        }

        static void FazerJogada(int x, int y)
        {
            tabuleiro[x, y] = $" {tipoJogador} ";
            jogadas++;
        }

        static void VerificarVencedor()
        {
            string tipoJogadorComEspacos = $" {tipoJogador} ";

            // Definir o jogador a jogar
            if (tipoJogador == "X")
                jogadorAtual = nomeJogadorX;
            else
                jogadorAtual = nomeJogadorO;

            // Definir o jogador que perdeu
            if (tipoJogador == "X")
                jogadorPerdedor = nomeJogadorO;
            else
                jogadorPerdedor = nomeJogadorX;

            // Verificar se ganhou em linha
            if (tamanhoTabuleiro == 3)
            {
                for (int i = 0; i < 3; i++)
                {
                    if ((tabuleiro[i, 0] == tipoJogadorComEspacos && tabuleiro[i, 1] == tipoJogadorComEspacos && tabuleiro[i, 2] == tipoJogadorComEspacos))
                    {
                        jogoTerminado = true;

                        // Atualizar highscores com o jogador vencedor
                        AtualizarHighscores(jogadorAtual);

                        if (jogoTerminado)
                            MostrarTabuleiro();

                        Console.WriteLine($"Jogador {jogadorAtual} ({tipoJogador}) ganhou em {jogadas} jogadas!");
                        Console.WriteLine($"Jogador {jogadorPerdedor} perdeu!");
                    }
                }
            }
            else if (tamanhoTabuleiro == 5)
            {
                for (int i = 0; i < 5; i++)
                {
                    if ((tabuleiro[i, 0] == tipoJogadorComEspacos && tabuleiro[i, 1] == tipoJogadorComEspacos && tabuleiro[i, 2] == tipoJogadorComEspacos && tabuleiro[i, 3] == tipoJogadorComEspacos && tabuleiro[i, 4] == tipoJogadorComEspacos))
                    {
                        jogoTerminado = true;
                        AtualizarHighscores(jogadorAtual);

                        if (jogoTerminado)
                            MostrarTabuleiro();

                        Console.WriteLine($"Jogador {jogadorAtual} ({tipoJogador}) ganhou em {jogadas} jogadas!");
                        Console.WriteLine($"Jogador {jogadorPerdedor} perdeu!");
                    }
                }
            }
            else if (tamanhoTabuleiro == 7)
            {
                for (int i = 0; i < 7; i++)
                {
                    if ((tabuleiro[i, 0] == tipoJogadorComEspacos && tabuleiro[i, 1] == tipoJogadorComEspacos && tabuleiro[i, 2] == tipoJogadorComEspacos && tabuleiro[i, 3] == tipoJogadorComEspacos && tabuleiro[i, 4] == tipoJogadorComEspacos && tabuleiro[i, 5] == tipoJogadorComEspacos && tabuleiro[i, 6] == tipoJogadorComEspacos))
                    {
                        jogoTerminado = true;
                        AtualizarHighscores(jogadorAtual);

                        if (jogoTerminado)
                            MostrarTabuleiro();

                        Console.WriteLine($"Jogador {jogadorAtual} ({tipoJogador}) ganhou em {jogadas} jogadas!");
                        Console.WriteLine($"Jogador {jogadorPerdedor} perdeu!");
                    }
                }
            }

            // Verificar se ganhou em coluna
            if (tamanhoTabuleiro == 3)
            {
                for (int i = 0; i < 3; i++)
                {
                    if ((tabuleiro[0, i] == tipoJogadorComEspacos && tabuleiro[1, i] == tipoJogadorComEspacos && tabuleiro[2, i] == tipoJogadorComEspacos))
                    {
                        jogoTerminado = true;
                        AtualizarHighscores(jogadorAtual);

                        if (jogoTerminado)
                            MostrarTabuleiro();

                        Console.WriteLine($"Jogador {jogadorAtual} ({tipoJogador}) ganhou em {jogadas} jogadas!");
                        Console.WriteLine($"Jogador {jogadorPerdedor} perdeu!");
                    }
                }
            }
            else if (tamanhoTabuleiro == 5)
            {
                for (int i = 0; i < 5; i++)
                {
                    if ((tabuleiro[0, i] == tipoJogadorComEspacos && tabuleiro[1, i] == tipoJogadorComEspacos && tabuleiro[2, i] == tipoJogadorComEspacos && tabuleiro[3, i] == tipoJogadorComEspacos && tabuleiro[4, i] == tipoJogadorComEspacos))
                    {
                        jogoTerminado = true;
                        AtualizarHighscores(jogadorAtual);

                        if (jogoTerminado)
                            MostrarTabuleiro();

                        Console.WriteLine($"Jogador {jogadorAtual} ({tipoJogador}) ganhou em {jogadas} jogadas!");
                        Console.WriteLine($"Jogador {jogadorPerdedor} perdeu!");
                    }
                }
            }
            else if (tamanhoTabuleiro == 7)
            {
                for (int i = 0; i < 7; i++)
                {
                    if ((tabuleiro[0, i] == tipoJogadorComEspacos && tabuleiro[1, i] == tipoJogadorComEspacos && tabuleiro[2, i] == tipoJogadorComEspacos && tabuleiro[3, i] == tipoJogadorComEspacos && tabuleiro[4, i] == tipoJogadorComEspacos && tabuleiro[5, i] == tipoJogadorComEspacos && tabuleiro[6, i] == tipoJogadorComEspacos))
                    {
                        jogoTerminado = true;
                        AtualizarHighscores(jogadorAtual);

                        if (jogoTerminado)
                            MostrarTabuleiro();

                        Console.WriteLine($"Jogador {jogadorAtual} ({tipoJogador}) ganhou em {jogadas} jogadas!");
                        Console.WriteLine($"Jogador {jogadorPerdedor} perdeu!");
                    }
                }
            }

            // Verificar se ganhou em diagonal
            if (tamanhoTabuleiro == 3)
            {
                if ((tabuleiro[0, 0] == tipoJogadorComEspacos && tabuleiro[1, 1] == tipoJogadorComEspacos && tabuleiro[2, 2] == tipoJogadorComEspacos) ||
                    (tabuleiro[0, 2] == tipoJogadorComEspacos && tabuleiro[1, 1] == tipoJogadorComEspacos && tabuleiro[2, 0] == tipoJogadorComEspacos))
                {
                    jogoTerminado = true;
                    AtualizarHighscores(jogadorAtual);

                    if (jogoTerminado)
                        MostrarTabuleiro();

                    Console.WriteLine($"Jogador {jogadorAtual} ({tipoJogador}) ganhou em {jogadas} jogadas!");
                    Console.WriteLine($"Jogador {jogadorPerdedor} perdeu!");
                }
            }
            else if (tamanhoTabuleiro == 5)
            {
                if ((tabuleiro[0, 0] == tipoJogadorComEspacos && tabuleiro[1, 1] == tipoJogadorComEspacos && tabuleiro[2, 2] == tipoJogadorComEspacos && tabuleiro[3, 3] == tipoJogadorComEspacos && tabuleiro[4, 4] == tipoJogadorComEspacos) ||
                    (tabuleiro[0, 4] == tipoJogadorComEspacos && tabuleiro[1, 3] == tipoJogadorComEspacos && tabuleiro[2, 2] == tipoJogadorComEspacos && tabuleiro[3, 1] == tipoJogadorComEspacos && tabuleiro[4, 0] == tipoJogadorComEspacos))
                {
                    jogoTerminado = true;
                    AtualizarHighscores(jogadorAtual);

                    if (jogoTerminado)
                        MostrarTabuleiro();

                    Console.WriteLine($"Jogador {jogadorAtual} ({tipoJogador}) ganhou em {jogadas} jogadas!");
                    Console.WriteLine($"Jogador {jogadorPerdedor} perdeu!");
                }
            }
            else if (tamanhoTabuleiro == 7)
            {
                if ((tabuleiro[0, 0] == tipoJogadorComEspacos && tabuleiro[1, 1] == tipoJogadorComEspacos && tabuleiro[2, 2] == tipoJogadorComEspacos && tabuleiro[3, 3] == tipoJogadorComEspacos && tabuleiro[4, 4] == tipoJogadorComEspacos && tabuleiro[5, 5] == tipoJogadorComEspacos && tabuleiro[6, 6] == tipoJogadorComEspacos) ||
                    (tabuleiro[0, 6] == tipoJogadorComEspacos && tabuleiro[1, 5] == tipoJogadorComEspacos && tabuleiro[2, 4] == tipoJogadorComEspacos && tabuleiro[3, 3] == tipoJogadorComEspacos && tabuleiro[4, 2] == tipoJogadorComEspacos && tabuleiro[5, 1] == tipoJogadorComEspacos && tabuleiro[6, 0] == tipoJogadorComEspacos))
                {
                    jogoTerminado = true;
                    AtualizarHighscores(jogadorAtual);

                    if (jogoTerminado)
                        MostrarTabuleiro();

                    Console.WriteLine($"Jogador {jogadorAtual} ({tipoJogador}) ganhou em {jogadas} jogadas!");
                    Console.WriteLine($"Jogador {jogadorPerdedor} perdeu!");
                }
            }
        }

        static void VerificarEmpate()
        {
            // Verificar se houve empate (todas as posiçoes preenchidas) e se o jogo ainda nao terminou
            if (jogadas == tamanhoTabuleiro * tamanhoTabuleiro && !jogoTerminado)
            {
                jogoTerminado = true;

                if (jogoTerminado)
                    MostrarTabuleiro();

                Console.WriteLine("O jogo empatou");
            }
        }

        static void TrocarJogador()
        {
            if (tipoJogador == "X")
                tipoJogador = "O";
            else
                tipoJogador = "X";
        }

        static void JogoDoGalo()
        {

            // Inicializar variaveis (necessario para novo jogo)
            jogoTerminado = false;
            jogadas = 0;
            tipoJogador = "X";
            tabuleiro = new string[tamanhoTabuleiro, tamanhoTabuleiro];

            // Pedir o nome dos jogadores e so valida apos preenchido
            do
            {
                Console.Clear();
                Console.WriteLine("\nQual o nome do jogador X?");
                nomeJogadorX = Console.ReadLine().Trim();
            } while (string.IsNullOrEmpty(nomeJogadorX));
            do
            {
                Console.Clear();
                Console.WriteLine("\nQual o nome do jogador O?");
                nomeJogadorO = Console.ReadLine().Trim();
            } while (string.IsNullOrEmpty(nomeJogadorO));


            // Definir o tabuleiro
            for (int i = 0; i < tabuleiro.GetLength(0); i++)
            {
                for (int j = 0; j < tabuleiro.GetLength(1); j++)
                    tabuleiro[i, j] = " - ";
            }

            do
            {
                MostrarTabuleiro();

                // Pedir ao jogador as posiçoes
                int x, y;
                Console.WriteLine($"Jogador {tipoJogador}, qual a linha a jogar (1 a {tamanhoTabuleiro})?");
                Int32.TryParse(Console.ReadLine(), out x); // Validar se é um número e se não contem valor nulo

                Console.WriteLine($"Jogador {tipoJogador}, qual a coluna a jogar (1 a {tamanhoTabuleiro})?");
                Int32.TryParse(Console.ReadLine(), out y);

                // Validar se x ou y sao permitidos e converter para os intervalos corretos da array
                ValidarJogada(x - 1, y - 1);

            } while (!jogoTerminado);

            Console.ReadKey();
        }

        static void AtualizarHighscores(string vencedor)
        {
            FileInfo fi_highscores = new FileInfo(@"C:\ficheiros_cite\jogogalo_highscores.txt");
            StreamWriter fw = fi_highscores.AppendText();

            // Escrever no ficheiro o nome do jogador vencedor
            fw.WriteLine(vencedor);
            fw.Close();
        }

        static void VerHighscores()
        {
            Console.Clear();
            Console.WriteLine("\n");
            Console.WriteLine("  HIGHSCORES - TOP 10 VITORIAS");
            Console.WriteLine("────────────────────────────────\n");

            StreamReader fr = new StreamReader(@"C:\ficheiros_cite\jogogalo_highscores.txt", Encoding.GetEncoding("utf-8"));

            // Limpar a lista para evitar dados cumulativos
            lst_JogadorVencedores.Clear();

            string linha;
            // Ler cada linha do ficheiro
            while ((linha = fr.ReadLine()) != null)
            {
                // Verificar se o jogador ja esta na lista de jogadores vencedores
                JogadorVencedor jogadorExistente = lst_JogadorVencedores.Find(x => x.NomeJogador == linha);

                // Se estiver na lista incrementar vitorias
                if (jogadorExistente != null)
                    jogadorExistente.Vitorias++;
                else
                {
                    // Se nao estiver na lista adiciona
                    JogadorVencedor novoJogador = new JogadorVencedor();
                    novoJogador.adicionarJogadorVencedor(linha, 1);
                    lst_JogadorVencedores.Add(novoJogador);
                }
            }

            // Mostrar o top 10 dos jogadores com mais vitorias e por ordem decrescente
            lst_JogadorVencedores = lst_JogadorVencedores.OrderByDescending(x => x.Vitorias).Take(10).ToList();
            foreach (JogadorVencedor jogador in lst_JogadorVencedores)
            {
                if (jogador.Vitorias == 1)
                    Console.WriteLine($"{jogador.NomeJogador}: {jogador.Vitorias} vitoria");
                else
                    Console.WriteLine($"{jogador.NomeJogador}: {jogador.Vitorias} vitorias");
            }
            fr.Close();

            Console.WriteLine("\n\nPrima qualquer tecla para voltar ao menu inicial");
            Console.ReadKey();
        }

        static void NivelDificuldade()
        {
            // Obter o nivel de dificuldade
            string nivelDificuldade = "";
            if (tamanhoTabuleiro == 3)
                nivelDificuldade = "Fácil";
            else if (tamanhoTabuleiro == 5)
                nivelDificuldade = "Intermédio";
            else if (tamanhoTabuleiro == 7)
                nivelDificuldade = "Difícil";

            int op = 0;
            while (op != 4)
            {
                Console.Clear();
                Console.WriteLine($"\n  NIVEL DIFICULDADE ATUAL: {nivelDificuldade}");
                Console.WriteLine("────────────────────────────────\n");
                Console.WriteLine("  1. Facil: Tabuleiro 3x3");
                Console.WriteLine("  2. Intermédio: Tabuleiro 5x5");
                Console.WriteLine("  3. Dificil: Tabuleiro 7x7");
                Console.WriteLine("  4. Voltar ao menu");
                Console.WriteLine("\n────────────────────────────────");
                Console.Write("\n  Escolha uma opção: ");
                Int32.TryParse(Console.ReadLine(), out op); // Validar se é um número e se não contem valor nulo

                switch (op)
                {
                    case 1:
                        tamanhoTabuleiro = 3;
                        Console.WriteLine("\n Dificuldade definida para Fácil!");
                        Console.WriteLine("\n Prima qualquer tecla para voltar ao menu inicial.");
                        Console.ReadKey();
                        Menu();
                        break;
                    case 2:
                        tamanhoTabuleiro = 5;
                        Console.WriteLine("\n Dificuldade definida para Intermédio!");
                        Console.WriteLine("\n Prima qualquer tecla para voltar ao menu inicial.");
                        Console.ReadKey();
                        Menu();
                        break;
                    case 3:
                        tamanhoTabuleiro = 7;
                        Console.WriteLine("\n Dificuldade definida para Dificil!");
                        Console.WriteLine("\n Prima qualquer tecla para voltar ao menu inicial.");
                        Console.ReadKey();
                        Menu();
                        break;
                    case 4:
                        Menu();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n  Opção inválida. Escolha outra opção.");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;
                }
            }
            Console.WriteLine("\n Prima qualquer tecla para voltar ao menu inicial.");
            Console.ReadKey();
        }

        static void Menu()
        {
            int op = 0;
            while (op != 4)
            {
                Console.Clear();
                Console.WriteLine("\n");
                Console.WriteLine("  MENU");
                Console.WriteLine("────────────────────────────────\n");
                Console.WriteLine("  1. Novo Jogo");
                Console.WriteLine("  2. Ver Highscores");
                Console.WriteLine("  3. Alterar Nivel Dificuldade");
                Console.WriteLine("  4. Sair");
                Console.WriteLine("\n────────────────────────────────");
                Console.Write("\n  Escolha uma opção: ");
                Int32.TryParse(Console.ReadLine(), out op); // Validar se é um número e se não contem valor nulo

                switch (op)
                {
                    case 1:
                        JogoDoGalo();
                        break;
                    case 2:
                        VerHighscores();
                        break;
                    case 3:
                        NivelDificuldade();
                        break;
                    case 4:
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n  Opção inválida. Escolha outra opção.");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;
                }
            }
        }


        class JogadorVencedor
        {
            public void adicionarJogadorVencedor(string jogador, int vitorias)
            {
                NomeJogador = jogador;
                Vitorias = vitorias;
            }

            public string NomeJogador { get; set; }
            public int Vitorias { get; set; }
        }
    }
}
