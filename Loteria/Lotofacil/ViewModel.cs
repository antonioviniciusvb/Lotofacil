using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using GeradorProbalidades;

namespace Lotofacil
{
    public class ViewModel : INotifyPropertyChanged 
    {
        private static readonly string zip = @"D_LOTFAC_ANALISE.zip";
        private static readonly string file = @"D_LOTFAC_ANALISE.txt";
        private static readonly string fileHtm = @"D_lotfac.htm";

        public event PropertyChangedEventHandler PropertyChanged;
        private JogarLotofacil regrasLoto;
        private ConferirJogo conferir;
        private Fechamento fechamento;

        public Command ExecutaConferencia { get; set; }
        public Command ExecutaFechamento { get; set; }
        public Command GerarJogos { get; set; }
        public Command GerarVolantes { get; set; }
        public Command Clear { get; set; }
        public Command ClearJogos { get; set; }
        public Command ClearConferir { get; set; }
        public Command Swap { get; set; }
        public Command Export { get; set; }
        //public Command AtualizarDatabase { get; set; }
        public Command ImportArq { get; set; }
        public Command ConferirOutput { get; set; }
        public Command ConferirResults { get; set; }
        public Command CopiaJogoResultParaFechamento { get; set; }

        public ViewModel()
        {
            regrasLoto = new JogarLotofacil();
            conferir = new ConferirJogo();
            fechamento = new Fechamento(18,15,14,15);

            this.ExecutaFechamento = new Command(this.executaFechamento, () => { return Validacoes.validaFechamento(fechamento); });
            this.GerarJogos = new Command(this.gerarJogos, () => { return Validacoes.validaRegras(regrasLoto); });
            this.ExecutaConferencia = new Command(this.executaConferencia, () => { return Validacoes.validaConferir(conferir); });
            this.ClearConferir = new Command(Conferir.clear, () => { return true; });
            this.ImportArq = new Command(this.ImportarArquivos, () => { return string.IsNullOrWhiteSpace(conferir.Volante) && Conferir.Output == false; });
            this.GerarVolantes = new Command(this.gerarVolantes, () => { return regrasLoto.Jogos.Count > 0; });
            this.Clear = new Command(regrasLoto.limparDados, () => { return regrasLoto != null; });
            this.Export = new Command(this.ExportFile, () => { return regrasLoto.Jogos.Count > 0; });
            this.ClearJogos = new Command(this.limparJogos, () => { return regrasLoto.Jogos.Count > 0; });
            //this.AtualizarDatabase = new Command(this.atualizarDataBase, () => { return Util.TestaConexao(); });
            this.CopiaJogoResultParaFechamento = new Command(this.copiaJogoResultParaFechamento, () => { return regrasLoto.Jogos.Count == 1; });
            this.Swap = new Command(regrasLoto.Swap, () => { return true; });
        }

        private void copiaJogoResultParaFechamento()
        {
            Fechamento.Dezenas = Regras.Jogos[0];
        }

        private void executaFechamento()
        {
            Fechamento.Dezenas = Regex.Replace(Fechamento.Dezenas, " +", " ").Trim();
            var jogos = Gerador_Probalidades.Gerar_Fechamento_18_15_14(fechamento.Dezenas, Fechamento.Considerar_15_Primeiras);

            limparJogos();

            for (int i = 0; i < jogos.Count; i++)
            {
                regrasLoto.Add(jogos[i]);
            }

           System.Windows.MessageBox.Show($"As {Fechamento.Total_Dezenas} dezenas foram desdobradas em {jogos.Count} jogos de " +
                                          $"{Fechamento.Volante} dezenas", "Fim de processsamento!");

            Conferir.Habilita_Output = true;

        }

