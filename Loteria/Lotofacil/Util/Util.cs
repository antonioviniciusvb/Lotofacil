using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using System.Globalization;
using System.Diagnostics;
using System.Net;
using Newtonsoft.Json;

namespace Lotofacil
{
    public static class Util
    {


        //Indexidor para controles no form
         public struct Inbits
        {
            private int bits;

            public Inbits(int valorInicialBit)
            {
                bits = valorInicialBit;
            }

            // indexador a ser escrito aqui 
            public bool this[int index]
            {
                get
                {
                    return (bits & (1 << index)) != 0;
                }
                set
                {
                    if (value)//ativa o bit for verdadeiro; caso contrário, o desativa
                        bits |= (1 << index);
                    else
                        bits &= ~(1 << index);

                }
            }
        }

        public static string[] ConvertToArrayString(List<Result> results, int index)
        {
            List<string> aux = new List<string>(15);
            aux.Add(results[index].n_1);
            aux.Add(results[index].n_2);
            aux.Add(results[index].n_3);
            aux.Add(results[index].n_4);
            aux.Add(results[index].n_5);
            aux.Add(results[index].n_6);
            aux.Add(results[index].n_7);
            aux.Add(results[index].n_8);
            aux.Add(results[index].n_9);
            aux.Add(results[index].n_10);
            aux.Add(results[index].n_11);
            aux.Add(results[index].n_12);
            aux.Add(results[index].n_13);
            aux.Add(results[index].n_14);
            aux.Add(results[index].n_15);
            aux.Sort();
            aux.Add(results[index].arrecadacao_total);
            aux.Add(results[index].Win_15.ToString());
            aux.Add(results[index].Win_14.ToString());
            aux.Add(results[index].Win_13.ToString());
            aux.Add(results[index].Win_12.ToString());
            aux.Add(results[index].Win_11.ToString());
            aux.Add(results[index].vlr_Rateio_15);
            aux.Add(results[index].vlr_Rateio_14);
            aux.Add(results[index].vlr_Rateio_13);
            aux.Add(results[index].vlr_Rateio_12);
            aux.Add(results[index].vlr_Rateio_11);
            aux.Add(results[index].Acumulado_15);
            aux.Add(results[index].Estimativa_Premio);
            aux.Add(results[index].concurso);
            aux.Add(results[index].data);

            string[] resultsOrd = aux.ToArray<string>();

            return resultsOrd;
        }

        public static List<string[]> ConvertResultToArrayString(List<Result> results)
        {
            var resultsOrd = new List<string[]>();

            List<string> aux = new List<string>(15);

            foreach (var item in results)
            {
                aux.Add(item.n_1);
                aux.Add(item.n_2);
                aux.Add(item.n_3);
                aux.Add(item.n_4);
                aux.Add(item.n_5);
                aux.Add(item.n_6);
                aux.Add(item.n_7);
                aux.Add(item.n_8);
                aux.Add(item.n_9);
                aux.Add(item.n_10);
                aux.Add(item.n_11);
                aux.Add(item.n_12);
                aux.Add(item.n_13);
                aux.Add(item.n_14);
                aux.Add(item.n_15);
                aux.Sort();
                aux.Add(item.arrecadacao_total);
                aux.Add(item.Win_15.ToString());
                aux.Add(item.Win_14.ToString());
                aux.Add(item.Win_13.ToString());
                aux.Add(item.Win_12.ToString());
                aux.Add(item.Win_11.ToString());
                aux.Add(item.vlr_Rateio_15);
                aux.Add(item.vlr_Rateio_14);
                aux.Add(item.vlr_Rateio_13);
                aux.Add(item.vlr_Rateio_12);
                aux.Add(item.vlr_Rateio_11);
                aux.Add(item.Acumulado_15);
                aux.Add(item.Estimativa_Premio);
                aux.Add(item.concurso);
                aux.Add(item.data);
                aux.Add(item.pares);
                aux.Add(item.impares);
                aux.Add(item.primos);

                resultsOrd.Add(aux.ToArray<string>());
                aux.Clear();
            }

            return resultsOrd;
        }


