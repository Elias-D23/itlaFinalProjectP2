namespace VoteLine.Domain.Entities
{
    public class Vote
    {
        public int VoteId { get; set; }
        public int UserId { get; set; }
        public int CandidateId { get; set; }
        public DateTime VoteDate { get; set; } = DateTime.Now;

        public User? User { get; set; }
        public Candidate? Candidate { get; set; }
    }

}
