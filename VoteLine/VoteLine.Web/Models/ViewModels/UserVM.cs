namespace VoteLine.Web.Models.ViewModels
{
    public class UserVM
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        //public string ConfirmEmail { get; set; } = string.Empty;
        public string DNI { get; set; }
        //public string ConfirmDNI { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public bool IsAdmin { get; set; }



    }
}
