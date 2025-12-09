using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Shopping.Platform.Common.Entities;
using Shopping.Platform.Common.Entities.Enums;
using Shopping.Platform.Repositories.Context;
using Shopping.Platform.Repositories.Interface;

namespace Shopping.Platform.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ShoppingContext _context;

        public OrderRepository(ShoppingContext context)
        {
            _context = context;
        }
        
        public long Save (Order order)
        {
            _context.Order.Add(order);
            _context.SaveChanges();
            return order.Id;
        }

        public void Close (Order order)
        {
            order.Status = OrderStatus.Closed;
            order.UpdateDate = DateTime.UtcNow;
            _context.SaveChanges();
        }

        public Order? Get (long orderId)
        {
            var order = _context.Order.FirstOrDefault(o => o.Id == orderId);
            return order;
        }

        public async Task<List<Order>>? GetPagedList (int? pageIndex, int? pageSize, OrderStatus? status)
        {
            var result = await _context.Order.FromSqlRaw
            (
                "SELECT * FROM GetOrdersPaged({0}, {1}, {2})",
                pageSize,
                pageIndex,
                (int?)status
            ).ToListAsync();

            return result;
        }
    }
}
