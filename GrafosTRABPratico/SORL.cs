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

        public Grafo Grafo { get { return _grafo; } }

        
        private Algoritmos _algoritmos = new Algoritmos();

        public Algoritmos Agoritmos
        {
            get {  return _algoritmos; }
        }
        
        public SORL() { }

        public void CarregarGrafo(string caminho)
        {
            _grafo = new Grafo();
            _grafo.CarregarArquivo(caminho);
        }

        public void AdicionarHub()
        {
            _grafo.AddVertice();
        }

        public void AdicionarRota(int origem, int destino, double peso, double capacidade)
        {
            _grafo.AddAresta(origem, destino, peso, capacidade);
        }

        public void VisualizarGrafo()
        {
            _grafo.VisualizarGrafo();
        }

        public string QuantidadeVertices()
        {
            return _grafo.GetQNTDVertices();
        }
    }
}
