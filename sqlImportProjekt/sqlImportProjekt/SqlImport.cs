using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace sqlImportProjekt
{
    internal class SqlImport
    {
        private string fajlVizsga;
        private string fajlHallgato;
        private string fajlJelentkezes;
        private string fajlKollokviumImport;

        List<Vizsga> vizsgak = new List<Vizsga>();
        List<Hallgato> hallgatok = new List<Hallgato>();
        List<Jelentkezes> jelentkezesek = new List<Jelentkezes>();
        Dictionary<string, int> idSzotar = new Dictionary<string, int>();

        public SqlImport(string fajlVizsga, string fajlHallgato, string fajlJelentkezes, string fajlKollokviumImport)
        {
            this.fajlVizsga = fajlVizsga;
            this.fajlHallgato = fajlHallgato;
            this.fajlJelentkezes = fajlJelentkezes;
            this.fajlKollokviumImport = fajlKollokviumImport;
            BeolvasVizsga();
            BeolvasHallgato();
            BeolvasJelentkezes();
            ImportGenerátor();
        }

        private void ImportGenerátor()
        {
            delteGenerátor();
            hallgatóGenerátor();
            vizsgaGenerátor();
            jelentkezésGenerátor();
            selectGenerátor();
        }

        private void selectGenerátor()
        {
            List<string> fsorok = new List<string>();
            fsorok.Add("");
            fsorok.Add("SELECT * FROM hallgato;");
            fsorok.Add("SELECT * FROM jelentkezes;");
            fsorok.Add("SELECT * FROM vizsga;");
            File.AppendAllLines(fajlKollokviumImport, fsorok);
        }

        private void jelentkezésGenerátor()
        {
            List<string> fsorok = new List<string>();

            string szoveg = Environment.NewLine + "# jelentkezések" + Environment.NewLine;
            szoveg += "INSERT INTO jelentkezes (hallgatoid, vizsgaid, jeldatum, ledatum, igazolt, jegy) VALUES" + Environment.NewLine;

            foreach (var jelenkezes in jelentkezesek)
            {
                string sor = $"({jelenkezes.hallgatoid},{jelenkezes.vizsgaid},{jelenkezes.jeldatum},{jelenkezes.ledatum},{jelenkezes.igazolt},{jelenkezes.jegy})";
                fsorok.Add(sor);
            }

            szoveg += String.Join("," + Environment.NewLine, fsorok);
            szoveg += ";";

            File.AppendAllText(fajlKollokviumImport, szoveg);
        }

        private void vizsgaGenerátor()
        {
            List<string> fsorok = new List<string>();

            string szoveg = Environment.NewLine + "# vizsgák" + Environment.NewLine;
            szoveg += "INSERT INTO vizsga (id, datum, targy) VALUES" + Environment.NewLine;

            foreach (var vizsga in vizsgak)
            {
                string sor = $"({vizsga.id}, {vizsga.datum}, {vizsga.targy})";
                fsorok.Add(sor);
            }

            szoveg += String.Join("," + Environment.NewLine, fsorok);
            szoveg += ";";

            File.AppendAllText(fajlKollokviumImport, szoveg);
        }

        private void hallgatóGenerátor()
        {
            List<string> fsorok = new List<string>();

            string szoveg = Environment.NewLine + "# hallgatók" + Environment.NewLine;
            szoveg += "INSERT INTO hallgato (id, nev) VALUES" + Environment.NewLine;

            foreach (var hallgató in hallgatok)
            {
                string sor = $"({hallgató.id}, {hallgató.nev})";
                fsorok.Add(sor);
            }

            szoveg += String.Join("," + Environment.NewLine, fsorok);
            szoveg += ";";

            File.AppendAllText(fajlKollokviumImport, szoveg);
        }

        private void delteGenerátor()
        {
            List<string> fsorok = new List<string>();
            fsorok.Add("DELETE FROM jelentkezes;");
            fsorok.Add("DELETE FROM hallgato;");
            fsorok.Add("DELETE FROM vizsga;");
            File.WriteAllLines(fajlKollokviumImport, fsorok);
        }

        private void BeolvasJelentkezes()
        {
            string[] sorok = File.ReadAllLines(fajlJelentkezes);
            foreach (var sor in sorok.Skip(1))
            {
                string[] oszlopok = sor.Split('\t');
                string hallgatoIdRegi = oszlopok[0];
                int hallgatoid = idSzotar[hallgatoIdRegi];
                int vizsgaid = int.Parse(oszlopok[1]);
                string jeldatum = $"'{oszlopok[2]}'";
                string ledatum = oszlopok[3] == "" ? "Null" : $"'{oszlopok[3]}'";
                int igazolt = oszlopok[4] == "" ? 0 : int.Parse(oszlopok[4]);
                string jegy = oszlopok[5] == "" ? "Null" : $"'{oszlopok[5]}'";
                jelentkezesek.Add(new Jelentkezes(hallgatoid, vizsgaid, jeldatum, ledatum, igazolt, jegy));
            }
        }

        private void BeolvasHallgato()
        {
            string[] sorok = File.ReadAllLines(fajlHallgato);
            int id = 1;
            foreach (var sor in sorok.Skip(1))
            {
                string[] oszlopok = sor.Split('\t');
                string idRegi = oszlopok[0];
                string nev = $"'{oszlopok[1]}'";
                idSzotar.Add(idRegi, id);
                hallgatok.Add(new Hallgato(id, nev));
                id++;
            }
        }

        private void BeolvasVizsga()
        {
            string[] sorok = File.ReadAllLines(fajlVizsga);
            foreach (var sor in sorok.Skip(1))
            {
                string[] oszlopok = sor.Split('\t');
                int id = int.Parse(oszlopok[0]);
                string datum = $"'{oszlopok[1]}'";
                string targy = $"'{oszlopok[2]}'";
                vizsgak.Add(new Vizsga(id, datum, targy));
            }
        }
    }
}