using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReservaVuelosAPI.Models
{
    [MetadataType(typeof(Rol.Metadata))]
    public partial class Rol
    {
        sealed class Metadata
        {
            [Required(ErrorMessage = "Ingrese correctamente el email.")]
            [DataType(DataType.EmailAddress)]
            [EmailAddress]
            public string Email;

            [Required(ErrorMessage = "Ingrese correctamente la contraseña.")]
            [DataType(DataType.Password)]
            public string Contrasenia;

            [Required]
            public bool ID_Rol;

            [Key]
            public int ID;
        }
    }
}