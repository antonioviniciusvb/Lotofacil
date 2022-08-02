using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Lotofacil
{
    public static class ReadData
    {
        /// <summary>
        /// Este método retorna apenas os resultados dos últimos jogos definidos pelo usuario ou tudo se o parametro lastResult
        /// </summary>
        /// <param name="address"></param>
        /// <param name="lastResult"></param>
        /// <returns></returns>
        public static List<Datamodel> GetConcursos(int lastResult = 0)
        {
            using (StreamReader r = new StreamReader(PathData.fileConcursosJson))
            {
                string jsonString = r.ReadToEnd();
                
                var concursos = JsonConvert.DeserializeObject<List<Datamodel>>(jsonString);

                if (lastResult != 0)
                {
                    return concursos.Skip(concursos.Count - lastResult).ToList<Datamodel>();
                }

                return concursos;
            }            
        }





        /// <summary>
        /// Este método retorna apenas os resultados dos últimos jogos definidos pelo usuario ou tudo se o parametro lastResult
        /// </summary>
        /// <param name="address"></param>
        /// <param name="lastResult"></param>
        /// <returns></returns>
        public static List<Result> read(string address, int lastResult = 0)
        {
            List<Result> list = new List<Result>(lastResult);

            using (StreamReader str = new StreamReader(address, Encoding.GetEncoding(CultureInfo.GetCultureInfo("pt-BR").TextInfo.ANSICodePage)))
            {
                string line = string.Empty;
                int i = 0;

                while ((line = str.ReadLine()) != null)
                {
                    if (Regex.IsMatch(line, @"^[^C]"))
                    {
                        string[] split = line.Split(';');

                        list.Add(new Result
                        {
                            id = i,
                            n_1 = split[2],
                            n_2 = split[3],
                            n_3 = split[4],
                            n_4 = split[5],
                            n_5 = split[6],
                            n_6 = split[7],
                            n_7 = split[8],
                            n_8 = split[9],
                            n_9 = split[10],
                            n_10 = split[11],
                            n_11 = split[12],
                            n_12 = split[13],
                            n_13 = split[14],
                            n_14 = split[15],
                            n_15 = split[16],
                            arrecadacao_total = split[17],
                            Win_15 = int.Parse(split[18]),
                            Win_14 = int.Parse(split[19]),
                            Win_13 = int.Parse(split[20]),
                            Win_12 = int.Parse(split[21]),
                            Win_11 = int.Parse(split[22]),
                            vlr_Rateio_15 = split[23],
                            vlr_Rateio_14 = split[24],
                            vlr_Rateio_13 = split[25],
                            vlr_Rateio_12 = split[26],
                            vlr_Rateio_11 = split[27],
                            Acumulado_15 = split[28],
                            Estimativa_Premio = split[29],
                            vlr_Acumulado_Especial = split[30],
                            concurso = split[0],
                            data = split[1],
                            pares = split[31],
                            impares = split[32],
                            primos = split[33]
                        });
                        i++;
                    }
                }
            }

            if (lastResult != 0)
            {
                var list2 = list.Skip(list.Count - lastResult).ToList<Result>();
                return list2;
            }
            else
            {
                return list;
            }
        }



        public static string[] LerPossibilidade(string address, int pos)
        {
            int i = 0;
            string result = string.Empty;

            using (StreamReader str = new StreamReader(address))
            {
                string linhaAux = string.Empty;

                while ((linhaAux = str.ReadLine()) != null)
                {
                    if (i == pos)
                    {
                        result = linhaAux;
                        break;
                    }

                    i++;
                }
            }

            string[] array = result.Split(' ');

            return array;
        }


        public static List<string[]> LerArquivoPossibilidade(string address, int linhas_para_Pular)
        {
            List<string[]> result = new List<string[]>();

            using (StreamReader str = new StreamReader(address))
            {
                string linhaAux = string.Empty;
                StringBuilder stringBuilder = new StringBuilder();

                for (int i = 0; i != linhas_para_Pular; i++)
                {
                    str.ReadLine();
                }

                while ((linhaAux = str.ReadLine()) != null)
                {
                    stringBuilder.Append(linhaAux.Trim());

                    if(!string.IsNullOrWhiteSpace(linhaAux))
                       result.Add($"{stringBuilder}".Split(' '));

                    stringBuilder.Clear();
                }
            }

            return result;
        }
    }
}


