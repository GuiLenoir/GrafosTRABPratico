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
        private Log _logs;

        public Grafo Grafo;


        private Algoritmos _algoritmos;

        public Algoritmos Algoritmos
        {
            get {  return _algoritmos; }
        }
        
        public SORL()
        {
            _algoritmos = new Algoritmos();
        }

        public void CarregarGrafo(string caminho)
        {
            _grafo = new Grafo();
            _grafo.CarregarArquivo(caminho);
            _logs = new Log(caminho);
            _algoritmos = new Algoritmos(caminho);
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

        public string VisualizarGrafo()
        {
           return _grafo.VisualizarGrafo();
        }

        public string QuantidadeVertices()
        {
            return  $"{_grafo.GetQNTDVertices()}";
        }
        public Grafo GetGrafo()
        {
            return _grafo;
        }

        public Log GetLogs()
        {
            return _logs;
        }
    }
}
