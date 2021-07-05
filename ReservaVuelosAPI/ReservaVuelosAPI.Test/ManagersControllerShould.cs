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

namespace ReservaVuelosAPI.ReservaVuelosAPI.Test
{
    public class ManagersControllerShould
    {
        /// <summary>
        /// Metodo para probar si el metodo GetManager de ManagersController entrega corretamente a todos los administradores
        /// </summary>
        [Fact]
        public void GetManagerShould()
        {
            // Arrange
            DBEntities db = new DBEntities();
            Mock<DBEntities> _db = new Mock<DBEntities>();
            _db.Setup(x => x.Manager).Returns(db.Manager);

            ManagersController controller = new ManagersController();
            controller.ManagersControllerTest(_db.Object, 0);

            // Act
            IQueryable<Manager> expected = db.Manager;
            IQueryable<Manager> actual = controller.GetManager();

            // Assert
            Assert.True(actual != null);
            Assert.Equal(expected.Count(), actual.Count());
        }

        /// <summary>
        /// Metodo para probar si el metodo GetManager dado un ID de ManagersController entrega correctamente el administrador solicitado
        /// </summary>
        [Fact]
        public void GetManagerIdShould()
        {
            // Arrange
            int id = 3;
            DBEntities db = new DBEntities();
            Mock<DBEntities> _db = new Mock<DBEntities>();
            _db.Setup(x => x.Manager.Find(id)).Returns(db.Manager.Find(id));

            ManagersController controller = new ManagersController();
            controller.ManagersControllerTest(_db.Object, 0);

            // Act
            IHttpActionResult actionResult = controller.GetManager(id);
            var contentResult = actionResult as OkNegotiatedContentResult<Manager>;

            // Assert
            Assert.NotNull(contentResult);
            Assert.NotNull(contentResult.Content);
            Assert.Equal(id, contentResult.Content.ID);
        }

        /// <summary>
        /// Metodo para probar si el metodo GetManager dado un ID de ManagersController es resistente a IDs inexitentes
        /// </summary>
        [Fact]
        public void GetManagerIdShouldNot()
        {
            // Arrange
            int id = 1000;
            Mock<DBEntities> _db = new Mock<DBEntities>();
            ManagersController controller = new ManagersController();
            controller.ManagersControllerTest(_db.Object, 0);

            // Act
            IHttpActionResult actionResult = controller.GetManager(id);
            var contentResult = actionResult as NotFoundResult;

            // Assert
            Assert.IsType<NotFoundResult>(actionResult);
        }

        /// <summary>
        /// Metodo para probar si el metodo PostManager de ManagersController ingresa correctamente un administrador a la base de datos
        /// </summary>
        [Fact]
        public void PostManagerShould()
        {
            // Arrange
            Manager manager = new Manager("raul", "Castillo", "11286395-0", 100);
            DBEntities db = new DBEntities();

            ManagersController controller = new ManagersController();

            // Act
            IHttpActionResult actionResult = controller.PostManager(manager);
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<Manager>;

            try
            {
                if (createdResult.RouteName == "DefaultApi")
                {
                    db.Entry(manager).State = EntityState.Deleted;
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
        /// Metodo para probar si el metodo PostManager de ManagersController es resistente a datos invalidos
        /// </summary>
        [Fact]
        public void PostManagerShouldNotValidate()
        {
            // Arrange
            Manager manager = new Manager("raul", "Castillo", "1128g395-0", 100);
            DBEntities db = new DBEntities();

            ManagersController controller = new ManagersController();

            // Act
            IHttpActionResult actionResult = controller.PostManager(manager);
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<Manager>;

            try
            {
                if (createdResult.RouteName == "DefaultApi")
                {
                    db.Entry(manager).State = EntityState.Deleted;
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {

            }

            // Assert
            Assert.IsType<BadRequestResult>(actionResult);
        }

        /// <summary>
        /// Metodo para probar si el metodo PostManager de ManagersController es resistente a datos unicos duplicadas
        /// </summary>
        [Fact]
        public void PostManagerShouldNotDuplicate()
        {
            // Arrange
            Manager manager = new Manager("raul", "Castillo", "11286395-0", 3);
            DBEntities db = new DBEntities();

            ManagersController controller = new ManagersController();

            // Act
            IHttpActionResult actionResult = controller.PostManager(manager);
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<Manager>;

            try
            {
                if (createdResult.RouteName == "DefaultApi")
                {
                    db.Entry(manager).State = EntityState.Deleted;
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {

            }

            // Assert
            Assert.IsType<ConflictResult>(actionResult);
        }
    }
}