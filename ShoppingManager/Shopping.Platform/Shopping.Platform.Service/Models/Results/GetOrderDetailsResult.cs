using Shopping.Platform.Common.Entities;
using Shopping.Platform.Common.Entities.Enums;

namespace Shopping.Platform.Service.Models.Results
{
    public class GetOrderDetailsResult
    {
        public long Id { get; set; }
        public OrderStatus Status { get; set; }
        public string? Description { get; set; }
        public bool IsGift { get; set; }
        public List<Product>? ProductList { get; set; }
    }
}