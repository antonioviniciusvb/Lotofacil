using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Lotofacil
{
   public class ConferirJogo : INotifyPropertyChanged, IDataErrorInfo
    {

        private static readonly string file = @"D_LOTFAC_ANALISE.txt";

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ConferirJogo()
        {
            var ultimo_Concurso = ReadData.read(file, 1);
            Concurso_Ini = int.Parse(ultimo_Concurso[0].concurso);
            Concurso_End = int.Parse(ultimo_Concurso[0].concurso);
            Output = false;
            Habilita_Output = false;
        }


        private string _results;
        public string Results
        {
            get { return _results; }
            set
            {
                _results = value;
                this.OnPropertyChanged("Results");
            }
        }

        private string _resumo_Results;
        public string Resumo_Results
        {
            get { return _resumo_Results; }
            set
            {
                _resumo_Results = value;
                this.OnPropertyChanged("Resumo_Results");
            }
        }

        private string _filtro_Results;
        public string Filtro_Results
        {
            get { return _filtro_Results; }
            set
            {
                _filtro_Results = value;
                this.OnPropertyChanged("Filtro_Results");
            }
        }

        private int _pularLinhas;
        public int Pular_Linhas
        {
            get
            {
                return this._pularLinhas;
            }
            set
            {
                _pularLinhas = value;
                this.OnPropertyChanged("Pular_Linhas");
            }
        }

        private int _concurso_ini;
        public int Concurso_Ini
        {
            get
            {
                return this._concurso_ini;
            }
            set
            {
                _concurso_ini = value;
                this.OnPropertyChanged("Concurso_Ini");
            }
        }

        private int _concurso_end;
        public int Concurso_End
        {
            get
            {
                return this._concurso_end;
            }
            set
            {
                _concurso_end = value;
                this.OnPropertyChanged("Concurso_End");
            }
        }

        private Microsoft.Win32.OpenFileDialog _files;
        public Microsoft.Win32.OpenFileDialog Files
        {
            get
            {
                return _files;
            }
            set
            {
                _files = value;
                this.OnPropertyChanged("Files");
            }
        }

        private string _volante;
        public string Volante
        {
            get { return _volante; }
            set
            {
                _volante = value;
                this.OnPropertyChanged("Volante");
            }
        }

        private bool _output;
        public bool Output
        {
            get { return _output; }
            set
            {
                _output = value;
                this.OnPropertyChanged("Output");
            }
        }

        private bool _habilita_output;
        public bool Habilita_Output
        {
            get { return _habilita_output; }
            set
            {
                _habilita_output = value;
                this.OnPropertyChanged("Habilita_Output");
            }
        }

        public string Error => throw new NotImplementedException();

        public string this[string columnName]
        {
            get
            {
                if ((columnName.Equals("Volante") &&  !string.IsNullOrEmpty(Volante)))
                {

                    if (Regex.IsMatch(Volante, Validacoes.Apenas_Digitos_Espacos))
                    {
                        Volante = Regex.Replace(Volante, Validacoes.Apenas_Digitos_Espacos, "");
                        return $"Caracter inválido";
                    }

                    if (Regex.IsMatch(Volante, Validacoes.Dezena_Invalida))
                    {
                        Volante = Regex.Replace(Volante, Validacoes.Dezena_Invalida, "");
                        return $"Dezena inválida removida";
                    }


                    if (Regex.IsMatch(Volante, Validacoes.Dezena_Repetida))
                    {
                        Volante = Regex.Replace(Volante, Validacoes.Dezena_Repetida, "$1$2");
                        return $"Dezena repetida removida";
                    }
                    

                    int dezenas = Regex.Matches(Volante, Validacoes.Dezena_Valida).Count;

                    if (dezenas < 15)
                    {
                        return $"Falta {15 - dezenas} para 15";
                    }
                }

                if ((columnName.Equals("Concurso_Ini") || (columnName.Equals("Concurso_End"))))
                {
                    if (Concurso_Ini > Concurso_End)
                    {
                        Concurso_Ini = Concurso_End;
                        return null;
                    }

                    var results = ReadData.read(file, 1);

                    if (Concurso_Ini <= 0 || Concurso_Ini > int.Parse(results[0].concurso))
                    {
                        Concurso_Ini = int.Parse(results[0].concurso);
                        return $"de 1 até {results[0].concurso}";
                    }

                    if (Concurso_End <= 0 || Concurso_End > int.Parse(results[0].concurso))
                    {
                        Concurso_End = int.Parse(results[0].concurso);
                        return $"de 1 até {results[0].concurso}";
                    }
                }

                return null;
            }
        }

        public void clear()
        {
            var ultimo_Concurso = ReadData.read(file, 1);
            Concurso_Ini = int.Parse(ultimo_Concurso[0].concurso);
            Concurso_End = int.Parse(ultimo_Concurso[0].concurso);
            Pular_Linhas = 0;
            Files = null;
            Volante = "";
            Filtro_Results = null;
            Habilita_Output = false;
            Output = false;

        }
    }
}
