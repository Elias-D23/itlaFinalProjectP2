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
