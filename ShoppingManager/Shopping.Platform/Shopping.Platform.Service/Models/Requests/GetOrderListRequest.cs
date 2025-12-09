using Shopping.Platform.Common.Entities.Enums;

namespace Shopping.Platform.Service.Models.Requests
{
    public class GetOrderListRequest
    {
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
        public OrderStatus? Status { get; set;}
    }
}