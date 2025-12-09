using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Shopping.Platform.Common.Entities;
using Shopping.Platform.Repositories.Context;
using Shopping.Platform.Repositories.Interface;

namespace Shopping.Platform.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShoppingContext _context;

        public ProductRepository(ShoppingContext context)
        {
            _context = context;
        }
        
        public Product Save (Product product)
        {
            _context.Product.Add(product);
            _context.SaveChanges();
            return product;
        }

        public Product? GetFirstByOrderId (long orderId)
        {
            Product? product = _context.Product.FirstOrDefault(p => p.OrderId == orderId && p.DeletionDate == null);
            return product;
        }

        public Product? GetByIdAndOrderId (long productId, long orderId)
        {
            Product? product = _context.Product.FirstOrDefault(o => o.Id == productId && o.OrderId == orderId);
            return product;
        }

        public void Delete (Product product)
        {
            product.DeletionDate = DateTime.UtcNow;
            product.UpdateDate = DateTime.UtcNow;
            _context.SaveChanges();
        }

        public List<Product>? GetByOrderId(long orderId)
        {
            List<Product>? productList = _context.Product.Where(p => p.OrderId == orderId && p.DeletionDate == null).ToList();
            return productList;
        }
    }
}