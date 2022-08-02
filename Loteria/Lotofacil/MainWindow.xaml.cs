using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Linq;
using System.Windows.Media;

namespace Lotofacil
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        #region Globais
        private static string zip = @"D_LOTFAC_ANALISE.zip", file = @"D_LOTFAC_ANALISE.txt", fileHtm = @"D_lotfac.htm";
        private DispatcherTimer _timer = null;
        private string nameReportJogos = "Jogos", nameReportJogos_Lista = "Jogos_Lista", nameReportFrequencia = "Frequencia",
                       nameReportPrimos = "Primos", nameReportParesImpares = "ParesImpares";
        private Util.Inbits inbits;
        private Dictionary<Expander, bool> expanders;
        private const int qntdExpanders = 5;
        ViewModel vm;
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            _timer = new DispatcherTimer();
            _timer.Tick += new EventHandler(_timer_Tick);
            _timer.Interval = new TimeSpan(0, 100, 0);
            _timer.Start();

            inbits = new Util.Inbits(0);

            List<string> modo = new List<string> { "15", "16", "17", "18" };
            List<string> padroesJogos = new List<string>
            {
                "1) P=08 I=07", "2) P=07 I=08", "3) P=09 I=06",
                "4) P=06 I=09", "5) P=10 I=05", "6) P=05 I=10"
            };

            List<string> seqDezenas = new List<string>
            {
                "3","4","5","6","7","8","9","10"
            };

            List<string> filtro_Results = new List<string> {"Todos", "15", "14", "13", "12", "11"};

            lsBoxQntdDezenas.ItemsSource = modo;
            lstBoxSequenciaDezenas.ItemsSource = seqDezenas;
            lstBoxPadroes.ItemsSource = padroesJogos;
            lstFiltroConferirPontos.ItemsSource = filtro_Results;
            vm = new ViewModel();
            this.DataContext = vm;


            //StringBuilder stb = Util.TratarDados(PathData.concursos);
            //Util.criarArquivo(this.file, stb);

            //btnAtualizarDataBase.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

            var c = ReadData.GetConcursos(10).OrderBy(x => x.concurso);

            var results = ReadData.read(file, 0);
            Util.OrdenaResults(ref results);

            var results_ = Util.ConvertResultToArrayString(results);

            //var count = 0;
            //for (int i = 0; i < results_.Count; i++)
            //{
            //    if (Estatiscas.qntdNumeros_1_a_12(results_[i]))
            //        count++;
            //}

            //Debug.WriteLine(count);


            //Estatiscas.MaiorDezena20(results_, 0, 1, 5);
            //Estatiscas.MaiorDezena20(results_, 1, 1, 5);
            //Estatiscas.MaiorDezena20(results_, 2, 1, 5);
            //Estatiscas.MaiorDezena20(results_, 3, 1, 5);
            //Estatiscas.MaiorDezena20(results_, 4, 1, 5);
            //Estatiscas.MaiorDezena20(results_, 5, 1, 5);


            //Estatiscas.MaiorDezena20(results_, 0, 6, 10);
            //Estatiscas.MaiorDezena20(results_, 1, 6, 10);
            //Estatiscas.MaiorDezena20(results_, 2, 6, 10);
            //Estatiscas.MaiorDezena20(results_, 3, 6, 10);
            //Estatiscas.MaiorDezena20(results_, 4, 6, 10);
            //Estatiscas.MaiorDezena20(results_, 5, 6, 10);


            //Estatiscas.MaiorDezena20(results_, 0, 11, 15);
            //Estatiscas.MaiorDezena20(results_, 1, 11, 15);
            //Estatiscas.MaiorDezena20(results_, 2, 11, 15);
            //Estatiscas.MaiorDezena20(results_, 3, 11, 15);
            //Estatiscas.MaiorDezena20(results_, 4, 11, 15);
            //Estatiscas.MaiorDezena20(results_, 5, 11, 15);


            //Estatiscas.MaiorDezena20(results_, 0, 16, 20);
            //Estatiscas.MaiorDezena20(results_, 1, 16, 20);
            //Estatiscas.MaiorDezena20(results_, 2, 16, 20);
            //Estatiscas.MaiorDezena20(results_, 3, 16, 20);
            //Estatiscas.MaiorDezena20(results_, 4, 16, 20);
            //Estatiscas.MaiorDezena20(results_, 5, 16, 20);

            //Estatiscas.MaiorDezena20(results_, 0, 21, 25);
            //Estatiscas.MaiorDezena20(results_, 1, 21, 25);
            //Estatiscas.MaiorDezena20(results_, 2, 21, 25);
            //Estatiscas.MaiorDezena20(results_, 3, 21, 25);
            //Estatiscas.MaiorDezena20(results_, 4, 21, 25);
            //Estatiscas.MaiorDezena20(results_, 5, 21, 25);

        }


        #region Atualiza Relatorios

        private void AtualizaRelatorios(string filtro, string report, ref ReportViewer reportViewer)
        {
            bool sucesso = int.TryParse(filtro, out int result);

            if (sucesso)
            {
                if (report == nameReportJogos)
                {
                    if (checkBoxListarResults.IsChecked == true)
                        addResultados(result, "Jogos_Lista", ref reportViewer);
                    else
                        addResultados(result, "Jogos", ref reportViewer);
                }
                else
                {
                    addResultados(result, report, ref reportViewer);
                }

            }
            else
                addResultados(20, report, ref ReportViewer);
        }

        private void addResultados(int filtro, string report, ref ReportViewer reportViewer)
        {
            try
            {
                bool controle = true;
                var results = ReadData.read(file, filtro);
                Util.OrdenaResults(ref results);

                var results_ = Util.ConvertResultToArrayString(results);

                ReportParameter parametro = new ReportParameter();
                Bolas bl = new Bolas();

                //Caso seja 0, considera o total de jogos
                float nJogos = float.Parse(txtFiltroPrimos.Text) == 0 ?
                                     results_.Count : float.Parse(txtFiltroPrimos.Text);

                if (report == nameReportJogos || report == nameReportJogos_Lista)
                {
                    load_Report(report, reportViewer, parametro, results);
                    controle = false;
                }
                else
                if (report == nameReportFrequencia)
                {
                    bl = Estatiscas.frequenciaNumeros(results_);
                    Estatiscas.queryMinMax(ref bl, 8, false);
                }
                else
                if (report == nameReportPrimos)
                {
                    bl = Estatiscas.NumerosPrimos(results_);
                    Estatiscas.queryMinMax(ref bl, 4, true);

                    parametro = new Microsoft.Reporting.WinForms.ReportParameter
                    {
                        Name = "Media",
                    };

                    float media = (bl[1] + bl[2] + bl[4] + bl[6] + bl[10] + bl[12]
                      + bl[16] + bl[18] + bl[22]) / nJogos;

                    parametro.Values.Add($"{media}");
                }
                else
                if (report == nameReportParesImpares)
                {
                    bl = Estatiscas.BolasParesImpares(results_);
                }

                var bolas = new List<Bolas> { bl };
                if (controle)
                    load_Report(report, reportViewer, parametro, null, bolas);

                results = null;
                results_ = null;
                GC.Collect();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private static void load_Report(string report, ReportViewer reportViewer,
                                        ReportParameter parametro = null,
                                        List<Result> concursos = null, List<Bolas> bolas = null)
        {
            ReportDataSource dataSource;

            if (bolas == null)
                dataSource = new ReportDataSource(report, concursos);
            else
                dataSource = new ReportDataSource(report, bolas);

            reportViewer.LocalReport.DataSources.Clear();
            reportViewer.LocalReport.DataSources.Add(dataSource);
            reportViewer.LocalReport.ReportEmbeddedResource = $"Lotofacil.Relatorios.{report}.rdlc";

            //this.ReportViewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);

            if (parametro != null)
                reportViewer.LocalReport.SetParameters(parametro);

            reportViewer.RefreshReport();
        }

        #endregion

        #region Timer
        private void _timer_Tick(object sender, EventArgs e)
        {
            //vm.atualizarDataBase();

            if (inbits[0])
            {
                AtualizaRelatorios(txtFiltroJogos.Text, nameReportJogos, ref ReportViewer);
            }

            if (inbits[1])
            {
                AtualizaRelatorios(txtFiltroFr.Text, nameReportFrequencia,
                    ref ReportViewerFrequencia);
            }

            if (inbits[2])
            {
                AtualizaRelatorios(txtFiltroParesImpares.Text, nameReportParesImpares,
                    ref ReportViewerwfhPadraoParesImpares);
            }

            if (inbits[3])
            {
                AtualizaRelatorios(txtFiltroPrimos.Text, nameReportPrimos,
                    ref ReportViewerwfhNumerosPrimos);
            }

            _timer.Interval = _timer.Interval = new TimeSpan(0, 5, 0);
        }
        #endregion

        #region Concursos
        private void BtnExibirConcursos_Click(object sender, RoutedEventArgs e)
        {
            AtualizaRelatorios(txtFiltroJogos.Text, nameReportJogos, ref ReportViewer);
            checkBoxListarResults.IsEnabled = txtFiltroJogos.IsEnabled = btnFiltroJogos.IsEnabled = true;
        }

        private void BtnFiltroJogos_Click(object sender, RoutedEventArgs e)
        {
            if(checkBoxListarResults.IsChecked == true)
                AtualizaRelatorios(txtFiltroJogos.Text, nameReportJogos_Lista, ref ReportViewer);
            else
                AtualizaRelatorios(txtFiltroJogos.Text, nameReportJogos, ref ReportViewer);

            PainelJogos.Visibility = Visibility.Visible;
            inbits[0] = true;
        }

        #endregion

        private void BtnAtualizarDataBase_Click(object sender, RoutedEventArgs e)
        {
            exibirPaineisLeft(false);
            exibirPaineisRight(false);
            msgm();

            _timer.Interval = new TimeSpan(0, 0, 7);
        }

        #region ParesImpares
        private void BtnFiltroParesImpares_Click(object sender, RoutedEventArgs e)
        {
            AtualizaRelatorios(txtFiltroParesImpares.Text, nameReportParesImpares, ref ReportViewerwfhPadraoParesImpares);
            wfhPadraoParesImpares.Visibility = Visibility.Visible;
            inbits[2] = true;
        }

        #endregion

        #region Primos
        private void BtnFiltroPrimos_Click(object sender, RoutedEventArgs e)
        {
            AtualizaRelatorios(txtFiltroPrimos.Text, nameReportPrimos, ref ReportViewerwfhNumerosPrimos);
            wfhNumeroPrimos.Visibility = Visibility.Visible;
            inbits[3] = true;
        }


        #endregion

        #region Frequencias

        private void BtnFiltroFr_Click(object sender, RoutedEventArgs e)
        {
            AtualizaRelatorios(txtFiltroFr.Text, nameReportFrequencia, ref ReportViewerFrequencia);
            wfhFrenquencia.Visibility = Visibility.Visible;
            inbits[1] = true;
        }
        #endregion

        #region Exibicao
        private void exibirPaineisLeft(bool visibilidade)
        {
            if (visibilidade)
            {
                PainelJogos.Visibility = Visibility.Visible;
            }
            else
            {
                PainelJogos.Visibility = Visibility.Hidden;
            }
        }

        private void exibirPaineisRight(bool visibilidade)
        {
            if (visibilidade)
            {
                wfhFrenquencia.Visibility = wfhNumeroPrimos.Visibility = wfhPadraoParesImpares.Visibility = Visibility.Visible;
            }
            else
            {
                wfhFrenquencia.Visibility = wfhNumeroPrimos.Visibility =
                wfhPadraoParesImpares.Visibility = Visibility.Hidden;
            }
        }

        private void BtnGerar_Click(object sender, RoutedEventArgs e)
        {
            exibirPaineisRight(false);
            FlyoutSaida.IsOpen = true;
        }

        private void ArrowJogar_Click(object sender, RoutedEventArgs e)
        {
            exibirPaineisLeft(false);
            FlyoutJogar.IsOpen = true;
        }

        private void ArrowSaida_Click(object sender, RoutedEventArgs e)
        {
            exibirPaineisRight(false);
            FlyoutSaida.IsOpen = true;
        }

        private void btnFechamento_Click(object sender, RoutedEventArgs e)
        {
            FlyoutSaida.IsOpen = true;
        }

        private void FlyoutSaida_ClosingFinished(object sender, RoutedEventArgs e)
        {
            exibirPaineisRight(true);
        }

        private async void msgm()
        {
            var controll = await this.ShowProgressAsync("Atualizando base de dados", "Aguarde ...", false);

            try
            {
                controll.SetProgress(.40);
                await Task.Delay(1000);

                BaixaBaseDados bx = new BaixaBaseDados(zip, fileHtm, file);
                bx.AtualizarDataBase();

                controll.SetProgress(.90);
                await Task.Delay(1000);

                await controll.CloseAsync();
            }
            catch (Exception ex)
            {
                await controll.CloseAsync();
                var _controll = await this.ShowProgressAsync("Erro", ex.Message, false);
                await Task.Delay(2000);
                await _controll.CloseAsync();
            }

            exibirPaineisLeft(true);
            exibirPaineisRight(true);

        }

        private void FlyoutJogar_ClosingFinished(object sender, RoutedEventArgs e)
        {
            exibirPaineisLeft(true);
        }

        private void MetroWindow_StateChanged(object sender, EventArgs e)
        {

            //Irei verificar quais expanders estão expandido, pois preciso desativa-lo e quando o usuario 
            //máximar irei voltar ao eles ao estado atual, isso implica em um ganho de carregamento

            if (this.WindowState == WindowState.Minimized || this.WindowState == WindowState.Normal)
            {
                expanders = new Dictionary<Expander, bool>
                {
                    {exBaseDados, exBaseDados.IsExpanded},
                    {exConcursos, exConcursos.IsExpanded },
                    {exFrequencia, exFrequencia.IsExpanded },
                    {exParesImpares, exParesImpares.IsExpanded },
                    {exNumerosPrimos, exNumerosPrimos.IsExpanded}
                };

                foreach (var item in expanders)
                    item.Key.IsExpanded = false;
            }


            if (this.WindowState == WindowState.Maximized)
            {
                foreach (var item in expanders)
                    item.Key.IsExpanded = item.Value;
            }

            GC.Collect();

        }
        #endregion


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //conferir(int.Parse(Concr_n.Text));
        }

        private void HotKey_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Right)
            {
                exibirPaineisLeft(false);
                FlyoutJogar.IsOpen = true;
            }

            if (e.Key == System.Windows.Input.Key.Left)
            {
                exibirPaineisRight(false);
                FlyoutSaida.IsOpen = true;
            }
        }
    }
}
