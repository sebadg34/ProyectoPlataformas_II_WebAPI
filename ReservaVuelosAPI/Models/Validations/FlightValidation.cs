using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReservaVuelosAPI.Models
{
    /// <summary>
    /// Clase parcial del modelo de flight, usada para validar las entradas de la entidad Flight.
    /// Se utiliza Metadata para hacer la relacion con el modelo de datos Flight.
    /// </summary>
    [MetadataType(typeof(Flight.Metadata))]
    public partial class Flight
    {
        sealed class Metadata
        {
            // Validacion del ID de vuelo; es un campo requerido.
            [Required]
            public string ID_Vuelo;

            // Validacion de la ciudad de destino; es un campo requerido.
            [Required(ErrorMessage = "Ingrese correctamente la ciudad de destino.")]
            [RegularExpression("([A-Za-záéíóúñÁÉÍÓÚ ])*", ErrorMessage = "Solo letras.")]
            public string Ciudad_Destino;

            // Validacion de la ciudad de origen; es un campo requerido.
            [Required(ErrorMessage = "Ingrese correctamente la ciudad de origen.")]
            [RegularExpression("([A-Za-záéíóúñÁÉÍÓÚ ])*", ErrorMessage = "Solo letras.")]
            public string Ciudad_Origen;

            // Validacion de la fecha de salida; es un campo requerido.
            [Required(ErrorMessage = "Error en el ingreso de la fecha y hora de salida.")]
            public System.DateTime Fecha_Salida;

            // Validacion de la fecha de llegada; es un campo requerido.
            [Required(ErrorMessage = "Error en el ingreso de la fecha y hora de salida")]
            public System.DateTime Fecha_Llegada;

            // Validacion de la categoria; es un campo requerido. Debe ser basico, normal o premium.
            [Required(ErrorMessage = "Ingrese correctamente categoría.")]
            public string Categoria;

            // Validacion de los cupos; es un campo requerido.
            [Required(ErrorMessage = "Ingrese correctamente el cupo.")]
            public int Cupos;

            // ID es la clave primaria.Es autogenerado (incrementado automatico en la base de datos) asi que no es necesario validarlo mas que eso.
            [Key]
            public int ID;
        }
    }
}