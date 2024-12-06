﻿using System.ComponentModel.DataAnnotations.Schema;

namespace VoteLine.API.Dtos

{
    public class NewUserRequest
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string DNI { get; set; }

    }
}
