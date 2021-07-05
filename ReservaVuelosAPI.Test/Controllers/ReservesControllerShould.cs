using Autofac.Extras.Moq;
using ReservaVuelosAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Autofac;
using Moq;
using ReservaVuelosAPI.Controllers;
using System.Web.Http;
using System.Data.Entity.Infrastructure;
using System.Web.Http.Description;
using System.Web.Http.Results;
using System.Data.Entity.Validation;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace ReservaVuelosAPI.Test.Controllers
{
    public class ReservesControllerShould
    {
        /// <summary>
        /// Metodo para probar si el metodo GetReserve de ReservesController entrega corretamente a todas las reservas
        /// </summary>
        [Fact]
        public void GetReserveShould()
        {
            // Arrange
            DBEntities db = new DBEntities();
            Mock<DBEntities> _db = new Mock<DBEntities>();
            _db.Setup(x => x.Reserve).Returns(db.Reserve);

            ReservesController controller = new ReservesController();
            controller.ReservesControllerTest(_db.Object, 0);

            // Act
            IQueryable<Reserve> expected = db.Reserve;
            IQueryable<Reserve> actual = controller.GetReserve();

            // Assert
            Assert.True(actual != null);
            Assert.Equal(expected.Count(), actual.Count());
        }

        /// <summary>
        /// Metodo para probar si el metodo GetReserve dado un ID de ReservesController entrega correctamente la reserva solicitada
        /// </summary>
        /*[Fact]
        public void GetReserveIdShould()
        {
            // Arrange
            int id = 1;
            DBEntities db = new DBEntities();
            Mock<DBEntities> _db = new Mock<DBEntities>();
            _db.Setup(x => x.Reserve.Find(id)).Returns(db.Reserve.Find(id));

            ReservesController controller = new ReservesController();
            controller.ReservesControllerTest(_db.Object, 0);

            // Act
            IHttpActionResult actionResult = controller.GetReserve(id);
            var contentResult = actionResult as OkNegotiatedContentResult<Reserve>;

            // Assert
            Assert.NotNull(contentResult);
            Assert.NotNull(contentResult.Content);
            Assert.Equal(id, contentResult.Content.ID);
        }*/

        /// <summary>
        /// Metodo para probar si el metodo GetReserve dado un ID de ReservesController es resistente a IDs inexitentes
        /// </summary>
        [Fact]
        public void GetReserveIdShouldNot()
        {
            // Arrange
            int id = 1000;
            Mock<DBEntities> _db = new Mock<DBEntities>();
            ReservesController controller = new ReservesController();
            controller.ReservesControllerTest(_db.Object, 0);

            // Act
            IHttpActionResult actionResult = controller.GetReserve(id);
            var contentResult = actionResult as NotFoundResult;

            // Assert
            Assert.IsType<NotFoundResult>(actionResult);
        }

        /// <summary>
        /// Metodo para probar si el metodo PostReserve de ReservesController ingresa correctamente una reserva a la base de datos
        /// </summary>
        [Fact]
        public void PostReserveShould()
        {
            // Arrange
            Reserve reserve = new Reserve(1, 2, 1);
            DBEntities db = new DBEntities();

            ReservesController controller = new ReservesController();

            // Act
            IHttpActionResult actionResult = controller.PostReserve(reserve);
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<Reserve>;

            /*try
            {
                if (createdResult.RouteName == "DefaultApi")
                {
                    db.Entry(customer).State = EntityState.Deleted;
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {

            }*/

            // Assert
            Assert.NotNull(createdResult);
            Assert.Equal("DefaultApi", createdResult.RouteName);
        }
    }
}
