using CoreTemplate.ApplicationCore.Models;
using FluentValidation;

namespace CoreTemplate.Web.Validators
{
    public class ItemValidator : AbstractValidator<Item>
    {
        public ItemValidator()
        {
            //RuleFor(item => item.UniqueId).NotEmpty();
        }
    }
}
