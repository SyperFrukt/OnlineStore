using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.Entities;
using OnlineStore.Infrastructure.Data;

namespace OnlineStore.Infrastructure.Data;

/// <summary>
/// Класс для начального наполнения базы данных тестовыми данными.
/// Соответствует требованию: демонстрация работы приложения с реальными данными.
/// </summary>
public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        // Проверяем, есть ли уже данные
        if (context.Categories.Any())
            return;

        // Создаём категории
        var categories = new List<Category>
        {
            new Category { Name = "Электроника" },
            new Category { Name = "Одежда" },
            new Category { Name = "Книги" }
        };

        await context.Categories.AddRangeAsync(categories);
        await context.SaveChangesAsync();

        // Создаём товары
        var products = new List<Product>
        {
            new Product 
            { 
                Name = "Смартфон", 
                Description = "Современный смартфон с отличной камерой", 
                Price = 29990.00m, 
                Stock = 15,
                CategoryId = categories[0].Id
            },
            new Product 
            { 
                Name = "Ноутбук", 
                Description = "Мощный ноутбук для работы и игр", 
                Price = 75000.00m, 
                Stock = 8,
                CategoryId = categories[0].Id
            },
            new Product 
            { 
                Name = "Футболка", 
                Description = "Хлопковая футболка, размер M", 
                Price = 1200.00m, 
                Stock = 50,
                CategoryId = categories[1].Id
            },
            new Product 
            { 
                Name = "Книга по C#", 
                Description = "Изучаем C# и .NET 8 с нуля", 
                Price = 2500.00m, 
                Stock = 20,
                CategoryId = categories[2].Id
            }
        };

        await context.Products.AddRangeAsync(products);
        await context.SaveChangesAsync();
    }
}
