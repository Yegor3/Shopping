using Moq;
using Xunit;
using Shopping.Platform.Service;
using Shopping.Platform.Service.Models.Requests;
using Shopping.Platform.Repositories.Interface;
using Shopping.Platform.Common.Entities;
using Shopping.Platform.Common.Entities.Enums;

namespace Shopping.Platform.Services.Tests
{
    public class OrderServiceTests
    {
        private readonly Mock<IOrderRepository> _orderRepoMock;
        private readonly Mock<IProductRepository> _productRepoMock;
        private readonly OrderService _service;

        public OrderServiceTests()
        {
            _orderRepoMock = new Mock<IOrderRepository>();
            _productRepoMock = new Mock<IProductRepository>();
            _service = new OrderService(_orderRepoMock.Object, _productRepoMock.Object);
        }

        [Fact]
        public void CloseOrder_ShouldClose_WhenOrderHasProducts()
        {
            long orderId = 10;

            var order = new Order
            {
                Id = orderId,
                Status = OrderStatus.Open
            };

            _orderRepoMock.Setup(r => r.Get(orderId)).Returns(order);

            _productRepoMock.Setup(p => p.GetFirstByOrderId(orderId))
                            .Returns(new Product { Id = 1, OrderId = orderId });

            _service.CloseOrder(new CloseOrderRequest { OrderId = orderId });

            _orderRepoMock.Verify(r => r.Close(order), Times.Once());
        }

        [Fact]
        public void CloseOrder_ShouldThrow_WhenOrderHasNoProducts()
        {
            long orderId = 10;

            var order = new Order
            {
                Id = orderId,
                Status = OrderStatus.Open
            };

            _orderRepoMock.Setup(r => r.Get(orderId)).Returns(order);

            _productRepoMock.Setup(p => p.GetFirstByOrderId(orderId))
                            .Returns((Product?)null);

            var ex = Assert.Throws<Exception>(() =>
                _service.CloseOrder(new CloseOrderRequest { OrderId = orderId })
            );

            Assert.Equal("Order does not have any products", ex.Message);
        }

        [Fact]
        public void CloseOrder_ShouldThrow_WhenOrderDoesNotExist()
        {
            long orderId = 99;

            _orderRepoMock.Setup(r => r.Get(orderId)).Returns((Order?)null);

            var ex = Assert.Throws<Exception>(() =>
                _service.CloseOrder(new CloseOrderRequest { OrderId = orderId })
            );

            Assert.Equal("Order not found", ex.Message);
        }

        [Fact]
        public void CloseOrder_ShouldThrow_WhenOrderIsAlreadyClosed()
        {
            long orderId = 20;

            var order = new Order
            {
                Id = orderId,
                Status = OrderStatus.Closed
            };

            _orderRepoMock.Setup(r => r.Get(orderId)).Returns(order);

            var ex = Assert.Throws<Exception>(() =>
                _service.CloseOrder(new CloseOrderRequest { OrderId = orderId })
            );

            Assert.Equal("Order not found", ex.Message);
        }
    }
}
