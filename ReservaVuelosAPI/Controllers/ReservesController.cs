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
    public class ReservesController : ApiController
    {
        private DBEntities db = new DBEntities();

        // GET: api/Reserves
        public IQueryable<Reserve> GetReserve()
        {
            return db.Reserve;
        }

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
        }

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
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReserveExists(int id)
        {
            return db.Reserve.Count(e => e.ID_Flight == id) > 0;
        }
    }
}