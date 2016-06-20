//Készítette: Cicer Norbert (2015/2016/2.félév)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2YY8U_Joszomszediiszony
{
    public delegate void KiirasHandler(int azonosito);
    
    class Program
    {
        public static void Kiiras(int azonosito)
        {
            Console.WriteLine("A {0} számú csap bemenete 0 lett.", azonosito);
        }
        
        static void Main(string[] args)
        {
            //Random R = new Random(); //TESZT RÉSZ KEZDETE
            //Csapok csapok = new Csapok(3,R);
            //csapok.FatEpit(3);
            //Util.Tajekoztato();
            //csapok.kiiro += Kiiras;
            //csapok.Kirajzol();
            //csapok.PontokKiirasa();
            //int azon1 = int.Parse(Console.ReadLine());
            //int azon2 = int.Parse(Console.ReadLine());
            //Console.Clear();
            //csapok.CsapAllasAllitas(azon1, azon2, 0);            
            //csapok.Kirajzol();
            //csapok.PontokKiirasa();
            //Console.ReadLine(); //TESZT RÉSZ VÉGE
            bool kilepes = false; //FŐ PRG KEZDETE
            Random R = new Random();
            
            int maxCsapKivezetes = 2;
            int maxSzint = 3;
            do
            {
                float ellenfelPontja = 0;
                float jatekosPontja = 0;
                Csapok csapok = new Csapok(maxSzint, R);
                csapok.FatEpit(maxCsapKivezetes);
                Util.Tajekoztato();
                csapok.kiiro += Kiiras;
            
            
                do
                {                                   
                    csapok.Kirajzol();
                    csapok.PontokKiirasa();
                    Console.WriteLine("A Te pontod: {0}", jatekosPontja);
                    Console.WriteLine("Az ellefél pontja: {0}", ellenfelPontja);
                    int csapAzonosito = 0;
                    bool hiba;
                    do
                    {
                        hiba = false;
                        try
                        {
                            Console.WriteLine("Melyik csapot állítsuk? ");
                            csapAzonosito = int.Parse(Console.ReadLine());
                        }
                        catch (FormatException e)
                        {
                            hiba = true;
                        }
                    } while (csapAzonosito > csapok.ElemekSzama-1 || csapAzonosito < 0 || hiba);
                    int csapKivezetes = 0;
                    
                    do
                    {
                        hiba = false;
                        try
                        {
                            Console.WriteLine("Melyik kivezetést állítsuk? ");
                            csapKivezetes = int.Parse(Console.ReadLine());
                        }
                        catch (FormatException e)
                        {
                            hiba = true;
                        }

                    } while (csapKivezetes >= csapok.MaxKimenet(csapAzonosito) || csapKivezetes < 0 || hiba);
                    int szazalek = 0;
                    bool betuE = false;
                    do
                    {
                        betuE = false;
                        Console.WriteLine("Mekkora százalék legyen? ");
                        try
                        {
                            szazalek = int.Parse(Console.ReadLine());
                            if (szazalek > 100 || szazalek < 0)
                            {
                                throw new TulSokSzazalekException();
                            }
                        }
                        catch (TulSokSzazalekException e)
                        {
                            Console.WriteLine("Nem megfelelő százalékérték.");
                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine("Csak pozitív egész számok írhatók be!");
                            betuE = true;
                        }
                        
                    } while (szazalek > 100 || szazalek < 0 || betuE);
                    float[] valtozasok = new float[2];
                    valtozasok = csapok.CsapAllasAllitas(csapAzonosito, csapKivezetes, szazalek, false);
                    jatekosPontja += valtozasok[0];
                    ellenfelPontja += valtozasok[1];
                    int[] gepValasza = new int[3];
                    gepValasza = csapok.GepiEllenfel();
                    valtozasok = csapok.CsapAllasAllitas(gepValasza[0], gepValasza[1], gepValasza[2], true);

                    jatekosPontja += valtozasok[0];
                    ellenfelPontja += valtozasok[1];
                    //char kilepesBetu = 'n';
                    //do
                    //{
                    //    Console.WriteLine("Ki akarsz lépni? (i vagy n) ");
                    //    kilepesBetu = char.Parse(Console.ReadLine());
                    //} while (kilepesBetu == 'i' || kilepesBetu == 'n');
                    //if (kilepesBetu == 'i')
                    //{
                    //    kilepes = true;
                    //}

            
                } while (!kilepes || ellenfelPontja >= 1000 || jatekosPontja >= 1000 || csapok.KimenetOsszesito(true) <= 0 || csapok.KimenetOsszesito(false) <= 0);
                Console.WriteLine("A játéknak vége!");
                if (ellenfelPontja >= 1000 || csapok.KimenetOsszesito(true) == 0)
	            {
		            Console.WriteLine("Vesztettél.");
	            }
                if (jatekosPontja >= 1000 || csapok.KimenetOsszesito(false) == 0)
	            {
		            Console.WriteLine("Nyertél.");
	            }
                char masikKilepesBetu = 'n';
                do
                {
                    Console.WriteLine("Biztosan ki akarsz lépni? (i vagy n) ");
                    masikKilepesBetu = char.Parse(Console.ReadLine());
                } while (masikKilepesBetu != 'i' && masikKilepesBetu != 'n');
                if (masikKilepesBetu == 'i')
                {
                    kilepes = true;
                }
                else
                {
                    if (R.Next(0, 2) == 0)
                    {
                        maxCsapKivezetes++;
                    }
                    else
                    {
                        maxSzint++;
                    }
                    Console.Clear();
               }
            } while (!kilepes); //FŐ PRG VÉGE
            
            
        }
    }
}
//Készítette: Cicer Norbert (2015/2016/2.félév)