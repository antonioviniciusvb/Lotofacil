using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Reporting.WinForms;


namespace Lotofacil
{
    /// <summary>
    /// Lógica interna para Volantes.xaml
    /// </summary>
    public partial class Volantes : Window
    {
        List<JogosGerados> dadosRelatorio;

        //public Volantes(List<string> jogos)
        //{
        //    //Load = "ReportViewer_Load"
        //    InitializeComponent();
        //    addJogos(jogos);
        //    var dataSource = new Microsoft.Reporting.WinForms.ReportDataSource("Jogos", dadosRelatorio);
        //    ReportViewer.LocalReport.DataSources.Add(dataSource);
        //    ReportViewer.LocalReport.ReportEmbeddedResource = "Lotofacil.Volantes.rdlc";
        //    this.ReportViewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
        //    ReportViewer.RefreshReport();
        //}

        public Volantes(JogarLotofacil jogos)
        {
            //Load = "ReportViewer_Load"
            InitializeComponent();
            addJogos(jogos);
            var dataSource = new Microsoft.Reporting.WinForms.ReportDataSource("Jogos", dadosRelatorio);
            ReportViewer.LocalReport.DataSources.Add(dataSource);
            ReportViewer.LocalReport.ReportEmbeddedResource =   "Lotofacil.Relatorios.Volantes.rdlc";
            this.ReportViewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            ReportViewer.RefreshReport();
        }
        private void addJogos(JogarLotofacil regras)
        {
                dadosRelatorio = new List<JogosGerados>();
                List<bool[]> auxResults = new List<bool[]>();

                int nJogos = regras.Jogos.Count;

                for (int i = 0; i < regras.Jogos.Count; i++)
                {
                    string[] dezenas = Regex.Split(regras.Jogos[i], " ");
                    bool[] results = new bool[25];

                    for (int j = 0; j < 15; j++)
                    {
                        if (int.Parse(dezenas[j]) == 1)
                            results[0] = true;
                        else
                        if (int.Parse(dezenas[j]) == 2)
                            results[1] = true;
                        else
                        if (int.Parse(dezenas[j]) == 3)
                            results[2] = true;
                        else
                        if (int.Parse(dezenas[j]) == 4)
                            results[3] = true;
                        else
                        if (int.Parse(dezenas[j]) == 5)
                            results[4] = true;
                        else
                        if (int.Parse(dezenas[j]) == 6)
                            results[5] = true;
                        else
                        if (int.Parse(dezenas[j]) == 7)
                            results[6] = true;
                        else
                        if (int.Parse(dezenas[j]) == 8)
                            results[7] = true;
                        else
                        if (int.Parse(dezenas[j]) == 9)
                            results[8] = true;
                        else
                        if (int.Parse(dezenas[j]) == 10)
                            results[9] = true;
                        else
                        if (int.Parse(dezenas[j]) == 11)
                            results[10] = true;
                        else
                        if (int.Parse(dezenas[j]) == 12)
                            results[11] = true;
                        else
                        if (int.Parse(dezenas[j]) == 13)
                            results[12] = true;
                        else
                        if (int.Parse(dezenas[j]) == 14)
                            results[13] = true;
                        else
                        if (int.Parse(dezenas[j]) == 15)
                            results[14] = true;
                        else
                        if (int.Parse(dezenas[j]) == 16)
                            results[15] = true;
                        else
                        if (int.Parse(dezenas[j]) == 17)
                            results[16] = true;
                        else
                        if (int.Parse(dezenas[j]) == 18)
                            results[17] = true;
                        else
                        if (int.Parse(dezenas[j]) == 19)
                            results[18] = true;
                        else
                        if (int.Parse(dezenas[j]) == 20)
                            results[19] = true;
                        else
                        if (int.Parse(dezenas[j]) == 21)
                            results[20] = true;
                        else
                        if (int.Parse(dezenas[j]) == 22)
                            results[21] = true;
                        else
                        if (int.Parse(dezenas[j]) == 23)
                            results[22] = true;
                        else
                        if (int.Parse(dezenas[j]) == 24)
                            results[23] = true;
                        else
                        if (int.Parse(dezenas[j]) == 25)
                            results[24] = true;
                    }

                    auxResults.Add(results);
                }

                for (int i = 0; i < regras.Jogos.Count; i++)
                {
                    if (nJogos >= 3)
                    {

                    dadosRelatorio.Add(
                        new JogosGerados()
                        {
                            Id = i,
                            bola_1 = auxResults[i][0],
                            bola_2 = auxResults[i][1],
                            bola_3 = auxResults[i][2],
                            bola_4 = auxResults[i][3],
                            bola_5 = auxResults[i][4],

                            bola_6 = auxResults[i][5],
                            bola_7 = auxResults[i][6],
                            bola_8 = auxResults[i][7],
                            bola_9 = auxResults[i][8],
                            bola_10 = auxResults[i][9],

                            bola_11 = auxResults[i][10],
                            bola_12 = auxResults[i][11],
                            bola_13 = auxResults[i][12],
                            bola_14 = auxResults[i][13],
                            bola_15 = auxResults[i][14],

                            bola_16 = auxResults[i][15],
                            bola_17 = auxResults[i][16],
                            bola_18 = auxResults[i][17],
                            bola_19 = auxResults[i][18],
                            bola_20 = auxResults[i][19],

                            bola_21 = auxResults[i][20],
                            bola_22 = auxResults[i][21],
                            bola_23 = auxResults[i][22],
                            bola_24 = auxResults[i][23],
                            bola_25 = auxResults[i][24],

                            bola_26 = auxResults[i + 1][0],
                            bola_27 = auxResults[i + 1][1],
                            bola_28 = auxResults[i + 1][2],
                            bola_29 = auxResults[i + 1][3],
                            bola_30 = auxResults[i + 1][4],

                            bola_31 = auxResults[i + 1][5],
                            bola_32 = auxResults[i + 1][6],
                            bola_33 = auxResults[i + 1][7],
                            bola_34 = auxResults[i + 1][8],
                            bola_35 = auxResults[i + 1][9],

                            bola_36 = auxResults[i + 1][10],
                            bola_37 = auxResults[i + 1][11],
                            bola_38 = auxResults[i + 1][12],
                            bola_39 = auxResults[i + 1][13],
                            bola_40 = auxResults[i + 1][14],

                            bola_41 = auxResults[i + 1][15],
                            bola_42 = auxResults[i + 1][16],
                            bola_43 = auxResults[i + 1][17],
                            bola_44 = auxResults[i + 1][18],
                            bola_45 = auxResults[i + 1][19],

                            bola_46 = auxResults[i + 1][20],
                            bola_47 = auxResults[i + 1][21],
                            bola_48 = auxResults[i + 1][22],
                            bola_49 = auxResults[i + 1][23],
                            bola_50 = auxResults[i + 1][24],

                            bola_51 = auxResults[i + 2][0],
                            bola_52 = auxResults[i + 2][1],
                            bola_53 = auxResults[i + 2][2],
                            bola_54 = auxResults[i + 2][3],
                            bola_55 = auxResults[i + 2][4],

                            bola_56 = auxResults[i + 2][5],
                            bola_57 = auxResults[i + 2][6],
                            bola_58 = auxResults[i + 2][7],
                            bola_59 = auxResults[i + 2][8],
                            bola_60 = auxResults[i + 2][9],

                            bola_61 = auxResults[i + 2][10],
                            bola_62 = auxResults[i + 2][11],
                            bola_63 = auxResults[i + 2][12],
                            bola_64 = auxResults[i + 2][13],
                            bola_65 = auxResults[i + 2][14],

                            bola_66 = auxResults[i + 2][15],
                            bola_67 = auxResults[i + 2][16],
                            bola_68 = auxResults[i + 2][17],
                            bola_69 = auxResults[i + 2][18],
                            bola_70 = auxResults[i + 2][19],

                            bola_71 = auxResults[i + 2][20],
                            bola_72 = auxResults[i + 2][21],
                            bola_73 = auxResults[i + 2][22],
                            bola_74 = auxResults[i + 2][23],
                            bola_75 = auxResults[i + 2][24],
                            Jogos = $"------------------------------------- Regras  -------------------------------------" + Environment.NewLine + 
                            $"Padrão: {regras.Padrao}" + Environment.NewLine + $"Obrigatórios: {regras.Obrigatorio_1}-{regras.Obrigatorio_2}" +
                            $"-{regras.Obrigatorio_3}-{regras.Obrigatorio_4}-{regras.Obrigatorio_5}-{regras.Obrigatorio_6}-" +
                            $"{regras.Obrigatorio_7}-{regras.Obrigatorio_8}-{regras.Obrigatorio_9}-{regras.Obrigatorio_10}" + Environment.NewLine +
                            $"Exclusão:      {regras.Descartavel_1}-{regras.Descartavel_2}-{regras.Descartavel_3}-{regras.Descartavel_4}-{regras.Descartavel_5}" +
                            $"-{regras.Descartavel_6}-{regras.Descartavel_7}-{regras.Descartavel_8}-{regras.Descartavel_9}-{regras.Descartavel_10}" + Environment.NewLine +                            
                            $"Dezenas em Seq: {regras.SeqDezenas}" + Environment.NewLine +
                            $"------------------------------------- Jogos  -------------------------------------" + Environment.NewLine +
                            $"{i+1}/{regras.Jogos.Count}: {regras.Jogos[i]}" + Environment.NewLine +
                            $"{i+2}/{regras.Jogos.Count}: {regras.Jogos[i + 1]}"+ Environment.NewLine +
                            $"{i+3}/{regras.Jogos.Count}: {regras.Jogos[i + 2]}"
                            });

                        i++;
                        i++;
                        nJogos = nJogos - 3;

                    }
                    else
                    if (nJogos == 1)
                    {
                        dadosRelatorio.Add(
                         new JogosGerados()
                         {
                             Id = i,
                             bola_1 = auxResults[i][0],
                             bola_2 = auxResults[i][1],
                             bola_3 = auxResults[i][2],
                             bola_4 = auxResults[i][3],
                             bola_5 = auxResults[i][4],

                             bola_6 = auxResults[i][5],
                             bola_7 = auxResults[i][6],
                             bola_8 = auxResults[i][7],
                             bola_9 = auxResults[i][8],
                             bola_10 = auxResults[i][9],

                             bola_11 = auxResults[i][10],
                             bola_12 = auxResults[i][11],
                             bola_13 = auxResults[i][12],
                             bola_14 = auxResults[i][13],
                             bola_15 = auxResults[i][14],

                             bola_16 = auxResults[i][15],
                             bola_17 = auxResults[i][16],
                             bola_18 = auxResults[i][17],
                             bola_19 = auxResults[i][18],
                             bola_20 = auxResults[i][19],

                             bola_21 = auxResults[i][20],
                             bola_22 = auxResults[i][21],
                             bola_23 = auxResults[i][22],
                             bola_24 = auxResults[i][23],
                             bola_25 = auxResults[i][24],

                             bola_26 = false,
                             bola_27 = false,
                             bola_28 = false,
                             bola_29 = false,
                             bola_30 = false,

                             bola_31 = false,
                             bola_32 = false,
                             bola_33 = false,
                             bola_34 = false,
                             bola_35 = false,

                             bola_36 = false,
                             bola_37 = false,
                             bola_38 = false,
                             bola_39 = false,
                             bola_40 = false,

                             bola_41 = false,
                             bola_42 = false,
                             bola_43 = false,
                             bola_44 = false,
                             bola_45 = false,

                             bola_46 = false,
                             bola_47 = false,
                             bola_48 = false,
                             bola_49 = false,
                             bola_50 = false,

                             bola_51 = false,
                             bola_52 = false,
                             bola_53 = false,
                             bola_54 = false,
                             bola_55 = false,

                             bola_56 = false,
                             bola_57 = false,
                             bola_58 = false,
                             bola_59 = false,
                             bola_60 = false,

                             bola_61 = false,
                             bola_62 = false,
                             bola_63 = false,
                             bola_64 = false,
                             bola_65 = false,

                             bola_66 = false,
                             bola_67 = false,
                             bola_68 = false,
                             bola_69 = false,
                             bola_70 = false,

                             bola_71 = false,
                             bola_72 = false,
                             bola_73 = false,
                             bola_74 = false,
                             bola_75 = false,
                             Jogos = $"------------------------------------- Regras  -------------------------------------" + Environment.NewLine + 
                            $"Padrão: {regras.Padrao}" + Environment.NewLine + $"Obrigatórios: {regras.Obrigatorio_1}-{regras.Obrigatorio_2}" +
                            $"-{regras.Obrigatorio_3}-{regras.Obrigatorio_4}-{regras.Obrigatorio_5}-{regras.Obrigatorio_6}-" +
                            $"{regras.Obrigatorio_7}-{regras.Obrigatorio_8}-{regras.Obrigatorio_9}-{regras.Obrigatorio_10}" + Environment.NewLine +
                            $"Exclusão:      {regras.Descartavel_1}-{regras.Descartavel_2}-{regras.Descartavel_3}-{regras.Descartavel_4}-{regras.Descartavel_5}" +
                            $"-{regras.Descartavel_6}-{regras.Descartavel_7}-{regras.Descartavel_8}-{regras.Descartavel_9}-{regras.Descartavel_10}" + Environment.NewLine +
                            $"Dezenas em Seq: {regras.SeqDezenas}" + Environment.NewLine +
                            $"------------------------------------- Jogos  -------------------------------------" + Environment.NewLine +
                            $"{i+1}/{regras.Jogos.Count}: {regras.Jogos[i]}"
                         });

                        nJogos = nJogos - 1;
                    }
                    else
                    if (nJogos == 2)
                    {
                    dadosRelatorio.Add(
                    new JogosGerados()
                    {
                        Id = i,
                        bola_1 = auxResults[i][0],
                        bola_2 = auxResults[i][1],
                        bola_3 = auxResults[i][2],
                        bola_4 = auxResults[i][3],
                        bola_5 = auxResults[i][4],

                        bola_6 = auxResults[i][5],
                        bola_7 = auxResults[i][6],
                        bola_8 = auxResults[i][7],
                        bola_9 = auxResults[i][8],
                        bola_10 = auxResults[i][9],

                        bola_11 = auxResults[i][10],
                        bola_12 = auxResults[i][11],
                        bola_13 = auxResults[i][12],
                        bola_14 = auxResults[i][13],
                        bola_15 = auxResults[i][14],

                        bola_16 = auxResults[i][15],
                        bola_17 = auxResults[i][16],
                        bola_18 = auxResults[i][17],
                        bola_19 = auxResults[i][18],
                        bola_20 = auxResults[i][19],

                        bola_21 = auxResults[i][20],
                        bola_22 = auxResults[i][21],
                        bola_23 = auxResults[i][22],
                        bola_24 = auxResults[i][23],
                        bola_25 = auxResults[i][24],

                        bola_26 = auxResults[i + 1][0],
                        bola_27 = auxResults[i + 1][1],
                        bola_28 = auxResults[i + 1][2],
                        bola_29 = auxResults[i + 1][3],
                        bola_30 = auxResults[i + 1][4],

                        bola_31 = auxResults[i + 1][5],
                        bola_32 = auxResults[i + 1][6],
                        bola_33 = auxResults[i + 1][7],
                        bola_34 = auxResults[i + 1][8],
                        bola_35 = auxResults[i + 1][9],

                        bola_36 = auxResults[i + 1][10],
                        bola_37 = auxResults[i + 1][11],
                        bola_38 = auxResults[i + 1][12],
                        bola_39 = auxResults[i + 1][13],
                        bola_40 = auxResults[i + 1][14],

                        bola_41 = auxResults[i + 1][15],
                        bola_42 = auxResults[i + 1][16],
                        bola_43 = auxResults[i + 1][17],
                        bola_44 = auxResults[i + 1][18],
                        bola_45 = auxResults[i + 1][19],

                        bola_46 = auxResults[i + 1][20],
                        bola_47 = auxResults[i + 1][21],
                        bola_48 = auxResults[i + 1][22],
                        bola_49 = auxResults[i + 1][23],
                        bola_50 = auxResults[i + 1][24],

                        bola_51 = false,
                        bola_52 = false,
                        bola_53 = false,
                        bola_54 = false,
                        bola_55 = false,

                        bola_56 = false,
                        bola_57 = false,
                        bola_58 = false,
                        bola_59 = false,
                        bola_60 = false,

                        bola_61 = false,
                        bola_62 = false,
                        bola_63 = false,
                        bola_64 = false,
                        bola_65 = false,

                        bola_66 = false,
                        bola_67 = false,
                        bola_68 = false,
                        bola_69 = false,
                        bola_70 = false,

                        bola_71 = false,
                        bola_72 = false,
                        bola_73 = false,
                        bola_74 = false,
                        bola_75 = false,
                        Jogos = $"------------------------------------- Regras  -------------------------------------" + Environment.NewLine + 
                        $"Padrão: {regras.Padrao}" + Environment.NewLine + $"Obrigatórios: {regras.Obrigatorio_1}-{regras.Obrigatorio_2}" +
                        $"-{regras.Obrigatorio_3}-{regras.Obrigatorio_4}-{regras.Obrigatorio_5}-{regras.Obrigatorio_6}-" +
                        $"{regras.Obrigatorio_7}-{regras.Obrigatorio_8}-{regras.Obrigatorio_9}-{regras.Obrigatorio_10}" + Environment.NewLine +
                        $"Exclusão:      {regras.Descartavel_1}-{regras.Descartavel_2}-{regras.Descartavel_3}-{regras.Descartavel_4}-{regras.Descartavel_5}" +
                        $"-{regras.Descartavel_6}-{regras.Descartavel_7}-{regras.Descartavel_8}-{regras.Descartavel_9}-{regras.Descartavel_10}" + Environment.NewLine + 
                        $"Dezenas em Seq: {regras.SeqDezenas}" + Environment.NewLine +
                        $"------------------------------------- Jogos  -------------------------------------" + Environment.NewLine +
                        $"{ i + 1}/{regras.Jogos.Count}: {regras.Jogos[i]}" + Environment.NewLine +
                        $"{i + 2}/{regras.Jogos.Count}: {regras.Jogos[i + 1]}"
                        });

                        i = i + 2;
                        nJogos = nJogos - 2;
                    }

                }
   
            Console.WriteLine(dadosRelatorio);

        }
    }
}
