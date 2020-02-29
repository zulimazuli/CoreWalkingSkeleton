using CoreTemplate.ApplicationCore.Entities;
using FluentValidation;

namespace CoreTemplate.ApplicationCore.Validators
{
    public class ItemValidator : AbstractValidator<Item>
    {
        public ItemValidator()
        {
            //RuleFor(item => item.UniqueId).NotEmpty();
        }
    }
}
