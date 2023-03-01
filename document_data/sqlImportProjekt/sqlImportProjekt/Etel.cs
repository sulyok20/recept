namespace sqlImportProjekt
{
    internal class Etel
    {
        public string foodName;
        public int id;
        public int categoryID;
        public string descriptionDate;
        public string firstDate;

        public Etel(string foodName, int id, int categoryID, string descriptionDate, string firstDate)
        {
            this.foodName = foodName;
            this.id = id;
            this.categoryID = categoryID;
            this.descriptionDate = descriptionDate;
            this.firstDate = firstDate;
        }
    }
}