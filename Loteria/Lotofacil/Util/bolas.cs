using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotofacil
{
    public class Bolas
    {
        public Bolas()
        {
            this.Count = 25;
        }

        public int this[int index]
        {
            get
            {
                int bola = -1;

                if (index == 0)
                    bola = bola_1;
                else
                if (index == 1)
                    bola = bola_2;
                else
                if (index == 2)
                    bola = bola_3;
                else
                if (index == 3)
                    bola = bola_4;
                else
                if (index == 4)
                    bola = bola_5;
                else
                if (index == 5)
                    bola = bola_6;
                else
                if (index == 6)
                    bola = bola_7;
                else
                if (index == 7)
                    bola = bola_8;
                else
                if (index == 8)
                    bola = bola_9;
                else
                if (index == 9)
                    bola = bola_10;
                else
                if (index == 10)
                    bola = bola_11;
                else
                if (index == 11)
                    bola = bola_12;
                else
                if (index == 12)
                    bola = bola_13;
                else
                if (index == 13)
                    bola = bola_14;
                else
                if (index == 14)
                    bola = bola_15;
                else
                if (index == 15)
                    bola = bola_16;
                else
                if (index == 16)
                    bola = bola_17;
                else
                if (index == 17)
                    bola = bola_18;
                else
                if (index == 18)
                    bola = bola_19;
                else
              if (index == 19)
                    bola = bola_20;
                else
                if (index == 20)
                    bola = bola_21;
                else
                if (index == 21)
                    bola = bola_22;
                else
                if (index == 22)
                    bola = bola_23;
                else
                    if (index == 23)
                    bola = bola_24;
                else
                if (index == 24)
                    bola = bola_25;

                return bola;
            }
            set
            {
                if (value >= 0)
                {
                    if (index == 0)
                        bola_1 = value;
                    else
                    if (index == 1)
                        bola_2 = value;
                    else
                    if (index == 2)
                        bola_3 = value;
                    else
                    if (index == 3)
                        bola_4 = value;
                    else
                    if (index == 4)
                        bola_5 = value;
                    else
                    if (index == 5)
                        bola_6 = value;
                    else
                    if (index == 6)
                        bola_7 = value;
                    else
                    if (index == 7)
                        bola_8 = value;
                    else
                    if (index == 8)
                        bola_9 = value;
                    else
                    if (index == 9)
                        bola_10 = value;
                    else
                    if (index == 10)
                        bola_11 = value;
                    else
                    if (index == 11)
                        bola_12 = value;
                    else
                    if (index == 12)
                        bola_13 = value;
                    else
                    if (index == 13)
                        bola_14 = value;
                    else
                    if (index == 14)
                        bola_15 = value;
                    else
                    if (index == 15)
                        bola_16 = value;
                    else
                    if (index == 16)
                        bola_17 = value;
                    else
                    if (index == 17)
                        bola_18 = value;
                    else
                    if (index == 18)
                        bola_19 = value;
                    else
                  if (index == 19)
                        bola_20 = value;
                    else
                    if (index == 20)
                        bola_21 = value;
                    else
                    if (index == 21)
                        bola_22 = value;
                    else
                    if (index == 22)
                        bola_23 = value;
                    else
                        if (index == 23)
                        bola_24 = value;
                    else
                    if (index == 24)
                        bola_25 = value;

                }

            }
        }

        public int Count { get; private set; }
        public int bola_1 { get; set; }
        public int bola_2 { get; set; }
        public int bola_3 { get; set; }
        public int bola_4 { get; set; }
        public int bola_5 { get; set; }
        public int bola_6 { get; set; }
        public int bola_7 { get; set; }
        public int bola_8 { get; set; }
        public int bola_9 { get; set; }
        public int bola_10 { get; set; }
        public int bola_11 { get; set; }
        public int bola_12 { get; set; }
        public int bola_13 { get; set; }
        public int bola_14 { get; set; }
        public int bola_15 { get; set; }
        public int bola_16 { get; set; }
        public int bola_17 { get; set; }
        public int bola_18 { get; set; }
        public int bola_19 { get; set; }
        public int bola_20 { get; set; }
        public int bola_21 { get; set; }
        public int bola_22 { get; set; }
        public int bola_23 { get; set; }
        public int bola_24 { get; set; }
        public int bola_25 { get; set; }

        public string Max_1 { get; set; }
        public string Max_2 { get; set; }
        public string Max_3 { get; set; }
        public string Max_4 { get; set; }
        public string Max_5 { get; set; }
        public string Max_6 { get; set; }
        public string Max_7 { get; set; }
        public string Max_8 { get; set; }
        public string Max_9 { get; set; }
        public string Max_10 { get; set; }
        public string Min_1 { get; set; }
        public string Min_2 { get; set; }
        public string Min_3 { get; set; }
        public string Min_4 { get; set; }
        public string Min_5 { get; set; }
        public string Min_6 { get; set; }
        public string Min_7 { get; set; }
        public string Min_8 { get; set; }
        public string Min_9 { get; set; }
        public string Min_10 { get; set; }

    }
}
