using Shopping.Platform.Common.Entities;

namespace Shopping.Platform.Service.Models.Requests
{
    public class CreateProductListRequest
    {
        public long OrderId { get; set; }
        public List<ProductCreation> ProductList { get; set; }
    }
}