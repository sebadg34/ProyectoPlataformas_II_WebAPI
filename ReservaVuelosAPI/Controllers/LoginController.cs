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
    [EnableCors(origins: "http://localhost:52811", headers: "*", methods: "*")]
    public class LoginController : ApiController
    {
        private DBEntities db = new DBEntities();

        // GET: api/Login
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Login/5
        public string Get(int id)
        {
            return "value";
        }



        // POST: api/Login
        [ResponseType(typeof(Rol))]
        public IHttpActionResult Post([FromBody]Rol rol)
        {

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(rol.Contrasenia);
            if (rol.Email == null || rol.Contrasenia == null)
                return BadRequest();

            Rol registeredRol = db.Rol.SingleOrDefault(Rol => Rol.Email == rol.Email);

            if (registeredRol == null)
            {
                return NotFound();
            }

            bool verified = BCrypt.Net.BCrypt.Verify(rol.Contrasenia, registeredRol.Contrasenia);
            if (verified == false)
                return NotFound();

            return Ok();

        }

        // PUT: api/Login/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Login/5
        public void Delete(int id)
        {
        }
    }
}
