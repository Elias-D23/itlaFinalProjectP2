using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoteLine.Common.Dtos;
using VoteLine.Common.Requests;
using VoteLine.Domain.Entities;
using VoteLine.Persistence;
using VotLine.Common.Responses;
using VotLine.Infrastructure.Interfaces;

namespace VotLine.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly VoteLineDbContext _context;

        public UserRepository(VoteLineDbContext context) 
        {
            _context = context;
        }

        public async Task<List<UserDto>> GetUsers() {

            var users = await _context.Users.ToListAsync();
            var usersToReturn = new List<UserDto>();
            foreach (var userDb in users)
            {
                usersToReturn.Add(FromUserToDto(userDb));
            }
            return usersToReturn;
        }

        public async Task<UserDto> GetUser(int id)
        {

            var userDb = await _context.Users.FindAsync(id);
            if (userDb == null) 
            {
                throw new Exception("No data Found");
            }
                
            return FromUserToDto(userDb);
            
        }

        public async Task<NewUserResponse> AddUser(NewUserRequest request)
        {

            var userDb = FromUserDtoToUser(request);


            _context.Users.Add(userDb);
            await _context.SaveChangesAsync();
            return new NewUserResponse {UserId = userDb.UserId };

        }


        public async Task<bool> UpdateUser(int id, NewUserRequest request)
        {
            var userDb = await _context.Users.FindAsync(id);
            if (userDb == null)
            {
                return false;
            }

            userDb.FullName = request.FullName;
            userDb.Email = request.Email;
            userDb.Password = request.Password;
            userDb.DNI = request.DNI;

            _context.Users.Update(userDb);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var userDb = await _context.Users.Include(u => u.Votes).FirstOrDefaultAsync(u => u.UserId == id);

            if (userDb == null)
            {
                return false;
            }

            // Eliminar los votos asociados al usuario
            if (userDb.Votes != null && userDb.Votes.Any())
            {
                _context.Votes.RemoveRange(userDb.Votes);
            }

            _context.Users.Remove(userDb);
            await _context.SaveChangesAsync();
            return true;
        }

        private UserDto FromUserToDto(User userDb)
        {
            return new UserDto
            {
                UserId = userDb.UserId,
                FullName = userDb.FullName,
                Password = userDb.Password,
                Email = userDb.Email,
                DNI = userDb.DNI,
                HasVoted = userDb.HasVoted,
            };
        }

        private User FromUserDtoToUser(NewUserRequest dto)
        {
            return new User
            {
                FullName = dto.FullName,
                Password = dto.Password,
                Email = dto.Email,
                DNI = dto.DNI,
                HasVoted = dto.HasVoted,
            };
        }

        private User FromUserDtoToUser(UserDto dto)
        {
            return new User
            {
                FullName = dto.FullName,
                Password = dto.Password,
                Email = dto.Email,
                DNI = dto.DNI,
                HasVoted = dto.HasVoted,
            };
        }
    }
}
