using FluentValidation;
using testeItLab.domain.Models;

namespace testeItLab.domain.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(m => m.Name).NotNull();
            RuleFor(m => m.Value).GreaterThan(0).NotNull();
            RuleFor(m => m.Type).NotNull();
        }
    }
}
