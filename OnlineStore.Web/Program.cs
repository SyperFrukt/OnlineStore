using OnlineStore.Web.Components;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Регистрация репозиториев и сервисов (DI через IServiceCollection)
builder.Services.AddScoped(typeof(OnlineStore.Infrastructure.Repositories.GenericRepository<>));
builder.Services.AddScoped<OnlineStore.Services.Interfaces.IProductService, OnlineStore.Services.ProductService>();

// 1. Подключение EF Core (SQLite)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Регистрация сервисов/репозиториев (будет расширяться)
// builder.Services.AddScoped<IProductService, ProductService>();

// 3. Blazor Server (.NET 8 шаблон)
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<FluentValidation.IValidator<OnlineStore.Domain.Entities.Order>, 
                           OnlineStore.Services.OrderValidator>();

var app = builder.Build();

// Авто-заполнение БД тестовыми данными при запуске
// === АВТО-МИГРАЦИЯ + SEED (исправленная версия) ===
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        
        // 1. Сначала применяем миграции (создаём таблицы)
        var pending = context.Database.GetPendingMigrations();
        if (pending.Any())
        {
            Console.WriteLine($"🔄 Применяю миграции: {string.Join(", ", pending)}");
            context.Database.Migrate();
            Console.WriteLine("✅ Миграции применены");
        }
        
        // 2. Затем заполняем данными (если таблицы пустые)
        if (!context.Categories.Any())
        {
            await OnlineStore.Infrastructure.Data.DbSeeder.SeedAsync(context);
            Console.WriteLine("✅ База данных заполнена тестовыми данными");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Ошибка при инициализации БД: {ex.Message}");
        Console.WriteLine($"Stack: {ex.StackTrace}");
    }
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();