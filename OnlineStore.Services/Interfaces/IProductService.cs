using OnlineStore.Domain.Entities;

namespace OnlineStore.Services.Interfaces;

/// <summary>
/// Интерфейс сервиса работы с товарами.
/// Отделяет бизнес-логику от слоя данных.
/// </summary>
public interface IProductService
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
}
