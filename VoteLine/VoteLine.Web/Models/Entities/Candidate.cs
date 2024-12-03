namespace VoteLine.Web.Models.Entities
{
    public class Candidate
    {
        public int CandidateId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? Party { get; set; }
        public string? Position { get; set; }


        // Navegación
        public ICollection<Vote>? Votes { get; set; }
    }

}
