
using System.ComponentModel;


namespace WebApplication1.Models
{
    public partial class Utilizator
    {
        public int Id { get; set; }
        public string Username { get; set; }

        [DisplayName("Parola")]
        public string Password { get; set; } 

        [DisplayName("Email")]
        public string EmailId { get; set; }

        [DisplayName("Este email-ul verificat?")]
        public bool IsEmailVerified { get; set; }

        [DisplayName("Cod de activare")]
        public System.Guid ActivationCode { get; set; }
    }
}