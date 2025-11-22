using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafosTRABPratico
{
    public class Grafo
    {
        //CLASSE PADRÃO PRA REPRESENTAR UM GRAFO
        //REPRESENTAÇÃO VISUAL POR LISTA DE ADJACENCIA OU MATRIZ    *LISTA PARA GRAFO ESPERSO / MATRIZ PARA GRAFO DENSO
        //TODA VEZ QUE MUDAR TEM QUE VERIFICAR SE É ESPARSO OU DENSO

        //se é matriz ou lista (denso ou esparso)   "matriz" ou "lista"
        private string _tipoRepresentacao;

        //dicionario dos vértices
        private Dictionary<int, Hub> _hubs;

        //variaveis de representação (rotas/arestas) 
        private Rota[,] _matrizADJ;
        private Dictionary<Hub, List<Rota>> _listaADJ;
        private int _qntdVertice;
        private int _qntdAresta;
        
        public Grafo()
        {
            Hub.Resetar();
            _hubs = new Dictionary<int, Hub>();
            _tipoRepresentacao = "lista";
            _listaADJ = new Dictionary<Hub, List<Rota>>();
        }

        public void CarregarArquivo(string caminho)
        {
            
            //vetor de todas as linhas do arquivo
            string[] linhasArquivo = File.ReadAllLines(caminho);
            //vetor que armazena a 1 linha do arquivo (que contem num de vertices e arestas)
            string[] linhaCabecalho = linhasArquivo[0].Split(' ');

            _qntdVertice = int.Parse(linhaCabecalho[0]);
            _qntdAresta = int.Parse(linhaCabecalho[1]);

            //VE SE É DENSO OU ESPARSO E ATUALIZA NO ATRIBUTO
            AtualizarRepresentacao();

            //ADICIONA OS VÉRTICES NO GRAFO
            for (int i = 0; i < _qntdVertice; i++)
            {
                Hub h = new Hub();
                _hubs.Add(h.ID(), h);
            }

            for (int j = 1; j <= _qntdVertice; j++)
            {
                Console.WriteLine("J: " + j);
                Console.WriteLine($"OK : {_hubs[j].ID()}");
                _listaADJ.Add(_hubs[j], new List<Rota>());
            }
            _matrizADJ = new Rota[_qntdVertice + 1, _qntdVertice + 1];


            //ADICIONA AS ARESTAS NO GRAFO DEPENDENDO DA REPRESENTACAO
            //começa a partir da 2 linha (a 1 é de cabecalho)
            for (int i = 1; i < linhasArquivo.Length; i++)
            {
                //separa cada elemento da linha em vetor
                string[] linhaParte = linhasArquivo[i].Split(' ');

                //1 numero = vertice de origem
                //2 numero = vertice de destino
                //3 numero = peso da aresta
                //4 numero = capacidade da aresta
                int verticeOrigem = int.Parse(linhaParte[0]);
                int verticeDestino = int.Parse(linhaParte[1]);
                double peso = int.Parse(linhaParte[2]);
                double capacidade = int.Parse(linhaParte[3]);

                //pega os vertices baseado no que foi falado no dimacs
                Hub origem = _hubs[verticeOrigem];
                Hub destino = _hubs[verticeDestino];

                Rota rota = new Rota(origem, destino, peso, capacidade);

                //se for matriz adiciona na matriz, se for lista adiciona na lista


                if (_tipoRepresentacao == "matriz")
                {
                    _matrizADJ[verticeOrigem, verticeDestino] = rota;
                }
                else
                {
                    _listaADJ[_hubs[verticeOrigem]].Add(rota);
                }
            }
            
        }

        private double CalcularDensidade()
        {
            //A CONTA É -       QUANTIDADE DE ARESTAS / QUANTIDADE DE VERTICES * (QUANTIDADE DE VERTICES - 1)
            //SO SERVE PRA GRAFO DIRECIONADO
           return _qntdAresta / (_qntdVertice * (_qntdVertice - 1));
        }

        public void AtualizarRepresentacao()
        {
            //DENSIDADE IGUAL OU MAIOR QUE 0.5 = DENSO (MATRIZ)
            //DENSIDADE MENOR QUE 0.5 = ESPARSO (LISTA)
            if (CalcularDensidade() >= 0.5)
            {
                _tipoRepresentacao = "matriz";
            }
            else
            {
                _tipoRepresentacao = "lista";
            }
        }

        public void VisualizarGrafo()
        {
           
            if (_tipoRepresentacao == "matriz")
            {
                Console.WriteLine("\nMATRIZ DE ADJACENCIA");

                int linhas = _matrizADJ.GetLength(0); // número de linhas
                int colunas = _matrizADJ.GetLength(1); // número de colunas

                Console.Write("   ");
                for (int j = 0; j < colunas; j++)
                {
                    Console.Write(j + "  ");
                }
                Console.WriteLine();

                Console.Write("  ");
                for (int j = 0; j < colunas; j++)
                {
                    Console.Write("---");
                }
                Console.WriteLine();

                for (int i = 0; i < linhas; i++)
                {
                    Console.Write(i + "| "); // número da linha
                    for (int j = 0; j < colunas; j++)
                    {
                        
                        if (_matrizADJ[i, j] == null)
                        {
                            Console.Write("0  "); // imprime elemento com tabulação
                        }
                        else
                        {
                            Console.Write(_matrizADJ[i, j].GetPeso() + "  "); // imprime elemento com tabulação
                        }
                    }
                    Console.WriteLine(); // quebra de linha ao fim de cada linha
                }
                Console.ReadKey(true);
            }
            else
            {
                Console.WriteLine("\nLISTA DE ADJACENCIA");

                
                
                foreach (KeyValuePair<Hub, List<Rota>> rotas in _listaADJ)
                {
                    Console.Write(rotas.Key.ID() + ": ");

                    foreach (Rota rota in rotas.Value)
                    {
                        Console.Write(rota.GetPeso() + " ");
                    }

                    Console.WriteLine();

                }
                Console.ReadKey(true);
            }
        }
        private void CarregarMatriz()
        {
           
        }
    }
}
