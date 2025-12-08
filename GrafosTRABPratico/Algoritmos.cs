using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
                                                     //tratamento se colocar vertice que não existe



            try
            {
                if (source == null || final == null)
                    throw new Exception("Vértice inicial ou final não existe no grafo.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"ERRO: ({ex.Message})");
                return null;
            }





            Dictionary<Hub, double> Distancias = new Dictionary<Hub, double>();//distancias dos vertices

            Dictionary<Hub, Hub> Predecessores = new Dictionary<Hub, Hub>();//predecessores dos vertices

            List<Hub> Visitados = new List<Hub>();//vertices ja visitados

            //Inicializar as distâncias, predecessores e visitados conforme o pseudocodigo
            foreach (Hub vertice in Rotas.Keys)
            {
                Distancias[vertice] = double.MaxValue;
                Predecessores[vertice] = null;
            }

            Visitados.Add(source);//adiciona o source ao array de visitados
            Distancias[source] = 0;// inicia a distância da origem como 0


            for (int i = 1; i <= Rotas.Count - 1; i++)//execução do algoritmo
            {
                double menorDistancia = double.MaxValue;
                Hub melhorOrigem = null;
                Hub melhorDestino = null;


                //acha aresta com a menor distancia + peso
                foreach (Hub vertice in Visitados)
                {
                    foreach (Rota rota in Rotas[vertice])
                    {
                        Hub w = rota.GetDestino();
                        double peso = rota.GetPeso();

                        if (!Visitados.Contains(w))
                        {
                            double valor = Distancias[vertice] + peso;

                            if (valor < menorDistancia)
                            {
                                menorDistancia = valor;
                                melhorDestino = w;
                                melhorOrigem = vertice;
                            }
                        }
                    }
                }
                try
                {
                    if (melhorDestino == null)
                    {
                        // Nenhuma aresta disponível → grafo desconexo
                        throw new Exception($"Não existe caminho de {source.ID()} até {final.ID()}");
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"ERRO: ({ex.Message})");
                    return null;
                }


                //Relaxamento 

                Distancias[melhorDestino] = Distancias[melhorOrigem] + Rotas[melhorOrigem].Find(r => r.GetDestino() == melhorDestino).GetPeso();
                Predecessores[melhorDestino] = melhorOrigem;

                Visitados.Add(melhorDestino);

                if (melhorDestino == final)
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


        /// <summary>
        /// Algoritmo para encontrar A rota unica para todos os vértices AVG, implementa o algoritmo de prim
        /// </summary>
        /// <param name="grafo"></param>
        /// <returns></returns>
        public Grafo RotaUnica(Grafo grafo)
        {
            Hub raiz = grafo.GetSourceByID(1);//vértice raiz, por padrão o primeiro

            Grafo AGM = new Grafo();//cria o subgrafo da AGM
            List<Hub> HubsAGM = new List<Hub>();//lista de hubs da agm usado para percorrer

            Dictionary<Hub, List<Rota>> Rotas = grafo.GetRotas;//rotas do grafo original

            HubsAGM.Add(raiz);//add a raiz na lista de hubs
            AGM.AddVerticeEspecifico(raiz);//add a raiz na AGM


            while(AGM.GetRotas.Count <= Rotas.Count)
            {
                double menorCusto = double.MaxValue;
                Rota melhorAresta = null;//aresta a ser selecionada
                Hub melhorOrigem = null;
                Hub melhorDestino = null;

                foreach(Hub v in HubsAGM)//Percorre os hubs incluidos na AGM
                {
                    foreach(Rota rota in Rotas[v])//Percorre as rotas do grafo original
                    {
                        Hub destino = rota.GetDestino();

                        if (!HubsAGM.Contains(destino))//se destino não estiver nos hubs da AGM fazemos a verificação de custo e adicionamos
                        {
                            if (rota.GetPeso() < menorCusto)
                            {
                                menorCusto = rota.GetPeso();
                                melhorAresta = rota;
                                melhorOrigem = v;
                                melhorDestino = destino;
                                
                            }
                        }
                    }
                }

                if(melhorAresta == null)
                {
                    return AGM;//Se não ouver melhor aresta a execução termina
                }

                AGM.AddVerticeEspecifico(melhorDestino);//add destino na AVG
                HubsAGM.Add(melhorDestino);//add destino na lista de hubs

                if (!AGM.GetRotas.ContainsKey(melhorOrigem))//se não ouver o hub na lista de rotas da AGM ele cira
                {
                    AGM.AddHubRota(melhorOrigem);
                }

                AGM.AddAresta(melhorOrigem.ID(), melhorDestino.ID(), melhorAresta.GetPeso(), melhorAresta.GetCapacidade());//Adiciona a rota ao vértice

            }

            return AGM;
        }

        //tem que ver qual vai usar pra coloração (botei nome generico)
        public void Coloracao()
        {

        }

        public void CircuitoEuleriano()
        {

        }
        public List<Hub> CircuitoHamiltoniano(Grafo grafo)
        {
            Dictionary<Hub, List<Rota>> Rotas = grafo.GetRotas;
            Dictionary<Hub, int> Marcas = new Dictionary<Hub, int>();

            bool isCicle = false;

            foreach(Hub hub in Rotas.Keys)
            {
                Marcas[hub] = 0;
            }

            List<Hub> OrdenacaoTopologica = new List<Hub>();

            foreach(Hub hub in Rotas.Keys)
            {
                if (Marcas[hub] == 0 && isCicle == false)
                {
                    isCicle = Visitar(hub, Rotas, Marcas, OrdenacaoTopologica);
                }else if(isCicle == true)
                {
                    break;
                }
            }
            if (isCicle == true)
            {
                OrdenacaoTopologica = null;
            }
            return OrdenacaoTopologica;
        }

        private bool Visitar(Hub hub, Dictionary<Hub, List<Rota>> Rotas, Dictionary<Hub, int> Marcas, List<Hub> OrdenacaoTopologica)
        {
            if (Marcas[hub] != 2)
            {
                if (Marcas[hub] == 1)
                {
                    Console.WriteLine("CICLO DETECTADO: não existe ordenação topológica.");
                    return true;
                }

                Marcas[hub] = 1;


                foreach(Rota rota in Rotas[hub])
                {
                    Hub destino = rota.GetDestino();
                    Visitar(destino, Rotas, Marcas, OrdenacaoTopologica);
                }


                Marcas[hub] = 2;

                OrdenacaoTopologica.Insert(0,hub);
            }

            return false;
        }
        //talvez ainda precise de mais
    }
}
