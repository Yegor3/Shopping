using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Shopping.Platform.Common.Entities;

namespace Shopping.Platform.Repositories.Context
{
    public class ShoppingContext : DbContext
    {
        public ShoppingContext(DbContextOptions<ShoppingContext> options)
            : base(options)
        {}

        public DbSet<Order> Order => Set<Order>();
        public DbSet<Product> Product => Set<Product>();
    }
}
