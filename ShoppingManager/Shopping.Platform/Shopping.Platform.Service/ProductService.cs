using Shopping.Platform.Common.Entities;
using Shopping.Platform.Service.Interfaces;
using Shopping.Platform.Service.Models.Requests;
using Shopping.Platform.Service.Models.Results;
using Shopping.Platform.Common.Entities.Enums;
using Shopping.Platform.Repositories.Interface;

namespace Shopping.Platform.Service
{
    public class ProductService: IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public ProductService(IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        public CreateProductListResult CreateProductList(CreateProductListRequest request)
        {
            Order? order = _orderRepository.Get(request.OrderId);

            if (order == null || order.Status == OrderStatus.Closed || order.DeletionDate != null)
                throw new Exception("Order could not be accessed");

            CreateProductListResult result = new CreateProductListResult();
            result.Products = new List<Product>();

            foreach (ProductCreation x in request.ProductList)
            {
                Product product = new Product
                {
                    OrderId = request.OrderId,
                    Price = x.Price,
                    Type = x.Type,
                    Description = x.Description,
                    CreationDate = DateTime.UtcNow,
                    UpdateDate = DateTime.UtcNow
                };

                result.Products.Add(_productRepository.Save(product));
            }

            return result;
        }

        public void RemoveProductList(RemoveProductRequest request)
        {
            Order? order = _orderRepository.Get(request.OrderId);

            if (order == null || order.Status == OrderStatus.Closed || order.DeletionDate != null)
                throw new Exception("Order could not be accessed");

            if(request.ProductIds != null)
            {
                foreach (long x in request.ProductIds)
                {
                    Product? product = _productRepository.GetByIdAndOrderId(x, request.OrderId);

                    if (product != null && product.DeletionDate == null)
                        _productRepository.Delete(product);
                }
            }
        }
    }
}