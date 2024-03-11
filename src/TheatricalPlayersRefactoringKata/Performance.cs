namespace TheatricalPlayersRefactoringKata
{
    public class Performance
    {
        public string PlayId { get; set; }

        public int Audience { get; set; }

        public Performance(string playId, int audience)
        {
            this.PlayId = playId;
            this.Audience = audience;
        }

    }
}
