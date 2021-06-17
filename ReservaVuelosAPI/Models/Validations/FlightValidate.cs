﻿using System;
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
            public string Ciudad_Destino;

            // Validacion de la ciudad de origen; es un campo requerido.
            [Required(ErrorMessage = "Ingrese correctamente la ciudad de origen.")]
            public string Ciudad_Origen;

            // Validacion de la fecha de salida; es un campo requerido. Se utilizo una expresion regular para validar la entrada de la fecha. EJ: YYYY-mm-dd HH:MM:SS.
            [Required(ErrorMessage = "Error en el ingreso de la fecha y hora de salida.")]
            [RegularExpression("^20[0-9][0-9]-(01|02|03|04|05|06|07|08|09|10|11|12)-([0-2][0-9]|30|31) [0-2][0-9]:[0-5][0-9]:[0-5][0-9].[0]{3}$", ErrorMessage = "Fecha y hora inválida.")]
            public System.DateTime Fecha_Salida;

            // Validacion de la fecha de llegada; es un campo requerido. Se utilizo una expresion regular para validar la entrada de la fecha. EJ: YYYY-mm-dd HH:MM:SS.
            [Required(ErrorMessage = "Error en el ingreso de la fecha y hora de salida")]
            [RegularExpression("^20[0-9][0-9]-(01|02|03|04|05|06|07|08|09|10|11|12)-([0-2][0-9]|30|31) [0-2][0-9]:[0-5][0-9]:[0-5][0-9].[0]{3}$", ErrorMessage = "Fecha y hora inválida.")]
            public System.DateTime Fecha_Llegada;

            // Validacion de la categoria; es un campo requerido. Debe ser basico, normal o premium.
            [Required(ErrorMessage = "Ingrese correctamente categoría.")]
            public string Categoria;

            // Validacion de los cupos; es un campo requerido. Debe estar entre 30 y 40.
            [Required(ErrorMessage = "Ingrese correctamente el cupo.")]
            [Range(30,40, ErrorMessage = "Los cupos deben estar entre 30 y 40.")]
            public int Cupos;

            // ID es la clave primaria.Es autogenerado (incrementado automatico en la base de datos) asi que no es necesario validarlo mas que eso.
            [Key]
            public int ID;
        }
    } 
}