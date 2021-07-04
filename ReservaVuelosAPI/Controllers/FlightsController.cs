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
    /// Flight controller, esta relacionado directamente al modelo Flight.
    /// </summary>
    public class FlightsController : ApiController
    {
        // Obtiene la entidad de la base de datos.
        private DBEntities db = new DBEntities();

        /// <summary>
        /// Metodo que obtiene todos los vuelos de la base de datos.
        /// </summary>
        /// <returns>
        /// Retorna todos los vuelos existentes en la base de datos.
        /// </returns>
        // GET: api/Flights
        public IQueryable<Flight> GetFlight()
        {
            return db.Flight;
        }

        /// <summary>
        /// Metodo que obtiene a un vuelo dada una id en especifico.
        /// </summary>
        /// <param name="id">
        /// id se recibe desde la aplicacion web, si es -1 significa que quiere buscar el ultimo registro de vuelo
        /// </param>
        /// <returns>
        /// Retona la informacion del vuelo si es que lo encuentra.
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
        /// Metodo que obtiene el Ãºltimo vuelo.
        /// </summary>
        /// <returns>
        /// Retorna el ultimo vuelo.
        /// </returns>
        [Route("api/lastflight")]
        public IHttpActionResult GetLastFlight()
        {
            var maxVuelo = db.Flight.Max(Flight => Flight.ID);
            Flight flight = db.Flight.Find(maxVuelo);
            if (flight == null)
            {
                return NotFound();
            }
            return Ok(flight);
        }

        /// <summary>
        ///  Metodo que realiza una insercion en la base de datos dado un vuelo en especifico
        /// </summary>
        /// <param name="flight">
        /// flight se recibe desde la aplicacion web.
        /// </param>
        /// <returns>
        /// Retorna el estado del metodo, si se pudo realizar la insercion o no.
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
        /// Metodo que elimina un vuelo de la BD.
        /// </summary>
        /// <param name="id">
        /// Recibe el id desde la aplicacion web.
        /// </param>
        /// <returns>
        /// Retorna el estado: si se pudo eliminar o surgio algun problema.
        /// </returns>
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
        }

        /// <summary>
        /// Verifica si el vuelo existe.
        /// </summary>
        /// <param name="id">
        /// id se recibe desde la aplicacion web.
        /// </param>
        /// <returns>
        /// Devuelve el numero de elementos que satisfacen la condicion.
        /// </returns>
        private bool FlightExists(int id)
        {
            return db.Flight.Count(e => e.ID == id) > 0;
        }

        #region Metodos temporales
        /** Metodo aun no usado, se vera si se utilizara.
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