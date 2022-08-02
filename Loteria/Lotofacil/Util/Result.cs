using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotofacil
{
    public class Result:JogoPossivel
    {
        public int id { get; set; }
        public string arrecadacao_total { get; set; }
        public int Win_15 {get;set;}
        public int Win_14 {get;set;}
        public int Win_13 {get;set;}
        public int Win_12 {get;set;}
        public int Win_11 {get;set;}
        public string vlr_Rateio_15 {get;set;}
        public string vlr_Rateio_14 {get;set;}
        public string vlr_Rateio_13 {get;set;}
        public string vlr_Rateio_12 {get;set;}
        public string vlr_Rateio_11 {get;set;}
        public string Acumulado_15 {get;set;}
        public string Estimativa_Premio { get; set; }
        public string vlr_Acumulado_Especial { get; set; }
        public string concurso { get; set; }
        public string data { get; set; }
        public string pares { get; set; }
        public string impares { get; set; }
        public string primos { get; set; }


        public override string ToString()
        {
            string aux = String.Empty;
            aux = $"{this.n_1}{this.n_2}{this.n_3}{this.n_4}{this.n_5}{this.n_6}{this.n_7}{this.n_8}{this.n_9}{this.n_10}" +
                $"{this.n_11}{this.n_12}{this.n_13}{this.n_14}{this.n_15}";

            return aux;
        }
    }
}

