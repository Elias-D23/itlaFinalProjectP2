namespace VoteLine.Common.Dtos
{
    public class CandidateDto
    {
        public int CandidateId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? Party { get; set; }
        public string? Position { get; set; }
        public string Picture { get; set; }

    }

}
