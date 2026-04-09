namespace OnlineStore.Domain.Entities;

/// <summary>
/// Заказ покупателя.
/// Используется для демонстрации форм и валидации данных.
/// </summary>
public class Order
{
    public int Id { get; set; }

    /// <summary>Имя заказчика</summary>
    public required string CustomerName { get; set; }

    /// <summary>Email для связи</summary>
    public required string Email { get; set; }

    /// <summary>Телефон</summary>
    public required string Phone { get; set; }

    /// <summary>Адрес доставки</summary>
    public required string Address { get; set; }

    /// <summary>Общая сумма заказа</summary>
    public decimal TotalPrice { get; set; }

    /// <summary>Дата создания</summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
