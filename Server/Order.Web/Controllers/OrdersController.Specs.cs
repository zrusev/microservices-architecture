namespace Order.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Order.Data.Models;
    using Services.Contracts;
    using Services.Models;
    using StoreApi.Services.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    public class OrdersControllerSpec
    {
        [Fact]
        public async Task Create_ReturnsBadRequest_GivenInvalidModel()
        {
            // arrange
            var mockService = new Mock<IOrderService>();

            var model = new OrderInputModel();

            mockService.Setup(t => t.AddOrder(model)).Returns(Task.FromResult(new QueryResult()));

            // act
            var controller = new OrdersController(mockService.Object);

            var result = await controller.Create(model);

            // assert
            Assert.IsType<BadRequestObjectResult>(result);
        }


        [Fact]
        public async Task Create_ReturnsOK_GivenValidModel()
        {
            // arrange
            var mockService = new Mock<IOrderService>();

            var model = new OrderInputModel();

            mockService.Setup(t => t.AddOrder(model)).Returns(Task.FromResult(QueryResult.Success));

            // act
            var controller = new OrdersController(mockService.Object);

            var result = await controller.Create(model);

            // assert
            Assert.IsType<OkResult>(result);
        }
    }
}
