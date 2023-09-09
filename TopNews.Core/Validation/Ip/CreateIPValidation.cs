using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopNews.Core.DTOs.Ip;
using TopNews.Core.DTOs.Post;

namespace TopNews.Core.Validation.Ip
{
    public class CreateIPValidation : AbstractValidator<DashboardAccessDTO>
    {
        public CreateIPValidation()
        {
            RuleFor(da => da.IpAddress).NotEmpty();
        }
    }
}
