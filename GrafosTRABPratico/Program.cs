using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafosTRABPratico
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //INICIALIZADOR

            //inicia o programa com um grafo já selecionado, caso queira
            //caso não, existe opção de carregamento do grafo dentro do programa
            string grafo = "";

            //personalização
            Console.Title = "Máxima Logística S.A.";
            //

            //instanciação
            SORL sorl = new SORL();
            Menu menu = new Menu(sorl);

            if (grafo != "")
            {
                sorl.CarregarGrafo(grafo);
            }

            menu.Exibir();
            //
        }
    }
}
