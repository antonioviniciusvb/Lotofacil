using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Lotofacil
{
    public class JogarLotofacil:INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private List<int> listaObrigatorioDescartaveis = null;

        public JogarLotofacil()
        {
            listaObrigatorioDescartaveis = new List<int>();
        }


        private bool _utilizandoOpcaoNormal;
        public bool UtilizandoOpcaoNormal
        {
            get { return this._utilizandoOpcaoNormal; }
        }

        private bool _utilizandoNsObrigatorios;
        public bool UtilizandoNsObrigatorios
        {
            get { return this._utilizandoNsObrigatorios; }
        }

        private bool _utilizando_Descartavel;
        public bool UtilizandoDescartavel
        {
            get { return this._utilizando_Descartavel; }
        }

        private bool _utilizando_PrimosMinMax;
        public bool UtilizandoPrimosMinMax
        {
            get { return this._utilizando_PrimosMinMax; }
        }

        private bool _utilizando_SequenciaDezenas;
        public bool UtilizandoSequenciaDezenas
        {
            get { return this._utilizando_SequenciaDezenas; }
        }

        private List<string> _jogos;
        public List<string> Jogos
        {
            get
            {
                List<string> vs = new List<string>();
                if(_jogos != null)
                  vs.AddRange(_jogos);

                return vs;
            }
        }

        public void Add(string item)
        {
            if (this._jogos == null)
                this._jogos = new List<string>();

            this._jogos.Add(item);
            this.OnPropertyChanged("Jogos");
        }

        public void Clear()
        {
            if (this._jogos != null)
                this._jogos.Clear();

            this.OnPropertyChanged("Jogos");
        }

        public void Swap()
        {
            var obrigatorios = new List<int> { Obrigatorio_1, Obrigatorio_2, Obrigatorio_3,
                                      Obrigatorio_4, Obrigatorio_5, Obrigatorio_6,
                                      Obrigatorio_7, Obrigatorio_8, Obrigatorio_9, Obrigatorio_10 };

            var descartados = new List<int> { Descartavel_1, Descartavel_2, Descartavel_3,
                                      Descartavel_4, Descartavel_5, Descartavel_6,
                                      Descartavel_7, Descartavel_8, Descartavel_9, Descartavel_10 };


            ClearObrigatorioDescartaveis();
            listaObrigatorioDescartaveis.Clear();

            Obrigatorio_1 = descartados[0];
            Obrigatorio_2 = descartados[1];
            Obrigatorio_3 = descartados[2];
            Obrigatorio_4 = descartados[3];
            Obrigatorio_5 = descartados[4];
            Obrigatorio_6 = descartados[5];
            Obrigatorio_7 = descartados[6];
            Obrigatorio_8 = descartados[7];
            Obrigatorio_9 = descartados[8];
            Obrigatorio_10 = descartados[9];

            Descartavel_1 = obrigatorios[0];
            Descartavel_2 = obrigatorios[1];
            Descartavel_3 = obrigatorios[2];
            Descartavel_4 = obrigatorios[3];
            Descartavel_5 = obrigatorios[4];
            Descartavel_6 = obrigatorios[5];
            Descartavel_7 = obrigatorios[6];
            Descartavel_8 = obrigatorios[7];
            Descartavel_9 = obrigatorios[8];
            Descartavel_10 = obrigatorios[9];


        }

        private int _qntdJogos;
        public int QntdJogos
        {
            get { return this._qntdJogos; }
            set
            {
                if (value > 0 && value <= 1000)
                    this._qntdJogos = value;
                else
                    this._qntdJogos = 0;

                this.OnPropertyChanged("QntdJogos");

                this._utilizandoOpcaoNormal = UtilizandoOpNormal();
                this.OnPropertyChanged("UtilizandoOpcaoNormal");
            }
        }

        private bool UtilizandoOpNormal()
        {
            if ((this._modoJogo != 0) && (this._qntdJogos > 0) && (string.IsNullOrEmpty(_padrao) == false))
                return true;
            else
                return false;
        }

        private int _modoJogo;
        public int ModoJogo
        {
            get { return this._modoJogo; }
            set
            {
                this._modoJogo = value;
                this.OnPropertyChanged("ModoJogo");

                this._utilizandoOpcaoNormal = UtilizandoOpNormal();
                this.OnPropertyChanged("UtilizandoOpcaoNormal");
            }
        }

        private string _padrao;
        public string Padrao
        {
            get { return this._padrao; }
            set
            {
                this._padrao = value;
                this.OnPropertyChanged("Padrao");

                this._utilizandoOpcaoNormal = UtilizandoOpNormal();
                this.OnPropertyChanged("UtilizandoOpcaoNormal");
            }
        }

        private string _sequenciaDezenas;
        public string SeqDezenas
        {
            get { return this._sequenciaDezenas; }
            set
            {
                this._sequenciaDezenas = value;
                this.OnPropertyChanged("SeqDezenas");

                this._utilizando_SequenciaDezenas = true;
                this.OnPropertyChanged("UtilizandoSequenciaDezenas");
            }
        }

        private int _obrigatorio_1;
        public int Obrigatorio_1
        {
            get { return this._obrigatorio_1; }
            set
            {
                validaObrigatorioDescartes(ref this._obrigatorio_1, value);

                this.OnPropertyChanged("Obrigatorio_1");

                if (utilizandoOpcionais(this._obrigatorio_1, this._obrigatorio_2, this._obrigatorio_3, this._obrigatorio_4, this._obrigatorio_5,
                    this._obrigatorio_6, this._obrigatorio_7, this._obrigatorio_8, this._obrigatorio_9, this._obrigatorio_10))
                    this._utilizandoNsObrigatorios = true;
                else
                    this._utilizandoNsObrigatorios = false;

                this.OnPropertyChanged("UtilizandoNsObrigatorios");
            }
        }

        private int _obrigatorio_2;
        public int Obrigatorio_2
        {
            get { return this._obrigatorio_2; }
            set
            {
                validaObrigatorioDescartes(ref this._obrigatorio_2, value);

                this.OnPropertyChanged("Obrigatorio_2");

                if (utilizandoOpcionais(this._obrigatorio_1, this._obrigatorio_2, this._obrigatorio_3, this._obrigatorio_4, this._obrigatorio_5,
                       this._obrigatorio_6, this._obrigatorio_7, this._obrigatorio_8, this._obrigatorio_9, this._obrigatorio_10))
                    this._utilizandoNsObrigatorios = true;
                else
                    this._utilizandoNsObrigatorios = false;

                this.OnPropertyChanged("UtilizandoNsObrigatorios");
            }
        }

        private int _obrigatorio_3;
        public int Obrigatorio_3
        {
            get { return this._obrigatorio_3; }
            set
            {
                validaObrigatorioDescartes(ref this._obrigatorio_3, value);

                this.OnPropertyChanged("Obrigatorio_3");

                if (utilizandoOpcionais(this._obrigatorio_1, this._obrigatorio_2, this._obrigatorio_3, this._obrigatorio_4, this._obrigatorio_5,
                       this._obrigatorio_6, this._obrigatorio_7, this._obrigatorio_8, this._obrigatorio_9, this._obrigatorio_10))
                    this._utilizandoNsObrigatorios = true;
                else
                    this._utilizandoNsObrigatorios = false;

                this.OnPropertyChanged("UtilizandoNsObrigatorios");
            }
        }

        private int _obrigatorio_4;
        public int Obrigatorio_4
        {
            get { return this._obrigatorio_4; }
            set
            {
                validaObrigatorioDescartes(ref this._obrigatorio_4, value);

                this.OnPropertyChanged("Obrigatorio_4");

                if (utilizandoOpcionais(this._obrigatorio_1, this._obrigatorio_2, this._obrigatorio_3, this._obrigatorio_4, this._obrigatorio_5,
                       this._obrigatorio_6, this._obrigatorio_7, this._obrigatorio_8, this._obrigatorio_9, this._obrigatorio_10))
                    this._utilizandoNsObrigatorios = true;
                else
                    this._utilizandoNsObrigatorios = false;

                this.OnPropertyChanged("UtilizandoNsObrigatorios");
            }
        }

        private int _obrigatorio_5;
        public int Obrigatorio_5
        {
            get { return this._obrigatorio_5; }
            set
            {
                validaObrigatorioDescartes(ref this._obrigatorio_5, value);

                this.OnPropertyChanged("Obrigatorio_5");

                if (utilizandoOpcionais(this._obrigatorio_1, this._obrigatorio_2, this._obrigatorio_3, this._obrigatorio_4, this._obrigatorio_5,
                       this._obrigatorio_6, this._obrigatorio_7, this._obrigatorio_8, this._obrigatorio_9, this._obrigatorio_10))
                    this._utilizandoNsObrigatorios = true;
                else
                    this._utilizandoNsObrigatorios = false;

                this.OnPropertyChanged("UtilizandoNsObrigatorios");
            }
        }

        private int _obrigatorio_6;
        public int Obrigatorio_6
        {
            get { return this._obrigatorio_6; }
            set
            {
                validaObrigatorioDescartes(ref this._obrigatorio_6, value);

                this.OnPropertyChanged("Obrigatorio_6");

                if (utilizandoOpcionais(this._obrigatorio_1, this._obrigatorio_2, this._obrigatorio_3, this._obrigatorio_4, this._obrigatorio_5,
                       this._obrigatorio_6, this._obrigatorio_7, this._obrigatorio_8, this._obrigatorio_9, this._obrigatorio_10))
                    this._utilizandoNsObrigatorios = true;
                else
                    this._utilizandoNsObrigatorios = false;

                this.OnPropertyChanged("UtilizandoNsObrigatorios");
            }
        }

        private int _obrigatorio_7;
        public int Obrigatorio_7
        {
            get { return this._obrigatorio_7; }
            set
            {
                validaObrigatorioDescartes(ref this._obrigatorio_7, value);

                this.OnPropertyChanged("Obrigatorio_7");

                if (utilizandoOpcionais(this._obrigatorio_1, this._obrigatorio_2, this._obrigatorio_3, this._obrigatorio_4, this._obrigatorio_5,
                        this._obrigatorio_6, this._obrigatorio_7, this._obrigatorio_8, this._obrigatorio_9, this._obrigatorio_10))
                    this._utilizandoNsObrigatorios = true;
                else
                    this._utilizandoNsObrigatorios = false;

                this.OnPropertyChanged("UtilizandoNsObrigatorios");
            }
        }

        private int _obrigatorio_8;
        public int Obrigatorio_8
        {
            get { return this._obrigatorio_8; }
            set
            {
                validaObrigatorioDescartes(ref this._obrigatorio_8, value);

                this.OnPropertyChanged("Obrigatorio_8");

                if (utilizandoOpcionais(this._obrigatorio_1, this._obrigatorio_2, this._obrigatorio_3, this._obrigatorio_4, this._obrigatorio_5,
                       this._obrigatorio_6, this._obrigatorio_7, this._obrigatorio_8, this._obrigatorio_9, this._obrigatorio_10))
                    this._utilizandoNsObrigatorios = true;
                else
                    this._utilizandoNsObrigatorios = false;

                this.OnPropertyChanged("UtilizandoNsObrigatorios");
            }
        }

        private int _obrigatorio_9;
        public int Obrigatorio_9
        {
            get { return this._obrigatorio_9; }
            set
            {
                validaObrigatorioDescartes(ref this._obrigatorio_9, value);

                this.OnPropertyChanged("Obrigatorio_9");

                if (utilizandoOpcionais(this._obrigatorio_1, this._obrigatorio_2, this._obrigatorio_3, this._obrigatorio_4, this._obrigatorio_5,
                       this._obrigatorio_6, this._obrigatorio_7, this._obrigatorio_8, this._obrigatorio_9, this._obrigatorio_10))
                    this._utilizandoNsObrigatorios = true;
                else
                    this._utilizandoNsObrigatorios = false;

                this.OnPropertyChanged("UtilizandoNsObrigatorios");
            }
        }

        private int _obrigatorio_10;
        public int Obrigatorio_10
        {
            get { return this._obrigatorio_10; }
            set
            {
                validaObrigatorioDescartes(ref this._obrigatorio_10, value);

                this.OnPropertyChanged("Obrigatorio_10");

                if (utilizandoOpcionais(this._obrigatorio_1, this._obrigatorio_2, this._obrigatorio_3, this._obrigatorio_4, this._obrigatorio_5,
                       this._obrigatorio_6, this._obrigatorio_7, this._obrigatorio_8, this._obrigatorio_9, this._obrigatorio_10))
                    this._utilizandoNsObrigatorios = true;
                else
                    this._utilizandoNsObrigatorios = false;

                this.OnPropertyChanged("UtilizandoNsObrigatorios");
            }
        }

        private int _descartavel_1;
        public int Descartavel_1
        {
            get { return this._descartavel_1; }
            set
            {
                validaObrigatorioDescartes(ref this._descartavel_1, value);

                this.OnPropertyChanged("Descartavel_1");

                if (utilizandoOpcionais(this._descartavel_1, this._descartavel_2, this._descartavel_3, this._descartavel_4, this._descartavel_5,
                    this._descartavel_6, this._descartavel_7, this._descartavel_8, this._descartavel_9, this._descartavel_10))
                    this._utilizando_Descartavel = true;
                else
                    this._utilizando_Descartavel = false;

                this.OnPropertyChanged("UtilizandoDescartavel");
            }
        }

        private int _descartavel_2;
        public int Descartavel_2
        {
            get { return this._descartavel_2; }
            set
            {
                validaObrigatorioDescartes(ref this._descartavel_2, value);

                this.OnPropertyChanged("Descartavel_2");

                if (utilizandoOpcionais(this._descartavel_1, this._descartavel_2, this._descartavel_3, this._descartavel_4, this._descartavel_5,
                     this._descartavel_6, this._descartavel_7, this._descartavel_8, this._descartavel_9, this._descartavel_10))
                    this._utilizando_Descartavel = true;
                else
                    this._utilizando_Descartavel = false;

                this.OnPropertyChanged("UtilizandoDescartavel");
            }
        }
        private int _descartavel_3;
        public int Descartavel_3
        {
            get { return this._descartavel_3; }
            set
            {
                validaObrigatorioDescartes(ref this._descartavel_3, value);

                this.OnPropertyChanged("Descartavel_3");

                if (utilizandoOpcionais(this._descartavel_1, this._descartavel_2, this._descartavel_3, this._descartavel_4, this._descartavel_5,
                    this._descartavel_6, this._descartavel_7, this._descartavel_8, this._descartavel_9, this._descartavel_10))
                    this._utilizando_Descartavel = true;
                else
                    this._utilizando_Descartavel = false;

                this.OnPropertyChanged("UtilizandoDescartavel");
            }
        }

        private int _descartavel_4;
        public int Descartavel_4
        {
            get { return this._descartavel_4; }
            set
            {
                validaObrigatorioDescartes(ref this._descartavel_4, value);

                this.OnPropertyChanged("Descartavel_4");

                if (utilizandoOpcionais(this._descartavel_1, this._descartavel_2, this._descartavel_3, this._descartavel_4, this._descartavel_5,
                    this._descartavel_6, this._descartavel_7, this._descartavel_8, this._descartavel_9, this._descartavel_10))
                    this._utilizando_Descartavel = true;
                else
                    this._utilizando_Descartavel = false;

                this.OnPropertyChanged("UtilizandoDescartavel");
            }
        }

        private int _descartavel_5;
        public int Descartavel_5
        {
            get { return this._descartavel_5; }
            set
            {
                validaObrigatorioDescartes(ref this._descartavel_5, value);

                this.OnPropertyChanged("Descartavel_5");

                if (utilizandoOpcionais(this._descartavel_1, this._descartavel_2, this._descartavel_3, this._descartavel_4, this._descartavel_5,
                    this._descartavel_6, this._descartavel_7, this._descartavel_8, this._descartavel_9, this._descartavel_10))
                    this._utilizando_Descartavel = true;
                else
                    this._utilizando_Descartavel = false;

                this.OnPropertyChanged("UtilizandoDescartavel");
            }
        }

        private int _descartavel_6;
        public int Descartavel_6
        {
            get { return this._descartavel_6; }
            set
            {
                validaObrigatorioDescartes(ref this._descartavel_6, value);

                this.OnPropertyChanged("Descartavel_6");

                if (utilizandoOpcionais(this._descartavel_1, this._descartavel_2, this._descartavel_3, this._descartavel_4, this._descartavel_5,
                    this._descartavel_6, this._descartavel_7, this._descartavel_8, this._descartavel_9, this._descartavel_10))
                    this._utilizando_Descartavel = true;
                else
                    this._utilizando_Descartavel = false;

                this.OnPropertyChanged("UtilizandoDescartavel");
            }
        }

        private int _descartavel_7;
        public int Descartavel_7
        {
            get { return this._descartavel_7; }
            set
            {
                validaObrigatorioDescartes(ref this._descartavel_7, value);

                this.OnPropertyChanged("Descartavel_7");

                if (utilizandoOpcionais(this._descartavel_1, this._descartavel_2, this._descartavel_3, this._descartavel_4, this._descartavel_5,
                    this._descartavel_6, this._descartavel_7, this._descartavel_8, this._descartavel_9, this._descartavel_10))
                    this._utilizando_Descartavel = true;
                else
                    this._utilizando_Descartavel = false;

                this.OnPropertyChanged("UtilizandoDescartavel");
            }
        }

        private int _descartavel_8;
        public int Descartavel_8
        {
            get { return this._descartavel_8; }
            set
            {
                validaObrigatorioDescartes(ref this._descartavel_8, value);

                this.OnPropertyChanged("Descartavel_8");

                if (utilizandoOpcionais(this._descartavel_1, this._descartavel_2, this._descartavel_3, this._descartavel_4, this._descartavel_5,
                    this._descartavel_6, this._descartavel_7, this._descartavel_8, this._descartavel_9, this._descartavel_10))
                    this._utilizando_Descartavel = true;
                else
                    this._utilizando_Descartavel = false;

                this.OnPropertyChanged("UtilizandoDescartavel");
            }
        }

        private int _descartavel_9;
        public int Descartavel_9
        {
            get { return this._descartavel_9; }
            set
            {
                validaObrigatorioDescartes(ref this._descartavel_9, value);

                this.OnPropertyChanged("Descartavel_9");

                if (utilizandoOpcionais(this._descartavel_1, this._descartavel_2, this._descartavel_3, this._descartavel_4, this._descartavel_5,
                     this._descartavel_6, this._descartavel_7, this._descartavel_8, this._descartavel_9, this._descartavel_10))
                    this._utilizando_Descartavel = true;
                else
                    this._utilizando_Descartavel = false;

                this.OnPropertyChanged("UtilizandoDescartavel");
            }
        }

        private int _descartavel_10;
        public int Descartavel_10
        {
            get { return this._descartavel_10; }
            set
            {
                validaObrigatorioDescartes(ref this._descartavel_10, value);

                this.OnPropertyChanged("Descartavel_10");

                if (utilizandoOpcionais(this._descartavel_1, this._descartavel_2, this._descartavel_3, this._descartavel_4, this._descartavel_5,
                    this._descartavel_6, this._descartavel_7, this._descartavel_8, this._descartavel_9, this._descartavel_10))
                    this._utilizando_Descartavel = true;
                else
                    this._utilizando_Descartavel = false;

                this.OnPropertyChanged("UtilizandoDescartavel");
            }
        }

        private void validaObrigatorioDescartes(ref int propriedade, int value)
        {
            if (value <= 25 && value > 0)
            {
                if (propriedade != value && !(listaObrigatorioDescartaveis.Contains(value)))
                {
                    if (propriedade == 0)
                    {
                        propriedade = value;
                        listaObrigatorioDescartaveis.Add(propriedade);
                    }
                    else
                    {
                        listaObrigatorioDescartaveis.Remove(propriedade);
                        propriedade = value;
                        listaObrigatorioDescartaveis.Add(propriedade);
                    }
                }
                else
                {
                    listaObrigatorioDescartaveis.Remove(propriedade);
                    propriedade = 0;
                }
            }
            else
            {
                listaObrigatorioDescartaveis.Remove(propriedade);
                propriedade = 0;
            }
        }

        private int _primosMin;
        public int PrimosMin
        {
            get { return this._primosMin; }
            set
            {
                if (value <= 25 && value >= 3 && value < 7)
                    this._primosMin = value;
                else
                {
                    this._utilizando_PrimosMinMax = false;
                    this._primosMin = 0;
                    this._primosMax = 0;
                    this.OnPropertyChanged("PrimosMax");
                }

                if (_primosMax > 0 && _primosMin > 0)
                    if (validaMinMaxPrimos(this._primosMin, this._primosMax))
                    {
                        this._utilizando_PrimosMinMax = true;
                    }
                    else
                    {
                        this._utilizando_PrimosMinMax = false;
                        this._primosMax = 0;
                        this._primosMin = 0;
                        this.OnPropertyChanged("PrimosMax");
                    }

                this.OnPropertyChanged("PrimosMin");
                this.OnPropertyChanged("UtilizandoPrimosMinMax");

            }
        }

        private int _primosMax;
        public int PrimosMax
        {
            get { return this._primosMax; }
            set
            {
                if (value <= 25 && value > 3 && value <= 7)
                    this._primosMax = value;
                else
                {
                    this._utilizando_PrimosMinMax = false;
                    this._primosMin = 0;
                    this._primosMax = 0;
                    this.OnPropertyChanged("PrimosMin");
                }

                if (_primosMax > 0 && _primosMin > 0)
                    if (validaMinMaxPrimos(this._primosMin, this._primosMax))
                    {
                        this._utilizando_PrimosMinMax = true;
                    }
                    else
                    {
                        this._utilizando_PrimosMinMax = false;
                        this._primosMax = 0;
                        this._primosMin = 0;
                        this.OnPropertyChanged("PrimosMin");
                    }

                this.OnPropertyChanged("PrimosMax");
                this.OnPropertyChanged("UtilizandoPrimosMinMax");
            }
        }

        private bool utilizandoOpcionais(int n1, int n2, int n3 = 0, int n4 = 0, int n5 = 0, int n6 = 0, int n7 = 0, int n8 = 0, int n9 = 0, int n10 = 0)
        {
            if(n1 + n2 + n3 + n4 + n5 + n6 + n7 + n8 + n9 + n10 >  0)
            {
                return true;
            }
            return false;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private static bool validaMinMaxPrimos(int min, int max)
        {
            return (((min >= 3) && (min < max) && (max <= 7) && (max > min)) || min == max);
        }

        public void limparDados()
        {
            this.listaObrigatorioDescartaveis.Clear();
            ClearObrigatorioDescartaveis();

            this._modoJogo = 0;
            this._padrao = string.Empty;
            this._sequenciaDezenas = string.Empty;
            this._primosMax = 0;
            this._primosMin = 0;
            this._qntdJogos = 0;

            this._utilizandoNsObrigatorios = false;
            this._utilizandoOpcaoNormal = false;
            this._utilizando_Descartavel = false;
            this._utilizando_PrimosMinMax = false;

            this.OnPropertyChanged("UtilizandoNsObrigatorios");
            this.OnPropertyChanged("UtilizandoOpcaoNormal");
            this.OnPropertyChanged("UtilizandoDescartavel");
            this.OnPropertyChanged("PrimosMax");
            this.OnPropertyChanged("Jogos");

            

            this.OnPropertyChanged("SeqDezenas");
            this.OnPropertyChanged("Padrao");
            this.OnPropertyChanged("QntdJogos");
            this.OnPropertyChanged("PrimosMax");
            this.OnPropertyChanged("PrimosMin");
            this.OnPropertyChanged("ModoJogo");
        }

        private void ClearObrigatorioDescartaveis()
        {
            this._descartavel_1 = 0;
            this._descartavel_2 = 0;
            this._descartavel_3 = 0;
            this._descartavel_4 = 0;
            this._descartavel_5 = 0;
            this._descartavel_6 = 0;
            this._descartavel_7 = 0;
            this._descartavel_8 = 0;
            this._descartavel_9 = 0;
            this._descartavel_10 = 0;

            this._obrigatorio_1 = 0;
            this._obrigatorio_2 = 0;
            this._obrigatorio_3 = 0;
            this._obrigatorio_4 = 0;
            this._obrigatorio_5 = 0;
            this._obrigatorio_6 = 0;
            this._obrigatorio_7 = 0;
            this._obrigatorio_8 = 0;
            this._obrigatorio_9 = 0;
            this._obrigatorio_10 = 0;

            this.OnPropertyChanged("Obrigatorio_1");
            this.OnPropertyChanged("Obrigatorio_2");
            this.OnPropertyChanged("Obrigatorio_3");
            this.OnPropertyChanged("Obrigatorio_4");
            this.OnPropertyChanged("Obrigatorio_5");
            this.OnPropertyChanged("Obrigatorio_6");
            this.OnPropertyChanged("Obrigatorio_7");
            this.OnPropertyChanged("Obrigatorio_8");
            this.OnPropertyChanged("Obrigatorio_9");
            this.OnPropertyChanged("Obrigatorio_10");

            this.OnPropertyChanged("Descartavel_1");
            this.OnPropertyChanged("Descartavel_2");
            this.OnPropertyChanged("Descartavel_3");
            this.OnPropertyChanged("Descartavel_4");
            this.OnPropertyChanged("Descartavel_5");
            this.OnPropertyChanged("Descartavel_6");
            this.OnPropertyChanged("Descartavel_7");
            this.OnPropertyChanged("Descartavel_8");
            this.OnPropertyChanged("Descartavel_9");
            this.OnPropertyChanged("Descartavel_10");
        }
    }
}
