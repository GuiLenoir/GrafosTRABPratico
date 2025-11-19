using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafosTRABPratico
{
    public class Hub
    {
        //REPRESENTA O VÉRTICE DO GRAFO
        //HUB = CENTRO DE DISTRIBUIÇÕES, O DESTINO DA MERCADORIA

        private static int cont = 1;
        private int _id;
        private string _nome;

        public Hub(string nome)
        {
            _nome = nome;
            _id = cont;
            cont++;
        }
    }
}
