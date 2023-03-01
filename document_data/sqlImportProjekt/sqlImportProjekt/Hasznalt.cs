namespace sqlImportProjekt
{
    internal class Hasznalt
    {
        public int quantity;
        public string unit;
        public int foodID;
        public int ingredientID;

        public Hasznalt(int quantity, string unit, int foodID, int ingredientID)
        {
            this.quantity = quantity;
            this.unit = unit;
            this.foodID = foodID;
            this.ingredientID = ingredientID;
        }
    }
}