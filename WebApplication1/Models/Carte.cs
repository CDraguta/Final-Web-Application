using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace WebApplication1.Models
{
    public class Carte
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Acest camp este obligatoriu")]
        public string Nume { get; set; }

        [Required(ErrorMessage = "Acest camp este obligatoriu")]
        public int Pret { get; set; }

        [Required(ErrorMessage = "Acest camp este obligatoriu")]
        public string Descriere { get; set; }


        [DisplayName("Fotografie")]
        public string Imagine { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}