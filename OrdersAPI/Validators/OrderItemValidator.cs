using FluentValidation;
using OrdersAPI.Models;

namespace OrdersAPI.Validators
{
    public class OrderItemValidator : AbstractValidator<OrderItem>
    {
        public OrderItemValidator()
        {
            RuleFor(item => item.quantity)
                .NotNull()
                .WithMessage("Item quantity cannot be null")
                .GreaterThan(0)
                .WithMessage("Item quantity must be greater than 0");
        }
    }
}
