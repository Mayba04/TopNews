using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopNews.Core.DTOs.User;

namespace TopNews.Core.Validation.User
{
    public class UpdateUserValidation : AbstractValidator<UpdateUserDTO>
    {
        public UpdateUserValidation()
        {
            RuleFor(r => r.FirstName).NotEmpty();
            RuleFor(r => r.LastName).NotEmpty();
            RuleFor(r => r.Email).NotEmpty();
            RuleFor(r => r.PhoneNumber).NotEmpty();
        }
    }
}
