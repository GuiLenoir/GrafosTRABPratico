using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafosTRABPratico
{
    public class SORL
    {
        //Sistema de Otimização de Rotas Logísticas (SORL)
        //CLASSE PRINCIPAL DE ORGANIZAÇÃO
        private Grafo _grafo = new Grafo();

        
        private Algoritmos _algoritmos = new Algoritmos();
        
        public SORL() { }

        public void CarregarGrafo(string caminho)
        {
            _grafo = new Grafo();
            _grafo.CarregarArquivo(caminho);
        }

        public bool AdicionarHub()
        {
            try
            {
                if (_grafo.GetQNTDVertices() == 0)
                {
                    throw new Exception("Grafo vazio");
                }
                else
                {
                    _grafo.AddVertice();
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Erro ao adicionar vértice: O grafo está vazio");
                Console.Error.WriteLine($"ERRO: ({ex.Message})");
                return false;
            }
            return true;
            
        }

        public bool AdicionarRota(int origem, int destino, double peso, double capacidade)
        {
            return _grafo.AddAresta(origem, destino, peso, capacidade);
        }

        public void VisualizarGrafo()
        {
            _grafo.VisualizarGrafo();
        }

        public string QuantidadeVertices()
        {
            return  $"{_grafo.GetQNTDVertices()}";
        }
    }
}
