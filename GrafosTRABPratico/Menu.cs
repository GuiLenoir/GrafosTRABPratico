using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafosTRABPratico
{
    public class Menu
    {
        //MENU DA APLICAÇÃO

        private SORL _sorl;

        public Menu(SORL sorl)
        {
            _sorl = sorl;
        }

        public void Exibir()
        {
            
            MenuPrincipal();
            
        }

        private void Titulo()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║      ░░████   ░░██░░   ████░░     MÁXIMA LOGÍSTICA S.A.      ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");

            Console.ResetColor();
            Console.WriteLine();
        }

        private void MenuPrincipal()
        {

            while (true)  // repete o menu até o usuário pedir para sair
            {
                Titulo();

                Console.WriteLine("=============== MENU PRINCIPAL ===============");
                Console.WriteLine("1 - Grafos/Dados");
                Console.WriteLine("2 - ");
                Console.WriteLine("3 - Análises");
                Console.WriteLine("0 - Sair");
                Console.WriteLine("==============================================");

                Console.Write("Selecione uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        MenuGrafos();
                        break;

                    case "2":
                        break;

                    case "3":
                        MenuAnalises();
                        break;

                    case "0":
                        Console.WriteLine("\nSaindo...");
                        return;  // encerra o menu

                    default:
                        Console.WriteLine("Opção inválida!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void MenuAnalises()
        {
            while (true)  // repete o menu até o usuário pedir para sair
            {
                Titulo();

                Console.WriteLine("=============== MENU DE ANÁLISES ===============");
                Console.WriteLine("1 - Roteamento de Menor Custo");
                Console.WriteLine("2 - Capacidade Máxima de Escoamento");
                Console.WriteLine("3 - Expansão de Rede de Comunicação");
                Console.WriteLine("4 - Agendamento de Manutenções sem Conflito");
                Console.WriteLine("4 - Rota Única de Inspeção");
                Console.WriteLine("0 - Sair");
                Console.WriteLine("==============================================");

                Console.Write("Selecione uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":

                        break;

                    case "2":
                        break;

                    case "3":
                        break;

                    case "4":
                        break;

                    case "5":
                        break;

                    case "0":
                        Console.WriteLine("\nSaindo...");
                        return;  // encerra o menu

                    default:
                        Console.WriteLine("Opção inválida!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void MenuGrafos()
        {
            while (true)  // repete o menu até o usuário pedir para sair
            {
                Titulo();

                Console.WriteLine("=============== MENU DE GRAFOS ===============");
                Console.WriteLine("1 - Carregar Grafo (DIMACS)");
                Console.WriteLine("2 - Adicionar Novo HUB (VÉRTICE)");
                Console.WriteLine("2 - Adicionar Nova ROTA (ARESTA)");
                Console.WriteLine("0 - Sair");
                Console.WriteLine("==============================================");

                Console.Write("Selecione uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":

                        break;

                    case "0":
                        Console.WriteLine("\nSaindo...");
                        return;  // encerra o menu

                    default:
                        Console.WriteLine("Opção inválida!");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
