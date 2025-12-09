using Shopping.Platform.Service.Models.Requests;
using Shopping.Platform.Service.Models.Results;

namespace Shopping.Platform.Service.Interfaces
{
    public interface IOrderService
    {
        CreateOrderResult CreateOrder(CreateOrderRequest request);
        void CloseOrder(CloseOrderRequest request);
        Task<GetOrderListResult> GetOrderList(GetOrderListRequest request);
        GetOrderDetailsResult GetOrderDetails(GetOrderDetailsRequest request);
    }
}