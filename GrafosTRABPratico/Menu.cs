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
                Console.WriteLine("5 - Rota Única de Inspeção");
                Console.WriteLine("0 - Voltar");
                Console.WriteLine("==============================================");

                Console.Write("Selecione uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        Console.WriteLine($"Qual o hub (vertice) de origem?");
                        int origem = int.Parse(Console.ReadLine());

                        Console.WriteLine($"Qual o hub (vertice) de destino?");
                        int destino = int.Parse(Console.ReadLine());

                        List<Hub> roteamento = _sorl.Agoritmos.RoteamentoMenorCusto(_sorl.Grafo, origem, destino);

                        if (roteamento != null)
                        {
                            Console.WriteLine($"O Rotemanto de menor custo entre o HUB {origem} e o HUB {destino} é passando por: ");
                            foreach (Hub h in roteamento)
                            {
                                Console.Write($"{h.ID()} ");
                            }
                        }

                        
                        Console.ReadKey();
                        Console.WriteLine();
                        break;

                    case "2":
                        Console.WriteLine($"Qual o hub (vertice) de origem?");
                        int origem2 = int.Parse(Console.ReadLine()); 

                        Console.WriteLine($"Qual o hub (vertice) de destino?");
                        int destino2 = int.Parse(Console.ReadLine());

                        string resultado = _sorl.Agoritmos.FluxoMaximoMinimoCorte(_sorl.Grafo, origem2, destino2);

                        if (resultado != null)
                        {
                            Console.WriteLine(resultado);
                        }
                        Console.ReadKey(true);
                        break;

                    case "3":
                        Console.WriteLine("A Solução ótima para expansão é: ");
                        Grafo RotaUnica = _sorl.Agoritmos.RotaUnica(_sorl.Grafo);

                        RotaUnica.VisualizarGrafo();
                        Console.ReadKey();
                        Console.WriteLine();
                        break;

                    case "4":
                        Console.WriteLine("Agendador de manuteções:");

                        Console.WriteLine("Rotas de manutenção sem conflito\n");
                        Console.WriteLine("Turnos:");

                        Dictionary<Rota, int> coresPorAresta = _sorl.Agoritmos.AgendamentoManutencoes(_sorl.Grafo);

                        var turnos = coresPorAresta.GroupBy(kvp => kvp.Value).OrderBy(g => g.Key);

                        foreach (var turno in turnos)
                        {
                            Console.WriteLine($"Turno {turno.Key}:");
                            foreach (var kvp in turno)
                            {
                                var r = kvp.Key;
                                Console.WriteLine($"  Rota {r.GetOrigem().ID()}-{r.GetDestino().ID()}");
                            }
                        }

                        int numeroTurnos = turnos.Count();
                        Console.WriteLine($"Número de turnos = {numeroTurnos}");

                        Console.ReadKey(true);
                        break;

                    case "5":
                        Console.WriteLine("As rotas únicas de inspeção são");
                        Console.WriteLine("Visitando todas os Hubs uma única vez:");
                        List<Hub> caminhoHamiltoniano = _sorl.Agoritmos.CircuitoHamiltoniano(_sorl.Grafo);
                        if (caminhoHamiltoniano != null)
                        {
                            foreach (Hub h in caminhoHamiltoniano)
                            {
                                Console.Write($"{h.ID()} ");
                            }
                        }
                        else
                        {
                            Console.WriteLine("CICLO DETECTADO: não existe Rota única");
                        }

                        Console.WriteLine();


                        Console.WriteLine("Visitando todas as Rotas uma única vez:");
                        List<Rota> caminho = _sorl.Agoritmos.CircuitoEuleriano(_sorl.Grafo);

                        foreach (Rota r in caminho)
                        {
                            Console.Write($"{r.GetOrigem().ID()} -> {r.GetDestino().ID()} | ");
                        }


                        Console.ReadKey(true);
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
                Console.WriteLine("3 - Adicionar Nova ROTA (ARESTA)");
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

                    case "2":
                        if (_sorl.AdicionarHub())
                        {
                            Console.WriteLine($"O vértice {_sorl.QuantidadeVertices()} foi adicionado.");
                        }
                        
                        Console.ReadKey(true);
                        break;

                    case "3":

                        Console.WriteLine($"Qual o hub (vertice) de origem?");
                        int origem = int.Parse(Console.ReadLine());

                        Console.WriteLine($"Qual o hub (vertice) de destino?");
                        int destino = int.Parse(Console.ReadLine());

                        Console.WriteLine($"Qual o peso da rota (aresta)?");
                        double peso = double.Parse(Console.ReadLine());

                        Console.WriteLine($"Qual a capacidade da rota (aresta)?");
                        double capacidade = double.Parse(Console.ReadLine());


                       if ( _sorl.AdicionarRota(origem, destino, peso, capacidade))
                        {
                            Console.WriteLine($"\nA rota {origem} -- [{peso}] --> {destino} foi adicionada");
                        }
                        Console.ReadKey(true);
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
