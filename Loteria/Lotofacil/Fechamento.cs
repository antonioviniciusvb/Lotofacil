using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lotofacil
{
    public class Fechamento : INotifyPropertyChanged, IDataErrorInfo
    {
        public string Error => throw new NotImplementedException();

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        

        public Fechamento(int dezena, int volante, int garantia, int condicao)
        {
            this.Total_Dezenas = dezena;
            this.Volante = volante;
            this.Garantia = garantia;
            this.Condicao = condicao;
            this.Ativa_ToggleSwitch = false;
        }

        private string _dezenas;
        public string Dezenas
        {
            get { return _dezenas; }
            set
            {
                _dezenas = value;
                Dezenas_Count = Regex.Matches(Dezenas, Validacoes.Dezena_Valida).Count;

                if (this._total_dezenas == this._dezenas_Count)
                {
                    Ativa_ToggleSwitch = true;
                }
                else
                {
                    Ativa_ToggleSwitch = false;
                    Considerar_15_Primeiras = false;
                }

                config_18_15_14_15();
                this.OnPropertyChanged("Dezenas");
            }
        }

        private int _dezenas_Count;
        public int Dezenas_Count
        {
            get { return _dezenas_Count; }
            set
            {
                _dezenas_Count = value;
                this.OnPropertyChanged("Dezenas_Count");
            }
        }

        private int _total_dezenas;
        public int Total_Dezenas
        {
            get { return _total_dezenas; }
            set
            {
                _total_dezenas = value;

                config_18_15_14_15();
                this.OnPropertyChanged("Dezenas");
            }
        }

        private int _volante;
        public int Volante
        {
            get { return _volante; }
            set
            {
                _volante = value;
                config_18_15_14_15();
                this.OnPropertyChanged("Volante");
            }
        }



        private int _garantia;
        public int Garantia
        {
            get { return _garantia; }
            set
            {
                _garantia = value;
                config_18_15_14_15();
                this.OnPropertyChanged("Garantia");
            }
        }


        private int _condicao;
        public int Condicao
        {
            get { return _condicao; }
            set
            {
                _condicao = value;
                config_18_15_14_15();
                this.OnPropertyChanged("Condicao");

            }
        }


        private string _informativo;
        public string Informativo
        {
            get { return _informativo; }
            set
            {
                _informativo = value;
                this.OnPropertyChanged("Informativo");

            }
        }

        private bool _ativa_ToggleSwitch;
        public bool Ativa_ToggleSwitch
        {
            get { return _ativa_ToggleSwitch; }
            set
            {
                _ativa_ToggleSwitch = value;
                this.OnPropertyChanged("Ativa_ToggleSwitch");

            }
        }

        private bool _considerar_15_Primeiras;
        public bool Considerar_15_Primeiras
        {
            get { return _considerar_15_Primeiras; }
            set
            {
                _considerar_15_Primeiras = value;
                this.OnPropertyChanged("Considerar_15_Primeiras");

            }
        }

        private bool _config_Correta;
        public bool Config_Correta
        {
            get { return _config_Correta; }
            set
            {
                _config_Correta = value;
                this.OnPropertyChanged("Config_Correta");

            }
        }

        public void config_18_15_14_15()
        {
            if(Total_Dezenas == 18 && Volante == 15 && Garantia == 14 && Condicao == 15)
            {
                Informativo = "São necessários 24 jogos para o fechamento configurado";
                Config_Correta = true;
            }
            else
            {
                Informativo = "Configurações Inválidas.";
                Config_Correta = false;
            }
        }


        public string this[string columnName]
        {
            get
            {
                if ((columnName.Equals("Dezenas") && !string.IsNullOrEmpty(Dezenas)))
                {

                    if (Regex.IsMatch(Dezenas, Validacoes.Apenas_Digitos_Espacos))
                    {
                        Dezenas = Regex.Replace(Dezenas, Validacoes.Apenas_Digitos_Espacos, "");
                        return $"Caracter inválido";
                    }

                    if (Regex.IsMatch(Dezenas, Validacoes.Dezena_Invalida))
                    {
                        Dezenas = Regex.Replace(Dezenas, Validacoes.Dezena_Invalida, "");
                        return $"Dezena inválida removida";
                    }


                    if (Regex.IsMatch(Dezenas, Validacoes.Dezena_Repetida))
                    {
                        Dezenas = Regex.Replace(Dezenas, Validacoes.Dezena_Repetida, "$1$2");
                        return $"Dezena repetida removida";
                    }


                    

                    if (Dezenas_Count < Total_Dezenas)
                    {
                        return $"Falta {Total_Dezenas - Dezenas_Count} para {Total_Dezenas}";
                    }

                    if(Dezenas_Count > Total_Dezenas)
                    {
                        Dezenas = Regex.Replace(Dezenas, Validacoes.Ultima_Dezena, "");
                    }
                }

        

                return null;
            }
        }
    }
}
