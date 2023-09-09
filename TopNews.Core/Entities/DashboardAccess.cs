using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopNews.Core.Interfaces;

namespace TopNews.Core.Entities
{
    public class DashboardAccess : IEntity
    {
        public int Id { get; set; }
        public string IpAddress { get; set; }
    }
}
