using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopNews.Core.DTOs.Category;
using TopNews.Core.Entities;

namespace TopNews.Core.AutoMapper.Categories
{
    public class AutoMapperCategoryProfile: Profile
    {
        public AutoMapperCategoryProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
        }
    }
}
