using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReservaVuelosAPI.Models
{
    /// <summary>
    /// Clase parcial del modelo de rol, usada para validar las entradas de la entidad Rol.
    /// Se utiliza Metadata para hacer la relación con el modelo de datos Rol.
    /// </summary>
    [MetadataType(typeof(Rol.Metadata))]
    public partial class Rol
    {
        sealed class Metadata
        {
            // Validación del email; es un campo requerido y exige el dato tipo email.
            [Required(ErrorMessage = "Ingrese correctamente el email.")]
            [DataType(DataType.EmailAddress)]
            [EmailAddress]
            public string Email;

            //Validación de la contraseña; es un campo requerido y exige el dato tipo password.
            [Required(ErrorMessage = "Ingrese correctamente la contraseña.")]
            [DataType(DataType.Password)]
            public string Contrasenia;

            //Validación del ID rol; es un campo requerido. Es false cuando es cliente, true cuando es administrador.
            [Required]
            public bool ID_Rol;

            //ID es la clave primaria. Es autogenerado (incrementado automático en la base de datos) así que no es necesario validarlo más que eso.
            [Key]
            public int ID;
        }
    }
}