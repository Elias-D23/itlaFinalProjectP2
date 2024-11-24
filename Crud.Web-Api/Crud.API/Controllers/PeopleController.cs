using Azure.Core;
using Crud.API.Dtos;
using Crud.Domain.Entities;
using Crud.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Crud.API.Controllers
{
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly CrudDbContext _context;

        public PeopleController(CrudDbContext context)
        {
            _context = context;
        }


        [HttpGet(nameof(GetPerson))]
        public async Task<ActionResult<List<Person>>> GetPeople()
        {
            return await _context.People.ToListAsync();
        }

        [HttpGet("GetPerson/{id}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
            if (id == 0)
                return BadRequest("You need to give me a valid Id");

            var personDb = await _context.People.FindAsync(id);
            if (personDb == null)
            {
                return NotFound();
            }

            return personDb;
        }

        [HttpPost("AddPerson/{id}")]
        public async Task<ActionResult<List<Person>>> AddPerson(NewPersonRequest request)
        {
            if (string.IsNullOrEmpty(request.Name))
                return BadRequest("You need to set a name");

            if (string.IsNullOrEmpty(request.Phone))
                return BadRequest("You need to set a Phone");

            if (string.IsNullOrEmpty(request.LastName))
                return BadRequest("You need to set a last names");

            if (string.IsNullOrEmpty(request.Email))
                return BadRequest("You need to set a email");

            if (!request.Email.Contains("@"))
                return BadRequest("you need to provide a valid email");


            var personDb = new Person();

            personDb.Name = request.Name;
            personDb.LastName = request.LastName;
            personDb.Email = request.Email;
            personDb.Phone = request.Phone;

            _context.People.Add(personDb);
            await _context.SaveChangesAsync();
            return Ok(personDb);
        }

        [HttpPut("UpdatePerson/{id}")]
        public async Task<IActionResult> UpdatePerson(int id, [FromBody] NewPersonRequest request)
        {
            var personDb = await _context.People.FindAsync(id);
            if (personDb == null)
            {
                return NotFound();
            }

            personDb.Name = request.Name;
            personDb.LastName = request.LastName;
            personDb.Email = request.Email;
            personDb.Phone = request.Phone;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("DeletePerson/{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var person = await _context.People.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            _context.People.Remove(person);
            await _context.SaveChangesAsync();
            return NoContent();
        }


    }
}
