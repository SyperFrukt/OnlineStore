using FluentValidation;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Services;

/// <summary>
/// Валидатор заказа. Проверяет обязательность и формат полей.
/// Соответствует требованию: валидация форм через FluentValidation.
/// </summary>
public class OrderValidator : AbstractValidator<Order>
{
    public OrderValidator()
    {
        RuleFor(x => x.CustomerName)
            .NotEmpty().WithMessage("Имя обязательно для заполнения.")
            .MaximumLength(100).WithMessage("Имя не должно превышать 100 символов.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email обязателен.")
            .EmailAddress().WithMessage("Некорректный формат Email.");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Телефон обязателен.")
            .Matches(@"^\+?[0-9]{10,15}$").WithMessage("Телефон должен содержать от 10 до 15 цифр.");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Адрес доставки обязателен.")
            .MaximumLength(200).WithMessage("Адрес не должен превышать 200 символов.");
    }
}
