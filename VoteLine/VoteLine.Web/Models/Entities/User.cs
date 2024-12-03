using System.ComponentModel.DataAnnotations.Schema;

namespace VoteLine.Web.Models.Entities
{

    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        [Column("PasswordHash")]
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string DNI { get; set; }
        public bool IsAdmin { get; set; }

        public ICollection<Vote>? Votes { get; set; }
    }
}
