using Shopping.Domain.Common.Entities.Enums;

namespace Shopping.Domain.Common.Entities
{
    public class Order : BaseEntity
    {
        public long Id { get; set; }
        public OrderStatus Status { get; set; }
        public string? Description { get; set; }
        public bool IsGift { get; set; }
    }
}