using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopNews.Core.Interfaces;

namespace TopNews.Core.Entities
{
    public class Category: IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Post> Post { get; set; }
    }
}
