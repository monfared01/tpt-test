using FluentValidation;
using Sales.Application.Command.Order;


namespace Sales.Application.Validation.Order;
public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderValidator()
    {
        RuleFor(p => p.CustomerName)
                .NotEmpty().WithMessage("نام مشتری نمیتواند خالی باشد.");
        RuleFor(p => p.TotalAmount)
            .GreaterThanOrEqualTo(0).WithMessage("مبلغ سفارش نمیتواند منفی باشد.");
    }
}