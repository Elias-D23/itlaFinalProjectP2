using Azure.Core;
using Crud.Common.Requests;
using Crud.Common.Dtos;
using Crud.Domain.Entities;
using Crud.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Crud.Infrastructure.Repositories;
using Crud.Common.Responses;

namespace Crud.API.Controllers
{
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly PersonRepository _repo;

        public PeopleController(PersonRepository repo)
        {
            _repo = repo;
        }


        [HttpGet(nameof(GetPeople))]
        public async Task<ActionResult<List<PersonDto>>> GetPeople()
        {
            var people = await _repo.GetPeople();
            if (!people.Any())
                return BadRequest("No data Found");

            return people;
        }

        [HttpGet("GetPerson/{id}")]
        public async Task<ActionResult<PersonDto>> GetPerson(int id)
        {
            if (id == 0)
                return BadRequest("You need to give me a valid Id");

            var personDto = await _repo.GetPerson(id);
            if (personDto == null)
            {
                return NotFound();
            }

            return personDto;
        }

        [HttpPost("AddPerson/{id}")]
        public async Task<ActionResult<NewPersonResponse>> AddPerson(NewPersonRequest request)
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


            //var personDb = new PersonDto();

            //personDb.Name = request.Name;
            //personDb.LastName = request.LastName;
            //personDb.Email = request.Email;
            //personDb.Phone = request.Phone;

            //_repo.People.Add(personDb);
            //await _repo.SaveChangesAsync();

            return await _repo.AddPerson(request);
        }

        [HttpPut("UpdatePerson/{id}")]
        public async Task<IActionResult> UpdatePerson(int id, [FromBody] NewPersonRequest request)
        {
            //var personDb = await _repo.People.FindAsync(id);
            //if (personDb == null)
            //{
            //    return NotFound();
            //}

            //personDb.Name = request.Name;
            //personDb.LastName = request.LastName;
            //personDb.Email = request.Email;
            //personDb.Phone = request.Phone;

            //await _repo.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("DeletePerson/{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            //var person = await _repo.People.FindAsync(id);
            //if (person == null)
            //{
            //    return NotFound();
            //}

            //_repo.People.Remove(person);
            //await _repo.SaveChangesAsync();
            return NoContent();
        }


    }
}
