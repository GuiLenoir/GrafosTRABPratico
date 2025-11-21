using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


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
                Console.WriteLine("2 - Visualizar Grafo");
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
                        _sorl.VisualizarGrafo();
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
                Console.WriteLine("0 - Voltar");
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
                Console.WriteLine("0 - Voltar");
                Console.WriteLine("==============================================");

                Console.Write("Selecione uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        //INSTANCIA A JANELA DE SELECIONAR
                        OpenFileDialog janela = new OpenFileDialog
                        {
                            //TITULO DA JANELA
                            Title = "Selecione um arquivo DIMACS",
                            //FILTRAR ESCOLHAS E PERSONALIZAÇÃO
                            Filter = "Grafos (*.dimacs)|*.dimacs|Outros arquivos de matriz/lista (*.*)|*.*"
                        };

                        //ABRE A JANELA E SE RESULTADO FOR OK A STRING VIRA O CAMINHO PRO ARQUIVO
                        if (janela.ShowDialog() == DialogResult.OK)
                        {
                            //STRING VIRA O CAMINHO DO ARQUIVO SELECIONADO
                            string caminho = janela.FileName;
                            Console.WriteLine("\nVocê selecionou: " + caminho);

                            
                            Console.ReadKey(true);

                            // Aqui você pode chamar seu leitor DIMACS
                            // var reader = new DimacsReader();
                            // reader.LerArquivo(caminho);
                            _sorl.CarregarGrafo(caminho);
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Nenhum arquivo selecionado.");
                            Console.ReadKey(true);
                        }
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
