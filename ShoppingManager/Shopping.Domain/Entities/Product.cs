using Shopping.Domain.Common.Entities.Enums;

namespace Shopping.Domain.Common.Entities
{
    public class Product : BaseEntity
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public decimal Price { get; set; }
        public ProductType Type { get; set; }
        public string Description { get; set; }
    }
}