        public void executaConferencia()
        {
            Concursos concursos = new Concursos();
            var c = concursos.getConcursos(Conferir.Concurso_Ini, Conferir.Concurso_End);
            
            List<string[]> arquivoJogos = new List<string[]>();

            StringBuilder stb_Results = new StringBuilder();
            StringBuilder stb_aux = new StringBuilder();
            StringBuilder stb_Resumo_Results = new StringBuilder();
            bool filtraResults = Regex.IsMatch(Conferir.Filtro_Results, @"\d\d");


            int[] acertos_count = new int[11];
            int acertos = 0;
            string acertos_formatado = string.Empty;

            if (Conferir.Files == null && Conferir.Output == false)
            {
                Conferir.Volante = Regex.Replace(conferir.Volante, " +", " ").Trim();

                var dezenas = conferir.Volante.Split('\x20');
                
                for (int i = 0; i < c.Count; i++)
                {
                    acertos = Estatiscas.Acertos(c[i], dezenas);
                    acertos_formatado = $"{string.Format("{0:00}", acertos)}";

                    stb_aux.AppendLine("---------------------------------------------------------------");
                    stb_aux.Append($"Concurso - {c[i][28]}");
                    stb_aux.AppendLine();
                    stb_aux.Append($"".PadLeft(20));

                    for (int k = 0; k < 15; k++)
                        stb_aux.Append($"{c[i][k]} ");

                    stb_aux.AppendLine();

                    acertos_count[15 - acertos]++;

                    stb_aux.AppendLine($"{acertos_formatado} pontos - {conferir.Volante}");

                    if (filtraResults)
                    {
                        if (Regex.IsMatch($"{acertos_formatado}", $"{Conferir.Filtro_Results}"))
                        {
                            stb_Results.AppendLine($"{stb_aux}");
                        }
                    }
                    else
                    {
                        stb_Results.AppendLine($"{stb_aux}");
                    }

                    stb_aux.Clear();
                }

            }
            else
            if (conferir.Output)
            {
                for (int i = 0; i < Regras.Jogos.Count; i++)
                {
                    var dezenas = Regras.Jogos[i].Split('\x20');

                    for (int j = 0; j < c.Count; j++)
                    {
                        acertos = Estatiscas.Acertos(c[j], dezenas);
                        acertos_formatado = $"{string.Format("{0:00}", acertos)}";

                        acertos_count[15 - acertos]++;

                        stb_aux.AppendLine("---------------------------------------------------------------");
                        stb_aux.AppendLine($"Linha: {string.Format("{0:000}", i + 1)}");

                        stb_aux.Append($"".PadLeft(20));

                        stb_aux.AppendLine($"{Regras.Jogos[i]}");

                        stb_aux.Append($"Concurso - {c[j][28]}");
                        stb_aux.AppendLine();
                        stb_aux.Append($"".PadLeft(20));

                        for (int m = 0; m < 15; m++)
                            stb_aux.Append($"{c[j][m]} ");

                        stb_aux.AppendLine();

                        stb_aux.AppendLine($"{acertos_formatado} pontos");

                        if (filtraResults)
                        {
                            if (Regex.IsMatch($"{acertos_formatado}", $"{Conferir.Filtro_Results}"))
                            {
                                stb_Results.AppendLine($"{stb_aux}");
                            }
                        }
                        else
                        {
                            stb_Results.AppendLine($"{stb_aux}");
                        }

                        stb_aux.Clear();
                    }
                }
            }
            else
            {
                for (int i = 0; i < Conferir.Files.FileNames.Length; i++)
                {
                    arquivoJogos = ReadData.LerArquivoPossibilidade(Conferir.Files.FileNames[i], Conferir.Pular_Linhas);
                    FileInfo flInfo = new FileInfo(Conferir.Files.FileNames[i]);

                    for (int k = 0; k < arquivoJogos.Count; k++)
                    {
                        for (int j = 0; j < c.Count; j++)
                        {
                            acertos = Estatiscas.Acertos(c[j], arquivoJogos[k]);
                            acertos_formatado = $"{string.Format("{0:00}", acertos)}";

                            acertos_count[15 - acertos]++;


                            stb_aux.AppendLine("---------------------------------------------------------------");
                            stb_aux.AppendLine($"{string.Format("{0:0000} - ", i+1)}Arquivo: {flInfo.Name} - Linha: " +
                                               $"{string.Format("{0:000}", k + Conferir.Pular_Linhas + 1)}");

                            stb_aux.Append($"".PadLeft(20));

                            for (int n = 0; n < arquivoJogos[k].Length; n++)
                                stb_aux.Append($"{arquivoJogos[k][n]} ");

                            stb_aux.AppendLine();

                            stb_aux.Append($"Concurso - {c[j][28]}");
                            stb_aux.AppendLine();
                            stb_aux.Append($"".PadLeft(20));

                            for (int m = 0; m < 15; m++)
                                stb_aux.Append($"{c[j][m]} ");

                            stb_aux.AppendLine();

                            stb_aux.AppendLine($"{acertos_formatado} pontos");

                            if (filtraResults)
                            {
                                if (Regex.IsMatch($"{acertos_formatado}", $"{Conferir.Filtro_Results}"))
                                {
                                    stb_Results.AppendLine($"{stb_aux}");
                                }
                            }
                            else
                            {
                                stb_Results.AppendLine($"{stb_aux}");
                            }

                            stb_aux.Clear();
                        }
                    }
                }
            }

            stb_Resumo_Results.AppendLine($"Pontos{"".PadLeft(10)}Qntd");

            if (filtraResults)
            {
                stb_Resumo_Results.AppendLine($"{"".PadLeft(4)}{Conferir.Filtro_Results}{"".PadRight(14)}" +
                                              $"{string.Format("{0:00000}", acertos_count[15 - int.Parse($"{Conferir.Filtro_Results}")])}");
            }
            else
            {
                for (int i = 0, j = 15; i < 11; i++, j--)
                {
                    if (acertos_count[i] != 0)
                    {
                        stb_Resumo_Results.AppendLine($"{"".PadLeft(4)}{string.Format("{0:00}", j)}{"".PadRight(14)}" +
                                                      $"{string.Format("{0:00000}", acertos_count[i])}");
                    }
                }
            }

            Conferir.Results = $"{stb_Results}";
            Conferir.Resumo_Results = $"{stb_Resumo_Results}";
        }
           
