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
    public class UsersControllerShould
    {
        /// <summary>
        /// Metodo para probar si el metodo GetUser de UsersController entrega corretamente a todos los usuarios
        /// </summary>
        [Fact]
        public void GetUserShould()
        {
            // Arrange
            DBEntities db = new DBEntities();
            Mock<DBEntities> _db = new Mock<DBEntities>();
            _db.Setup(x => x.User).Returns(db.User);

            UsersController controller = new UsersController();
            controller.UsersControllerTest(_db.Object, 0);

            // Act
            IQueryable<Rol> expected = db.User;
            IQueryable<Rol> actual = controller.GetUser();

            // Assert
            Assert.True(actual != null);
            Assert.Equal(expected.Count(), actual.Count());
        }

        /// <summary>
        /// Metodo para probar si el metodo GetRol dado un Email de RolesController entrega correctamente el usuario solicitado
        /// </summary>
        [Fact]
        public void GetUserIdShould()
        {
            // Arrange
            string email = "juan@gmail.com";
            DBEntities db = new DBEntities();

            UsersController controller = new UsersController();

            // Act
            IHttpActionResult actionResult = controller.GetUser(email);
            var contentResult = actionResult as OkNegotiatedContentResult<User>;

            // Assert
            Assert.NotNull(contentResult);
            Assert.NotNull(contentResult.Content);
            Assert.Equal(email, contentResult.Content.Email);
        }

        /// <summary>
        /// Metodo para probar si el metodo GetCustomer dado un ID de CustomersController es resistente a IDs inexitentes
        /// </summary>
        [Fact]
        public void GetUserIdShouldNot()
        {
            // Arrange
            string email = "imposible@gmail.com";
            DBEntities db = new DBEntities();

            UsersController controller = new UsersController();

            // Act
            IHttpActionResult actionResult = controller.GetUser(email);

            // Assert
            Assert.IsType<NotFoundResult>(actionResult);
        }

        /// <summary>
        /// Metodo para probar si el metodo PostRol de RolesController ingresa correctamente un usuario a la base de datos
        /// </summary>
        [Fact]
        public void PostUserShould()
        {
            // Arrange
            User user = new User("raulaaaaaaa@hotmail.com", "Castillo");
            DBEntities db = new DBEntities();

            UsersController controller = new UsersController();

            // Act
            IHttpActionResult actionResult = controller.PostRol(rol);
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<Rol>;

            try
            {
                if (createdResult.RouteName == "DefaultApi")
                {
                    db.Entry(rol).State = EntityState.Deleted;
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
        /// Metodo para probar si el metodo PostRol de RolesController es resistente a datos invalidos
        /// </summary>
        [Fact]
        public void PostRolShouldNotValidate()
        {
            // Arrange
            Rol rol = new Rol("raulhotmail.com", "Castillo");
            DBEntities db = new DBEntities();

            UsersController controller = new UsersController();

            // Act
            IHttpActionResult actionResult = controller.PostRol(rol);
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<Rol>;

            try
            {
                if (createdResult.RouteName == "DefaultApi")
                {
                    db.Entry(rol).State = EntityState.Deleted;
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {

            }

            // Assert
            Assert.IsType<NotFoundResult>(actionResult);
        }
    }
}
