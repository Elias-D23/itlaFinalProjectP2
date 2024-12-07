using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VoteLine.Domain.Entities;
using VoteLine.Persistence;

namespace VoteLine.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class VotesController : ControllerBase
    {
        private readonly VoteLineDbContext _context;

        public VotesController(VoteLineDbContext context)
        {
            _context = context;
        }

        [HttpPost("SubmitVote")]
        public async Task<IActionResult> SubmitVote(Vote model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == model.UserId);
            if (user == null || user.HasVoted)
            {
                return BadRequest("El usuario ya ha votado o no existe.");
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
            return Ok("Voto registrado exitosamente.");
        }

        [HttpGet("GetResults")]
        public async Task<IActionResult> GetResults()
        {
            var results = await _context.Votes
                .GroupBy(v => v.CandidateId)
                .Select(group => new
                {
                    CandidateName = group.FirstOrDefault().Candidate.FullName,
                    VoteCount = group.Count()
                })
                .ToListAsync();

            return Ok(results);
        }

    }
}