using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreTemplate.Models;
using FluentValidation;

namespace CoreTemplate.Validators
{
    public class ItemValidator : AbstractValidator<Item>
    {
        public ItemValidator()
        {
            //RuleFor(item => item.UniqueId).NotEmpty();
        }
    }
}
