using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReservaVuelosAPI.Models
{
    [MetadataType(typeof(Customer.Metadata))]
    public partial class Customer 
    {
        sealed class Metadata
        {
            [Required(ErrorMessage = "Ingrese el nombre.")]
            public string Nombres;

            [Required(ErrorMessage = "Ingrese el apellido.")]
            public string Apellidos;

            [RegularExpression("(^[0-9]{7,8}-([0-9]|k)$)", ErrorMessage = "Ingrese correctamente el rut.")]
            public string Rut;

            [RegularExpression("((^([A-Z]|[0-9]){9})$)", ErrorMessage = "Ingrese correctamente el número de pasaporte.")]
            public string Numero_Pasaporte;

            [Required(ErrorMessage = "Ingrese la dirección.")]
            public string Direccion;

            [Required(ErrorMessage = "Ingrese el número de la dirección.")]
            public int Numero_Direccion;

            [Required(ErrorMessage = "Ingrese el número de teléfono.")]
            [RegularExpression("^9[0-9]{8}$", ErrorMessage = "Ingrese correctamente el número de teléfono.")]
            public int Numero_Telefono;

            [Required(ErrorMessage = "Ingrese el nombre de emergencia.")]
            public string Nombres_Emergencia;

            [Required(ErrorMessage = "Ingrese el apellido de emergencia.")]
            public string Apellidos_Emergencia;

            [Required(ErrorMessage = "Ingrese el número de teléfono de emergencia.")]
            [RegularExpression("^9[0-9]{8}$}", ErrorMessage = "Ingrese correctamente el número de teléfono.")]
            public int Numero_Telefono_Emergencia;

            [Key]
            [Required]
            public int ID;
        }
    }
}