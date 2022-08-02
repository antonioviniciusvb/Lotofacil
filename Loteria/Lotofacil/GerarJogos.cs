using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Lotofacil
{
    public class GerarJogos
    {
        public int Jogos { get; set; }
        private Random random;
        private List<string[]> ls;

        public GerarJogos(int jogos)
        {
            this.Jogos = jogos;
            random = new Random();
        }

        public List<string[]> Gerar(JogarLotofacil regras)
        {
            string patternPar = @"P=0?(\d{1,2})", patternImpar = @"I=0?(\d{1,2})", path = string.Empty;
            bool achou = false;
            int contRegras = 0, auxAchou = 0, max = 0;

            ls = new List<string[]>();

            string par = Regex.Match(regras.Padrao, patternPar).Groups[1].ToString();
            string impar = Regex.Match(regras.Padrao, patternImpar).Groups[1].ToString();

            #region Define qual Arquivo acessar
            if (par == "8")
            {
                path = PathData.filePr15_8_7;
                max = 849419;
            }
            else
            if (par == "7")
            {
                path = PathData.filePr15_7_8;
                max = 1019303;
            }
            else
            if (par == "9")
            {
                path = PathData.filePr15_9_6;
                max = 377519;
            }
            else
            if (par == "6")
            {
                path = PathData.filePr15_6_9;
                max = 660659;
            }
            else
            if (par == "10")
            {
                path = PathData.filePr15_10_5;
                max = 84941;
            }
            else
            if (par == "5")
            {
                path = PathData.filePr15_5_10;
                max = 226511;
            }
            #endregion

            List<string[]> possibilidades = ReadData.LerArquivoPossibilidade(path, 0);

            for (int i = 0; i < regras.QntdJogos; i++)
            {
                achou = false;

                while (achou == false)
                {
                    //contTentativas++;

                    //Quebra o loop
                    if (possibilidades.Count == 0)
                    {
                        i = regras.QntdJogos + 1;
                        break;
                    }
                        
                    int aux = random.Next(0, max);
                    string[] resultCandidato = possibilidades[aux];

                    //string[] paresImpares = Estatiscas.PadraoParesImpares(resultCandidato);
                    auxAchou = 0;

                    //Número de regras utilizadas
                    contRegras = 0;

                    if (Estatiscas.qntdNumeros_1_a_12(resultCandidato))
                    {
                            #region NObrigatório
                            if (regras.UtilizandoNsObrigatorios)
                            {
                                contRegras++;

                                int contObr = 0;
                                int somaObr = Util.isZero(regras.Obrigatorio_1) + Util.isZero(regras.Obrigatorio_2) + Util.isZero(regras.Obrigatorio_3) +
                                              Util.isZero(regras.Obrigatorio_4) + Util.isZero(regras.Obrigatorio_5) + Util.isZero(regras.Obrigatorio_6) +
                                              Util.isZero(regras.Obrigatorio_7) + Util.isZero(regras.Obrigatorio_8) + Util.isZero(regras.Obrigatorio_9) +
                                              Util.isZero(regras.Obrigatorio_10);

                                if (resultCandidato.Contains(Util.zeroEsquerda(regras.Obrigatorio_1.ToString(), 2)))
                                    contObr++;

                                if (resultCandidato.Contains(Util.zeroEsquerda(regras.Obrigatorio_2.ToString(), 2)))
                                    contObr++;

                                if (resultCandidato.Contains(Util.zeroEsquerda(regras.Obrigatorio_3.ToString(), 2)))
                                    contObr++;

                                if (resultCandidato.Contains(Util.zeroEsquerda(regras.Obrigatorio_4.ToString(), 2)))
                                    contObr++;

                                if (resultCandidato.Contains(Util.zeroEsquerda(regras.Obrigatorio_5.ToString(), 2)))
                                    contObr++;

                                if (resultCandidato.Contains(Util.zeroEsquerda(regras.Obrigatorio_6.ToString(), 2)))
                                    contObr++;

                                if (resultCandidato.Contains(Util.zeroEsquerda(regras.Obrigatorio_7.ToString(), 2)))
                                    contObr++;

                                if (resultCandidato.Contains(Util.zeroEsquerda(regras.Obrigatorio_8.ToString(), 2)))
                                    contObr++;

                                if (resultCandidato.Contains(Util.zeroEsquerda(regras.Obrigatorio_9.ToString(), 2)))
                                    contObr++;

                                if (resultCandidato.Contains(Util.zeroEsquerda(regras.Obrigatorio_10.ToString(), 2)))
                                    contObr++;

                                if (contObr == somaObr)
                                    auxAchou++;
                            }
                            #endregion

                            #region Descartaveis
                            if (regras.UtilizandoDescartavel)
                            {
                                contRegras++;

                                if ((resultCandidato.Contains(Util.zeroEsquerda(regras.Descartavel_1.ToString(), 2)) == false) &&
                                    (resultCandidato.Contains(Util.zeroEsquerda(regras.Descartavel_2.ToString(), 2)) == false) &&
                                    (resultCandidato.Contains(Util.zeroEsquerda(regras.Descartavel_3.ToString(), 2)) == false) &&
                                    (resultCandidato.Contains(Util.zeroEsquerda(regras.Descartavel_4.ToString(), 2)) == false) &&
                                    (resultCandidato.Contains(Util.zeroEsquerda(regras.Descartavel_5.ToString(), 2)) == false) &&
                                    (resultCandidato.Contains(Util.zeroEsquerda(regras.Descartavel_6.ToString(), 2)) == false) &&
                                    (resultCandidato.Contains(Util.zeroEsquerda(regras.Descartavel_7.ToString(), 2)) == false) &&
                                    (resultCandidato.Contains(Util.zeroEsquerda(regras.Descartavel_8.ToString(), 2)) == false) &&
                                    (resultCandidato.Contains(Util.zeroEsquerda(regras.Descartavel_9.ToString(), 2)) == false) &&
                                    (resultCandidato.Contains(Util.zeroEsquerda(regras.Descartavel_10.ToString(), 2)) == false))
                                {
                                    auxAchou++;
                                }

                            }
                            #endregion

                            #region Primos
                            if (regras.UtilizandoPrimosMinMax)
                            {
                                contRegras++;

                                if (Estatiscas.NumerosPrimos(resultCandidato) >= regras.PrimosMin && Estatiscas.NumerosPrimos(resultCandidato) <= regras.PrimosMax)
                                {
                                    auxAchou++;
                                }
                            }
                            #endregion

                            #region SequenciaDezenas
                            if (regras.UtilizandoSequenciaDezenas)
                            {
                                contRegras++;

                                string sequencias = Estatiscas.analisaSequencias(resultCandidato);

                                if (sequencias.Contains(regras.SeqDezenas))
                                    auxAchou++;
                            }
                            #endregion

                            #region Adicionando ou Descartando Jogo
                            if (contRegras == auxAchou)
                            {
                                ls.Add(resultCandidato);
                                achou = true;
                            }
                            #endregion
                     
                    }
                    possibilidades.RemoveAt(aux);
                    max--;
                }

                //Debug.WriteLine(contTentativas);
            }

            possibilidades.Clear();
            GC.Collect();

            return ls;
        }
    }
}
