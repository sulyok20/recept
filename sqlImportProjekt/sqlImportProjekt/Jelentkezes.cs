namespace sqlImportProjekt
{
    internal class Jelentkezes
    {
        public int hallgatoid;
        public int vizsgaid;
        public string jeldatum;
        public string ledatum;
        public int igazolt;
        public string jegy;

        public Jelentkezes(int hallgatoid, int vizsgaid, string jeldatum, string ledatum, int igazolt, string jegy)
        {
            this.hallgatoid = hallgatoid;
            this.vizsgaid = vizsgaid;
            this.jeldatum = jeldatum;
            this.ledatum = ledatum;
            this.igazolt = igazolt;
            this.jegy = jegy;
        }
    }
}