        public static string ConvertToArrayString(string[] results)
        {
            string aux = string.Empty;

            aux = $"{results[0]} {results[1]} {results[2]} {results[3]} {results[4]} {results[5]} {results[6]} " +
                  $"{results[7]} {results[8]} {results[9]} {results[10]} {results[11]} {results[12]} {results[13]} " +
                  $"{results[14]}";
            
            return aux;
        }


        public static StringBuilder TratarDados(string fileName)
        {
            StringBuilder stb = new StringBuilder();
            using (StreamReader str = new StreamReader(fileName, Encoding.GetEncoding(CultureInfo.GetCultureInfo("pt-BR").TextInfo.ANSICodePage)))
            {
                int i = 0;
                string linha = string.Empty;
                string jogo = string.Empty;

                while ((linha = str.ReadLine()) != null)
                {
                    if (Regex.IsMatch(linha, @"^<td rowspan=""\d+"">\d+\D", RegexOptions.IgnoreCase))
                    {
                        i++;

                        Match match = Regex.Match(linha, @">0([\d]{2})<");

                        if (!(match.Success))
                        {
                            match = Regex.Match(linha, @">([\d.,/]+)<");
                        }

                        //if (Regex.IsMatch(linha, @">0([\d]{2})<"))
                        //{

                        //}

                        //Match match = Regex.Match(linha, @">([\d.,/]+)<");
                        jogo += $"{match.Groups[1]};";

                        if (i == 31)
                        {
                            i = 0;
                            string[] aux = Regex.Split($"{jogo}", ";");
                            string[] jogos = new string[15];

                            for (int k = 0; k < 15; k++)
                                jogos[k] = $"{aux[k+2]}";

                            int primos = Estatiscas.NumerosPrimos(jogos);

                            string [] Padrao = Estatiscas.PadraoParesImpares(jogos);

                            stb.AppendLine($"{jogo}{Padrao[0]};{Padrao[1]};{primos}");

                            jogo = string.Empty;
                        }
                    }
                }
            }
            
            return stb;
        }

        public static StringBuilder TratarDadosJson(string fileName)
        {
            StringBuilder stb = new StringBuilder();

            using (StreamReader r = new StreamReader(PathData.fileConcursosJson))
            {
                string jsonString = r.ReadToEnd();
                string jogo = string.Empty;

                var concursos = JsonConvert.DeserializeObject<List<Datamodel>>(jsonString)
                                .OrderBy(x => x.concurso);

                foreach (var item in concursos)
                {
                    jogo = extraiCamposDataModelJson(item);

                    string[] aux = Regex.Split($"{jogo}", ";");
                    string[] jogos = new string[15];

                    for (int k = 0; k < 15; k++)
                        jogos[k] = $"{aux[k + 2]}";

                    int primos = Estatiscas.NumerosPrimos(jogos);

                    string[] Padrao = Estatiscas.PadraoParesImpares(jogos);

                    stb.AppendLine($"{jogo};{Padrao[0]};{Padrao[1]};{primos}");

                    jogo = string.Empty;
                }
            }

          
            return stb;
        }

        private static string extraiCamposDataModelJson(Datamodel item)
        {
            return $"{item.concurso};{item.data};{item.dezenas[0]:00};" +
                   $"{item.dezenas[1]:00};{item.dezenas[2]:00};{item.dezenas[3]:00};" +
                   $"{item.dezenas[4]:00};{item.dezenas[5]:00};{item.dezenas[6]:00};" +
                   $"{item.dezenas[7]:00};{item.dezenas[8]:00};{item.dezenas[9]:00};" +
                   $"{item.dezenas[10]:00};{item.dezenas[11]:00};{item.dezenas[12]:00};" +
                   $"{item.dezenas[13]:00};{item.dezenas[14]:00};0,00;" +
                   $"{item.premiacoes[0].vencedores};{item.premiacoes[1].vencedores};" +
                   $"{item.premiacoes[2].vencedores};{item.premiacoes[3].vencedores};" +
                   $"{item.premiacoes[4].vencedores};0;0;0;0;0;0;0;0";
        }




        /// <summary>
        /// Verifica se o int recebido é 0
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int isZero(int value)
        {
            return value > 0 ? 1 : 0; 
        }

