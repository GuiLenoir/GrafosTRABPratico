using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafosTRABPratico
{
    public class Log
    {
        private StringBuilder _log = new StringBuilder();
        private string _path = "logs.txt";

        public Log()
        {
            _log.AppendLine($"Relatório - {DateTime.Now}");
        }

        public void Registrar(string registrar)
        {
            _log.AppendLine(registrar);
        }

        public void Salvar()
        {
            using (StreamWriter sw = new StreamWriter(_path, true))
            {
                sw.WriteLine(_log + "\n\n");
            }
        }



    }
}
