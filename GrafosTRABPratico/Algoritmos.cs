using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafosTRABPratico
{
    public class Algoritmos 
    {

        //TEM QUE VERIFICAR PRA VER O QUE CADA ALGORTIMO REALMENTE VAI RETORNAR (SE RETORNA UM INT COM INFORMAÇÃO ESPECIFICA OU UMA STRING COM VÁRIAS INFORMAÇÕES POR EXEMPLO)


        /// <summary>
        /// Método que implementa o algoritmo de Djksita para encontrar o roteamento de menor custo entra vertices
        /// </summary>
        /// <param name="grafo">Grafo</param>
        /// <param name="inicio">Vertice de inicio</param>
        /// <param name="destino">Vertice de destino</param>
        /// <returns></returns>
        public List<Hub> RoteamentoMenorCusto(Grafo grafo, int inicio, int destino)//Dijkstra
        {
            
            //Adiciona todas as rotas(o grafo em geral)
            Dictionary<Hub, List<Rota>> Rotas = grafo.GetRotas;

            Hub source = grafo.GetSourceByID(inicio);//vertice inicio
            Hub final = grafo.GetSourceByID(destino);//vertice destino 

            Dictionary<Hub,double> Distancias = new Dictionary<Hub,double>();//distancias dos vertices

            Dictionary<Hub, Hub> Predecessores = new Dictionary<Hub, Hub>();//predecessores dos vertices

            List<Hub> Visitados = new List<Hub>();//vertices ja visitados

            //Inicializar as distâncias, predecessores e visitados conforme o pseudocodigo
            foreach (Hub vertice in Rotas.Keys )
            {
                Distancias[vertice] = double.MaxValue;
                Predecessores[vertice] = null;
            }

            Visitados.Add(source);//adiciona o source ao array de visitados
            Distancias[source] = 0;// inicia a distância da origem como 0

            
            for (int i = 1; i<= Rotas.Count - 1; i++)//execução do algoritmo
            {
                double menorDistancia = double.MaxValue;
                Hub melhorOrigem = null;
                Hub melhorDestino = null;


                //acha aresta com a menor distancia + peso
                foreach(Hub vertice in Visitados)
                {
                    foreach(Rota rota in Rotas[vertice])
                    {
                        Hub w = rota.GetDestino();
                        double peso =rota.GetPeso();

                        if (!Visitados.Contains(w))
                        {
                            double valor = Distancias[vertice] + peso;

                            if(valor < menorDistancia)
                            {
                                menorDistancia = valor;
                                melhorDestino = w;
                                melhorOrigem = vertice;
                            }
                        }
                    }
                }

                //Relaxamento 

                Distancias[melhorDestino] = Distancias[melhorOrigem] + Rotas[melhorOrigem].Find(r => r.GetDestino() == melhorDestino).GetPeso();
                Predecessores[melhorDestino] = melhorOrigem;

                Visitados.Add(melhorDestino);

                if(melhorDestino == final)
                {
                    i = Rotas.Count;
                }


                
            }

            //Construção da lista de caminho

            List<Hub> caminhoMinimo = new List<Hub>();
            Hub atual = final;

            while (atual != null)
            {
                caminhoMinimo.Add(atual);
                atual = Predecessores[atual];
            }

            caminhoMinimo.Reverse();

            return caminhoMinimo;

        }

        public void BellmanFord()
        {

        }

        public void Kruskal()
        {

        }
        public void Prim()
        {

        }
        //tem que ver qual vai usar pra coloração (botei nome generico)
        public void Coloracao()
        {

        }

        public void CircuitoEuleriano()
        {

        }
        public void CircuitoHamiltoniano()
        {

        }
        //talvez ainda precise de mais
    }
}
