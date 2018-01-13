namespace Je_Banken
{
    public class Feedback
    {
        public int totalPoints { get; set; } = 0;
        public int pointsEtik { get; set; } = 0;
        public int pointsProdukter { get; set; } = 0;
        public int pointsEkonomi { get; set; } = 0;
        public bool passed { get; set; }
    }
}