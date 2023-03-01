using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqlImportProjekt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string fajlVizsga = "vizsga.txt", fajlHallgato = "hallgato.txt", fajlJelentkezes = "jelentkezes.txt", fajlKollokviumImport = "kollokviumimport.sql";
            SqlImport s = new SqlImport(fajlVizsga, fajlHallgato, fajlJelentkezes, fajlKollokviumImport);

            Console.WriteLine("forrás -> {0}", fajlKollokviumImport);
            Console.ReadLine();


        }
    }
}
