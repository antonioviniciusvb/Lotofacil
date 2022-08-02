using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Lotofacil
{
    public class BaixaBaseDados
    {
        //private string servidor = "http://www1.caixa.gov.br/loterias/_arquivos/loterias/D_lotfac.zip";

        private string servidor = "https://loterias-caixa-gov.herokuapp.com/api/lotofacil/";

        //private string servidor = "http://loterias.caixa.gov.br/wps/portal/loterias/landing/lotofacil/!ut/p/a1/04_Sj9CPykssy0xPLMnMz0vMAfGjzOLNDH0MPAzcDbz8vTxNDRy9_Y2NQ13CDA0sTIEKIoEKnN0dPUzMfQwMDEwsjAw8XZw8XMwtfQ0MPM2I02-AAzgaENIfrh-FqsQ9wBmoxN_FydLAGAgNTKEK8DkRrACPGwpyQyMMMj0VAcySpRM!/dl5/d5/L2dBISEvZ0FBIS9nQSEh/pw/Z7_HGK818G0K85260Q5OIRSC42046/res/id=historicoHTML/c=cacheLevelPage/=/";
        private string ArquivoZip { get; set; }
        private string fileHtm { get; set; }
        private string fileJson { get; set; }
        private string file { get; set; }

        public BaixaBaseDados(string ArquivoZip,string fileHtm, string file)
        {
            this.ArquivoZip = ArquivoZip;
            this.file = file;
            this.fileHtm = fileHtm;
        }


        private void baixarDados()
        {
            try
            {
                CookieContainer myContainer = new CookieContainer();

                var request = (HttpWebRequest)WebRequest.Create(servidor);
                request.MaximumAutomaticRedirections = 1;
                request.AllowAutoRedirect = true;
                request.CookieContainer = myContainer;
                var response = (HttpWebResponse)request.GetResponse();
                using (var responseStream = response.GetResponseStream())
                {
                    using (var fileStream = new FileStream(
                        Path.Combine(PathData.fileConcursosJson), FileMode.Create))
                    {
                        responseStream.CopyTo(fileStream);
                    }
                }
            }
            catch (Exception)
            {

                throw new Exception($"Erro ao atualizar sua base de dados!{"\n"}");
            }

        }

        public void AtualizarDataBase()
        {
            baixarDados();
            //ExtrairZip.Extrair(this.ArquivoZip, System.AppDomain.CurrentDomain.BaseDirectory.ToString());
            StringBuilder stb = Util.TratarDadosJson(PathData.concursos);
            Util.criarArquivo(this.file, stb);
        }


    }
}
