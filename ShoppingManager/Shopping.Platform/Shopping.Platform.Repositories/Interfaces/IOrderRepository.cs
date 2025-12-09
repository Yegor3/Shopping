using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Shopping.Platform.Common.Entities;
using Shopping.Platform.Common.Entities.Enums;

namespace Shopping.Platform.Repositories.Interface
{
    public interface IOrderRepository
    {
        long Save(Order order);
        void Close(Order order);
        Order? Get(long orderId);
        Task<List<Order>>? GetPagedList (int? pageIndex, int? pageSize, OrderStatus? status);
    }
}
