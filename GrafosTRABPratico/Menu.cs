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
                Console.WriteLine("1 - Grafos");
                Console.WriteLine("2 - Visualizar Grafo");
                Console.WriteLine("3 - Análises");
                Console.WriteLine("4 - Dados");
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
                        Console.WriteLine(_sorl.VisualizarGrafo());
                        Console.ReadKey(true);
                        break;

                    case "3":
                        MenuAnalises();
                        break;

                        case "4":
                        if (_sorl.GetGrafo() == null)
                        {
                            Console.WriteLine("Não há grafo carregado");
                        }
                        else
                        {
                            int qntVertices = _sorl.GetGrafo().GetQNTDVertices();
                            int qntArestas = _sorl.GetGrafo().GetQNTDArestas() / 2;
                            Console.WriteLine($"Densidade: {(double)qntArestas / (qntVertices * (qntVertices - 1))}");
                            Console.WriteLine($"Número de Vértices: {qntVertices}");
                            Console.WriteLine($"Número de Arestas: {qntArestas}");
                        }
                        Console.ReadKey(true);
                        break;

                    case "0":
                        Console.WriteLine("\nSaindo...");
                        return;  // encerra o menu

                    default:
                        Console.WriteLine("Opção inválida!");
                        Console.ReadKey(true);
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


                        if (_sorl.AdicionarRota(origem, destino, peso, capacidade))
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
                        Console.ReadKey(true);
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

                        List<Hub> roteamento = _sorl.Algoritmos.DijkstraCaminhoMinimo(_sorl.GetGrafo(), origem, destino);

                        if (roteamento != null)
                        {
                            Console.WriteLine($"O Rotemanto de menor custo entre o HUB {origem} e o HUB {destino} é passando por: ");
                            foreach (Hub h in roteamento)
                            {
                                Console.Write($"{h.ID()} ");
                            }
                        }

                        
                        Console.ReadKey(true);
                        Console.WriteLine();
                        break;

                    case "2":
                        Console.WriteLine($"Qual o hub (vertice) de origem?");
                        int origem2 = int.Parse(Console.ReadLine()); 

                        Console.WriteLine($"Qual o hub (vertice) de destino?");
                        int destino2 = int.Parse(Console.ReadLine());

                        string resultado = _sorl.Algoritmos.FluxoMaximoMinimoCorte(_sorl.GetGrafo(), origem2, destino2);

                        if (resultado != null)
                        {
                            Console.WriteLine(resultado);
                        }
                        Console.ReadKey(true);
                        break;

                    case "3":
                        Console.WriteLine("A Solução ótima para expansão é: ");
                        Grafo RotaUnica = _sorl.Algoritmos.PrimAGM(_sorl.GetGrafo());

                        Console.WriteLine(RotaUnica.VisualizarGrafo());
                        
                        Console.ReadKey(true);
                        Console.WriteLine();
                        break;

                    case "4":
                        Console.WriteLine("\nAgendador de manuteções:");

                        Console.WriteLine("Rotas de manutenção sem conflito\n");
                        Console.WriteLine("Turnos:");

                        Dictionary<Rota, int> coresPorAresta = _sorl.Algoritmos.AgendamentoManutencoes(_sorl.GetGrafo());

                        var turnos = coresPorAresta.GroupBy(kvp => kvp.Value).OrderBy(g => g.Key);

                        foreach (var turno in turnos)
                        {
                            Console.WriteLine($"\nTurno {turno.Key}:");
                            foreach (var kvp in turno)
                            {
                                var r = kvp.Key;
                                Console.WriteLine($"  Rota {r.GetOrigem().ID()}-{r.GetDestino().ID()}");
                            }
                        }

                        int numeroTurnos = turnos.Count();
                        Console.WriteLine($"\nNúmero de turnos = {numeroTurnos}");

                        Console.ReadKey(true);
                        break;

                    case "5":
                        
                        List<Hub> caminhoHamiltoniano = _sorl.Algoritmos.CircuitoHamiltoniano(_sorl.GetGrafo());
                        Log logs = _sorl.GetLogs();
                        logs.Limpar();
                        logs.Registrar("Caminho Euleriano | Caminho Hamiltoniano - Rota Única de Inspeção\n");
                        logs.Registrar("Fleury e Ordenação Topológica (DFS)");
                        logs.Registrar("\nAs rotas únicas de inspeção são");
                        logs.Registrar("\nVisitando todas os Hubs uma única vez:");


                        Console.WriteLine("\nAs rotas únicas de inspeção são");
                        Console.WriteLine("\nVisitando todas os Hubs uma única vez:");
                        StringBuilder logger = new StringBuilder();
                        if (caminhoHamiltoniano != null)
                        {
                            foreach (Hub h in caminhoHamiltoniano)
                            {
                                Console.Write($"{h.ID()} ");
                                logger.Append($"{h.ID()} ");
                            }
                        }
                        else
                        {
                            Console.WriteLine("CICLO DETECTADO: não existe Rota única");
                            logger.AppendLine("CICLO DETECTADO: não existe Rota única");
                        }
                        Console.WriteLine();
                        logger.AppendLine();

                        Console.WriteLine("\nVisitando todas as Rotas uma única vez:");
                        logger.AppendLine("\nVisitando todas as Rotas uma única vez:");
                        List<Rota> caminho = _sorl.Algoritmos.CircuitoEuleriano(_sorl.GetGrafo());

                        if (caminho == null)
                        {
                            Console.WriteLine("Não existe Rota única");
                            logger.AppendLine("Não existe Rota única");

                        }
                        else
                        {
                            foreach (Rota r in caminho)
                            {
                                Console.Write($"{r.GetOrigem().ID()} -> {r.GetDestino().ID()} | ");
                                logger.Append($"{r.GetOrigem().ID()} -> {r.GetDestino().ID()} | ");
                            }


                           
                            
                        }
                        logs.Registrar(logger.ToString());
                        logs.Salvar();


                        Console.ReadKey(true);
                        break;

                    case "0":
                        Console.WriteLine("\nSaindo...");
                        return;  // encerra o menu

                    default:
                        Console.WriteLine("Opção inválida!");
                        Console.ReadKey(true);
                        break;
                }
            }
        }

       

        
    }
}
