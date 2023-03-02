namespace sqlImportProjekt
{
    internal class Hasznalt
    {
        public string quantity;
        public string unit;
        public int foodID;
        public int ingredientID;

        public Hasznalt(string quantity, string unit, int foodID, int ingredientID)
        {
            this.quantity = quantity;
            this.unit = unit;
            this.foodID = foodID;
            this.ingredientID = ingredientID;
        }
    }
}