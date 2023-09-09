using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopNews.Core.DTOs.Ip;
using TopNews.Core.Entities;

namespace TopNews.Core.AutoMapper.Ip
{
    internal class AutoMapperDashboardAccsessProfile: Profile
    {
        public AutoMapperDashboardAccsessProfile()
        {
            CreateMap<DashboardAccess, DashboardAccessDTO>().ReverseMap();
        }
    }
}
