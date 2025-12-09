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
        private string _path;

        public Log(string path)
        {
            string nomePath = Path.GetFileName(path);
            _log.AppendLine($"\nRelatório ({nomePath}) - {DateTime.Now}\n");
            _path = $"{Path.GetFileNameWithoutExtension(nomePath)}.txt";
        }

        public void Registrar(string registrar)
        {
            _log.AppendLine(registrar);
           
        }

          public void Salvar()
        {
            _log.AppendLine("\n------------------------------------------");
            using (StreamWriter sw = new StreamWriter(_path, true))
            {
                sw.WriteLine(_log + "\n\n");
                sw.Close();
            }
            Limpar();
        }

        public void Limpar()
        {
            _log.Clear();
        }



    }
}
