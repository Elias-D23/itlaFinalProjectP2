namespace VoteLine.API.Dtos
{
    public class NewCandidateRequest
    {
        public string FullName { get; set; } = string.Empty;
        public string? Party { get; set; }
        public string? Position { get; set; }
        public string Picture { get; set; }

    }
}
