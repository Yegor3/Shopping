using Shopping.Api.Models.Results;
using Shopping.API.Models.Requests;
using ServiceRequestModel = Shopping.Platform.Service.Models.Requests;
using ServiceResultModel = Shopping.Platform.Service.Models.Results;

namespace Shopping.API.Mapping
{
    public class OrderMapper
    {
        public ServiceRequestModel.CreateOrderRequest Map(CreateOrderRequest request)
        {
            return new ServiceRequestModel.CreateOrderRequest
            {
                IsGift = request.IsGift,
                Description = request.Description
            };
        }

        public ServiceResultModel.CreateOrderResult Map(CreateOrderResult request)
        {
            return new ServiceResultModel.CreateOrderResult
            {
                OrderId = request.OrderId
            };
        }

        public ServiceRequestModel.CloseOrderRequest Map(CloseOrderRequest request)
        {
            return new ServiceRequestModel.CloseOrderRequest
            {
                OrderId = request.OrderId
            };
        }

        public ServiceRequestModel.GetOrderListRequest Map(GetOrderListRequest request)
        {
            return new ServiceRequestModel.GetOrderListRequest
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Status = request.Status,
            };
        }

        public ServiceRequestModel.GetOrderDetailsRequest Map(GetOrderDetailsRequest request)
        {
            return new ServiceRequestModel.GetOrderDetailsRequest
            {
                OrderId = request.OrderId
            };
        }
    }
}