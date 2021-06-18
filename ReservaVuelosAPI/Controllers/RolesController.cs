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
    /// Rol controller, está relacionado directamente al modelo Rol.
    /// </summary>
    [EnableCors(origins: "http://localhost:52811", headers: "*", methods: "*")]
    public class RolesController : ApiController
    {
        //Obtiene la entidad de la base de datos.
        private DBEntities db = new DBEntities();

        /// <summary>
        /// Método que obtiene todos los roles de la base de datos.
        /// </summary>
        /// <returns>
        /// Retorna todos las filas de roles existentes en la base de datos.
        /// </returns>
        // GET: api/Roles
        public IQueryable<Rol> GetRol()
        {
            return db.Rol;
        }

        /// <summary>
        /// Método que obtiene el rol dado un email en específico.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>
        /// Retona la información del rol si es que lo encuentra.
        /// </returns>
        // GET: api/Roles?email=hola@ucn.cl
        [ResponseType(typeof(Rol))]
        public IHttpActionResult GetRol(string email)
        {
            // Busca en la entidad de la base de datos si existe un rol que tenga el email enviado por parametro.
            Rol roles = db.Rol.SingleOrDefault(Rol => Rol.Email == email);

            if (roles == null)
            {
                return NotFound();
            }

            return Ok(roles);
        }

        /// <summary>
        /// Método que realiza una inserción en la base de datos dado un rol en específico.
        /// </summary>
        /// <param name="rol"></param>
        /// <returns>
        /// Retorna el estado del método, si se pudo realizar la inserción o no.
        /// </returns>
        // POST: api/Roles
        [ResponseType(typeof(Rol))]
        public IHttpActionResult PostRol(Rol rol)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Encripta la contraseña
            rol.Contrasenia = BCrypt.Net.BCrypt.HashPassword(rol.Contrasenia);

            db.Rol.Add(rol);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = rol.ID }, rol);
        }

        
        // PUT: api/Roles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRol(int id, Rol rol)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rol.ID)
            {
                return BadRequest();
            }

            db.Entry(rol).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RolExists(id))
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

        // DELETE: api/Roles/5
        [ResponseType(typeof(Rol))]
        public IHttpActionResult DeleteRol(int id)
        {
            Rol rol = db.Rol.Find(id);
            if (rol == null)
            {
                return NotFound();
            }

            db.Rol.Remove(rol);
            db.SaveChanges();

            return Ok(rol);
        }

        /**Método no usado
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }**/

        /// <summary>
        /// Verifica si el rol existe.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// Devuelve el número de elementos que satisfacen la condición.</returns>
        private bool RolExists(int id)
        {
            return db.Rol.Count(e => e.ID == id) > 0;
        }
    }
}