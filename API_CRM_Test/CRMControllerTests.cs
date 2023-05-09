using API_CRM.Context;
using API_CRM.Class;
using API_CRM.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_ERP.Class;
using Microsoft.AspNetCore.Mvc;

namespace API_CRM_Test
{
    public class CRMControllerTests
    {
        private CRMControllerTests _contextMock = new CRMControllerTests();
        //[Fact]
        //public async Task GetAllCustomers_ReturnsListCustomers()
        //{
        //    // Arrange 
        //    ClientsController controller = new ClientsController(_contextMock);

        //    // Act
        //    var result = await controller.GetAllCustomers();

        //    // Assert 
        //    var actionResult = Assert.IsType<OkObjectResult>(result);
        //    var returnValue = Assert.IsType<List<Customer>>(actionResult.Value);
        //    Assert.NotNull(returnValue);
        //}
        //[Fact]
        //public async Task GetCustomer_ReturnsCustomer()
        //{
        //    // Arrange
        //    ClientsController controller = new ClientsController(_contextMock);
        //    Customer customerResult = _contextMock.customers.FirstOrDefault();

        //    // Act
        //    IActionResult result = await controller.GetCustomer(customerResult.Id);

        //    // Assert
        //    Assert.IsType<OkObjectResult>(result);
        //    OkObjectResult objectResult = (OkObjectResult)result;
        //    Assert.IsType<Customer>(objectResult.Value);
        //    Customer customer = (Customer)objectResult.Value;
        //    Assert.Equal(customerResult.Id, customer.Id);
        //    Assert.Equal(customerResult.Name, customer.Name);
        //}
        //[Fact]
        //public async Task AddCustomer_ReturnsCreatedResult()
        //{
        //    // Arrange
        //    ClientsController controller = new ClientsController(_contextMock);
        //    Customer customer = new Customer
        //    {
        //        Name = "Test Customer",
        //        CreatedAt = DateTime.Now,
        //        Address = new Address { PostalCode = "12345", City = "Test City" }
        //    };

        //    // Act
        //    IActionResult result = await controller.AddCustomer(customer);

        //    // Assert
        //    Assert.IsType<OkObjectResult>(result);
        //    OkObjectResult createdResult = (OkObjectResult)result;
        //    Assert.IsType<Customer>(createdResult.Value);
        //    Customer createdCustomer = (Customer)createdResult.Value;
        //    Assert.Equal(customer.Name, createdCustomer.Name);
        //    Assert.Equal(customer.CreatedAt, createdCustomer.CreatedAt);
        //    Assert.Equal(customer.Address.PostalCode, createdCustomer.Address.PostalCode);
        //    Assert.Equal(customer.Address.City, createdCustomer.Address.City);
        //}
        //[Fact]
        //public async Task UpdateCustomer_ModelIsValid()
        //{
        //    // Arrange
        //    ClientsController controller = new ClientsController(_contextMock);
        //    Customer customerResult = _contextMock.customers.FirstOrDefault();
        //    Customer customerUpdated = new Customer
        //    {
        //        Id = customerResult.Id,
        //        Name = "Updated Name",
        //        CreatedAt = customerResult.CreatedAt,
        //        Address = customerResult.Address
        //    };

        //    // Act
        //    IActionResult result = await controller.UpdateCustomer(customerUpdated);

        //    // Assert
        //    Assert.IsType<OkObjectResult>(result);
        //    OkObjectResult updatedResult = (OkObjectResult)result;
        //    Customer updatedCustomer = (Customer)updatedResult.Value;
        //    Assert.Equal(customerUpdated.Id, updatedCustomer.Id);
        //    Assert.Equal(customerUpdated.Name, updatedCustomer.Name);
        //}
        //[Fact]
        //public async Task DeleteCustomer_ReturnsOkResult()
        //{
        //    // Arrange
        //    ClientsController controller = new ClientsController(_contextMock);
        //    Customer customerToDelete = _contextMock.customers.FirstOrDefault();
        //    int customerIdToDelete = customerToDelete.Id;

        //    // Act
        //    IActionResult result = await controller.DeleteCustomer(customerIdToDelete);

        //    // Assert
        //    Assert.IsType<OkObjectResult>(result);
        //}

    }
}
