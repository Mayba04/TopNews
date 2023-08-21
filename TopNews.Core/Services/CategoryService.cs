using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopNews.Core.DTOs.Category;
using TopNews.Core.Interfaces;
using TopNews.Core.Entities;

namespace TopNews.Core.Services
{
    public class CategoryService: ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Category> _category;

        public CategoryService(IMapper mapper, IRepository<Category> CategoryRep)
        {
            _category = CategoryRep;
            _mapper = mapper;
        }

        public async Task<List<CategoryDTO>> GetAll() 
        {
            var result = await _category.GetAll();
            return _mapper.Map<List<CategoryDTO>>(result);
        }
    }
}
