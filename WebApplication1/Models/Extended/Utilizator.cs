using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using
System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    [MetadataType(typeof(UtilizatorMetaData))]
    public partial class Utilizator
    {
        public string ConfirmPassword { get; set; }
    }
    public class UtilizatorMetaData
    {
        [Display(Name ="Username")]
        [Required(AllowEmptyStrings = false , ErrorMessage = "Username obligatoriu")]
        public string Username { get; set; }

        [Required(AllowEmptyStrings = false , ErrorMessage = "Password obligatorie")]
        [DataType(DataType.Password)]
        [MinLength(6,ErrorMessage = "Minim 6 caractere obligatoriu")]
        public string Password { get; set; }

        [Display(Name ="Email ID")]
        [Required(AllowEmptyStrings =false , ErrorMessage = "Email obligatoriu")]
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }

        [Display(Name ="Confirmare Password")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "Parolele nu se potrivesc")]
        public string ConfirmPassword { get; set; }
    }
}