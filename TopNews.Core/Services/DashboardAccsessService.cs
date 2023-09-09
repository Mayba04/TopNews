using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopNews.Core.DTOs.Ip;
using TopNews.Core.Entities;
using TopNews.Core.Entities.Specifications;
using TopNews.Core.Interfaces;

namespace TopNews.Core.Services
{
    internal class DashboardAccsessService : IDashdoardAccessService
    {
        private readonly IRepository<DashboardAccess> _ipRepo;
        private readonly IMapper _mapper;
        public DashboardAccsessService(IRepository<DashboardAccess> ipRepo, IMapper mapper)
        {
            _ipRepo = ipRepo;
            _mapper = mapper;
        }

        public async Task Create(DashboardAccessDTO model)
        {
            await _ipRepo.Insert(_mapper.Map<DashboardAccess>(model));
            await _ipRepo.Save();
        }

        public async Task Delete(int id)
        {
            var model = await Get(id);
            if (model == null) return;

            await _ipRepo.Delete(model.Id);
            await _ipRepo.Save();
        }

        public async Task<DashboardAccessDTO?> Get(string IpAddress)
        {
            return _mapper.Map<DashboardAccessDTO>(await _ipRepo.GetItemBySpec(new DashboardAccessSpecification.GetByIpAddress(IpAddress)));
        }

        public async Task<DashboardAccessDTO?> Get(int id)
        {
            return _mapper.Map<DashboardAccessDTO>(await _ipRepo.GetByID(id));
        }

        public async Task<List<DashboardAccessDTO>> GetAll()
        {
            return _mapper.Map<List<DashboardAccessDTO>>(await _ipRepo.GetAll());
        }

        public async Task Update(DashboardAccessDTO model)
        {
            await _ipRepo.Update(_mapper.Map<DashboardAccess>(model));
            await _ipRepo.Save();
        }
    }
}
