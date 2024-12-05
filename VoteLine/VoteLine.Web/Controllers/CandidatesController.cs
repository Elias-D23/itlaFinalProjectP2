using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using VoteLine.Domain.Entities;
using VoteLine.Domain;

namespace VoteLine.Web.Controllers
{
    public class CandidatesController : Controller
    {
        private readonly VoteLineDbContext _Dbcontext;

        public CandidatesController(VoteLineDbContext context)
        {
            _Dbcontext = context;
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Candidate candidate)
        {
            await _Dbcontext.Candidates.AddAsync(candidate);
            await _Dbcontext.SaveChangesAsync();
            return RedirectToAction(nameof(ListCandidates));
        }


        [HttpGet]
        public async Task<IActionResult> ListCandidates()
        {
            List<Candidate> listCandidates = await _Dbcontext.Candidates.ToListAsync();
            return View(listCandidates);
        }

        //[HttpGet]
        //public async Task<IActionResult> Edit(int id)
        //{
        //    Person person = await _Dbcontext.People.FirstAsync(e => e.Id == id);
        //    return View(person);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Edit(Person person)
        //{
        //    _Dbcontext.People.Update(person);
        //    await _Dbcontext.SaveChangesAsync();
        //    return RedirectToAction(nameof(List));
        //}
    }
}
