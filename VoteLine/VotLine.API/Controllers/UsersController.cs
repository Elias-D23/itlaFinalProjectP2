using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using VoteLine.Common.Requests;
using VoteLine.Common.Dtos;
using VoteLine.Domain;
using VoteLine.Domain.Entities;
using VoteLine.Persistence;
using VotLine.Infrastructure.Repositories;
using VotLine.Common.Responses;
using VotLine.Infrastructure.Interfaces;

namespace VoteLine.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repo;

        public UsersController(IUserRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("GetUsers")]
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        {
            var users = await _repo.GetUsers();
            if (!users.Any())
                return NotFound("No data Found");

            return users;
        }

        [HttpGet("GetUser/{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            if (id == 0)
                return BadRequest("You need to give me a valid Id");

            var userDto = await _repo.GetUser(id);
            if (userDto == null)
            {
                return NotFound();
            }

            return userDto;
        }


        [HttpPost("AddUser")]
        public async Task<ActionResult<NewUserResponse>> AddUser(NewUserRequest request)
        {
            if (string.IsNullOrEmpty(request.FullName) ||
                string.IsNullOrEmpty(request.Email) ||
                string.IsNullOrEmpty(request.Password) ||
                string.IsNullOrEmpty(request.DNI))
            {
                return BadRequest("All fields are required.");
            }

            return await _repo.AddUser(request);
            //return Ok(userDb);
        }

        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] NewUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            var success = await _repo.UpdateUser(id, request);
            if (!success)
            {
                return NotFound("User not found.");
            }

            return NoContent();
        }

        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var success = await _repo.DeleteUser(id);
            if (!success)
            {
                return NotFound("User not found.");
            }

            return NoContent();
        }

    }
}

