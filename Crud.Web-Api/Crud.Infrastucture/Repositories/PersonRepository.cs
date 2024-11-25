using Crud.Common.Dtos;
using Crud.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Infrastructure.Repositories
{
    internal class PersonRepository
    {
        private readonly CrudDbContext _context;
        public PersonRepository(CrudDbContext context) 
        {
            _context = context;
        }

        public async Task<PersonDto> GetPeople() 
        {
            var people =  await _context.People.ToListAsync();
              
            var peopleToReturn = new List<PersonDto>();

            return null;
       
        }
    }
}
