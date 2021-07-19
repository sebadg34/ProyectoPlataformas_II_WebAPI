using ReservaVuelosAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ReservaVuelosAPI.Controllers;
using System.Web.Http;
using System.Data.Entity.Infrastructure;
using System.Web.Http.Description;
using System.Web.Http.Results;
using System.Data.Entity.Validation;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace ReservaVuelosAPI.ReservaVuelosAPI.Test
{
    public class FlightsControllerShould
    {
        /// <summary>
        /// Metodo para probar si el metodo GetFlight de FlightsController entrega corretamente a todos los vuelos
        /// </summary>
        [Fact]
        public void GetFlightShould()
        {
            // Arrange
            DBEntities db = new DBEntities();

            FlightsController controller = new FlightsController();

            // Act
            IQueryable<Flight> expected = db.Flight;
            IQueryable<Flight> actual = controller.GetFlight();

            // Assert
            Assert.True(actual != null);
            Assert.Equal(expected.Count(), actual.Count());
        }

        /// <summary>
        /// Metodo para probar si el metodo GetFlight dado un ID de FlightsController entrega correctamente el vuelo solicitado
        /// </summary>
        [Fact]
        public void GetFlightIdShould()
        {
            // Arrange
            int id = 1;

            FlightsController controller = new FlightsController();

            // Act
            IHttpActionResult actionResult = controller.GetFlight(id);
            var contentResult = actionResult as OkNegotiatedContentResult<Flight>;

            // Assert
            Assert.NotNull(contentResult);
            Assert.NotNull(contentResult.Content);
            Assert.Equal(id, contentResult.Content.ID);
        }

        /// <summary>
        /// Metodo para probar si el metodo GetFlight dado un ID de FlightsController es resistente a IDs inexitentes
        /// </summary>
        [Fact]
        public void GetFlightIdShouldNot()
        {
            // Arrange
            int id = 1000;
            FlightsController controller = new FlightsController();

            // Act
            IHttpActionResult actionResult = controller.GetFlight(id);
            var contentResult = actionResult as NotFoundResult;

            // Assert
            Assert.IsType<NotFoundResult>(actionResult);
        }

        /// <summary>
        /// Metodo para probar si el metodo GetLastFlight de FlightsController entrega el ultimo vuelo ingresado a la base de datos
        /// </summary>
        [Fact]
        public void GetLastFlightShould()
        {
            // Arrange
            DBEntities db = new DBEntities();

            FlightsController controller = new FlightsController();

            // Act
            IHttpActionResult actionResult = controller.GetLastFlight();
            var contentResult = actionResult as OkNegotiatedContentResult<Flight>;
            int expected = db.Flight.Max(Flight => Flight.ID);

            // Assert
            Assert.NotNull(contentResult);
            Assert.NotNull(contentResult.Content);
            Assert.Equal(expected, contentResult.Content.ID);
        }

        /// <summary>
        /// Metodo para probar si el metodo PostFlight de FlightsController ingresa correctamente un vuelo a la base de datos
        /// </summary>
        [Fact]
        public void PostFlightShould()
        {
            // Arrange
            Flight flight = new Flight("Premium", "Santiago", "Antofagasta", 40, 100, "101", new DateTime(2021, 07, 20, 20, 00, 00), new DateTime(2021, 07, 20, 23, 00, 00));
            DBEntities db = new DBEntities();

            FlightsController controller = new FlightsController();

            // Act
            IHttpActionResult actionResult = controller.PostFlight(flight);
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<Flight>;

            try
            {
                if (createdResult.RouteName == "DefaultApi")
                {
                    db.Entry(flight).State = EntityState.Deleted;
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {

            }

            // Assert
            Assert.NotNull(createdResult);
            Assert.Equal("DefaultApi", createdResult.RouteName);
        }

        /// <summary>
        /// Metodo para probar si el metodo PostFlight de FlightsController es resistente a datos invalidos
        /// </summary>
        [Fact]
        public void PostFlightShouldNotValidate()
        {
            // Arrange
            Flight flight = new Flight(null, "Santiago", "Antofagasta", 40, 100, "101", new DateTime(2021, 07, 20, 20, 00, 00), new DateTime(2021, 07, 20, 23, 00, 00));
            DBEntities db = new DBEntities();

            FlightsController controller = new FlightsController();

            // Act
            IHttpActionResult actionResult = controller.PostFlight(flight);
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<Flight>;

            try
            {
                if (createdResult.RouteName == "DefaultApi")
                {
                    db.Entry(flight).State = EntityState.Deleted;
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {

            }

            // Assert
            Assert.IsType<BadRequestResult>(actionResult);
        }
    }
}