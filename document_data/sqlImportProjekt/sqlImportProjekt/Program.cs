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
            string categoryFile = "category.txt", ingredientFile = "ingredient.txt", usedFile = "used.txt", foodFile = "food.txt", fileReceptImport = "receptImport.sql";
            SqlImport s = new SqlImport(categoryFile, ingredientFile, usedFile, foodFile, fileReceptImport);


            Console.WriteLine("forrás -> {0}", fileReceptImport);
            Console.ReadLine();


        }
    }
}
