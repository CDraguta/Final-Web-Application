using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Reteta
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Acest camp este obligatoriu")]
        public string Nume { get; set; }

        [DisplayName("Data publicare")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Acest camp este obligatoriu")]
        [DisplayFormat(ApplyFormatInEditMode = true , DataFormatString ="{0:MM/dd/yyyy" +
            "}")]
        public DateTime DataPublicare { get; set; }

        [DisplayName("Tip masa")]
        public string TipMasa { get;set; }
        public IEnumerable<SelectListItem> TipMasaList
        {
            get
            {
                return new List<SelectListItem>
        {
            new SelectListItem { Text = "Pranz", Value = "Pranz"},
            new SelectListItem { Text = "Fel principal", Value = "Fel principal"},
            new SelectListItem { Text = "Desert", Value = "Desert"},
            new SelectListItem { Text = "Aperitiv" ,Value="Aperitiv"}
        };
            }
        }

        [DisplayName("Tip bucatarie")]
        public string TipBucatarie { get; set; }
        public IEnumerable<SelectListItem> TipBucatarieList
        {
            get
            {
                return new List<SelectListItem>
        {
            new SelectListItem { Text = "Romaneasca", Value = "Romaneasca"},
            new SelectListItem { Text = "Asiatica", Value = "Asiatica"},
            new SelectListItem { Text = "Germana", Value = "Germana"},
            new SelectListItem { Text = "Italiana" ,Value="Italiana"}
        };
            }
        }
        
        [DisplayName("Fel de mancare")]
        public string FelMancare { get; set; }
        public IEnumerable<SelectListItem> FelMancareList
        {
            get
            {
                return new List<SelectListItem>
        {
            new SelectListItem { Text = "Supe/Ciorbe", Value = "Supe/Ciorbe"},
            new SelectListItem { Text = "Prajituri/Torturi", Value = "Prajituri/Torturi"},
            new SelectListItem { Text = "Legume", Value = "Legume"},
            new SelectListItem { Text = "Aperitiv" ,Value="Aperitiv"}
        };
            }
        }

        [Required(ErrorMessage = "Acest camp este obligatoriu")]
        public string Descriere { get; set; }

        [DisplayName("Fotografie")]
        public string Imagine { get; set; } 

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

    }
}