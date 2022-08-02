using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotofacil
{
    public class Concursos
    {
        private List<Result> aux_results;
        private  List<string[]> Data;

        public Concursos()
        {
            Data = new List<string[]>();

            aux_results = ReadData.read(PathData.concursos,0);
            
            for (int i = 0; i < aux_results.Count; i++)
                Data.Add(Util.ConvertToArrayString(aux_results, i));
        }


        private bool validaConcurso(int concurso)
        {
            if (concurso > 0)
               return true;
            else
                throw new Exception("Número expecificado deve ser maior que zero.");
        }

        public string[] getConcursos(int concurso)
        {
            validaConcurso(concurso);
            var _concurso = Data.Skip(concurso - 1).Take(1).ToList().ToArray();
            return _concurso[0];
        }

        public List<string[]> getConcursos(int concurso_inicial, int concurso_final)
        {
            int ini = 0, fim = 0;

            validaConcurso(concurso_inicial);
            validaConcurso(concurso_final);

            ini = concurso_inicial-1;
            Int16 aux = (Int16)(concurso_final - concurso_inicial);
            fim = aux == 0 ? 1 : aux+1;

            return Data.Skip(ini).Take(fim).ToList<string[]>();
        }

    }
        
        
}
