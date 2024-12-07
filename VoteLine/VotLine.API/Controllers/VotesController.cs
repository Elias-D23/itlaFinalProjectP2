using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VoteLine.Domain.Entities;
using VoteLine.Persistence;
using VotLine.Infrastructure.Interfaces;
using VotLine.Infrastructure.Repositories;

namespace VoteLine.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class VotesController : ControllerBase
    {
        private readonly IVoteRepository _repo;

        public VotesController(IVoteRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("SubmitVote")]
        public async Task<IActionResult> SubmitVote(Vote model)
        {
            var success = await _repo.SubmitVote(model);
            if (!success)
            {
                return BadRequest("You have already voted in this election.");
            }

            return Ok("Vote successfully registered.");
        }

        [HttpGet("GetResults")]
        public async Task<IActionResult> GetResults()
        {
            var results = await _repo.GetResults();
            if (results == null || results.Count == 0)
            {
                return NotFound("No voting results available.");
            }

            return Ok(results);
        }
    }
}

//namespace VoteLine.API.Controllers
//{
//    [ApiController]
//    [Route("[Controller]")]
//    public class VotesController : ControllerBase
//    {
//        private readonly VoteLineDbContext _context;

//        public VotesController(VoteLineDbContext context)
//        {
//            _context = context;
//        }

//        [HttpPost("SubmitVote")]
//        public async Task<IActionResult> SubmitVote(Vote model)
//        {
//            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == model.UserId);
//            if (user == null || user.HasVoted)
//            {
//                return BadRequest("You have already voted in this election.");
//            }

//            var vote = new Vote
//            {
//                CandidateId = model.CandidateId,
//                UserId = model.UserId,
//                VoteDate = DateTime.Now
//            };

//            _context.Votes.Add(vote);
//            user.HasVoted = true;

//            await _context.SaveChangesAsync();
//            return Ok("Vote successfully registered.");
//        }

//        [HttpGet("GetResults")]
//        public async Task<IActionResult> GetResults()
//        {
//            var results = await _context.Votes
//                .GroupBy(v => v.CandidateId)
//                .Select(group => new
//                {   id = group.FirstOrDefault().CandidateId,
//                    CandidateName = group.FirstOrDefault().Candidate.FullName,
//                    CandidatePosition = group.FirstOrDefault().Candidate.Position,
//                    VoteCount = group.Count()
//                })
//                .ToListAsync();

//            return Ok(results);
//        }

//    }
//}