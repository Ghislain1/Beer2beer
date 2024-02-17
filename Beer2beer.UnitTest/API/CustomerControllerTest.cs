
namespace Beer2beer.UnitTest.API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Beer2beer.API.Controllers;
using Beer2beer.Core.Entities;
using NUnit.Framework;
using Beer2beer.Core.Interfaces;

public class CustomerControllerTest
{

    private Mock<ICustomerService> customerServiceMock;
    private Mock<ILogger<CustomerController>> loggerMock;
    private CustomerController customerController;

    [SetUp]
    public void Setup()
    {
        this.customerServiceMock = new Mock<ICustomerService>();
        this.loggerMock = new Mock<ILogger<CustomerController>>();
        this.customerController = new CustomerController(loggerMock.Object, this.customerServiceMock.Object);
    }

    [Test]
    public async Task Get_ReturnsViewWithListOfCustomers()
    {
        // Arrange
        var Customers = new List<CustomerViewModel>
            {
                new CustomerViewModel { Id = 1,   FullName = "Customer A", Balance = 9, Email = "Customer@yahoo.fr" },
                new CustomerViewModel { Id = 2,  FullName = "Customer B", Balance = 20, Email = "Customer@yahoo.fr"  }
            };

        this.customerServiceMock.Setup(service => service.GetCustomers())
                           .ReturnsAsync(Customers);

        // Act
        var result = await this.customerController.Get();

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
        var okObjectResult = (OkObjectResult)result;
        Assert.NotNull(okObjectResult);

        var model = (IEnumerable<CustomerViewModel>)okObjectResult.Value;
        Assert.NotNull(model);
        Assert.That(model.Count(), Is.EqualTo(Customers.Count));

    }
}

