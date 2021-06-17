using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReservaVuelosAPI.Models
{
    /// <summary>
    /// Clase parcial del modelo de manager, usada para validar las entradas de la entidad Manager.
    /// Se utiliza Metadata para hacer la relación con el modelo de datos Manager.
    /// </summary>
    [MetadataType(typeof(Manager.Metadata))]
    public partial class Manager
    {
        sealed class Metadata
        {
            //Validación del nombre; es un campo requerido.
            [Required(ErrorMessage = "Ingrese el nombre.")]
            public string Nombres;
            
            //Validación del apellido; es un campo requerido.
            [Required(ErrorMessage = "Ingrese el nombre.")]
            public string Apellidos;

            //Validación del rut; es un campo requerido. Se utilizó una expresión regular para validar el formato en el cuál se ingresan los ruts. EJ: 12345678-9.
            [Required(ErrorMessage = "Ingrese el rut.")]
            [RegularExpression("(^[0-9]{7,8}-([0-9]|k)$)", ErrorMessage = "Ingrese correctamente el rut.")]
            public string Rut;

            //ID es la clave primaria. Correlativo al ID del rol.
            [Key]
            [Required]
            public int ID;
        }
    }
}