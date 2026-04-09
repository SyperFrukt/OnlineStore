using OnlineStore.Domain.Entities;
using OnlineStore.Infrastructure.Repositories;
using OnlineStore.Services.Interfaces;

namespace OnlineStore.Services;

/// <summary>
/// Сервис бизнес-логики товаров.
/// Использует GenericRepository для доступа к БД.
/// </summary>
public class ProductService : IProductService
{
    private readonly GenericRepository<Product> _productRepo;

    public ProductService(GenericRepository<Product> productRepo)
    {
        _productRepo = productRepo;
    }

    public async Task<IEnumerable<Product>> GetAllAsync() => await _productRepo.GetAllAsync();
    public async Task<Product?> GetByIdAsync(int id) => await _productRepo.GetByIdAsync(id);
}
