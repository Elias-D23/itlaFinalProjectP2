using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VoteLine.API.Dtos;
using VoteLine.Domain.Entities;
using VoteLine.Persistence;

namespace VoteLine.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class CandidatesController : ControllerBase
    {
        private readonly VoteLineDbContext _context;

        public CandidatesController(VoteLineDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetCandidates")]
        public async Task<ActionResult<List<Candidate>>> GetCandidates()
        {
            return await _context.Candidates.ToListAsync();
        }

        [HttpGet("GetCandidate/{id}")]
        public async Task<ActionResult<Candidate>> GetCandidate(int id)
        {
            if (id == 0)
                return BadRequest("You need to give me a valid Id");

            var candidateDb = await _context.Candidates.FindAsync(id);
            if (candidateDb == null)
            {
                return NotFound();
            }

            return candidateDb;
        }


        [HttpPost("AddCandidate")]
        public async Task<ActionResult<Candidate>> AddCandidate(NewCandidateRequest request)
        {
            //if (string.IsNullOrEmpty(request.FullName) ||
            //    string.IsNullOrEmpty(request.Email) ||
            //    string.IsNullOrEmpty(request.Password) ||
            //    string.IsNullOrEmpty(request.DNI))
            //{
            //    return BadRequest("All fields are required.");
            //}
            var candidateDb = new Candidate();

            candidateDb.FullName = request.FullName;
            candidateDb.Party = request.Party;
            candidateDb.Position = request.Position;
            candidateDb.Picture = request.Picture;

            _context.Candidates.Add(candidateDb);
            await _context.SaveChangesAsync();
            return candidateDb;
            //return Ok(userDb);
        }


        [HttpPut("UpdateCandidate/{id}")]
        public async Task<IActionResult> UpdateCandidate(int id, [FromBody] NewCandidateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }


            var candidateDb = await _context.Candidates.FindAsync(id);
            if (candidateDb == null)
            {
                return NotFound();
            }
            candidateDb.FullName = request.FullName;
            candidateDb.Party = request.Party;
            candidateDb.Position = request.Position;
            candidateDb.Picture = request.Picture;

            _context.Candidates.Update(candidateDb);
            await _context.SaveChangesAsync();
            return NoContent();
        }

  

        [HttpDelete("DeleteCandidate/{id}")]
        public async Task<IActionResult> DeleteCandidate(int id)
        {
            var candidateDb = await _context.Candidates.FindAsync(id);

            if (candidateDb == null)
            {
                return NotFound();
            }

            _context.Candidates.Remove(candidateDb);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
