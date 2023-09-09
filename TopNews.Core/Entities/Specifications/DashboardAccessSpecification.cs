using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TopNews.Core.Entities.Specifications
{
    internal class DashboardAccessSpecification
    {
        public class GetByIpAddress : Specification<DashboardAccess>
        {
            public GetByIpAddress(string ipAdress)
            {
                Query.Where(da => da.IpAddress == ipAdress);
            }
        }
    }
}
