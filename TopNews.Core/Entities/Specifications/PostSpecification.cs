using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TopNews.Core.Entities.Specifications
{
    public class PostSpecification
    {
        public class ByCategory : Specification<Post>
        {
            public ByCategory(int categoryId)
            {
                Query
                  .Include(x => x.Category)
                  .Where(c => c.CategoryId == categoryId).OrderByDescending(x => x.Id); ;
            }
        }
    }
}
