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
    /// Customer controller, esta relacionado directamente al modelo Customer.
    /// </summary>
    [EnableCors(origins: "http://localhost:52811", headers: "*", methods: "*")]
    public class CustomersController : ApiController
    {
        // Obtiene la entidad de la base de datos.
        private DBEntities db = new DBEntities();

        /// <summary>
        /// Metodo que obtiene todos los clientes de la base de datos.
        /// </summary>
        /// <returns>
        /// Retorna todos los clientes existentes en la base de datos.
        /// </returns>
        // GET: api/Customers
        public IQueryable<Customer> GetCustomer()
        {
            return db.Customer;
        }

        /// <summary>
        /// Metodo que obtiene a un cliente dada una id en específico.
        /// </summary>
        /// <param name="id">
        /// id es recibido desde la aplicacion web.
        /// </param>
        /// <returns>
        /// Retona la información del cliente si es que lo encuentra.
        /// </returns>
        // GET: api/Customers/5
        [ResponseType(typeof(Customer))]
        public IHttpActionResult GetCustomer(int id)
        {
            Customer customer = db.Customer.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        /// <summary>
        /// Metodo que realiza una inserción en la base de datos dado un cliente en especifico.
        /// </summary>
        /// <param name="customer">
        /// Customer es recibido desde la aplicacion web.
        /// </param>
        /// <returns>
        ///  Retorna el estado del metodo, si se pudo realizar la insercion o no.
        /// </returns>
        // POST: api/Customers
        [ResponseType(typeof(Customer))]
        public IHttpActionResult PostCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Customer.Add(customer);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CustomerExists(customer.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = customer.ID }, customer);
        }
      
        /// <summary>
        /// Verifica si el cliente existe.
        /// </summary>
        /// <param name="id">
        /// id se recibe desde la aplicacion web.
        /// </param>
        /// <returns>
        /// Devuelve el numero de elementos que satisfacen la condicion.</returns>
        private bool CustomerExists(int id)
        {
            return db.Customer.Count(e => e.ID == id) > 0;
        }

        #region Metodos temporales
        /** Metodo aun no usado, se vera si se utilizara.
       // PUT: api/Customers/5
       [ResponseType(typeof(void))]
       public IHttpActionResult PutCustomer(int id, Customer customer)
       {
           if (!ModelState.IsValid)
           {
               return BadRequest(ModelState);
           }

           if (id != customer.ID)
           {
               return BadRequest();
           }

           db.Entry(customer).State = EntityState.Modified;

           try
           {
               db.SaveChanges();
           }
           catch (DbUpdateConcurrencyException)
           {
               if (!CustomerExists(id))
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
        // DELETE: api/Customers/5
        [ResponseType(typeof(Customer))]
        public IHttpActionResult DeleteCustomer(int id)
        {
            Customer customer = db.Customer.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            db.Customer.Remove(customer);
            db.SaveChanges();

            return Ok(customer);
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