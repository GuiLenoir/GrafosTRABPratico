using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrafosTRABPratico
{
    public class Rota
    {
        //REPRESENTA UMA ARESTA DO GRAFO
        //É O CAMINHO POR ONDE A MERCADORIA PASSA

        private Hub _origem; //de onde a aresta ta saindo
        private Hub _destino; //aonde a aresta ta chegando
        private double _peso; // CUSTO POR TRANSPORTE DE UNIDADE             *A CADA 1 UNIDADE CONSIDERANDO PEDAGIO , COMBUSTIVEL E ETC
        private double _capacidade; // LIMITE DE ESCOAMENTO DIARIO         *EM TONELADAS
        private double _fluxo;
        private bool _direta;

        public Rota(Hub origem, Hub destino, double custo, double capacidade)
        {
            _origem = origem;
            _destino = destino;
            _peso = custo;
            _capacidade = capacidade;
        }



        public double GetPeso()
        {
            return _peso;
        }

        public void MudarFluxo(double fluxo)
        {
            _fluxo = fluxo;
        }
        public double GetFluxo()
        {
            return _fluxo;
        }

        public double GetCapacidade()
        {
            return _capacidade;
        }



        public Hub GetDestino()
        {
            return _destino;
        }

        public Hub GetOrigem()
        {
            return _origem;
        }
    }
}
