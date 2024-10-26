using CURDOperation.Models;

namespace CURDOperation.Service.IService
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategoryAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task<Category> CreateCategoryAsync(Category category);
        Task<bool> UpdateCategoryAsync(int id, Category category);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
