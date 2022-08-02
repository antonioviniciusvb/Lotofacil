using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ionic.Zip;
using System.IO;

namespace Lotofacil
{
    public static class ExtrairZip
    {

        public static void Extrair(string Filelocalization, string Destiny)
        {
            string file = "D_lotfac.htm";
            if (File.Exists(file))
                File.Delete(file);

            if (File.Exists(Filelocalization))
            {
                using(ZipFile zip = new ZipFile(Filelocalization))
                {
                    zip.ExtractSelectedEntries(file);
                }
            }
        }
    }
}
