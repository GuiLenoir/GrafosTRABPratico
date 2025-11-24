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
            //sempre reseta os IDs staticos dos vertices ao contruir/refazer o grafo
            Hub.Resetar();
            _hubs = new Dictionary<int, Hub>();

            //começa como lista já que é mais comum
            _tipoRepresentacao = "lista";

            //inicializa a lista de uma vez
            _listaADJ = new Dictionary<Hub, List<Rota>>();
        }

        //metodo pra construir a lista
        private Dictionary<Hub, List<Rota>> InicializarLista()
        {
            //inicializa a lista
            Dictionary < Hub, List < Rota >> lista = new Dictionary<Hub, List<Rota>>();

            foreach (Hub hub in _hubs.Values)
            {
                lista.Add(hub, new List<Rota>());
            }

            return lista;
        }

        //metodo pra construir a matriz
        private Rota[,] InicializarMatriz()
        {
            //inicializa a matriz
            Rota[,] matriz = new Rota[_qntdVertice, _qntdVertice];
            return matriz;
        }
        public void CarregarArquivo(string caminho)
        {
            
            //vetor de todas as linhas do arquivo
            string[] linhasArquivo = File.ReadAllLines(caminho);
            //vetor que armazena a 1 linha do arquivo (que contem num de vertices e arestas)
            string[] linhaCabecalho = linhasArquivo[0].Split(' ');

            int quantVertices = int.Parse(linhaCabecalho[0]);
            _qntdAresta = int.Parse(linhaCabecalho[1]);

            //VE SE É DENSO OU ESPARSO E ATUALIZA NO ATRIBUTO
            

            //ADICIONA OS VÉRTICES NO GRAFO
            for (int i = 0; i < quantVertices; i++)
            {
                CarregarVertice();
            }

            //atualiza a representação de acordo com a densidade
            AtualizarRepresentacao();

            if (_tipoRepresentacao == "lista")
            {
                //se densidade baixa inicializa a lista
                _listaADJ = InicializarLista();
            }
            else
            {
                //se densidade alta inicializa a matriz
                _matrizADJ = InicializarMatriz();
            }
            


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

                //metodo que carrega as arestas (diferente do adicionar arestas)
                CarregarAresta(verticeOrigem, verticeDestino, peso, capacidade);
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

                //só formatação daq pra baixo
                Console.Write("   ");
                for (int j = 0; j < colunas; j++)
                {
                    Console.Write(j+1 + "  ");
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
                    Console.Write(i+1 + "| "); // número da linha
                    for (int j = 0; j < colunas; j++)
                    {
                        
                        if (_matrizADJ[i, j] == null)
                        {
                            Console.Write("0  ");
                        }
                        else
                        {
                            Console.Write(_matrizADJ[i, j].GetPeso() + "  ");
                        }
                    }
                    Console.WriteLine(); // quebra de linha
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
        
        private Hub CarregarVertice()
        {
            //adiciona um vertice no dicionario hub
            Hub h = new Hub();
            _hubs.Add(h.ID(), h);
            _qntdVertice++;

            return h;
        }
        public void AddVertice()
        {
            //adiciona no dicionario
            Hub h = CarregarVertice();

            //string pra ver se a representação vai mudar depois
            string mudanca = _tipoRepresentacao;

            //verifica a representação
            AtualizarRepresentacao();

            //bool que indica se houve mudança na representação ou não
            bool mudou = mudanca == _tipoRepresentacao ? true : false;
            
            // se mudou, faz a conversão (que já cria o vertice automaticamente também)
            if (mudou == false)
            {
                if (_tipoRepresentacao == "lista")
                {
                    ConverterMatrizParaLista();
                    
                }
                else
                {
                    ConverterListaParaMatriz();
                }
            }

            //se não houve mudança, só adiciona o vertice sem conversão
            else
            {
                if (_tipoRepresentacao == "lista")
                {
                    _listaADJ.Add(_hubs[h.ID()], new List<Rota>());
                }

                else
                {
                    //pra adicionar um vertice na matriz tem que criar outra e substituir

                    Rota[,] novaMatriz = new Rota[_qntdVertice, _qntdVertice];

                    // copia a matriz antiga e bota na nova
                    for (int i = 0; i < _qntdVertice - 1; i++)
                    {
                        for (int j = 0; j < _qntdVertice - 1; j++)
                        {
                            novaMatriz[i, j] = _matrizADJ[i, j];
                        }
                    }

                    // substitui
                    _matrizADJ = novaMatriz;
                }
            }
        }

        private void ConverterMatrizParaLista()
        {
            Dictionary<Hub, List<Rota>> novaLista = new Dictionary<Hub, List<Rota>>();

            // inicializa lista
            novaLista = InicializarLista();

            
            int linhas = _matrizADJ.GetLength(0); // número de linhas
            int colunas = _matrizADJ.GetLength(1); // número de colunas



            // percorre matriz
            for (int i = 0; i < linhas; i++)
            {
                for (int j = 0; j < colunas; j++)
                {
                    Rota rota = _matrizADJ[i, j];

                    if (rota != null)
                    {
                        Hub origem = _hubs[i + 1];
                        novaLista[origem].Add(rota);
                    }
                }
            }
            

            // substitui
            _listaADJ = novaLista;
        }

        private void ConverterListaParaMatriz()
        {
            // cria nova matriz vazia
            //Rota[,] novaMatriz = new Rota[_qntdVertice, _qntdVertice];

            Rota[,] novaMatriz = InicializarMatriz();

            // para cada hub na lista de adjacência
            foreach (KeyValuePair<Hub, List<Rota>> par in _listaADJ)
            {
                Hub origem = par.Key;
                int idOrigem = origem.ID();

                foreach (Rota rota in par.Value)
                {
                    int idDestino = rota.GetDestino().ID();
                    novaMatriz[idOrigem - 1, idDestino - 1] = rota;
                }
            }

            // substitui estrutura
            _matrizADJ = novaMatriz;
        }

        //ESSE METODO É PRA COLOCAR A ARESTA AUMENTANDO NO CONTADOR
        private void AddAresta(int verticeOrigem, int verticeDestino, double peso, double capacidade)
        {
            CarregarAresta(verticeOrigem, verticeDestino, peso, capacidade);
            _qntdAresta++;
        }

        //ESSE METODO ADICIONA ARESTA SEM AUMENTAR O CONTADOR (APENAS CARREGA A ARESTA DO DIMAC)
        public void CarregarAresta(int verticeOrigem, int verticeDestino, double peso, double capacidade)
        {
            //pega os vertices baseado no que foi falado no dimacs
            Hub origem = _hubs[verticeOrigem];
            Hub destino = _hubs[verticeDestino];

            Rota rota = new Rota(origem, destino, peso, capacidade);

            //se for matriz adiciona na matriz, se for lista adiciona na lista


            if (_tipoRepresentacao == "matriz")
            {
                _matrizADJ[verticeOrigem - 1, verticeDestino - 1] = rota;
            }
            else
            {
                _listaADJ[_hubs[verticeOrigem]].Add(rota);
            }
        }

        public string GetQNTDVertices()
        {
            return $"{_qntdVertice}";
        }
    }
}
