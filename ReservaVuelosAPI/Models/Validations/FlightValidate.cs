using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReservaVuelosAPI.Models
{
    [MetadataType(typeof(Flight.Metadata))]
    public partial class Flight
    {
        sealed class Metadata
        {
            [Required]
            public string ID_Vuelo;

            [Required(ErrorMessage = "Ingrese correctamente la ciudad de destino.")]
            public string Ciudad_Destino;

            [Required(ErrorMessage = "Ingrese correctamente la ciudad de origen.")]
            public string Ciudad_Origen;

            [Required(ErrorMessage = "Error en el ingreso de la fecha y hora de salida.")]
            [RegularExpression("^20[0-9][0-9]-(01|02|03|04|05|06|07|08|09|10|11|12)-([0-2][0-9]|30|31) [0-2][0-9]:[0-5][0-9]:[0-5][0-9].[0]{3}$", ErrorMessage = "Fecha y hora inválida.")]
            public System.DateTime Fecha_Salida;

            [Required(ErrorMessage = "Error en el ingreso de la fecha y hora de salida")]
            [RegularExpression("^20[0-9][0-9]-(01|02|03|04|05|06|07|08|09|10|11|12)-([0-2][0-9]|30|31) [0-2][0-9]:[0-5][0-9]:[0-5][0-9].[0]{3}$", ErrorMessage = "Fecha y hora inválida.")]
            public System.DateTime Fecha_Llegada;

            [Required(ErrorMessage = "Ingrese correctamente categoría.")]
            public string Categoria;

            [Required(ErrorMessage = "Ingrese correctamente el cupo.")]
            [Range(30,40, ErrorMessage = "Los cupos deben estar entre 30 y 40.")]
            public int Cupos;

            [Key]
            public int ID;
        }
    } 
}