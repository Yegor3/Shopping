using Shopping.Platform.Common.Entities.Enums;

namespace Shopping.Platform.Common.Entities
{
    public class ProductCreation
    {
        public decimal Price { get; set;}
        public ProductType Type { get; set; }
        public string Description { get; set; }
    }
}