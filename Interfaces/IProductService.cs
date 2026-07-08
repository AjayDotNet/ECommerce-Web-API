using ECommerceApi.DTOs;

namespace ECommerceApi.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();

        Task<ProductDto?> GetByIdAsync(int id);

        Task AddAsync(CreateProductDto product);

        Task UpdateAsync(int id, UpdateProductDto product);

        Task DeleteAsync(int id);
    }
}