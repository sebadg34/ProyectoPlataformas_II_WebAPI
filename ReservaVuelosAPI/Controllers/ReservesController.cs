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
    /// Reserve controller, está relacionado directamente al modelo Reserve.
    /// </summary>
    [EnableCors(origins: "http://localhost:52811", headers: "*", methods: "*")]
    public class ReservesController : ApiController
    {
        //Obtiene la entidad de la base de datos.
        private DBEntities db = new DBEntities();

        /// <summary>
        /// Método que obtiene todas las reservas en la base de datos.
        /// </summary>
        /// <returns>
        /// Retorna todas las filas de reservas existentes en la base de datos.</returns>
        // GET: api/Reserves
        public IQueryable<Reserve> GetReserve()
        {
            return db.Reserve;
        }

        /// <summary>
        /// Método que obtiene una reserva dado un id en específico.
        /// </summary>
        /// <param name="id">
        ///  id se recibe desde la aplicación web.
        /// </param>
        /// <returns>
        /// Retona la información de la reserva si es que lo encuentra.
        /// </returns>
        // GET: api/Reserves/5
        [ResponseType(typeof(Reserve))]
        public IHttpActionResult GetReserve(int id)
        {
            Reserve reserve = db.Reserve.Find(id);
            if (reserve == null)
            {
                return NotFound();
            }

            return Ok(reserve);
        }

        /// <summary>
        /// Método que realiza una inserción en la base de datos dada una reserva en específico.
        /// </summary>
        /// <param name="reserve">
        ///  reserve se recibe desde la aplicación web.
        ///  </param>
        /// <returns>
        /// Retorna el estado del método, si se pudo realizar la inserción o no.
        /// </returns>
        // POST: api/Reserves
        [ResponseType(typeof(Reserve))]
        public IHttpActionResult PostReserve(Reserve reserve)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Reserve.Add(reserve);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ReserveExists(reserve.ID_Flight))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = reserve.ID_Flight }, reserve);
        }

        /// <summary>
        /// Verifica si la reserva existe.
        /// </summary>
        /// <param name="id">
        ///  id se recibe desde la aplicación web.
        /// </param>
        /// <returns>
        /// Devuelve el número de elementos que satisfacen la condición.</returns>
        private bool ReserveExists(int id)
        {
            return db.Reserve.Count(e => e.ID_Flight == id) > 0;
        }

        #region Métodos temporales
        /**Método aún no usado, se verá si se utilizará.
        // PUT: api/Reserves/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutReserve(int id, Reserve reserve)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reserve.ID_Flight)
            {
                return BadRequest();
            }

            db.Entry(reserve).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReserveExists(id))
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
        // DELETE: api/Reserves/5
        [ResponseType(typeof(Reserve))]
        public IHttpActionResult DeleteReserve(int id)
        {
            Reserve reserve = db.Reserve.Find(id);
            if (reserve == null)
            {
                return NotFound();
            }

            db.Reserve.Remove(reserve);
            db.SaveChanges();

            return Ok(reserve);
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