        public void ImportarArquivos()
        {
            Microsoft.Win32.OpenFileDialog openfdlg = new Microsoft.Win32.OpenFileDialog
            {
                Multiselect = true,
                CheckFileExists = true,
                InitialDirectory = @"D:\Debug\DADOS\LOTOFACIL",
                Filter = @"Arquivo de texto|*.txt",
                Title = "Selecione os arquivos da lotofácil"
            };

            Nullable<bool> result = openfdlg.ShowDialog();

            if (result == true)
            {
                Conferir.Files = openfdlg;
            }

        }

        public void limpaPrimos()
        {
            regrasLoto.PrimosMin = 0;
            regrasLoto.PrimosMax = 0;
        }

        public JogarLotofacil Regras
        {
            get { return this.regrasLoto; }
        }

        public Fechamento Fechamento
        {
            get { return this.fechamento; }
        }

        public ConferirJogo Conferir
        {
            get { return this.conferir; }
        }

        protected virtual void OnPropertyEventHandler(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ExportFile()
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog
            {
                FileName = @"JogosLoto",
                DefaultExt = @".txt",
                Filter = @"Arquivo de texto|*.txt"
            };

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                Util.criarArquivo(dlg.FileName, regrasLoto.Jogos);
                System.Windows.MessageBox.Show($"{dlg.FileName}", "Arquivo exportado com sucesso!");
            }
        }

        public void limparJogos()
        {
            regrasLoto.Clear();
            conferir.Output = false;
            conferir.Habilita_Output = false;
        }

        public void gerarJogos()
        {
            GerarJogos gej = new GerarJogos(regrasLoto.QntdJogos);
            List<string[]> jogos = gej.Gerar(regrasLoto);

            for (int i = 0; i < jogos.Count; i++)
            {
                regrasLoto.Add(Util.ConvertToArrayString(jogos[i]));
            }

            if (regrasLoto.QntdJogos == jogos.Count)
                System.Windows.MessageBox.Show($"Foram gerados {jogos.Count} jogos", "Fim de processsamento!");
            else
                System.Windows.MessageBox.Show($"Foram gerados {jogos.Count} jogos, pois não existe {regrasLoto.QntdJogos} possibilidades " +
                    $"com os padrões configurados", "Fim de processsamento!");

            Conferir.Habilita_Output = true;

        }

        public void gerarVolantes()
        {
            Volantes vl = new Volantes(regrasLoto);
            vl.Show();
        }

        //public void atualizarDataBase()
        //{
        //    try
        //    {
        //        BaixaBaseDados bx = new BaixaBaseDados(zip, fileHtm, file);
        //        bx.AtualizarDataBase();


        //        var results = ReadData.read(file, 1);
        //        Conferir.Concurso_Ini = int.Parse(results[0].concurso);
        //        Conferir.Concurso_End = int.Parse(results[0].concurso);
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }
        //}
    }
}
