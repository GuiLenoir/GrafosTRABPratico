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
        private string tipoRepresentacao;

        //dicionario dos vértices
        private Dictionary<string, Hub> hubs;

        //variaveis de representação (rotas/arestas) 
        private double[,] matrizADJ;
        private Dictionary<Hub, List<Rota>> listaADJ;
        
        public Grafo()
        {
            hubs = new Dictionary<string, Hub>();
            tipoRepresentacao = "lista";
        }

        public void CarregarArquivo(string caminho)
        {
            string[] linhasArquivo = File.ReadAllLines(caminho);
            int n, m = 0;

            foreach (string linha in linhasArquivo)
            {
                string[] partes = linha.Split(' ');

                if (partes[0] == "p")
                {
                    n = int.Parse(partes[2]); // número de vértices
                    m = int.Parse(partes[3]); // número de arestas

                    // Criar vértices
                    for (int i = 1; i <= n; i++)
                    {
                        // AdicionarVertice("V" + i);
                    }
                }
                else if (partes[0] == "e")
                {
                    int origem = int.Parse(partes[1]);
                    int destino = int.Parse(partes[2]);
                    double custo = double.Parse(partes[3]);
                    double capacidade = double.Parse(partes[4]);

                    //AdicionarAresta(origem, destino, custo, capacidade);
                }

                // Após carregar, decidir representação
                //DefinirRepresentacao();
            }
        }

        public void CalcularDensidade()
        {
            int qntdVertices = hubs.Count;
            //int qntdArestas = 
        }
        private void CarregarMatriz()
        {

        }
    }
}
