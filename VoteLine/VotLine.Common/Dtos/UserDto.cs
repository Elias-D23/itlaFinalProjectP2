using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoteLine.Common.Dtos
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        [Column("PasswordHash")]
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string DNI { get; set; }
        public bool HasVoted { get; set; }
    }
}
