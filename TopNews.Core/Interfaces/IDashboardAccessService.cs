using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopNews.Core.DTOs.Ip;
using TopNews.Core.Entities;

namespace TopNews.Core.Services
{
    public interface IDashdoardAccessService
    {
        Task<List<DashboardAccessDTO>> GetAll();
        Task Create(DashboardAccessDTO model);
        Task Delete(int id);
        Task<DashboardAccessDTO?> Get(string IpAddress);
        Task<DashboardAccessDTO?> Get(int id);
        Task Update(DashboardAccessDTO model);
    }
}
