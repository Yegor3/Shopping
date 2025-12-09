using Shopping.Platform.Common.Entities;
using Shopping.Platform.Service.Interfaces;
using Shopping.Platform.Service.Models.Requests;
using Shopping.Platform.Service.Models.Results;
using Shopping.Platform.Common.Entities.Enums;
using Shopping.Platform.Repositories.Interface;

namespace Shopping.Platform.Service
{
    public class OrderService: IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public CreateOrderResult CreateOrder(CreateOrderRequest request)
        {
            Order newOrder = new Order
            {
                Status = OrderStatus.Open,
                IsGift = request.IsGift,
                Description = request.Description,
                CreationDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow
            };

            long OrderId = _orderRepository.Save(newOrder);
            return new CreateOrderResult
            {
                OrderId = OrderId,
            };
        }

        public void CloseOrder(CloseOrderRequest request)
        {
            Order? order = _orderRepository.Get(request.OrderId);
            if (order == null || order.Status == OrderStatus.Closed)
                throw new Exception("Order not found");

            Product? product = _productRepository.GetFirstByOrderId(request.OrderId);
            if (product == null)
                throw new Exception("Order does not have any products");

            _orderRepository.Close(order);
        }

        public async Task<GetOrderListResult> GetOrderList(GetOrderListRequest request)
        {
            List<Order>? orderList = await _orderRepository.GetPagedList(request.PageIndex, request.PageSize, request.Status);
            return new GetOrderListResult {OrderList = orderList};
        }

        public GetOrderDetailsResult GetOrderDetails(GetOrderDetailsRequest request)
        {
            Order? order = _orderRepository.Get(request.OrderId);
            if (order == null)
                throw new Exception("Order not found");

            List<Product>? productList = _productRepository.GetByOrderId(request.OrderId);

            return new GetOrderDetailsResult
            {
                Id = order.Id,
                Status = order.Status,
                Description = order.Description,
                IsGift = order.IsGift,
                ProductList = productList
            };
        }
    }
}