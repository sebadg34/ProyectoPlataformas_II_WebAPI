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

        /// <param name="user">Objeto recibido que contiene el email y contraseña del usuario entrante</param>
        /// <returns></returns>
        // POST: api/Login
        [ResponseType(typeof(User))]
        // se podria usar HttpResponseMessage  con return Request.CreateResponse(HttpStatusCode.NotFound, id);
        public IHttpActionResult Post([FromBody]User user)
        {

            // En caso de enviar un dato vacio
            if (user.Email == null || user.Contrasenia == null)
            {
                string message = "";
                if (user.Email == null && user.Contrasenia == null)
                {
                    message = "Correo y contraseña faltante";
                }
                else if (user.Email == null)
                {
                    message = "Correo faltante";
                }
                else if (user.Contrasenia == null) {
                    message = "Contraseña faltante";
                }
                return BadRequest(message);
            } 

            // Buscar el usuario registrado en el sistema.
            User registeredUser = db.User.SingleOrDefault(User => User.Email == user.Email);
            
            // En caso de no encontrar el usuario registrado
            if (registeredUser == null)

            {
                return NotFound();
            }

            // verificar si la contraseña es correcta.
            string pass = user.Contrasenia.ToString();

            if (!BCrypt.Net.BCrypt.Verify(pass, (registeredUser.Contrasenia).ToString()))

            {
                return Unauthorized();
            }
            // En caso de un logeo exitoso, se realiza los requests internos
            else
            {
                LoggedUser loggedUser = new LoggedUser();

                // En caso de que sea un cliente 

                if (registeredUser.ID_Rol == false) {
                    Customer customer = db.Customer.SingleOrDefault(Customer => Customer.ID == registeredUser.ID);
                    loggedUser.Name = customer.Nombres + " " + customer.Apellidos;
                    loggedUser.Id = customer.ID;
                }

                // En caso de que sea un administrador
                else if (registeredUser.ID_Rol == true)
                {
                    Manager admin = db.Manager.SingleOrDefault(Manager => Manager.ID == registeredUser.ID);
                    loggedUser.Name = admin.Nombres + " " + admin.Apellidos;
                    loggedUser.Id = admin.ID;
                }

                loggedUser.IdRol = registeredUser.ID_Rol;


                return Ok(loggedUser);
            }
        }
    }

    /// <summary>
    /// Clase auxiliar para empaquetar la respuesta del login al cliente
    /// </summary>
    public class LoggedUser{

        public string Name { get; set; }
        public bool IdRol { get; set; }
        public int Id { get; set; }
        
    }
}
