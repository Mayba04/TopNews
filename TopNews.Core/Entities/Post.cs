using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopNews.Core.Interfaces;

namespace TopNews.Core.Entities
{
    public class Post : IEntity
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Text { get; set; } = string.Empty;

        public DateTime DatePublication { get; set; } 
        public string? Image { get; set; } = "Default.png";

        public int CategoryId { get; set; }

        public Category? Category { get; set; }
    }
}
