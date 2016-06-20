//Készítette: Cicer Norbert (2015/2016/2.félév)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2YY8U_Joszomszediiszony
{
    class Csapok
    {
        class CsapElem
        {
            int kulcs;
            CsapElem szulo;
            float bemenet;

            public float Bemenet
            {
                get { return bemenet; }
                set { bemenet = value; }
            }
            int gyerekDarab;
            CsapElem[] gyerekek;
            float[] kimenetek;
            int[] szazalekok;

            public int[] Szazalekok
            {
                get { return szazalekok; }
                set { szazalekok = value; }
            }

            public CsapElem[] Gyerekek
            {
                get { return gyerekek; }
                set { gyerekek = value; }
            }

            public CsapElem Szulo
            {
                get { return szulo; }
            }

            public int Kulcs
            {
                get { return kulcs; }
            }

            public float[] Kimenetek
            {
                get { return kimenetek; }
            }

            public CsapElem(int kulcs, CsapElem szulo, float bemenet, int gyerekDarab, int[] szazalekok) //CsapElem konstruktora
            {
                this.kulcs = kulcs; 
                this.szulo = szulo;
                this.bemenet = bemenet;
                this.gyerekDarab = gyerekDarab;
                this.gyerekek = new CsapElem[gyerekDarab];
                this.kimenetek = new float[gyerekDarab];
                this.szazalekok = szazalekok;
                int tempOsszeg = 0; //a kimenetek kiszámítása a százalékokból
                
                for (int i = 0; i < gyerekDarab; i++)
                {
                    tempOsszeg += szazalekok[i];
                }
                for (int j = 0; j < gyerekDarab; j++)
                {
                    this.kimenetek[j] = (float)bemenet * ((float)szazalekok[j] / (float)tempOsszeg);
                }
                
                
            }
//Készítette: Cicer Norbert (2015/2016/2.félév)
            public CsapElem()
            {                
            } //sima konstruktor, köztes műveletekhez

            public string CsapAzonositoKirajzolo() //kiírja az azonositosort
            {
                string outputString;
                
                outputString = "|";
                int csapSzelesseg = this.CsapSzazalekKirajzolo().Length;
                int szokozok = (csapSzelesseg - 2 - kulcs.ToString().Length); //-2 az elválasztók miatt, le kell vonni a kulcs hosszát              
                outputString += kulcs;
                for (int i = 0; i < szokozok; i++)
                {
                    outputString += " ";
                }
                outputString += "|";
                return outputString;
            }

            public string CsapSzazalekKirajzolo() //kiírja a szazaleksort
            {
                string outputString = "|";
                for (int i = 0; i < szazalekok.Length; i++)
                {
                    if (szazalekok.ToString().Length == 1)
                    {
                        outputString += "  " + szazalekok[i] + "|";
                    }
                    else if (szazalekok.ToString().Length == 2)
                    {
                        outputString += " " + szazalekok[i] + "|";
                    }
                    else
                    {
                        outputString += szazalekok[i] + "|";
                    }

                }
                return outputString;
            }

            public string CsapErtekKirajzolo() //kiírja az értéksort
            {
                string outputString = "|";
                for (int i = 0; i < kimenetek.Length; i++)
                {
                    string tmp = string.Format("0.00l",kimenetek[i]);
                    outputString += tmp;
                    outputString += "|";
                }
                return outputString;
            }

            public string CsapKimenetAzonositoKirajzolo() //kiírja a kimenetek azonosítóját
            {
                string outputString = "|";
                for (int i = 0; i < kimenetek.Length; i++)
                {
                    outputString += "" + i + "| ";
                }
                return outputString;
            }

            public string CsapIránysorKirajzolo() //kiírja az iránysort (ami azt jelképezi, merre van a gyerekelem)
            {
                string outputString = " /";
                if (gyerekDarab > 2)
                {
                    for (int i = 0; i < gyerekDarab - 2; i++)
                    {
                        outputString += "  |";
                    }
                }
                outputString += "  \\  ";
                return outputString;
            }

            public string CsapTulajKirajzolo(bool pirosKezdoElem) //a levélelemeknél kiírja, melyik kivezetés kié
            {
                string outputString = "|";
                char tulaj;
                bool pirosE = pirosKezdoElem;
                for (int i = 0; i < kimenetek.Length; i++)
                {
                    if (pirosE)
                    {
                        tulaj = 'p';
                    }
                    else
                    {
                        tulaj = 'k';
                    }
                    outputString += tulaj + "| ";
                    pirosE = !pirosE;
                }
                return outputString;
            }
            

        }
//Készítette: Cicer Norbert (2015/2016/2.félév)
        public KiirasHandler kiiro;
        CsapElem csucs;
        Random R;
        int elemekSzama;

        public int ElemekSzama
        {
            get { return elemekSzama; }
        }
        int maxSzint;
        int pirosNullasCsapok; //hány olyan piros csap van, aminek a kimenete 0 (a bónuszpontok miatt)
        int kekNullasCsapok; //hány olyan kék csap van, aminek a kimenete 0

        public Csapok(int maxSzint,Random R)
        {
            this.R = R;
            this.maxSzint = maxSzint;
            this.csucs = null;
            this.elemekSzama = 0;
            this.pirosNullasCsapok = 0;
            this.kekNullasCsapok = 0;
            
        }

        private static int[] RandomSzazalekFeltoltes(int kimenetekSzama, Random R) //feltölti a tömböt random százalékokkal
        {
            int[] tempSzazalekok = new int[kimenetekSzama];
            for (int i = 0; i < kimenetekSzama; i++)
            {
                tempSzazalekok[i] = R.Next(0, 101);
            }
            return tempSzazalekok;
        }

        private static float[] ErtekSzamitasSzazalekbol(float bemenet, int gyerekekSzama, int[] szazalekTomb) //a bemenetből és a százalékok tömbjéből kiszámolja a kimeneteket
        {
            float[] outputTomb = new float[gyerekekSzama];
            int osszeg = 0;
            for (int i = 0; i < gyerekekSzama; i++)
            {
                osszeg += szazalekTomb[i];
            }
            for (int i = 0; i < gyerekekSzama; i++)
            {
                outputTomb[i] = (szazalekTomb[i] / osszeg) * bemenet;
            }
            return outputTomb;
        }
        
        public void FatEpit(int maxKivezetes) //legenerálja a fát adott szintig, a csapoknak legfeljebb maxKivezetés kimenete van
        {
            RekFatEpit(ref csucs, null, 1, 0, maxSzint, maxKivezetes);
            //Console.WriteLine("Fa létrehozva"); //TESZTELÉS
        }

        private void RekFatEpit(ref CsapElem aktElem, CsapElem elozoElem, float bemenet, int aktSzint, int maxSzint, int maxKivezetes) //a faépítés rekurzív megvalósítása
        {
            if(aktSzint < maxSzint)
            {
                if (aktElem == null) //ha az ág végére értünk
                {
                    int gyerekSzam = R.Next(2, maxKivezetes + 1);
                    int[] tempTomb = RandomSzazalekFeltoltes(gyerekSzam, R);
                    aktElem = new CsapElem(elemekSzama, elozoElem, bemenet, gyerekSzam, tempTomb);
                    elemekSzama++;
                }
                for (int i = 0; i < aktElem.Gyerekek.Length; i++)
                {
                    RekFatEpit(ref aktElem.Gyerekek[i], aktElem, aktElem.Kimenetek[i], aktSzint+1, maxSzint, maxKivezetes);                    
                }
            }
        }

        public int MaxKimenet(int azonosito)
        {
            CsapElem tempCsapElem = new CsapElem();
            Kereses(azonosito, ref tempCsapElem);
            int outputKimenetekSzama = tempCsapElem.Gyerekek.Length;
            return outputKimenetekSzama;
        }

        private void Kereses(int kulcs, ref CsapElem outputCsapElem) //adott kulcsú elem megkeresése
        {
            //CsapElem outputCsapElem = new CsapElem();
            RekKereses(kulcs, csucs,ref outputCsapElem);
            //return outputCsapElem;
        }

        public bool VanE(int kulcs) //van-e adott kulcsú elem a CsapElemek között
        {
            CsapElem outputCsapElem = null;
            RekKereses(kulcs, csucs, ref outputCsapElem);
            return outputCsapElem != null;
        }

        private void RekKereses(int kulcs, CsapElem aktElem,ref CsapElem outputElem) //rekurzív keresés
        {
            if (aktElem != null)
            {
                if (aktElem.Kulcs == kulcs)
                {
                    outputElem = aktElem;
                }
                for (int i = 0; i < aktElem.Gyerekek.Length; i++)
                {
                    RekKereses(kulcs, aktElem.Gyerekek[i],ref outputElem);
                }
            }                      
        }

        public float KimenetOsszesito(bool pirose) //összesíti a levél elemben lévő piros vagy kék gazdához tartozó mezőket
        {
            int darab = 0;
            float osszeg = 0F;
            int aktNullaKimenetuek = 0; //éppen hány 0 kimenetű csap van az adott típusból
            RekKimenetOsszesito(csucs, ref darab, ref osszeg, pirose, ref aktNullaKimenetuek);
            osszeg = osszeg * 1000F; //a ml-es pontokat kell számolni, de mindenütt l-ben számol a prg, ezért 1000-es szorzó kell
            if (pirose && (aktNullaKimenetuek - pirosNullasCsapok) > 0) //csak akkor van bónuszpont, ha nőtt a számuk
            {
                osszeg += 100F * (aktNullaKimenetuek - pirosNullasCsapok); //amennyivel nőtt a számuk, annyi bónuszpontot adunk
            }
            else
                if ((aktNullaKimenetuek - kekNullasCsapok) > 0)
                {
                    osszeg += 100F * (aktNullaKimenetuek - kekNullasCsapok); // u.a. mint a pirosnál
                }
            
            
            return osszeg;
        }

        private void RekKimenetOsszesito(CsapElem aktElem, ref int darab, ref float osszeg, bool pirose, ref int aktNullaKimenetuek) //az összesítő rekurzív része
        {
            if (aktElem.Gyerekek[0] == null) //ha levélelem
            {
                int j;
                if ((pirose && (darab % 2 == 0)) || (!pirose && (darab % 2 == 1))) //a páros indexű kivezetések pirosak, a páratlanok kékek
                {
                    j = 0;
                }
                else
                {
                    j = 1;
                }
                while (j < aktElem.Kimenetek.Length)
                {
                    osszeg += aktElem.Kimenetek[j];
                    
                    if (aktElem.Kimenetek[j] == 0)
                    {
                        aktNullaKimenetuek++;
                    }
                    j = j + 2;
                }
                darab += aktElem.Gyerekek.Length; //eddig hány kivezetésen ment végig (akár piros, akár kék)       
            }
            else
            {
                for (int i = 0; i < aktElem.Gyerekek.Length; i++)
                {
                    RekKimenetOsszesito(aktElem.Gyerekek[i], ref darab, ref osszeg, pirose, ref aktNullaKimenetuek); //ha nem levél, továbbmegy a gyerekekre
                }
            }
        }

        private CsapElem[] SzintKigyujtese(int szint, ref int darab) //sorban összegyűjti az adott szint elemeit
        {
            CsapElem[] outputTomb = new CsapElem[(int)(Math.Pow(3, maxSzint))];
            RekSzintKigyujtese(szint, 0, csucs,ref outputTomb, ref darab);
            return outputTomb;
        }

        private void RekSzintKigyujtese(int celSzint, int aktSzint, CsapElem aktElem, ref CsapElem[] outputElemek, ref int darab) //rekurzív szintkigyűjtés
        {
            if (aktElem != null)
            {
                if (aktSzint == celSzint)
                {
                    outputElemek[darab] = aktElem;
                    darab++;
                }
                else
                {
                    for (int i = 0; i < aktElem.Gyerekek.Length; i++)
                    {
                        RekSzintKigyujtese(celSzint, aktSzint +1, aktElem.Gyerekek[i], ref outputElemek, ref darab);
                    }
                }
            }
        }
//Készítette: Cicer Norbert (2015/2016/2.félév)        
        
        public void Kirajzol() //kirajzolja a pályát
        {
            int hanyadikGyerek = 0;
            for (int i = 0; i < maxSzint; i++)
            {
                int darab = 0;
                CsapElem[] sor = SzintKigyujtese(i, ref darab);
                int csapKoz = 100 - (darab * 15); //100 a console szélessége és kb. 15 karakternyi 1 csap (!FELESLEGES)
                string uresHely = "  ";
                //for (int k = 0; k < csapKoz; k++)
                //{
                //    uresHely += " ";
                //}
                Console.Write(uresHely);
                for (int j = 0; j < darab; j++)
                {
                    Console.Write(sor[j].CsapAzonositoKirajzolo());
                    Console.Write(uresHely);
                }
                Console.WriteLine();
                Console.Write(uresHely);
                for (int l = 0; l < darab; l++)
                {
                    Console.Write(sor[l].CsapSzazalekKirajzolo());
                    Console.Write(uresHely);
                }
                Console.WriteLine();

                Console.Write(uresHely);
                for (int o = 0; o < darab; o++)
                {
                    Console.Write(sor[o].CsapKimenetAzonositoKirajzolo());
                    Console.Write(uresHely);
                }
                Console.WriteLine();

                if (sor[0].Gyerekek[0] == null) //levélelem
                {
                    bool pirosE;                    
                    Console.Write(uresHely);
                    for (int n = 0; n < darab; n++)
                    {
                        if (hanyadikGyerek % 2 == 0)
                        {
                            pirosE = true;
                        }
                        else
                        {
                            pirosE = false;
                        }
                        Console.Write(sor[n].CsapTulajKirajzolo(pirosE));
                        Console.Write(uresHely);
                        hanyadikGyerek += sor[n].Gyerekek.Length;
                    }
                    Console.WriteLine();
                }

                Console.Write(uresHely);
                for (int m = 0; m < darab; m++)
                {
                    Console.Write(sor[m].CsapIránysorKirajzolo());
                    Console.Write(uresHely);
                }
                Console.WriteLine();
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public float[] CsapAllasAllitas(int kulcs, int azonosito, int ertek, bool gepE)
        {
            float[] kimenet = new float[2];
            kimenet[0] = this.KimenetOsszesito(true);
            kimenet[1] = this.KimenetOsszesito(false);
            CsapElem tmpCsap = new CsapElem();
            Kereses(kulcs, ref tmpCsap);
            tmpCsap.Szazalekok[azonosito] = ertek;
            
            this.UjraSzamolas(ref tmpCsap, gepE);
            kimenet[0] = this.KimenetOsszesito(true) - kimenet[0];
            kimenet[1] = this.KimenetOsszesito(false) - kimenet[1];
            return kimenet;
        }

        private void UjraSzamolas(ref CsapElem csap, bool gepE)
        {
            
            RekUjraSzamolas(ref csap, csap.Bemenet, gepE);
        }

        private void RekUjraSzamolas(ref CsapElem aktCsap, float ujBemenet,bool gepE)
        {            
            if (aktCsap != null)
            {
                int tempOsszeg = 0; //a kimenetek kiszámítása a százalékokból
                aktCsap.Bemenet = ujBemenet;
                for (int i = 0; i < aktCsap.Szazalekok.Length; i++)
                {
                    tempOsszeg += aktCsap.Szazalekok[i];
                }
                for (int j = 0; j < aktCsap.Kimenetek.Length; j++)
                {
                    if (tempOsszeg == 0) //ha a kimenetek mind 0-ások
                    {
                        aktCsap.Kimenetek[j] = 0;
                    }
                    else
                    {
                        aktCsap.Kimenetek[j] = (float)ujBemenet * ((float)aktCsap.Szazalekok[j] / (float)tempOsszeg); //a százalékok összegének arányában mennyi jut a kimenetre
                    }
                    if ((aktCsap.Kimenetek[j] == 0) && (aktCsap.Gyerekek[j] != null) && !gepE) //a gépi ellenfél próbálkozásainál ne írja ki folyton a 0-s csapokat
                    {
                        kiiro(aktCsap.Gyerekek[j].Kulcs);
                    }
                    RekUjraSzamolas(ref aktCsap.Gyerekek[j], aktCsap.Kimenetek[j], gepE);
                }
                //for (int k = 0; k < aktCsap.Gyerekek.Length; k++)
                //{
                //    RekUjraSzamolas(aktCsap.Gyerekek[k], aktCsap.Kimenetek[k]);
                //}
            }
        }

        public void PontokKiirasa()
        {
            Console.WriteLine("Piros játékos vízhozama: {0}",this.KimenetOsszesito(true));
            Console.WriteLine("Kék jétékos vízhozama: {0}", this.KimenetOsszesito(false));
        }

        public int[] GepiEllenfel()
        {
            Csapok teszt = (Csapok)this.MemberwiseClone();
            int[] megoldas = new int[3];
            //int[] tmpMegoldas = new int[3];
            float jelenlegiLegjobb = 0;
            //float tmpLegjobb;
            //for (int i = 0; i <= 100; i++)
            //{
            //    tmpLegjobb = 0;
                RekGepiEllenfel(teszt, csucs, ref megoldas, ref jelenlegiLegjobb, 100);
                //if (tmpLegjobb > jelenlegiLegjobb)
                //{
                //    megoldas = tmpMegoldas;
                //}
            //}
            
            return megoldas;
        }

        private void RekGepiEllenfel(Csapok teszt, CsapElem aktElem, ref int[] megoldas, ref float jelenlegiLegjobb, int allitas) //rekurzív gépi ellenfél
        {
            if (aktElem != null)
            {
                for (int j = 0; j < aktElem.Gyerekek.Length; j++)
			    {
			        int tmpKulcs = aktElem.Kulcs;
                    int tmpAzonosito = j;
                    int tmpAllas = aktElem.Szazalekok[j];
                    if (CsapAllasAllitas(aktElem.Kulcs,j,allitas, true)[1] > jelenlegiLegjobb)
	                {
		                megoldas[0] = aktElem.Kulcs;
                        megoldas[1] = j;
                        megoldas[2] = allitas;                        
	                }
                    CsapAllasAllitas(tmpKulcs, tmpAzonosito, tmpAllas, true);
			    }
                for (int i = 0; i < aktElem.Gyerekek.Length; i++)
                {
                    RekGepiEllenfel(teszt, aktElem.Gyerekek[i],ref megoldas, ref jelenlegiLegjobb, allitas);
                }
            }
        }
    }
}
//Készítette: Cicer Norbert (2015/2016/2.félév)