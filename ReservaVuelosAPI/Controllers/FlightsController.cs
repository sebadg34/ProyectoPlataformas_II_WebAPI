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
    /// Flight controller, está relacionado directamente al modelo Flight.
    /// </summary>
    [EnableCors(origins: "http://localhost:52811", headers: "*", methods: "*")]
    public class FlightsController : ApiController
    {
        //Obtiene la entidad de la base de datos.
        private DBEntities db = new DBEntities();

        /// <summary>
        /// Método que obtiene todos los vuelos de la base de datos.
        /// </summary>
        /// <returns>
        ///  Retorna todos los vuelos existentes en la base de datos.
        /// </returns>
        // GET: api/Flights
        public IQueryable<Flight> GetFlight()
        {
            return db.Flight;
        }

        /// <summary>
        /// Método que obtiene a un vuelo dada una id en específico.
        /// </summary>
        /// <param name="id">
        /// id se recibe desde la aplicación web.
        /// </param>
        /// <returns>
        /// Retona la información del vuelo si es que lo encuentra.
        /// </returns>
        // GET: api/Flights/5
        [ResponseType(typeof(Flight))]
        public IHttpActionResult GetFlight(int id)
        {
            Flight flight = db.Flight.Find(id);
            if (flight == null)
            {
                return NotFound();
            }

            return Ok(flight);
        }

        /// <summary>
        ///  Método que realiza una inserción en la base de datos dado un vuelo en específico
        /// </summary>
        /// <param name="flight">
        /// flight se recibe desde la aplicación web.
        /// </param>
        /// <returns>
        /// Retorna el estado del método, si se pudo realizar la inserción o no.
        /// </returns>
        // POST: api/Flights
        [ResponseType(typeof(Flight))]
        public IHttpActionResult PostFlight(Flight flight)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Flight.Add(flight);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = flight.ID }, flight);
        }

        /// <summary>
        /// Verifica si el vuelo existe.
        /// </summary>
        /// <param name="id">
        /// id se recibe desde la aplicación web.
        /// </param>
        /// <returns>
        /// Devuelve el número de elementos que satisfacen la condición.</returns>
        private bool FlightExists(int id)
        {
            return db.Flight.Count(e => e.ID == id) > 0;
        }
        #region Métodos temporales
        /**Método aún no usado, se verá si se utilizará.
        // PUT: api/Flights/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFlight(int id, Flight flight)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != flight.ID)
            {
                return BadRequest();
            }

            db.Entry(flight).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FlightExists(id))
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
        // DELETE: api/Flights/5
        [ResponseType(typeof(Flight))]
        public IHttpActionResult DeleteFlight(int id)
        {
            Flight flight = db.Flight.Find(id);
            if (flight == null)
            {
                return NotFound();
            }

            db.Flight.Remove(flight);
            db.SaveChanges();

            return Ok(flight);
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