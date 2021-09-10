using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebApplication1.Models
{
    public class UtilizatorLogin 
    {
        public int Id { get; set; }

        [DisplayName("Email")]
        [Required(AllowEmptyStrings = false , ErrorMessage = "Email necesar")]
        public string EmailID { get; set; }

        [DisplayName("Parola")] //adaugat dupa verificare logare
        [Required(AllowEmptyStrings = false , ErrorMessage ="Parola necesara")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Salveaza datele")]
        public bool RememberMe { get; set; }
    }
}