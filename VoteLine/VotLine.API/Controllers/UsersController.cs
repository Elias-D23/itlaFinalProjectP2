using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using VoteLine.API.Requets;
using VoteLine.Domain;
using VoteLine.Domain.Entities;

namespace VoteLine.API.Controllers
{

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

        [HttpPost("AddUsers")]
        public async Task<ActionResult<List<User>>> AddUsers(NewUserRequest request)
        {

            var userDb = new User();

            userDb.FullName = request.FullName;
            userDb.Email = request.Email;
            userDb.Password = request.Password;
            userDb.DNI = request.DNI;

            _context.Users.Add(userDb);
            await _context.SaveChangesAsync();
            return Ok(userDb);
        }

        [HttpPut("UpdateUsers")]
        public async Task<IActionResult> UpdateUsers(int id, [FromBody] NewUserRequest request)
        {
            var userDb = await _context.Users.FindAsync(id);
            if (userDb == null)
            {
                return NotFound();
            }

            userDb.FullName = request.FullName;
            userDb.Email = request.Email;
            userDb.Password = request.Password;
            userDb.DNI = request.DNI;

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
