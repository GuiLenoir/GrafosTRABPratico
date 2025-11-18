using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafosTRABPratico
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //INICIALIZADOR

            //personalização
            Console.Title = "Máxima Logística S.A.";
            //

            //instanciação
            SORL sorl = new SORL();
            Menu menu = new Menu(sorl);
            menu.Exibir();
            //
        }
    }
}
