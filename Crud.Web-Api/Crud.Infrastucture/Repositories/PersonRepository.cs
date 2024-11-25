using Crud.Common.Dtos;
using Crud.Common.Requests;
using Crud.Common.Responses;
using Crud.Domain.Entities;
using Crud.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Infrastructure.Repositories
{
    public class PersonRepository
    {
        private readonly CrudDbContext _context;
        public PersonRepository(CrudDbContext context) 
        {
            _context = context;
        }

        public async Task<List<PersonDto>> GetPeople() 
        {
            var people =  await _context.People.ToListAsync();
              
            var peopleToReturn = new List<PersonDto>();
            foreach (var personDb in people)
            {
                peopleToReturn.Add( FromPersonToPersonDto(personDb));
            }
            return peopleToReturn;
        }

        public async Task<PersonDto> GetPerson(int id)
        {
            var personDb = await _context.People.FindAsync(id);
            if (personDb == null)
            {
                throw new Exception("No person found");
            }
            return FromPersonToPersonDto(personDb);

        }

        public async Task<NewPersonResponse> AddPerson(NewPersonRequest request) 
        {
            var personDb = FromPersonDtoToPerson(request);

            _context.People.Add(personDb);
            await _context.SaveChangesAsync();

            return new NewPersonResponse { Id = personDb.Id };
        }

        private PersonDto FromPersonToPersonDto(Person personDb)
        {
            return new PersonDto
            {
                Id = personDb.Id,
                Name = personDb.Name,
                LastName = personDb.LastName,
                Phone = personDb.Phone,
                Email = personDb.Email,
            };
        }

        private Person FromPersonDtoToPerson(NewPersonRequest dto)
        {
            return new Person
            {
                Id = dto.Id,
                Name = dto.Name,
                LastName = dto.LastName,
                Phone = dto.Phone,
                Email = dto.Email,
            };
        }

        private Person FromPersonDtoToPerson(PersonDto dto)
        {
            return new Person
            {
                Id = dto.Id,
                Name = dto.Name,
                LastName = dto.LastName,
                Phone = dto.Phone,
                Email = dto.Email,
            };
        }

    }
}
