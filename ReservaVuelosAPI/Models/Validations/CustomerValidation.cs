using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReservaVuelosAPI.Models
{
    /// <summary>
    /// Clase parcial del modelo de customer, usada para validar las entradas de la entidad Customer.
    /// Se utiliza Metadata para hacer la relacion con el modelo de datos Customer.
    /// </summary>
    [MetadataType(typeof(Customer.Metadata))]
    public partial class Customer
    {
        sealed class Metadata
        {
            // Validacion del nombre; es un campo requerido.
            [Required(ErrorMessage = "Ingrese el nombre.")]
            [RegularExpression("([A-Za-záéíóúñÁÉÍÓÚ])*", ErrorMessage = "Solo letras.")]
            public string Nombres;

            // Validacion del apellido; es un campo requerido.
            [Required(ErrorMessage = "Ingrese el apellido.")]
            [RegularExpression("([A-Za-záéíóúñÁÉÍÓÚ])*", ErrorMessage = "Solo letras.")]
            public string Apellidos;

            // Validacion del rut; es un campo requerido. Se utilizo una expresion regular para validar el formato en el cual se ingresan los ruts. EJ: 12345678-9.
            [RegularExpression("(^[0-9]{7,8}-([0-9]|k)$)", ErrorMessage = "Ingrese correctamente el rut.")]
            public string Rut;

            // Validacion del numero de pasaporte; es un campo requerido. Se utilizo una expresion regular para validar el formato en el cual se ingresan los numeros de pasaporte. EJ: AAP123123.
            [RegularExpression("((^([A-Z]|[0-9]){9})$)", ErrorMessage = "Ingrese correctamente el número de pasaporte.")]
            public string Numero_Pasaporte;

            // Validacion de la direccion fisica del cliente; es un campo requerido.
            [Required(ErrorMessage = "Ingrese la dirección.")]
            [RegularExpression("([A-Za-z])*", ErrorMessage = "Solo letras.")]
            public string Direccion;

            // Validacion del numero de direccion del cliente; es un campo requerido.
            [Required(ErrorMessage = "Ingrese el número de la dirección.")]
            public int Numero_Direccion;

            // Validacion del número de telefono del cliente; es un campo requerido. Se utilizo una expresion regular para validar el formato del numero de telefono. EJ: 912345678.
            [Required(ErrorMessage = "Ingrese el número de teléfono.")]
            [RegularExpression("^9[0-9]{8}$", ErrorMessage = "Ingrese correctamente el número de teléfono.")]
            public int Numero_Telefono;

            // Validacion del nombre de emergencia; es un campo requerido.
            [Required(ErrorMessage = "Ingrese el nombre de emergencia.")]
            [RegularExpression("([A-Za-záéíóúñÁÉÍÓÚ])*", ErrorMessage = "Solo letras.")]
            public string Nombres_Emergencia;

            // Validacion del apellido de emergencia; es un campo requerido.
            [Required(ErrorMessage = "Ingrese el apellido de emergencia.")]
            [RegularExpression("([A-Za-záéíóúñÁÉÍÓÚ])*", ErrorMessage = "Solo letras.")]
            public string Apellidos_Emergencia;

            // Validacion del numero de telefono de emergencia; es un campo requerido. Se utilizo una expresion regular para validar el formato del numero de telefono. EJ: 912345678.
            [Required(ErrorMessage = "Ingrese el número de teléfono de emergencia.")]
            [RegularExpression("^9[0-9]{8}$", ErrorMessage = "Ingrese correctamente el número de teléfono.")]
            public int Numero_Telefono_Emergencia;

            // ID es la clave primaria. Correlativo al ID del rol.
            [Key]
            [Required]
            public int ID;
        }
    }
}