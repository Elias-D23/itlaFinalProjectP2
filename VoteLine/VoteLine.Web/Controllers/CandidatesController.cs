using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using VoteLine.Domain.Entities;
using VoteLine.Domain;
using VoteLine.Persistence;

namespace VoteLine.Web.Controllers
{
    public class CandidatesController : Controller
    {
        //private readonly VoteLineDbContext _Dbcontext;

        //public CandidatesController(VoteLineDbContext context)
        //{
        //    _Dbcontext = context;
        //}


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> ListCandidates()
        {
            //List<Candidate> listCandidates = await _Dbcontext.Candidates.ToListAsync();
            //return View(listCandidates);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            //Candidate candidate = await _Dbcontext.Candidates.FirstAsync(e => e.CandidateId == id);
            //return View(candidate);
            return View();

        }


        //[HttpPost]
        //public async Task<IActionResult> Edit(Person person)
        //{
        //    _Dbcontext.People.Update(person);
        //    await _Dbcontext.SaveChangesAsync();
        //    return RedirectToAction(nameof(List));
        //}
    }
}
