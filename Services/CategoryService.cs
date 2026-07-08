using AutoMapper;
using ECommerceApi.DTOs;
using ECommerceApi.Interfaces;
using ECommerceApi.Models;

namespace ECommerceApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryService(
            ICategoryRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = await _repository.GetAllAsync();

            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }


        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);

            if (category == null)
                return null;

            return _mapper.Map<CategoryDto>(category);
        }


        public async Task AddAsync(CreateCategoryDto dto)
        {
            var category = _mapper.Map<Category>(dto);

            await _repository.AddAsync(category);
        }


        public async Task UpdateAsync(int id, CreateCategoryDto dto)
        {
            var category = await _repository.GetByIdAsync(id);

            if (category == null)
                return;

            _mapper.Map(dto, category);

            await _repository.UpdateAsync(category);
        }


        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}