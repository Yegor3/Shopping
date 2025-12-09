using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Shopping.Platform.Common.Entities;

namespace Shopping.Platform.Repositories.Interface
{
    public interface IProductRepository
    {
        Product Save(Product product);
        Product? GetFirstByOrderId(long orderId);
        Product? GetByIdAndOrderId(long productId, long orderId);
        void Delete(Product product);
        List<Product>? GetByOrderId(long orderId);
    }
}
