using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopNews.Core.DTOs.User;

namespace TopNews.Core.Validation.User
{
    public class CreateUserValidation: AbstractValidator<CreateUserDTO>
    {
        public CreateUserValidation()
        {
            RuleFor(r => r.FirstName).MinimumLength(2).NotEmpty().MaximumLength(12);
            RuleFor(r => r.LastName).MinimumLength(2).NotEmpty().MaximumLength(12);
            RuleFor(r => r.Email).NotEmpty().EmailAddress();
            RuleFor(r => r.Role).NotEmpty();
            RuleFor(r => r.Password).NotEmpty().MinimumLength(6);
            RuleFor(r => r.ConfirmPassword).NotEmpty().MinimumLength(6).Equal(p => p.Password); ;
        }
    }
   
}
