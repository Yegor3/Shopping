using Shopping.Platform.Service.Models.Requests;
using Shopping.Platform.Service.Models.Results;

namespace Shopping.Platform.Service.Interfaces
{
    public interface IProductService
    {
        CreateProductListResult CreateProductList(CreateProductListRequest request);
        void RemoveProductList (RemoveProductRequest request);
    }
}