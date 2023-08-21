using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopNews.Infrastructure.Entities
{
    public  class Post
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Text { get; set; }

        public string Image { get; set; }

        public DateTime DatePublication { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
