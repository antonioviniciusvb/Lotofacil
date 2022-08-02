using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotofacil
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
    public class Cidade
    {
        public string cidade;
        public string vencedores;
        public string latitude;
        public string longitude;
    }

    public class EstadosPremiado
    {
        public string nome;
        public string uf;
        public string vencedores;
        public string latitude;
        public string longitude;
        public List<Cidade> cidades;
    }

    public class Premiaco
    {
        public string acertos;
        public int vencedores;
        public string premio;
    }

    public class Datamodel
    {
        public string loteria;
        public string nome;
        public int concurso;
        public string data;
        public string local;
        public List<int> dezenas;
        public List<Premiaco> premiacoes;
        public List<EstadosPremiado> estadosPremiados;
        public bool acumulou;
        public string acumuladaProxConcurso;
        public string dataProxConcurso;
        public int proxConcurso;
        public string timeCoracao;
        public string mesSorte;
    }


}
