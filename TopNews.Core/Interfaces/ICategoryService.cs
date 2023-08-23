using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopNews.Core.DTOs.Category;

namespace TopNews.Core.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> GetAll();
       
        Task<CategoryDTO?> Get(int id);
        Task Create(CategoryDTO model);
        Task Update(CategoryDTO model);
        Task Delete(int id);

    }
}
