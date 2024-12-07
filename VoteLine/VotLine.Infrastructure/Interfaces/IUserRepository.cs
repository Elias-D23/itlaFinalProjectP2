using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoteLine.Common.Dtos;
using VoteLine.Common.Requests;
using VotLine.Common.Responses;

namespace VotLine.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<List<UserDto>> GetUsers();
        Task<UserDto> GetUser(int id);
        Task<NewUserResponse> AddUser(NewUserRequest request);
        Task<bool> UpdateUser(int id, NewUserRequest request);
        Task<bool> DeleteUser(int id);
    }
}
