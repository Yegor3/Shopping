using Xunit;
using Moq;
using Shopping.Platform.Service.Models.Requests;
using Shopping.Platform.Common.Entities;
using Shopping.Platform.Common.Entities.Enums;
using Shopping.Platform.Repositories.Interface;

namespace Shopping.Platform.Service.Tests
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly ProductService _service;

        public ProductServiceTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _orderRepositoryMock = new Mock<IOrderRepository>();

            _service = new ProductService(
                _productRepositoryMock.Object,
                _orderRepositoryMock.Object
            );
        }

        [Fact]
        public void CreateProductList_ShouldThrow_WhenOrderIsClosed()
        {
            var request = new CreateProductListRequest
            {
                OrderId = 10,
                ProductList = new List<ProductCreation>
                {
                    new ProductCreation { Price = 100, Description = "Test", Type = ProductType.ComputerHardware }
                }
            };

            _orderRepositoryMock
                .Setup(o => o.Get(10))
                .Returns(new Order
                {
                    Id = 10,
                    Status = OrderStatus.Closed
                });

            var ex = Assert.Throws<Exception>(() => _service.CreateProductList(request));
            Assert.Equal("Order could not be accessed", ex.Message);

            _productRepositoryMock.Verify(p => p.Save(It.IsAny<Product>()), Times.Never);
        }

        [Fact]
        public void RemoveProductList_ShouldThrow_WhenOrderIsClosed()
        {
            var request = new RemoveProductRequest
            {
                OrderId = 10,
                ProductIds = new List<long> { 5 }
            };

            _orderRepositoryMock
                .Setup(o => o.Get(10))
                .Returns(new Order
                {
                    Id = 10,
                    Status = OrderStatus.Closed
                });

            var ex = Assert.Throws<Exception>(() => _service.RemoveProductList(request));
            Assert.Equal("Order could not be accessed", ex.Message);

            _productRepositoryMock.Verify(p => p.Delete(It.IsAny<Product>()), Times.Never);
        }

        [Fact]
        public void CreateProductList_ShouldAddProducts_WhenOrderIsOpen()
        {
            var request = new CreateProductListRequest
            {
                OrderId = 1,
                ProductList = new List<ProductCreation>
                {
                    new ProductCreation { Price = 50, Description = "Item A", Type = ProductType.AutomotivePartsAndAccessories },
                    new ProductCreation { Price = 20, Description = "Item B", Type = ProductType.Game }
                }
            };

            _orderRepositoryMock
                .Setup(o => o.Get(1))
                .Returns(new Order
                {
                    Id = 1,
                    Status = OrderStatus.Open
                });

            _productRepositoryMock
                .Setup(p => p.Save(It.IsAny<Product>()))
                .Returns((Product p) => p);

            var result = _service.CreateProductList(request);

            Assert.Equal(2, result.Products.Count);
            _productRepositoryMock.Verify(p => p.Save(It.IsAny<Product>()), Times.Exactly(2));
        }

        [Fact]
        public void RemoveProductList_ShouldDeleteProducts_WhenOrderIsOpen()
        {
            var request = new RemoveProductRequest
            {
                OrderId = 1,
                ProductIds = new List<long> { 5 }
            };

            _orderRepositoryMock
                .Setup(o => o.Get(1))
                .Returns(new Order
                {
                    Id = 1,
                    Status = OrderStatus.Open
                });

            _productRepositoryMock
                .Setup(p => p.GetByIdAndOrderId(5, 1))
                .Returns(new Product { Id = 5, OrderId = 1, DeletionDate = null });

            _service.RemoveProductList(request);

            _productRepositoryMock.Verify(p => p.Delete(It.IsAny<Product>()), Times.Once);
        }
    }
}

