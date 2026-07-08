using ECommerceApi.DTOs;

namespace ECommerceApi.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();

        Task<CategoryDto?> GetByIdAsync(int id);

        Task AddAsync(CreateCategoryDto category);

        Task UpdateAsync(int id, CreateCategoryDto category);

        Task DeleteAsync(int id);
    }
}