using CURDOperation.Models;
using CURDOperation.Service.IService;
using Microsoft.EntityFrameworkCore;

namespace CURDOperation.Service.IServiceImpl
{
    public class ProductService : IProductService
    {
        private readonly CURDOperationDbContext _context;

        public ProductService(CURDOperationDbContext context)
        {
            _context = context;
        }

        public async Task<Product> GetProductByIdAsync(int productId) =>
            await _context.Products.FirstOrDefaultAsync(p => p.ProductId == productId);

        public async Task<Product> CreateProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(int pageNumber, int pageSize)
        {
            return await _context.Products
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<bool> UpdateProductAsync(int id, Product product)
        {
            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null) return false;

            existingProduct.ProductName = product.ProductName;
            existingProduct.CategoryId = product.CategoryId;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null) return false;

            _context.Products.Remove(existingProduct);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
