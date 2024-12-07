using System.ComponentModel.DataAnnotations.Schema;

namespace VoteLine.Common.Requests

{
    public class NewUserRequest
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string DNI { get; set; }
        public bool HasVoted { get; set; } = false;



    }
}
