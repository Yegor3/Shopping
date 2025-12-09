using Shopping.Platform.Common.Entities.Enums;

namespace Shopping.Platform.Common.Entities
{
    public class Order : BaseEntity
    {
        public long Id { get; set; }
        public OrderStatus Status { get; set; }
        public string? Description { get; set; }
        public bool IsGift { get; set; }
    }
}