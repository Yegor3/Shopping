using Shopping.Platform.Common.Entities;

namespace Shopping.Platform.Service.Models.Results
{
    public class GetOrderListResult
    {
        public List<Order>? OrderList { get; set; }
    }
}