using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ReservaVuelosAPI.Models;
using System.Web.Http.Cors;

namespace ReservaVuelosAPI.Controllers
{

    /// <summary>
    /// Clase encargada de manejar el login del cliente, encargada de encriptar y validar el inicio de sesion.
    /// </summary>
    [EnableCors(origins: "http://localhost:52811", headers: "*", methods: "*")]
    public class LoginController : ApiController
    {
        private DBEntities db = new DBEntities();

        /// <summary>
        /// Metodo utilizado para entrar al sistema mediante el recibo de un email y contraseña.
        /// La verificacion se realiza mediante comparacion de hashes creados por Bcrypt.
        /// </summary>
        /// <param name="rol">Objeto recibido que contiene el email y contraseña del usuario entrante</param>
        /// <returns></returns>
        // POST: api/Login
        [ResponseType(typeof(Rol))]
        public IHttpActionResult Post([FromBody]Rol rol)
        {
            // En caso de enviar un dato vacio
            if (rol.Email == null || rol.Contrasenia == null)
            {
                return BadRequest();
            }
            // Buscar el usuario registrado en el sistema.
            Rol registeredRol = db.Rol.SingleOrDefault(Rol => Rol.Email == rol.Email);

            // En caso de no encontrar el usuario registrado
            if (registeredRol == null)
            {
                return NotFound();
            }

            // verificar si la contraseña es correcta.
            // bool verified = BCrypt.Net.BCrypt.Verify(rol.Contrasenia, registeredRol.Contrasenia);
            if (!BCrypt.Net.BCrypt.Verify(rol.Contrasenia, registeredRol.Contrasenia))
            {
                return NotFound();
            }
            else
            {
                return Ok(registeredRol);
            }
        }
    }
}
