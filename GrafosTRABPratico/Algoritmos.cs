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
            Dictionary<Hub, List<Rota>> Rotas;
            if (grafo.GetTipoRepresentacao() == "matriz")
            {             
                Rotas = grafo.GetMatrizPraLista();
            }
            else
            {
                Rotas = grafo.GetRotas;
            }

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

            Dictionary<Hub, List<Rota>> Rotas;
            if (grafo.GetTipoRepresentacao() == "matriz")
            {
                Rotas = grafo.GetMatrizPraLista();
            }
            else
            {
                Rotas = grafo.GetRotas;
            }

            HubsAGM.Add(raiz);//add a raiz na lista de hubs
            AGM.AddVerticeEspecifico(raiz);//add a raiz na AGM


            while (AGM.GetRotas.Count <= Rotas.Count)
            {
                double menorCusto = double.MaxValue;
                Rota melhorAresta = null;//aresta a ser selecionada
                Hub melhorOrigem = null;
                Hub melhorDestino = null;

                foreach (Hub v in HubsAGM)//Percorre os hubs incluidos na AGM
                {
                    foreach (Rota rota in Rotas[v])//Percorre as rotas do grafo original
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

                if (melhorAresta == null)
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


        /// <summary>
        /// Método de implementação do algoritmo de vizing para coloração de arestas (rotas)
        /// </summary>
        /// <param name="grafo">Grafo a ser analizado</param>
        /// <returns>Dicionário com cada rota e sua respectiva cor </returns>
        public Dictionary<Rota, int> AgendamentoManutencoes(Grafo grafo)
        {
            //Pega todas arestas
            List<Rota> arestas = new List<Rota>();

            Dictionary<Hub, List<Rota>> Rotas;
            if (grafo.GetTipoRepresentacao() == "matriz")
            {
                Rotas = grafo.GetMatrizPraLista();
            }
            else
            {
                Rotas = grafo.GetRotas;
            }

            foreach (KeyValuePair<Hub, List<Rota>> rota in Rotas)
            {
                arestas.AddRange(rota.Value);
            }

            //Dicionário para cores por aresta
            Dictionary<Rota, int> cores = new Dictionary<Rota, int>();
               
            //grau máximo do grafo
            int grauMaximo = grafo.GetHubs.Values.Max(h => Grau(grafo, h));

            //Máximo de cores que o grafo pode ter
            int maxCores = grauMaximo + 1;

            //Percorre as arestas para colori-las
            foreach (var aresta in arestas)
            {
                // Conjunto de cores já usadas por arestas adjacentes
                HashSet<int> coresAdj = new HashSet<int>();


                foreach (var rota in arestas)
                {
                    //Verifica adjacencia
                    if (!Object.ReferenceEquals(rota, aresta))
                    {
                        bool adjacentes =
                            rota.GetOrigem() == aresta.GetOrigem() ||
                            rota.GetOrigem() == aresta.GetDestino() ||
                            rota.GetDestino() == aresta.GetOrigem() ||
                            rota.GetDestino() == aresta.GetDestino();

                        if (adjacentes && cores.ContainsKey(rota))
                        {
                            coresAdj.Add(cores[rota]);
                        }
                    }
                }

                //Melhor cor disponivel
                int corEscolhida = Enumerable.Range(1, maxCores)
                                     .FirstOrDefault(c => !coresAdj.Contains(c));

                //Adicina a cor
                cores[aresta] = corEscolhida;
            }

            return cores;
        }

        /// <summary>
        /// Método para olhar grau do vértice
        /// </summary>
        /// <param name="g"></param>
        /// <param name="h"></param>
        /// <returns>Grau do vértice</returns>
        private int Grau(Grafo g ,Hub h)
        {
            if (g.GetRotas.ContainsKey(h))
                return g.GetRotas[h].Count;
            return 0;
        }


        /// <summary>
        /// Algoritmo de Edmonds-Karp para calcular o fluxo máximo entre dois vértices.
        /// </summary>
        public string FluxoMaximoMinimoCorte(Grafo grafo, int origemID, int destinoID)
        {

            Hub origemHub = grafo.GetSourceByID(origemID); //vertice de origem
            Hub destinoHub = grafo.GetSourceByID(destinoID); //vertice de destino
            try { 
            if (origemHub == null || destinoHub == null)
            {
                throw new ArgumentException("Origem ou destino não existem no grafo."); //tratamento de vertices não existentes
                
            }
            }
            catch(Exception ex)
            {
                Console.Error.WriteLine($"ERRO ({ex.Message})");
                Console.Error.WriteLine(ex.ToString());
                return null;
            }

            Dictionary<Hub, List<Rota>> listaADJ;
            if (grafo.GetTipoRepresentacao() == "matriz")
            {
                listaADJ = grafo.GetMatrizPraLista();
            }
            else
            {
                listaADJ = grafo.GetRotas;
            }


            Dictionary<(Hub, Hub), Rota> mapaArestas = new Dictionary<(Hub, Hub), Rota>(); //cria um mapa de arestas que mapeia cada par de vertices (origem, destino) pra sua aresta, facilita pra ver se uma aresta é direta
            //(hub, hub) = tupla, chave composta por dois objetos

            // 1. Inicializar fluxo f(e) = 0 para toda aresta
            foreach (KeyValuePair<Hub, List<Rota>> par in listaADJ)
            {
                Hub verticeOrigem = par.Key; //vertice atual
                foreach (Rota rota in par.Value) //vai percorrer cada aresta que sai desse vértice
                {
                    Hub verticeDestino = rota.GetDestino(); 
                    mapaArestas[(verticeOrigem, verticeDestino)] = rota; // salva a aresta no mapa com a chave tupla (origem, destino)
                    rota.MudarFluxo(0.0); // fluxo inicial = 0
                }
            }

            // 2. Construir rede residual G’(f)
            Dictionary<Hub, Dictionary<Hub, double>> redeResidual = new Dictionary<Hub, Dictionary<Hub, double>>(); //cria a rede residual
            //primeiro dicionario = vertices do grafo
            //segundo dicionario dentro do primeiro = vizinhos desse vertice
            //double é a capacidade residual entre dois vertices

            //se o vertice ainda nao foi adicionado a rede residual, adiciona ele como um dicionario
            void GarantirVerticeResidual(Hub vertice) //garante que cada vertice realmente vai existir na rede residual
            {
                if (!redeResidual.ContainsKey(vertice))
                    redeResidual[vertice] = new Dictionary<Hub, double>(); //pra cada vertice, guarda um dicionario pros seus vizinhos
                
            }

            //inicializa a rede residual a partir das capacidades originais dos vertices
            foreach (KeyValuePair<Hub, List<Rota>> par in listaADJ)
            {
                Hub verticeOrigem = par.Key;
                GarantirVerticeResidual(verticeOrigem);

                foreach (Rota rota in par.Value)
                {
                    Hub verticeDestino = rota.GetDestino();
                    GarantirVerticeResidual(verticeDestino);

                    //capacidade direta é capacidade original
                    redeResidual[verticeOrigem][verticeDestino] = rota.GetCapacidade(); // capacidade original

                    //se for reversa é 0
                    if (!redeResidual[verticeDestino].ContainsKey(verticeOrigem))
                        redeResidual[verticeDestino][verticeOrigem] = 0.0; // reversa começa com 0
                }
            }

            double fluxoMaximo = 0.0;

            // 3. Enquanto existir caminho aumentante P em G’(f)
            while (true)
            {
                // a. Encontrar caminho aumentante com BFS
                Dictionary<Hub, Hub> predecessores = new Dictionary<Hub, Hub>(); //reconstruir o caminho
                HashSet<Hub> visitados = new HashSet<Hub>();
                Queue<Hub> fila = new Queue<Hub>();

                visitados.Add(origemHub);
                fila.Enqueue(origemHub);

                while (fila.Count > 0)
                {
                    Hub verticeAtual = fila.Dequeue();

                    //percorre pelos vizinhos na rede residual
                    foreach (KeyValuePair<Hub, double> vizinho in redeResidual[verticeAtual])
                    {
                        Hub verticeVizinho = vizinho.Key;
                        double capacidadeResidual = vizinho.Value;

                        //só avança se a capacidade residual for > 0 e ainda nao for visitado
                        if (capacidadeResidual > 0 && !visitados.Contains(verticeVizinho))
                        {
                            visitados.Add(verticeVizinho);
                            predecessores[verticeVizinho] = verticeAtual;

                            //chegou ao destino
                            if (verticeVizinho.Equals(destinoHub))
                            {
                                fila.Clear(); // encerra BFS
                                break;
                            }
                            fila.Enqueue(verticeVizinho);
                        }
                    }
                }

                //se nao encontrou destino
                if (!visitados.Contains(destinoHub))
                    break; // não existe mais caminho aumentante

                // b. Δ = min { ur(e) | e ∈ P }
                // Δ = gargalo
                //encontrar a menor capacidade residual (gargalo)
                double gargalo = double.MaxValue;
                Hub verticeCaminho = destinoHub;
                while (!verticeCaminho.Equals(origemHub))
                {
                    Hub verticeAnterior = predecessores[verticeCaminho];
                    gargalo = Math.Min(gargalo, redeResidual[verticeAnterior][verticeCaminho]);
                    verticeCaminho = verticeAnterior;
                }

                // c. Atualizar fluxo e rede residual ao longo do caminho
                verticeCaminho = destinoHub;
                while (!verticeCaminho.Equals(origemHub))
                {
                    Hub verticeAnterior = predecessores[verticeCaminho];

                    // i. Se (v,w) for direta → f(v,w) = f(v,w) + Δ
                    //se aresta (verticeAnterior -> verticeCaminho) é uma aresta que tem no grafo original
                    //aumenta o fluxo dessa aresta em (gargalo)
                    if (mapaArestas.TryGetValue((verticeAnterior, verticeCaminho), out Rota rotaDireta))
                    {
                        rotaDireta.MudarFluxo(rotaDireta.GetFluxo() + gargalo);
                    }

                    // ii. Senão → f(w,v) = f(w,v) – Δ
                    //senão, aresta reversa
                    //diminui o fluxo em (gargalo)
                    else if (mapaArestas.TryGetValue((verticeCaminho, verticeAnterior), out Rota rotaReversa))
                    {
                        rotaReversa.MudarFluxo(rotaReversa.GetFluxo() - gargalo);
                    }

                    // d. Atualizar rede residual
                    //atualiza a rede, diminuindo a capacidade na direção direta e aumentando na direção reversa
                    redeResidual[verticeAnterior][verticeCaminho] -= gargalo;
                    redeResidual[verticeCaminho][verticeAnterior] += gargalo;

                    verticeCaminho = verticeAnterior; //avança
                }

                fluxoMaximo += gargalo; //atualiza o fluxomaximo toda vez que muda
            }

            //corte minimo
            // Conjunto S = vértices alcançáveis a partir da origem na rede residual final
            HashSet<Hub> verticesAlcancaveis = new HashSet<Hub>();
            //fila pra bfs
            Queue<Hub> filaBusca = new Queue<Hub>();

            //começa pela origem
            filaBusca.Enqueue(origemHub);
            verticesAlcancaveis.Add(origemHub);

            //bfs pra encontrar todos os vertices alcançaveis
            while (filaBusca.Count > 0)
            {
                Hub verticeAtual = filaBusca.Dequeue();

                //percorre todos os vizinhos de verticeAtual na rede residual
                foreach (KeyValuePair<Hub, double> vizinhoResidual in redeResidual[verticeAtual])
                {
                    Hub verticeVizinho = vizinhoResidual.Key;
                    double capacidadeResidual = vizinhoResidual.Value;

                    //só segue se a capacidade residual for maior que 0 (se o fluxo ainda pode passar) e se o vertice ainda nao foi visitado
                    if (capacidadeResidual > 0 && !verticesAlcancaveis.Contains(verticeVizinho))
                    {
                        verticesAlcancaveis.Add(verticeVizinho);
                        filaBusca.Enqueue(verticeVizinho);
                    }
                }
            }

            // Conjunto T = vértices não alcançáveis
            List<Rota> arestasCorteMinimo = new List<Rota>();

            //percorre todas as arestas do grafo original
            foreach (KeyValuePair<Hub, List<Rota>> par in listaADJ)
            {
                Hub verticeOrigem = par.Key;
                foreach (Rota rota in par.Value)
                {
                    Hub verticeDestino = rota.GetDestino();
                    // Aresta cruza de S para T
                    //faz parte do corte minimo se vertice de origem está em verticesAlcancaveis
                    //e o de destino não está
                    //capacidade original = 0
                    if (verticesAlcancaveis.Contains(verticeOrigem) && !verticesAlcancaveis.Contains(verticeDestino) && rota.GetCapacidade() > 0)
                    {
                        arestasCorteMinimo.Add(rota);
                    }
                }
            }


            // monta a string de resultado
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"\nFluxo Máximo: {fluxoMaximo}");
            sb.AppendLine($"\nCorte mínimo");

            foreach (Rota aresta in arestasCorteMinimo)
            {
                sb.AppendLine($"({aresta.GetOrigem().ID()} -> {aresta.GetDestino().ID()}) | [CAPACIDADE: {aresta.GetCapacidade()}]");
            }
            if (arestasCorteMinimo.Count == 0)
            {
                sb.AppendLine($"(NÃO POSSUI CORTE MÍNIMO)");
            }

            return sb.ToString();
        }







        public void CircuitoEuleriano()
        {

        }
        public List<Hub> CircuitoHamiltoniano(Grafo grafo)
        {
            Dictionary<Hub, List<Rota>> Rotas;
            if (grafo.GetTipoRepresentacao() == "matriz")
            {
                Rotas = grafo.GetMatrizPraLista();
            }
            else
            {
                Rotas = grafo.GetRotas;
            }

            Dictionary<Hub, int> Marcas = new Dictionary<Hub, int>();

            bool isCicle = false;

            foreach (Hub hub in Rotas.Keys)
            {
                Marcas[hub] = 0;
            }

            List<Hub> OrdenacaoTopologica = new List<Hub>();

            foreach (Hub hub in Rotas.Keys)
            {
                if (Marcas[hub] == 0 && isCicle == false)
                {
                    isCicle = Visitar(hub, Rotas, Marcas, OrdenacaoTopologica);
                }
                else if (isCicle == true)
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


                foreach (Rota rota in Rotas[hub])
                {
                    Hub destino = rota.GetDestino();
                    Visitar(destino, Rotas, Marcas, OrdenacaoTopologica);
                }


                Marcas[hub] = 2;

                OrdenacaoTopologica.Insert(0, hub);
            }

            return false;
        }
        //talvez ainda precise de mais
    }
}