        /// <summary>
        /// Este método prencher uma string com zeros a esquerda
        /// </summary>
        /// <param name="num"></param>
        /// <param name="qntZeros"></param>
        /// <returns></returns>
        public static string zeroEsquerda(string num, int qntZeros)
        {
            if (num.Length == qntZeros)
                return num;

            string aux = string.Empty;

            for (int i = 0; i < qntZeros-num.Length; i++)
            {
                aux += "0";
            }

            return aux += num;
        }

        public static void criarArquivo(string fileName, StringBuilder data)
        {
            using (StreamWriter stw = new StreamWriter(fileName, false, Encoding.GetEncoding(CultureInfo.GetCultureInfo("pt-BR").TextInfo.ANSICodePage)))
            {
                stw.WriteLine("Concurso;Data_Sorteio;Bola1;Bola2;Bola3;Bola4;Bola5;Bola6;Bola7;Bola8;Bola9;Bola10;Bola11;Bola12;Bola13;Bola14;Bola15;Arrecadacao_Total;Ganhadores_15_Numeros;Ganhadores_14_Numeros;Ganhadores_13_Numeros;Ganhadores_12_Numeros;Ganhadores_11_Numeros;Valor_Rateio_15_Numeros;Valor_Rateio_14_Numeros;Valor_Rateio_13_Numeros;Valor_Rateio_12_Numeros;Valor_Rateio_11_Numeros;Acumulado_15_Numeros;Estimativa_Premio;Valor_Acumulado_Especial;Pares;Impares;N_Primos");
                stw.WriteLine(data);
            }
        }

        public static void criarArquivo(string fileName, List<string> data)
        {
            using (StreamWriter stw = new StreamWriter(fileName, false, Encoding.GetEncoding(CultureInfo.GetCultureInfo("pt-BR").TextInfo.ANSICodePage)))
            {
                for (int i = 0; i < data.Count; i++)
                {
                    stw.WriteLine(data[i]);
                }
            }
        }

        public static void limparArquivos(string filename) => File.Delete(filename);

        /// <summary>
        /// Este método irá retornará n bolas de um determinado concurso
        /// </summary>
        /// <param name="result"></param>
        /// <param name="bolas"></param>
        /// <returns></returns>
        public static string ballsReturn(string[] concurso, int bolas)
        {
            string aux = string.Empty;

            for (int i = 0; i < bolas; i++)
               aux += $"{concurso[i]}-";

            return aux = aux.Remove(aux.Length - 1, 1);
        }

        public static void OrdenaResults(ref List<Result> results)
        {
            for (int i = 0; i < results.Count; i++)
            {

                List<string> aux = new List<string>(15);
                aux.Add(results[i].n_1);
                aux.Add(results[i].n_2);
                aux.Add(results[i].n_3);
                aux.Add(results[i].n_4);
                aux.Add(results[i].n_5);
                aux.Add(results[i].n_6);
                aux.Add(results[i].n_7);
                aux.Add(results[i].n_8);
                aux.Add(results[i].n_9);
                aux.Add(results[i].n_10);
                aux.Add(results[i].n_11);
                aux.Add(results[i].n_12);
                aux.Add(results[i].n_13);
                aux.Add(results[i].n_14);
                aux.Add(results[i].n_15);
                aux.Sort();

                results[i].n_1 = aux[0];
                results[i].n_2 = aux[1];
                results[i].n_3 = aux[2];
                results[i].n_4 = aux[3];
                results[i].n_5 = aux[4];
                results[i].n_6 = aux[5];
                results[i].n_7 = aux[6];
                results[i].n_8 = aux[7];
                results[i].n_9 = aux[8];
                results[i].n_10 = aux[9];
                results[i].n_11 = aux[10];
                results[i].n_12 = aux[11];
                results[i].n_13 = aux[12];
                results[i].n_14 = aux[13];
                results[i].n_15 = aux[14];
            }

            return;
        }

        public static bool TestaConexao()
        {
            
            try
            {
                //CookieContainer myContainer = new CookieContainer();
                //var request = (HttpWebRequest)WebRequest.Create(siteTeste);
                //request.MaximumAutomaticRedirections = 1;
                //request.AllowAutoRedirect = true;
                //request.CookieContainer = myContainer;
                //var response = (HttpWebResponse)request.GetResponse();
                return true;
            }
            catch (Exception)
            {
                throw new Exception($"Erro ao atualizar sua base de dados!{"\n"}");
            }

        }
    }
}
