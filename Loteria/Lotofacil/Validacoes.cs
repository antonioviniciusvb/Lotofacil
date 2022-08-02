using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Lotofacil
{
   public static class Validacoes
    {

        public static string Apenas_Digitos_Espacos = @"[^ \d]";
        public static string Apenas_Digitos = @"[^\d]";
        public static string Dezena_Invalida = @"\b2[6-9]\b|\b\d{3,}\b|00|\b[3-9]\d+\b";
        public static string Dezena_Repetida = @"(\b0[1-9]\b|\b1\d\b|\b2[0-5]\b)(.*)\1";
        public static string Dezena_Valida = @"\b0[1-9]\b|\b1\d\b|\b2[0-5]\b";
        public static string Ultima_Dezena = @"\d\d *$";



        public static bool validaConferir(ConferirJogo config)
        {
            if (((!string.IsNullOrWhiteSpace(config.Volante) && Regex.Matches(config.Volante, Dezena_Valida).Count > 14) && (!string.IsNullOrWhiteSpace(config.Filtro_Results)) ||
               (config.Files != null && config.Files.FileNames.Count() > 0 ) && (!string.IsNullOrWhiteSpace(config.Filtro_Results)) ||
               (config.Output) && (!string.IsNullOrWhiteSpace(config.Filtro_Results))))
                 return true;

            return false;
        }

        public static bool validaRegras(JogarLotofacil regras)
        {
            //Debug.WriteLine($"OP -- NO  {regras.UtilizandoOpcaoNormal}");
            //Debug.WriteLine($"OP -- OBR {regras.UtilizandoNsObrigatorios}");
            //Debug.WriteLine($"OP -- DES {regras.UtilizandoDescartavel}");
            //Debug.WriteLine($"OP -- MIX {regras.UtilizandoPrimosMinMax}");
            //Debug.WriteLine($"OP -- SEQ {regras.UtilizandoSequenciaDezenas}");


            if (regras.UtilizandoOpcaoNormal && regras.UtilizandoDescartavel == false && regras.UtilizandoNsObrigatorios == false &&
                  regras.UtilizandoPrimosMinMax == false && regras.UtilizandoSequenciaDezenas == false)
                return true;
            else
            if(regras.UtilizandoOpcaoNormal == true && (regras.UtilizandoDescartavel == true || regras.UtilizandoNsObrigatorios == false ||
                  regras.UtilizandoPrimosMinMax == false || regras.UtilizandoSequenciaDezenas == false))
            {
                return true;
            }

            return false;
        }


        public static bool validaFechamento(Fechamento fechamento)
        {
            if(fechamento.Config_Correta && !string.IsNullOrWhiteSpace(fechamento.Dezenas) && fechamento.Dezenas_Count == fechamento.Total_Dezenas)
              return true;

            return false;
        }

        private static bool obrigatoriosForamInseridos(JogarLotofacil regras)
        {
            if (regras.Obrigatorio_1 > 0 || regras.Obrigatorio_2 > 0 || regras.Obrigatorio_3 > 0 ||
                regras.Obrigatorio_4 > 0 || regras.Obrigatorio_5 > 0)
                return true;
            else
                return false;
        }

        private static bool descartaveisForamInseridos(JogarLotofacil regras)
        {
            if (regras.Descartavel_1 > 0 || regras.Descartavel_2 > 0 || regras.Descartavel_3 > 0 ||
                regras.Descartavel_4 > 0 || regras.Descartavel_5 > 0)
                return true;
            else
                return false;
        }

        
    }
}
