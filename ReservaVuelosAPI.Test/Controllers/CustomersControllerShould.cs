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
    public class CustomersControllerShould
    {
        /// <summary>
        /// Metodo para probar si el metodo GetCustomer de CustomersController entrega corretamente a todos los clientes
        /// </summary>
        [Fact]
        public void GetCustomerShould()
        {
            // Arrange
            DBEntities db = new DBEntities();
            Mock<DBEntities> _db = new Mock<DBEntities>();
            _db.Setup(x => x.Customer).Returns(db.Customer);

            CustomersController controller = new CustomersController();
            controller.CustomersControllerTest(_db.Object, 0);

            // Act
            IQueryable<Customer> expected = db.Customer;
            IQueryable<Customer> actual = controller.GetCustomer();

            // Assert
            Assert.True(actual != null);
            Assert.Equal(expected.Count(), actual.Count());
        }

        /// <summary>
        /// Metodo para probar si el metodo GetCustomer dado un ID de CustomersController entrega correctamente el cliente solicitado
        /// </summary>
        [Fact]
        public void GetCustomerIdShould()
        {
            // Arrange
            int id = 42;
            DBEntities db = new DBEntities();
            Mock<DBEntities> _db = new Mock<DBEntities>();
            _db.Setup(x => x.Customer.Find(id)).Returns(db.Customer.Find(id));

            CustomersController controller = new CustomersController();
            controller.CustomersControllerTest(_db.Object, 0);

            // Act
            IHttpActionResult actionResult = controller.GetCustomer(id);
            var contentResult = actionResult as OkNegotiatedContentResult<Customer>;

            // Assert
            Assert.NotNull(contentResult);
            Assert.NotNull(contentResult.Content);
            Assert.Equal(id, contentResult.Content.ID);
        }

        /// <summary>
        /// Metodo para probar si el metodo GetCustomer dado un ID de CustomersController es resistente a IDs inexitentes
        /// </summary>
        [Fact]
        public void GetCustomerIdShouldNot()
        {
            // Arrange
            int id = 1000;
            Mock<DBEntities> _db = new Mock<DBEntities>();
            CustomersController controller = new CustomersController();
            controller.CustomersControllerTest(_db.Object, 0);

            // Act
            IHttpActionResult actionResult = controller.GetCustomer(id);

            // Assert
            Assert.IsType<NotFoundResult>(actionResult);
        }

        /// <summary>
        /// Metodo para probar si el metodo PostCustomer de CustomersController ingresa correctamente un cliente a la base de datos
        /// </summary>
        [Fact]
        public void PostCustomerShould()
        {
            // Arrange
            Customer customer = new Customer("raul", "Castillo", "11298395-0", null, "rio bueno", 5654, 940962046, "mi", "papa", 942864046, 48);
            DBEntities db = new DBEntities();

            CustomersController controller = new CustomersController();

            // Act
            IHttpActionResult actionResult = controller.PostCustomer(customer);
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<Customer>;

            try
            {
                if (createdResult.RouteName == "DefaultApi")
                {
                    db.Entry(customer).State = EntityState.Deleted;
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
        /// Metodo para probar si el metodo PostCustomer de CustomersController es resistente a datos invalidos
        /// </summary>
        [Fact]
        public void PostCustomerShouldNotValidate()
        {
            // Arrange
            Customer customer = new Customer("raul", "Castillo", "1129s395-0", null, "rio bueno", 5654, 940962046, "mi", "papa", 942864046, 48);
            DBEntities db = new DBEntities();

            CustomersController controller = new CustomersController();

            // Act
            IHttpActionResult actionResult = controller.PostCustomer(customer);
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<Customer>;

            try
            {
                if (createdResult.RouteName == "DefaultApi")
                {
                    db.Entry(customer).State = EntityState.Deleted;
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {

            }

            //Assert
            Assert.IsType<BadRequestResult>(actionResult);
        }

        /// <summary>
        /// Metodo para probar si el metodo PostCustomer de CustomersController es resistente a datos unicos duplicadas
        /// </summary>
        [Fact]
        public void PostCustomerShouldNotDuplicate()
        {
            // Arrange
            Customer customer = new Customer("raul", "Castillo", "19690420-4", null, "rio bueno", 5654, 940962046, "mi", "papa", 942864046, 1000);
            DBEntities db = new DBEntities();

            CustomersController controller = new CustomersController();

            // Act
            IHttpActionResult actionResult = controller.PostCustomer(customer);
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<Customer>;

            try
            {
                if (createdResult.RouteName == "DefaultApi")
                {
                    db.Entry(customer).State = EntityState.Deleted;
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {

            }

            //Assert
            Assert.IsType<ConflictResult>(actionResult);
        }
    }
}
