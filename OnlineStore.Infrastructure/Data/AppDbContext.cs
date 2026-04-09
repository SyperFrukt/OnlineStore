using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Infrastructure.Data;

/// <summary>
/// Контекст базы данных онлайн-магазина.
/// Настраивает сущности, связи и ограничения через Fluent API.
/// </summary>
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    /// <summary>Набор категорий</summary>
    public DbSet<Category> Categories => Set<Category>();

    /// <summary>Набор товаров</summary>
    public DbSet<Product> Products => Set<Product>();

    public DbSet<Order> Orders => Set<Order>();

    /// <summary>
    /// Конфигурация моделей и связей (Fluent API).
    /// </summary>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Category → 1:N → Product
        builder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name)
                  .IsRequired()
                  .HasMaxLength(100);
            entity.HasIndex(e => e.Name).IsUnique();
        });

        builder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name)
                  .IsRequired()
                  .HasMaxLength(200);
            entity.Property(e => e.Price)
                  .HasColumnType("decimal(18,2)");
            entity.Property(e => e.Stock)
                  .IsRequired();

            // Связь 1:N: у одной категории много товаров
            entity.HasOne(e => e.Category)
                  .WithMany(c => c.Products)
                  .HasForeignKey(e => e.CategoryId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
