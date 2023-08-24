using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopNews.Core.DTOs.Category;

namespace TopNews.Core.Validation.Category
{
    public class CreateCategoryValidation: AbstractValidator<CategoryDTO>
    {
        public CreateCategoryValidation()
        {
            RuleFor(r => r.Name).MinimumLength(2).NotEmpty().MaximumLength(12).WithMessage("The role name must not be empty, have more than 2 characters and no more than 12 characters");
        }
    }
}
