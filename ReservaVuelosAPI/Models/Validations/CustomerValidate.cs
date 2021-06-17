using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReservaVuelosAPI.Models
{
    /// <summary>
    /// Clase parcial del modelo de customer, usada para validar las entradas de la entidad Customer.
    /// Se utiliza Metadata para hacer la relación con el modelo de datos Customer.
    /// </summary>
    [MetadataType(typeof(Customer.Metadata))]
    public partial class Customer 
    {
        sealed class Metadata
        {
            //Validación del nombre; es un campo requerido.
            [Required(ErrorMessage = "Ingrese el nombre.")]
            public string Nombres;

            //Validación del apellido; es un campo requerido.
            [Required(ErrorMessage = "Ingrese el apellido.")]
            public string Apellidos;

            //Validación del rut; es un campo requerido. Se utilizó una expresión regular para validar el formato en el cuál se ingresan los ruts. EJ: 12345678-9.
            [RegularExpression("(^[0-9]{7,8}-([0-9]|k)$)", ErrorMessage = "Ingrese correctamente el rut.")]
            public string Rut;

            //Validación del número de pasaporte; es un campo requerido. Se utilizó una expresión regular para validar el formato en el cuál se ingresan los números de pasaporte. EJ: AAP123123.
            [RegularExpression("((^([A-Z]|[0-9]){9})$)", ErrorMessage = "Ingrese correctamente el número de pasaporte.")]
            public string Numero_Pasaporte;

            //Valdiación de la dirección física del cliente; es un campo requerido.
            [Required(ErrorMessage = "Ingrese la dirección.")]
            public string Direccion;

            //Validación del número de dirección del cliente; es un campo requerido.
            [Required(ErrorMessage = "Ingrese el número de la dirección.")]
            public int Numero_Direccion;

            //Validación del número de teléfono del cliente; es un campo requerido. Se utilizó una expresión regular para validar el formato del número de teléfono. EJ: 912345678.
            [Required(ErrorMessage = "Ingrese el número de teléfono.")]
            [RegularExpression("^9[0-9]{8}$", ErrorMessage = "Ingrese correctamente el número de teléfono.")]
            public int Numero_Telefono;

            //Validación del nombre de emergencia; es un campo requerido.
            [Required(ErrorMessage = "Ingrese el nombre de emergencia.")]
            public string Nombres_Emergencia;

            //Validación del apellido de emergencia; es un campo requerido.
            [Required(ErrorMessage = "Ingrese el apellido de emergencia.")]
            public string Apellidos_Emergencia;

            //Validación del número de teléfono de emergencia; es un campo requerido. Se utilizó una expresión regular para validar el formato del número de teléfono. EJ: 912345678.
            [Required(ErrorMessage = "Ingrese el número de teléfono de emergencia.")]
            [RegularExpression("^9[0-9]{8}$", ErrorMessage = "Ingrese correctamente el número de teléfono.")]
            public int Numero_Telefono_Emergencia;

            //ID es la clave primaria.Correlativo al ID del rol.
            [Key]
            [Required]
            public int ID;
        }
    }
}