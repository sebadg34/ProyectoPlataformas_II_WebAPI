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
    /// Manager controller, está relacionado directamente al modelo Manager.
    /// </summary>
    [EnableCors(origins: "http://localhost:52811", headers: "*", methods: "*")]
    public class ManagersController : ApiController
    {
        //Obtiene la entidad de la base de datos.
        private DBEntities db = new DBEntities();

        /// <summary>
        /// Método que obtiene todos los administradores de la base de datos.
        /// </summary>
        /// <returns>
        /// Retorna todos los administradores existentes en la base de datos.
        /// </returns>
        // GET: api/Managers
        public IQueryable<Manager> GetManager()
        {
            return db.Manager;
        }

        /// <summary>
        /// Método que obtiene a un administrador dada una id en específico.
        /// </summary>
        /// <param name="id">
        ///  id se recibe desde la aplicación web.
        /// </param>
        /// <returns>
        /// Retona la información del administrador si es que lo encuentra.
        /// </returns>
        // GET: api/Managers/5
        [ResponseType(typeof(Manager))]
        public IHttpActionResult GetManager(int id)
        {
            Manager manager = db.Manager.Find(id);
            if (manager == null)
            {
                return NotFound();
            }

            return Ok(manager);
        }
        /// <summary>
        ///  Método que realiza una inserción en la base de datos dado un administrador en específico.
        /// </summary>
        /// <param name="manager">
        ///  manager se recibe desde la aplicación web.
        /// </param>
        /// <returns>
        /// Retorna el estado del método, si se pudo realizar la inserción o no.
        /// </returns>
        // POST: api/Managers
        [ResponseType(typeof(Manager))]
        public IHttpActionResult PostManager(Manager manager)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Manager.Add(manager);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ManagerExists(manager.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = manager.ID }, manager);
        }

        /// <summary>
        /// Verifica si el adminsitrador existe.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// Devuelve el número de elementos que satisfacen la condición.</returns>
        private bool ManagerExists(int id)
        {
            return db.Manager.Count(e => e.ID == id) > 0;
        }

        #region Métodos temporales
        /**Método aún no usado, se verá si se utilizará.
        // PUT: api/Managers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutManager(int id, Manager manager)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != manager.ID)
            {
                return BadRequest();
            }

            db.Entry(manager).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ManagerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }**/

        /**Método aún no usado, se verá si se utilizará.
        // DELETE: api/Managers/5
        [ResponseType(typeof(Manager))]
        public IHttpActionResult DeleteManager(int id)
        {
            Manager manager = db.Manager.Find(id);
            if (manager == null)
            {
                return NotFound();
            }

            db.Manager.Remove(manager);
            db.SaveChanges();

            return Ok(manager);
        }**/

        /**Método aún no usado, se verá si se utilizará.
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }**/
        #endregion
    }
}