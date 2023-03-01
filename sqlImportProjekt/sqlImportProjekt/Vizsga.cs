namespace sqlImportProjekt
{
    internal class Vizsga
    {
        public int id;
        public string datum;
        public string targy;

        public Vizsga(int id, string datum, string targy)
        {
            this.id = id;
            this.datum = datum;
            this.targy = targy;
        }
    }
}