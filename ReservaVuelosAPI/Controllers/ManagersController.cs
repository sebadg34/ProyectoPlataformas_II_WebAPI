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
    /// Manager controller, esta relacionado directamente al modelo Manager.
    /// </summary>
    [EnableCors(origins: "http://localhost:52811", headers: "*", methods: "*")]
    public class ManagersController : ApiController
    {
        // Obtiene la entidad de la base de datos.
        private DBEntities db = new DBEntities();

        /// <summary>
        /// Metodo que obtiene todos los administradores de la base de datos.
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
        /// Metodo que obtiene a un administrador dada una id en especifico.
        /// </summary>
        /// <param name="id">
        ///  id se recibe desde la aplicacion web.
        /// </param>
        /// <returns>
        /// Retona la informacion del administrador si es que lo encuentra.
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
        ///  Metodo que realiza una inserción en la base de datos dado un administrador en especifico.
        /// </summary>
        /// <param name="manager">
        ///  manager se recibe desde la aplicacion web.
        /// </param>
        /// <returns>
        /// Retorna el estado del metodo, si se pudo realizar la insercion o no.
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
        /// Devuelve el numero de elementos que satisfacen la condicion.</returns>
        private bool ManagerExists(int id)
        {
            return db.Manager.Count(e => e.ID == id) > 0;
        }

        #region Metodos temporales
        /** Metodo aun no usado, se vera si se utilizara.
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
        } **/

        /** Metodo aun no usado, se vera si se utilizara.
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
        } **/

        /** Metodo aun no usado, se vera si se utilizara.
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        } **/
        #endregion
    }
}