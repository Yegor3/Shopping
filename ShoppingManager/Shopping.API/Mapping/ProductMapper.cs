using Shopping.API.Models.Requests;
using ServiceRequestModel = Shopping.Platform.Service.Models.Requests;
using ServiceResultModel = Shopping.Platform.Service.Models.Results;

namespace Shopping.API.Mapping
{
    public class ProductMapper
    {
        public ServiceRequestModel.CreateProductListRequest Map(CreateProductListRequest request)
        {
            return new ServiceRequestModel.CreateProductListRequest
            {
                OrderId = request.OrderId,
                ProductList = request.ProductList
            };
        }

        public ServiceRequestModel.RemoveProductRequest Map(RemoveProductRequest request)
        {
            return new ServiceRequestModel.RemoveProductRequest
            {
                OrderId = request.OrderId,
                ProductIds = request.ProductIds
            };
        }
    }
}