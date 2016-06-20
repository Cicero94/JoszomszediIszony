using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2YY8U_Joszomszediiszony
{
    class Util
    {
        public static void Tajekoztato()
        {
            Console.WriteLine("Üdv a játékban! /n A csapok első sorában a csap azonosítója, a második sorában a kivezetés nyitottságának százaléka, a harmadik sorban a kivezetés azonosítója található.");
            Console.WriteLine("A csapok a kirajzolás során elcsúszhatnak egymáshoz képest, de az azonosítók alapján lehet tudni, melyik melyik.");
            Console.WriteLine("Az első csap a tiéd, Te vagy a piros játékos. Onnantól felváltva egy a tied, egy az ellenfélé.");
            Console.WriteLine("Csapok kiírásának módja:");
            string pelda = "|#azonosító|" + Environment.NewLine + "|#százalék|#százalék|" + Environment.NewLine + "|#kimeneti azonosító|#kimeneti azonosító|" + Environment.NewLine + "|tulaj betűjele|tulaj betűjele| (csak utolsó sornál)";
            Console.WriteLine(pelda);
            Console.WriteLine();
        }
    }
}
