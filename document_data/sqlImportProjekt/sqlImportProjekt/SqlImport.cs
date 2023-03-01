using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace sqlImportProjekt
{
    internal class SqlImport
    {
        private string categoryFile;
        private string ingredientFile;
        private string usedFile;
        private string foodFile;
        private string fileReceptImport;

        List<Kategoria> kategoriak = new List<Kategoria>();
        List<Hozzavalo> hozzavalok = new List<Hozzavalo>();
        List<Hasznalt> hasznaltLista = new List<Hasznalt>();
        List<Etel> etelek = new List<Etel>();

        public SqlImport(string categoryFile, string ingredientFile, string usedFile, string foodFile, string fileReceptImport)
        {
            this.categoryFile = categoryFile;
            this.ingredientFile = ingredientFile;
            this.usedFile = usedFile;
            this.foodFile = foodFile;
            this.fileReceptImport = fileReceptImport;
            BeolvasKategoria();
            BeolvasHozzavalo();
            BeolvasHasznalt();
            BeolvasEtel();
            ImportGenerátor();
        }

        private void BeolvasEtel()
        {
            string[] sorok = File.ReadAllLines(foodFile);
            foreach (var sor in sorok.Skip(1))
            {
                string[] oszlopok = sor.Split(';');
                string foodName = oszlopok[0];
                int id = int.Parse(oszlopok[1]);
                int categoryID = int.Parse(oszlopok[2]);
                string descriptionDate = oszlopok[3];
                string firstDate = oszlopok[4];

                etelek.Add(new Etel(foodName, id, categoryID, descriptionDate, firstDate));
            }
        }

        private void ImportGenerátor()
        {
            delteGenerátor();
            etelGenerátor();
            hozzavaloGenerátor();
            jelentkezésGenerátor();
            jelentkezésGenerátor();
            selectGenerátor();
        }

        private void selectGenerátor()
        {
            List<string> fsorok = new List<string>();
            fsorok.Add("");
            fsorok.Add("SELECT * FROM ingredient;");
            fsorok.Add("SELECT * FROM category;");
            fsorok.Add("SELECT * FROM food;");
            fsorok.Add("SELECT * FROM used;");
            File.AppendAllLines(fileReceptImport, fsorok);
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

            File.AppendAllText(fileReceptImport, szoveg);
        }

        private void hozzavaloGenerátor()
        {
            List<string> fsorok = new List<string>();

            string szoveg = Environment.NewLine + "# hozzávalók" + Environment.NewLine;
            szoveg += "INSERT INTO ingredient (id, ingredientName) VALUES" + Environment.NewLine;

            foreach (var hozzavalok in hozzavalok)
            {
                string sor = $"({hozzavalok.id}, {hozzavalok.ingredientName})";
                fsorok.Add(sor);
            }

            szoveg += String.Join("," + Environment.NewLine, fsorok);
            szoveg += ";";

            File.AppendAllText(fileReceptImport, szoveg);
        }

        private void etelGenerátor()
        {
            List<string> fsorok = new List<string>();

            string szoveg = Environment.NewLine + "# étel" + Environment.NewLine;
            szoveg += "INSERT INTO food (foodName ,id, categoryID, descriptionDate, firstDate) VALUES" + Environment.NewLine;

            foreach (var food in etelek)
            {
                string sor = $"({food.foodName}, {food.id}, {food.categoryID}, {food.descriptionDate}, {food.firstDate})";
                fsorok.Add(sor);
            }

            szoveg += String.Join("," + Environment.NewLine, fsorok);
            szoveg += ";";

            File.AppendAllText(fileReceptImport, szoveg);
        }

        private void delteGenerátor()
        {
            List<string> fsorok = new List<string>();
            fsorok.Add("DELETE FROM used;");
            fsorok.Add("DELETE FROM food;");
            fsorok.Add("DELETE FROM category;");
            fsorok.Add("DELETE FROM ingredient;");
            File.WriteAllLines(fileReceptImport, fsorok);
        }

        private void BeolvasHasznalt()
        {
            string[] sorok = File.ReadAllLines(usedFile);
            foreach (var sor in sorok.Skip(1))
            {
                string[] oszlopok = sor.Split(';');
                int quantity = int.Parse(oszlopok[0]);
                string unit = oszlopok[1];
                int foodID = int.Parse(oszlopok[2]);
                int ingredientID = int.Parse(oszlopok[3]);
                hasznaltLista.Add(new Hasznalt(quantity, unit, foodID, ingredientID));
            }
        }

        private void BeolvasHozzavalo()
        {
            string[] sorok = File.ReadAllLines(ingredientFile);
            foreach (var sor in sorok.Skip(1))
            {
                string[] oszlopok = sor.Split(';');
                int id = int.Parse(oszlopok[0]);
                string ingredientName = $"'{oszlopok[1]}'";
                hozzavalok.Add(new Hozzavalo(id, ingredientName));
            }
        }

        private void BeolvasKategoria()
        {
            string[] sorok = File.ReadAllLines(categoryFile);
            foreach (var sor in sorok.Skip(1))
            {
                string[] oszlopok = sor.Split(';');
                int id = int.Parse(oszlopok[0]);
                string categoryName = $"'{oszlopok[1]}'";
                kategoriak.Add(new Kategoria(id, categoryName));
            }
        }
    }
}