using Shopping.Domain.Entities;
using Shopping.Domain.Entities.Entities.Enums;

namespace Shopping.API.Models.Requests
{
    public class CreateProductListRequest
    {
        public long OrderId { get; set; }
        public List<ProductCreation> ProductList { get; set; }
    }
}