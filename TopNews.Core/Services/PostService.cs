using AutoMapper;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopNews.Core.DTOs.Category;
using TopNews.Core.DTOs.Post;
using TopNews.Core.Entities;
using TopNews.Core.Entities.Specifications;
using TopNews.Core.Interfaces;

namespace TopNews.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Post> _postRepo;

        public PostService(IMapper mapper, IRepository<Post> categoryRepo)
        {
            _postRepo = categoryRepo;
            _mapper = mapper;
        }

        public async Task Create(PostDTO model)
        {
            await _postRepo.Insert(_mapper.Map<Post>(model));
            await _postRepo.Save();
        }

        public async Task Delete(int id)
        {
            var model = await Get(id);
            if (model == null) return;

            await _postRepo.Delete(model.Id);
            await _postRepo.Save();
        }

        public async Task<PostDTO?> Get(int id)
        {
            if (id < 0) return null;
            var category = await _postRepo.GetByID(id);
            if (category == null) return null;
            return _mapper.Map<PostDTO?>(category);
        }

        public async Task<List<PostDTO>> GetAll()
        {
            var result = await _postRepo.GetAll();
            return _mapper.Map<List<PostDTO>>(result);
        }

        public async Task<List<PostDTO>> GetByCategory(int id)
        {
            var result = await _postRepo.GetListBySpec(new PostSpecification.ByCategory(id));
            return _mapper.Map<List<PostDTO>>(result);
        }

        public async Task Update(PostDTO model)
        {
            await _postRepo.Update(_mapper.Map<Post>(model));
            await _postRepo.Save();
        }
    }
}
