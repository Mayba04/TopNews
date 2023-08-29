using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopNews.Core.DTOs.Category;
using TopNews.Core.Interfaces;
using TopNews.Core.Entities;
using TopNews.Core.Entities.Specifications;

namespace TopNews.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Category> _categoryRepo;

        public CategoryService(IMapper mapper, IRepository<Category> categoryRepo)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        public async Task Create(CategoryDTO model)
        {
            await _categoryRepo.Insert(_mapper.Map<Category>(model));
            await _categoryRepo.Save();
        }

        public async Task Delete(int id)
        {
            var model = await Get(id);
            if (model == null) return;

            await _categoryRepo.Delete(model.Id);
            await _categoryRepo.Save();
        }

        public async Task<CategoryDTO?> Get(int id)
        {
            if (id < 0) return null;
            var category = await _categoryRepo.GetByID(id);
            if (category == null) return null;
            return _mapper.Map<CategoryDTO?>(category);
            
        }

        public async Task<List<CategoryDTO>> GetAll()
        {
            var result = await _categoryRepo.GetAll();
            return _mapper.Map<List<CategoryDTO>>(result);
        }

        public async Task<ServiceResponse> GetByName(CategoryDTO model)
        {
            var result = await _categoryRepo.GetItemBySpec(new CategorySpecification.GetByName(model.Name));
            if (result != null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Category exists."
                };
            }
            var category = _mapper.Map<CategoryDTO>(result);
            return new ServiceResponse
            {
                Success = true,
                Message = "Category successfully loaded.",
                Payload = category
            };
        }

        public async Task<CategoryDTO> GetByName(string NameCategory)
        {
            var result = await _categoryRepo.GetItemBySpec(new CategorySpecification.GetByName(NameCategory));
            if (result != null)
            {
                CategoryDTO categoryDTO = _mapper.Map<CategoryDTO>(result);
                return categoryDTO;
            }
            return null;
        }

        public async Task Update(CategoryDTO model)
        {
           await _categoryRepo.Update(_mapper.Map<Category>(model));
           await _categoryRepo.Save();
        }
    }
}
