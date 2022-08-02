using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Lotofacil
{
    public static class Estatiscas
    {
        private static readonly int[] n_primos = new int[] { 2, 3, 5, 7, 11, 13, 17, 19, 23 };

        #region Frequencia

        public static Bolas frequenciaNumeros(List<string[]> list)
        {
            Bolas bol = new Bolas();
            int controle = 0;

            for (int i = 0; i < list.Count; i++)
            {
                controle = 0;

                foreach (var number in list[i])
                {
                    if (Regex.IsMatch(number, @"^\d{2}([^.,/\-\w]+)?"))
                    {
                        if (controle == 15)
                            break;

                        int aux_Dez = int.Parse(number);
                        bol[aux_Dez - 1]++;

                        controle++;
                    }
                }
            }

            return bol;
        }




        #endregion

        public static void queryMinMax(ref Bolas bl, int limit, bool clear)
        {
            List<string> valores = new List<string>(bl.Count);

            for (int j = 0; j < bl.Count; j++)
                valores.Add($"{(bl[j]).ToString("00000")}  -   {(j + 1).ToString("00")}");

            if (clear)
                valores.RemoveAll(vl => vl.Substring(0, 5) == "00000");

            valores.Sort();

            int first = 0, last = valores.Count - 1;

            //default
            bl.Min_1 = $"{valores[first++]}";
            bl.Min_2 = $"{valores[first++]}";
            bl.Min_3 = $"{valores[first++]}";

            bl.Max_1 = $"{valores[last--]}";
            bl.Max_2 = $"{valores[last--]}";
            bl.Max_3 = $"{valores[last--]}";


            int rest = (valores.Count()/2) - 3;

            if (rest > 0 &&  limit > 3)
            {
                bl.Min_4 = $"{valores[first++]}";
                bl.Max_4 = $"{valores[last--]}";

                rest--;

                if (rest > 0 && limit > 4)
                {
                    bl.Min_5 = $"{valores[first++]}";
                    bl.Max_5 = $"{valores[last--]}";

                    rest--;

                    if (rest > 0 && limit > 5)
                    {
                        bl.Min_6 = $"{valores[first++]}";
                        bl.Max_6 = $"{valores[last--]}";

                        if (limit > 6)
                        {
                            bl.Min_7 = $"{valores[first++]}";
                            bl.Max_7 = $"{valores[last--]}";

                            if (limit > 7)
                            {
                                bl.Min_8 = $"{valores[first]}";
                                bl.Max_8 = $"{valores[last]}";
                            }
                        }
                    }
                }
            }
        }

        #region Padrao pares e impares
        public static Dictionary<string, int[]> PadraoParesImpares(List<string[]> list)
        {
            Bolas bol = new Bolas();

            int y = 0;
            Dictionary<string, int[]> dictionary = new Dictionary<string, int[]>();
            string aux_first = string.Empty;
            int pares = 0, impares = 0;
            for (int i = 0; i < list.Count; i++)
            {

                for (int j = 0; j < 15; j++)
                {
                    if (y == 0)
                    {
                        if (int.Parse(list[i][j]) % 2 == 0)
                            aux_first = $"Concurso_{list[i][28]}_P";
                        else
                            aux_first = $"Concurso_{list[i][28]}_I";
                    }

                    if (int.Parse(list[i][j]) % 2 == 0)
                        pares++;
                    else
                        impares++;

                    y++;
                }

                dictionary.Add($"{aux_first}___P__{pares}__I__{impares}", new int[]{pares,impares});

                y = 0;
                pares = 0;
                impares = 0;
            }
            return dictionary;
        }

        public static string[] PadraoParesImpares(string[] list)
        {
            string[] paresImpares = new string[2];
            int pares = 0, impares = 0;

            for (int j = 0; j < 15; j++)
            {
                if (int.Parse(list[j]) % 2 == 0)
                    pares++;
                else
                    impares++;
            }

            paresImpares[0] = $"{pares}";
            paresImpares[1] = $"{impares}";

            return paresImpares;
        }

        public static Bolas BolasParesImpares(List<string[]> jogos)
        {
            var bls = new Bolas();

            int par = 0, impar = 0;

            for (int i = 0; i < jogos.Count; i++)
            {
                for (int j = 0; j < 15; j++)
                    if (int.Parse(jogos[i][j]) % 2 == 0)
                        par++;
                    else
                        impar++;

                #region incrementa Padrao
                if (par == 8 && impar == 7)
                    bls[0]++;
                else
                if (par == 7 && impar == 8)
                    bls[1]++;
                else
                if (par == 9 && impar == 6)
                    bls[2]++;
                else
                if (par == 6 && impar == 9)
                    bls[3]++;
                else
                if (par == 10 && impar == 5)
                    bls[4]++;
                else
                if (par == 5 && impar == 10)
                    bls[5]++;
                else
                if (par == 11 && impar == 4)
                    bls[6]++;
                else
                if (par == 4 && impar == 11)
                    bls[7]++;
                else
                    bls[8]++;

                #endregion

                par = 0;
                impar = 0;
            }
            return bls;
        }
        #endregion

        #region Primos
        public static Bolas NumerosPrimos(List<string[]> list)
        {
            Bolas bl = new Bolas();

            for (int j = 0; j < list.Count; j++)
            {
                for (int i = 0; i < 9; i++)
                {
                    for (int k = 0; k < 15; k++)
                    {
                        if (n_primos[i] == int.Parse(list[j][k]))
                            bl[n_primos[i] - 1]++;
                    }
                }
            }

            return bl;
        }

        public static int NumerosPrimos(string[] list)
        {
            int qntd_primos = 0;
            int aux_primos = 0;

            for (int i = 0; i < 9; i++)
            {
                for (int k = 0; k < 15; k++)
                {
                    if (n_primos[i] == int.Parse(list[k]))
                    {
                        aux_primos++;
                        break;
                    }
                }
            }

            qntd_primos = aux_primos;

            return qntd_primos;
        }

        #endregion

        public static void resultadoDuplicado(string find, List<string[]> resultado)
        {
            bool t = resultado.Exists(x => x.ToString() == find);
        }

        public static bool verificaIndice(List<int> encontrados, int i)
        {
            bool achou = false;

            for (int f = 0; f < encontrados.Count; f++)
            {
                if (i == encontrados[f])
                {
                    achou = true;
                    break;
                }
            }

            return achou;
        }

        public static void buscaResultadosIdenticos(List<string[]> results, int dezenas)
        {
            List<int> encontrados = new List<int>();

            int bolas = dezenas;

            for (int i = 0; i < results.Count; i++)
            {
                if (!(Estatiscas.verificaIndice(encontrados, i)))
                {
                    string find = string.Empty;

                    find = Util.ballsReturn(results[i], bolas);

                    for (int k = 0; k < results.Count; k++)
                    {
                        string aux = string.Empty;

                        if (k != i)
                        {
                            aux = Util.ballsReturn(results[k], bolas);

                            if (find == aux)
                            {
                                encontrados.Add(i);
                                encontrados.Add(k);

                            }
                        }
                    }
                }
            }
        }

        public static bool buscaResultadosIdenticos(List<string[]> results, string[] candidato)
        { 
            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].Contains(candidato[0]) && results[i].Contains(candidato[1]) && results[i].Contains(candidato[2]) &&
                    results[i].Contains(candidato[3]) && results[i].Contains(candidato[4]) && results[i].Contains(candidato[5]) &&
                    results[i].Contains(candidato[6]) && results[i].Contains(candidato[7]) && results[i].Contains(candidato[8]) &&
                    results[i].Contains(candidato[9]) && results[i].Contains(candidato[10]) && results[i].Contains(candidato[11]) &&
                    results[i].Contains(candidato[12]) && results[i].Contains(candidato[13]) && results[i].Contains(candidato[14]))
                {
                    return true;
                }
            }
            return false;
        }

        public static int Acertos(string[] results, string[] aposta)
        {
            int acertos = 0;

            for (int i = 0; i < aposta.Count(); i++)

                for (int j = 0; j < 15; j++)
                {
                    if(int.Parse(results[j]) == int.Parse(aposta[i]))
                        acertos++;
                }

            return acertos;
        }

        public static string analisaSequencias(string[] concurso)
        {
            int i = 0, j = 1;
            string seq = string.Empty;
            int encontrados = 0;

            while (true)
            {
                if((int.Parse(concurso[j]) - int.Parse(concurso[i])) == 1)
                {
                    if (encontrados == 0)
                        encontrados++;

                    encontrados++;
                }
                else
                {
                    if (encontrados > 0)
                        seq += $"{encontrados}".PadLeft(3);

                    encontrados = 0;
                }

                i = j;
                j++;

                if (j > 14)
                {
                    if (encontrados > 0)
                        seq += $"{encontrados}".PadLeft(3);
                    break;
                }
            }
            return seq;
        }

        public static bool qntdNumeros_1_a_12(string[] jogo)
        {
            int cont = 0;

            for (int i = 0; i < 15; i++)
                if (int.Parse(jogo[i]) <= 12)
                    cont++;

            return cont > 5 && cont <= 9 ? true : false;

        }


        public static int MaiorDezena20(List<string[]> results, int t_dezenas, int intervalo_ini, int intervalo_fim)
        {
            int r_count = 0;
            
            for (int i = 0; i < results.Count(); i++)
            {
                int count = 0;

                for (int j = 0; j < 15; j++)
                {
                    if(int.Parse(results[i][j]) >= intervalo_ini && int.Parse(results[i][j]) <= intervalo_fim)
                    {
                        count++;
                    }
                }

                if (count == t_dezenas)
                    r_count++;
            }


            return r_count;
        }
        
    }
}
