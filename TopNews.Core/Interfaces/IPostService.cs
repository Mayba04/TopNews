using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopNews.Core.DTOs.Category;
using TopNews.Core.DTOs.Post;

namespace TopNews.Core.Interfaces
{
    public interface IPostService
    {
        Task<List<PostDTO>> GetAll();

        Task<PostDTO?> Get(int id);
        Task Create(PostDTO model);
        Task Update(PostDTO model);
        Task Delete(int id);
    }
}
