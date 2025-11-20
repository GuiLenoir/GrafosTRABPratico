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

        public Hub()
        {
            _id = cont;
            cont++;
        }

        public int ID ()
        {
            return _id;
        }

       
    }
}
