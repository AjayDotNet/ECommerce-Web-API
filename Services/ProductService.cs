using AutoMapper;
using ECommerceApi.DTOs;
using ECommerceApi.Interfaces;
using ECommerceApi.Models;

namespace ECommerceApi.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(
            IProductRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _repository.GetAllAsync();

            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }


        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
                return null;

            return _mapper.Map<ProductDto>(product);
        }


        public async Task AddAsync(CreateProductDto dto)
        {
            var product = _mapper.Map<Product>(dto);

            await _repository.AddAsync(product);
        }


        public async Task UpdateAsync(int id, UpdateProductDto dto)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
                return;

            _mapper.Map(dto, product);

            await _repository.UpdateAsync(product);
        }


        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}