using Campeonato.Dados;
using Campeonato.Entidade;

namespace Campeonato
{
    internal class Program
    {
        static ManipulacaoBanco mb = new ManipulacaoBanco();
        static bool continuar = true;
        static int timeMaximo = 0, sairAdicionarTime = 1;
        static void EntrarPrimeiraVezPrograma()
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine(">>> CAMPEONATO DE FUTEBOL <<<");
            Console.WriteLine("------------------------------\n");
            Console.WriteLine("Adicione os times do campeonato para prosseguir:\n" +
                "(No mínimo 3, no máximo 5)");
            while (timeMaximo < 5 && sairAdicionarTime != 0)
            {
                bool sairDoIf = false;

                Console.WriteLine($"Adicione o {timeMaximo + 1}º time:");

                Console.WriteLine("Nome:");
                string nomeTime = Console.ReadLine();

                Console.WriteLine("Apelido: ");
                string apelidoTime = Console.ReadLine();

                Console.WriteLine("Data de criação: ");
                DateOnly dt_criacao = DateOnly.Parse(Console.ReadLine());

                mb.AdicionarTimes(new Time(nomeTime, apelidoTime, dt_criacao));
                timeMaximo++;

                Console.WriteLine("Time adicionado com sucesso!");
                Console.ReadKey();
                if (timeMaximo > 2)
                {
                    while (sairDoIf == false)
                    {
                        Console.Clear();
                        Console.WriteLine("Digite 0 para sair ou 1 para continuar:");
                        try
                        {
                            sairAdicionarTime = int.Parse(Console.ReadLine());
                            sairDoIf = true;
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Digite uma opção válida!");
                            Console.ReadKey();
                        }
                    }
                }
                Console.Clear();
            }
            mb.CadastrarJogos();
        }
        static void Menu()
        {
            while (continuar)
            {
                Console.Clear();
                Console.WriteLine("------------------------------");
                Console.WriteLine(">>> CAMPEONATO DE FUTEBOL <<<");
                Console.WriteLine("------------------------------\n" +
                    "Menu:\n" +
                    "1. Mostrar o campeão\n" +
                    "2. Mostrar lista de colocação de times do campeonato\n" +
                    "3. Mostrar o time que mais fez gols no campeonato\n" +
                    "4. Mostrar o time que tomou mais gols no campeonato\n" +
                    "5. Mostrar o jogo com mais gols\n" +
                    "6. Mostrar o maior número de gols que cada time fez em um único jogo\n" + 
                    "7. Mostrar a tabela de todos os jogos\n" +
                    "8. Sair\n" +
                    "------------------------------");

                Console.Write("Escolha uma opção: ");
                string opcao = Console.ReadLine();

                Console.WriteLine();

                switch (opcao)
                {
                    case "1":
                        mb.MostrarColocacaoTimes(1);
                        break;
                    case "2":
                        mb.MostrarColocacaoTimes(2);
                        break;
                    case "3":
                        mb.MostrarColocacaoTimes(3);
                        break;
                    case "4":
                        mb.MostrarColocacaoTimes(4);
                        break;
                    case "5":
                        mb.OperacaoTabelaPartida(1);
                        break;
                    case "6":
                        mb.OperacaoTabelaPartida(2);
                        break;
                    case "7":
                        mb.OperacaoTabelaPartida(3);
                        break;
                    case "8":
                        continuar = false;
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
                Console.ReadKey();
            }
        }
        static void Main(string[] args)
        {
            EntrarPrimeiraVezPrograma();
            Console.Clear();
            Menu();
            mb.ResetarTabelas();
        }
    }
}
