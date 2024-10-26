using CURDOperation.Models;
using CURDOperation.Service.IService;
using Microsoft.EntityFrameworkCore;

namespace CURDOperation.Service.IServiceImpl
{
    public class CategoryService : ICategoryService
    {
        private readonly CURDOperationDbContext _context;

        public CategoryService(CURDOperationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetCategoryAsync() => await _context.Categories.ToListAsync();

        public async Task<Category> GetCategoryByIdAsync(int id) => await _context.Categories.FindAsync(id);

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<bool> UpdateCategoryAsync(int id, Category category)
        {
            var existingCategory = await _context.Categories.FindAsync(id);
            if (existingCategory == null) return false;

            existingCategory.CategoryName = category.CategoryName;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return false;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
