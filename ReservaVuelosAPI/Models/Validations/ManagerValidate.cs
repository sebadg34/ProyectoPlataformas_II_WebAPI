using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReservaVuelosAPI.Models
{
    [MetadataType(typeof(Manager.Metadata))]
    public partial class Manager
    {
        sealed class Metadata
        {
            [Required(ErrorMessage = "Ingrese el nombre.")]
            public string Nombres;
            
            [Required(ErrorMessage = "Ingrese el nombre.")]
            public string Apellidos;
            
            [Required(ErrorMessage = "Ingrese el rut.")]
            [RegularExpression("(^[0-9]{7,8}-([0-9]|k)$)", ErrorMessage = "Ingrese correctamente el rut.")]
            public string Rut;
            
            [Key]
            [Required]
            public int ID;
        }
    }
}