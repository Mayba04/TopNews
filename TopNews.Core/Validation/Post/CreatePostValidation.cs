using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopNews.Core.DTOs.Category;
using TopNews.Core.DTOs.Post;

namespace TopNews.Core.Validation.Post
{
    public class CreatePostValidation : AbstractValidator<PostDTO>
    {
        public CreatePostValidation()
        {
            RuleFor(r => r.Title).MinimumLength(2).NotEmpty().WithMessage("The title must not be empty and have more than 2 characters ");
            RuleFor(r => r.Description).MinimumLength(2).NotEmpty().WithMessage("The Description must not be empty and have more than 2 characters ");
            RuleFor(r => r.Text).MinimumLength(2).NotEmpty().WithMessage("The Text must not be empty and have more than 2 characters ");
        }
    }
}
