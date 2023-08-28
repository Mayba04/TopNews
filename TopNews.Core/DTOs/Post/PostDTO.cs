using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopNews.Core.DTOs.Post
{
    public class PostDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Text { get; set; }

        public DateTime DatePublication { get; set; }

        public string? _image { get; set; }

        public string? Image 
        {
            get => _image;
            set => _image = value ?? defaultPath; 
        }

        const string defaultPath = "Default.png";

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
        public IFormFileCollection File { get; set; }
        public string Slug => Title?
            .Replace("а", "a")
            .Replace("б", "b")
            .Replace("в", "v")
            .Replace("г", "g")
            .Replace("д", "d")
            .Replace("е", "e")
            .Replace("є", "e")
            .Replace("ё", "e")
            .Replace("ж", "j")
            .Replace("з", "z")
            .Replace("и", "i")
            .Replace("ї", "yi")
            .Replace("й", "i")
            .Replace("к", "k")
            .Replace("л", "l")
            .Replace("м", "m")
            .Replace("н", "n")
            .Replace("о", "o")
            .Replace("п", "p")
            .Replace("р", "r")
            .Replace("с", "s")
            .Replace("т", "t")
            .Replace("у", "u")
            .Replace("ф", "f")
            .Replace("х", "h")
            .Replace("ц", "c")
            .Replace("ч", "ch")
            .Replace("ш", "sh")
            .Replace("щ", "shch")
            .Replace("ы", "y")
            .Replace("э", "e")
            .Replace("ю", "u")
            .Replace("я", "ya")
            .Replace(":", "-")
            .Replace(" ", "-").ToLower().ToString() + ".html";

    }
}
