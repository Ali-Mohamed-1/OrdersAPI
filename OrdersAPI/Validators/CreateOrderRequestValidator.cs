using FluentValidation;
using OrdersAPI.Models;

namespace OrdersAPI.Validators
{
    public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
    {
        public CreateOrderRequestValidator()
        {
            RuleFor(x => x.orderItems)
                .NotNull().WithMessage("Order items cannot be blank")
                .Must(items => items != null && items.Count > 0).WithMessage("At least one order item is required");

            RuleForEach(x => x.orderItems)
                .SetValidator(new OrderItemValidator());
        }
    }
}
