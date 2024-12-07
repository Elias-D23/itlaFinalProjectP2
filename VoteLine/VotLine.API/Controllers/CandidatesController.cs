using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VoteLine.Common.Dtos;
using VoteLine.Common.Requests;
using VoteLine.Common.Responses;
using VoteLine.Domain.Entities;
using VoteLine.Persistence;
using VotLine.Infrastructure.Interfaces;
using VotLine.Infrastructure.Repositories;

namespace VoteLine.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidateRepository _repo;

        public CandidatesController(ICandidateRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("GetCandidates")]
        public async Task<ActionResult<List<CandidateDto>>> GetCandidates()
        {
            var candidates = await _repo.GetCandidates();
            if (!candidates.Any())
                return NotFound("No data Found");

            return candidates;
        }

        [HttpGet("GetCandidate/{id}")]
        public async Task<ActionResult<CandidateDto>> GetCandidate(int id)
        {
            if (id == 0)
                return BadRequest("You need to give me a valid Id");

            var candidateDto = await _repo.GetCandidate(id);
            if (candidateDto == null)
            {
                return NotFound();
            }

            return candidateDto;
        }

        [HttpPost("AddCandidate")]
        public async Task<ActionResult<NewCandidateResponse>> AddCandidate(NewCandidateRequest request)
        {
            if (string.IsNullOrEmpty(request.FullName) ||
                string.IsNullOrEmpty(request.Party) ||
                string.IsNullOrEmpty(request.Position) ||
                string.IsNullOrEmpty(request.Picture))
            {
                return BadRequest("All fields are required.");
            }


            return await _repo.AddCandidate(request);

        }

        //[HttpPost("AddCandidate")]
        //public async Task<ActionResult<Candidate>> AddCandidate(NewCandidateRequest request)
        //{
        //    if (string.IsNullOrEmpty(request.FullName) ||
        //        string.IsNullOrEmpty(request.Party) ||
        //        string.IsNullOrEmpty(request.Position) ||
        //        string.IsNullOrEmpty(request.Picture))
        //    {
        //        return BadRequest("All fields are required.");
        //    }

        //    var newCandidate = await _repo.AddCandidate(request);
        //    return CreatedAtAction(nameof(GetCandidate), new { id = newCandidate.CandidateId }, newCandidate);
        //}

        [HttpPut("UpdateCandidate/{id}")]
        public async Task<IActionResult> UpdateCandidate(int id, [FromBody] NewCandidateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            var success = await _repo.UpdateCandidate(id, request);
            if (!success)
            {
                return NotFound("Candidate not found.");
            }

            return NoContent();
        }

        [HttpDelete("DeleteCandidate/{id}")]
        public async Task<IActionResult> DeleteCandidate(int id)
        {
            var success = await _repo.DeleteCandidate(id);
            if (!success)
            {
                return NotFound("Candidate not found.");
            }

            return NoContent();
        }
    }
}

//namespace VoteLine.API.Controllers
//{
//    [ApiController]
//    [Route("[Controller]")]
//    public class CandidatesController : ControllerBase
//    {
//        private readonly VoteLineDbContext _context;

//        public CandidatesController(VoteLineDbContext context)
//        {
//            _context = context;
//        }

//        [HttpGet("GetCandidates")]
//        public async Task<ActionResult<List<Candidate>>> GetCandidates()
//        {
//            return await _context.Candidates.ToListAsync();
//        }

//        [HttpGet("GetCandidate/{id}")]
//        public async Task<ActionResult<Candidate>> GetCandidate(int id)
//        {
//            if (id == 0)
//                return BadRequest("You need to give me a valid Id");

//            var candidateDb = await _context.Candidates.FindAsync(id);
//            if (candidateDb == null)
//            {
//                return NotFound();
//            }

//            return candidateDb;
//        }


//        [HttpPost("AddCandidate")]
//        public async Task<ActionResult<Candidate>> AddCandidate(NewCandidateRequest request)
//        {
//            var candidateDb = new Candidate();

//            candidateDb.FullName = request.FullName;
//            candidateDb.Party = request.Party;
//            candidateDb.Position = request.Position;
//            candidateDb.Picture = request.Picture;

//            _context.Candidates.Add(candidateDb);
//            await _context.SaveChangesAsync();
//            return candidateDb;
//            //return Ok(userDb);
//        }


//        [HttpPut("UpdateCandidate/{id}")]
//        public async Task<IActionResult> UpdateCandidate(int id, [FromBody] NewCandidateRequest request)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest("Invalid data.");
//            }


//            var candidateDb = await _context.Candidates.FindAsync(id);
//            if (candidateDb == null)
//            {
//                return NotFound();
//            }
//            candidateDb.FullName = request.FullName;
//            candidateDb.Party = request.Party;
//            candidateDb.Position = request.Position;
//            candidateDb.Picture = request.Picture;

//            _context.Candidates.Update(candidateDb);
//            await _context.SaveChangesAsync();
//            return NoContent();
//        }



//        [HttpDelete("DeleteCandidate/{id}")]
//        public async Task<IActionResult> DeleteCandidate(int id)
//        {
//            var candidateDb = await _context.Candidates.FindAsync(id);

//            if (candidateDb == null)
//            {
//                return NotFound();
//            }

//            _context.Candidates.Remove(candidateDb);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//    }
//}
