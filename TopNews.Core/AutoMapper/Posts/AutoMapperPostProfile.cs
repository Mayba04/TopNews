using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopNews.Core.DTOs.Post;
using TopNews.Core.Entities;

namespace TopNews.Core.AutoMapper.Posts
{
    public class AutoMapperPostProfile: Profile
    {
        public AutoMapperPostProfile()
        {
            CreateMap<Post, PostDTO>().ReverseMap();
        }
    }
}
