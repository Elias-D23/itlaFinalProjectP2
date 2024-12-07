using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using VoteLine.API.Dtos;
using VoteLine.Domain;
using VoteLine.Domain.Entities;
using VoteLine.Persistence;

namespace VoteLine.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UsersController : ControllerBase
    {
        private readonly VoteLineDbContext _context;

        public UsersController(VoteLineDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetUsers")]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpGet("GetUser/{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            if (id == 0)
                return BadRequest("You need to give me a valid Id");

            var userDb = await _context.Users.FindAsync(id);
            if (userDb == null)
            {
                return NotFound();
            }

            return userDb;
        }


        [HttpPost("AddUser")]
        public async Task<ActionResult<User>> AddUser(NewUserRequest request)
        {
            if (string.IsNullOrEmpty(request.FullName) ||
                string.IsNullOrEmpty(request.Email) ||
                string.IsNullOrEmpty(request.Password) ||
                string.IsNullOrEmpty(request.DNI))
            {
                return BadRequest("All fields are required.");
            }
            var userDb = new User();

            userDb.FullName = request.FullName;
            userDb.Password = request.Password;
            userDb.DNI = request.DNI;
            userDb.Email = request.Email;
            userDb.HasVoted = request.HasVoted;

            _context.Users.Add(userDb);
            await _context.SaveChangesAsync();
            return userDb;
            //return Ok(userDb);
        }


        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] NewUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }


            var userDb = await _context.Users.FindAsync(id);
            if (userDb == null)
            {
                return NotFound();
            }

            userDb.FullName = request.FullName;
            userDb.Email = request.Email;
            userDb.Password = request.Password;
            userDb.DNI = request.DNI;

            _context.Users.Update(userDb);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        //[HttpDelete("DeleteUsers")]
        //public async Task<IActionResult> DeleteUsers(int id)
        //{
        //    var user = await _context.Users.FindAsync(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Users.Remove(user);
        //    await _context.SaveChangesAsync();
        //    return NoContent();
        //}


        [HttpDelete("DeleteUsers/{id}")]
        public async Task<IActionResult> DeleteUsers(int id)
        {
            var userDb = await _context.Users.FindAsync(id);

            if (userDb == null)
            {
                return NotFound();
            }

            _context.Users.Remove(userDb);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}

