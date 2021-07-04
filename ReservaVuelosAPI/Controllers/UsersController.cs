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

namespace ReservaVuelosAPI.Controllers
{
    /// <summary>
    /// User controller, esta relacionado directamente al modelo User.
    /// </summary>
    public class UsersController : ApiController
    {
        // Obtiene la entidad de la base de datos.
        private DBEntities db = new DBEntities();

        /// <summary>
        /// Metodo que obtiene todos los usuarios de la base de datos.
        /// </summary>
        /// <returns>
        /// Retorna todos las filas de usuarios existentes en la base de datos.
        /// </returns>
        // GET: api/Users
        public IQueryable<User> GetUser()
        {
            return db.User;
        }

        /// <summary>
        /// Metodo que obtiene el user dado un email en especifico.
        /// </summary>
        /// <param name="email">
        /// Recibe el email, con este hara la busqueda en la BD.
        /// </param>
        /// <returns>
        /// Retona la informacion del rol si es que lo encuentra.
        /// </returns>
        // GET: api/Users?email=hola@ucn.cl
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(string email)
        {
            // Busca en la entidad de la base de datos si existe un usuario que tenga el email enviado por parametro.
            User users = db.User.SingleOrDefault(User => User.Email == email);

            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }

        /// <summary>
        /// Metodo que hace cambios de datos en el user que se le envia.
        /// </summary>
        /// <param name="id">
        /// id del user al cual se desea hacer el cambio.
        /// </param>
        /// <param name="user">
        /// user al cual se le desea hacer el cambio.
        /// </param>
        /// <returns>
        /// Retorna el estado: si se pudo realizar el cambio o no.
        /// </returns>
        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.ID)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Metodo que realiza una insercion en la base de datos dado un user en especifico.
        /// </summary>
        /// <param name="user">
        /// Recibe el user que se desea insertar, se recibe desde la aplicacion web.
        /// </param>
        /// <returns>
        /// Retorna el estado del metodo, si se pudo realizar la insercion o no.
        /// </returns>
        // POST: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Encripta la contraseña
            user.Contrasenia = BCrypt.Net.BCrypt.HashPassword(user.Contrasenia);

            db.User.Add(user);
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                    return NotFound();
            }

            return CreatedAtRoute("DefaultApi", new { id = user.ID }, user);
        }

        /// <summary>
        /// Metodo que elimina un user de la BD.
        /// </summary>
        /// <param name="id">
        /// Recibe el id desde la aplicacion web.
        /// </param>
        /// <returns>
        /// Retorna el estado: si se pudo eliminar o surgio algun problema.
        /// </returns>
        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = db.User.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.User.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        /** Metodo no utilizado
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        } **/

        /// <summary>
        /// Verifica si el usuario existe.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// Devuelve el número de elementos que satisfacen la condicion.
        /// </returns>
        private bool UserExists(int id)
        {
            return db.User.Count(e => e.ID == id) > 0;
        }
    }
}