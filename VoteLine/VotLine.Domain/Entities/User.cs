﻿using System.ComponentModel.DataAnnotations.Schema;

namespace VoteLine.Domain.Entities
{

    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        [Column("PasswordHash")]
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string DNI { get; set; }
        public bool HasVoted { get; set; }

        public ICollection<Vote>? Votes { get; set; }
    }
}
