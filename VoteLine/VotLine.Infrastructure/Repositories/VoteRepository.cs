using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoteLine.Domain.Entities;
using VoteLine.Persistence;
using VotLine.Infrastructure.Interfaces;

namespace VotLine.Infrastructure.Repositories
{
    public class VoteRepository : IVoteRepository
    {
        private readonly VoteLineDbContext _context;

        public VoteRepository(VoteLineDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SubmitVote(Vote model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == model.UserId);
            if (user == null || user.HasVoted)
            {
                return false; // Voto no permitido
            }

            var vote = new Vote
            {
                CandidateId = model.CandidateId,
                UserId = model.UserId,
                VoteDate = DateTime.Now
            };

            _context.Votes.Add(vote);
            user.HasVoted = true;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<object>> GetResults()
        {
            var results = await _context.Votes
                .GroupBy(v => v.CandidateId)
                .Select(group => new
                {
                    Id = group.FirstOrDefault().CandidateId,
                    CandidateName = group.FirstOrDefault().Candidate.FullName,
                    CandidatePosition = group.FirstOrDefault().Candidate.Position,
                    CandidateParty = group.FirstOrDefault().Candidate.Party,
                    VoteCount = group.Count()
                })
                .ToListAsync();

            return results.Cast<object>().ToList();
        }
    }
}
