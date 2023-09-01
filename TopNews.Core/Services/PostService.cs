using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
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
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;

        public PostService(IMapper mapper, IRepository<Post> postRepo, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _postRepo = postRepo;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }

        public async Task Create(PostDTO model)
        {
            if (model.File.Count > 0)
            {
                string wevRootPath = _webHostEnvironment.WebRootPath;
                string uploadt = wevRootPath + _configuration.GetValue<string>("ImageSettings:ImagePath");
                var files = model.File;
                var fileName = Guid.NewGuid().ToString();
                string extansions = Path.GetExtension(files[0].FileName);
                using (var fileStream = new FileStream(Path.Combine(uploadt, fileName + extansions), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }
                model.Image = fileName + extansions;
            }
            else
            {
                model.Image = "Default.png";
            }
            DateTime currentDate = DateTime.Now;
            model.DatePublication = currentDate;
            var postEntity = _mapper.Map<Post>(model);
            postEntity.Category = null;
            await _postRepo.Insert(postEntity);
            await _postRepo.Save();
        }

        public async Task Delete(int id)
        {
            var model = await Get(id);
            if (model == null) return;

            string webPathRoot = _webHostEnvironment.WebRootPath;
            string upload = webPathRoot + _configuration.GetValue<string>("ImageSettings:ImagePath");

            string existingFilePath = Path.Combine(upload, model.Image);

            if (File.Exists(existingFilePath) && model.Image != "Default.png")
            {
                File.Delete(existingFilePath);
            }

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
            var result = await _postRepo.GetListBySpec(new PostSpecification.All());
            return _mapper.Map<List<PostDTO>>(result);
        }

        public async Task<List<PostDTO>> GetByCategory(int id)
        {
            var result = await _postRepo.GetListBySpec(new PostSpecification.ByCategory(id));
            return _mapper.Map<List<PostDTO>>(result);
        }

        public async Task<PostDTO> GetById(int id)
        {
            if (id < 0) return null; 

            var post = await _postRepo.GetByID(id);

            if (post == null) return null; 

            return _mapper.Map<PostDTO>(post);
        }

        public async Task<List<PostDTO>> Search(string searchString)
        {
            var result = await _postRepo.GetListBySpec(new PostSpecification.Search(searchString));
            return _mapper.Map<List<PostDTO>>(result);
        }

        public async Task Update(PostDTO model)
        {
            var currentPost = await _postRepo.GetByID(model.Id);
            if (model.File.Count > 0)
            {
                string webPathRoot = _webHostEnvironment.WebRootPath;
                string upload = webPathRoot + _configuration.GetValue<string>("ImageSettings:ImagePath");

                string existingFilePath = Path.Combine(upload, currentPost.Image);

                if (File.Exists(existingFilePath) && model.Image != "Default.png")
                {
                    File.Delete(existingFilePath);
                }

                var files = model.File;

                string fileName = Guid.NewGuid().ToString();
                string extension = Path.GetExtension(files[0].FileName);
                using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }
                model.Image = fileName + extension;

            }
            else
            {
                model.Image = currentPost.Image;
            }
            var postEntity = _mapper.Map<Post>(model);
            postEntity.Category = null;
            await _postRepo.Update(postEntity);
            await _postRepo.Save();
        }
    }
}
