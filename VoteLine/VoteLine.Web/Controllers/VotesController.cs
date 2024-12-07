using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VoteLine.Web;
using System.Linq;
using VoteLine.Domain.Entities;
using VoteLine.Domain;
using VoteLine.Persistence;


namespace VoteLine.Web.Controllers
{
    public class VotesController : Controller
    {
        //private readonly VoteLineDbContext _Dbcontext;

        //public VotesController(VoteLineDbContext context)
        //{
        //    _Dbcontext = context;
        //}

        public ActionResult ListVotes() { 
        
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Vote(int candidateId)
        //{
        //    // Usar AccessController para obtener el usuario autenticado
        //    var accessController = new AccessController(_Dbcontext);
        //    var user = accessController.GetAuthenticatedUser(HttpContext);

        //    // Verificar si el usuario ya votó
        //    var existingVote = await _Dbcontext.Votes.FirstOrDefaultAsync(v => v.UserId == user.UserId);
        //    if (existingVote != null)
        //    {
        //        TempData["InfoMessage"] = "You have already voted in this election..";
        //        return RedirectToAction("ListVotes");
        //    }

        //    // Registrar el voto
        //    var vote = new Vote
        //    {
        //        UserId = user.UserId,
        //        CandidateId = candidateId,
        //        VoteDate = DateTime.Now
        //    };

        //    _Dbcontext.Votes.Add(vote);
        //    await _Dbcontext.SaveChangesAsync();

        //    TempData["SuccessMessage"] = "¡Vote successfully registered.!";
        //    return RedirectToAction("ListVotes");
        //}

        //[HttpGet]
        //public async Task<IActionResult> ListVotes()
        //{
        //    var votes = await _Dbcontext.Votes
        //        .Include(v => v.User)
        //        .Include(v => v.Candidate)
        //        .Select(v => new
        //        {
        //            v.VoteId,
        //            v.UserId,
        //            UserName = v.User.FullName,
        //            UserEmail = v.User.Email,
        //            CandidateName = v.Candidate.FullName,
        //            CandidateParty = v.Candidate.Party,
        //            CandidatePosition = v.Candidate.Position,
        //            VoteDate = v.VoteDate
        //        })
        //        .ToListAsync();

        //    // Preparar los datos para la gráfica
        //    var votesByCandidate = _Dbcontext.Votes
        //        .Include(v => v.Candidate)
        //        .GroupBy(v => v.Candidate.FullName)
        //        .Select(g => new
        //        {
        //            CandidateName = g.Key,
        //            TotalVotes = g.Count()
        //        })
        //        .ToList();

        //    ViewBag.VoteData = Newtonsoft.Json.JsonConvert.SerializeObject(votesByCandidate);


        //    return View(votes);
        //}

        //[HttpGet]
        //public async Task<IActionResult> DeleteVote(int voteId)
        //{
        //    var vote = await _Dbcontext.Votes.FindAsync(voteId);
        //    if (vote == null)
        //    {
        //        return NotFound("The vote does not exist.");
        //    }

        //    _Dbcontext.Votes.Remove(vote);
        //    await _Dbcontext.SaveChangesAsync();

        //    TempData["SuccessMessage"] = "¡Vote successfully eliminated!";

        //    return RedirectToAction("ListVotes");
        //}

        //[HttpPost]
        //public async Task<IActionResult> EditVote(int voteId, int newCandidateId)
        //{
        //    var vote = await _Dbcontext.Votes.FindAsync(voteId);
        //    if (vote == null)
        //    {
        //        return NotFound("El voto no existe.");
        //    }

        //    vote.CandidateId = newCandidateId;
        //    await _Dbcontext.SaveChangesAsync();

        //    return RedirectToAction("ListVotes");
        //}

    }
}

