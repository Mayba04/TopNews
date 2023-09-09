using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopNews.Core.Entities;


namespace TopNews.Infrastructure.Initializers
{
    internal static class DBInitializer
    {
        public static void SeedCategory(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(new Category[]
            {
                new Category
                {
                    Id = 1,
                    Name = "Sports"
                },
                new Category
                {
                    Id = 2,
                    Name = "Technology"
                },
                new Category
                {
                    Id = 3,
                    Name = "Entertainment"
                }
            });
        }

        public static void SeedPost(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>().HasData(
              new Post
              {
                  Id = 1,
                  Title = "Local Council Elections",
                  Description = "Tomorrow, local council elections will take place in the city. Citizens will choose the new composition of local representatives.",
                  Text = "On Monday, August 15, local council elections will take place in the city. This is an important event for the local community as citizens will determine the composition of regional representatives for the next four years. Several parties have nominated their candidates, and the competition promises to be intense. Citizens are encouraged to actively participate in voting and elections.",
                  DatePublication = new DateTime(2023, 8, 14),
                  CategoryId = 1,
                  Image = "election.jpg"
              },
              new Post
              {
                  Id = 2,
                  Title = "International Diplomacy",
                  Description = "Presidents of two countries have signed an agreement on cooperation in trade and cultural relations.",
                  Text = "On Tuesday, July 25, the presidents of Alpha and Beta countries signed an important bilateral agreement to expand cooperation. According to the agreement, the countries have committed to promoting trade development between each other, as well as jointly implementing cultural projects and exchanging educational experiences. This is a step towards strengthening international relations and improving economic ties between the countries.",
                  DatePublication = new DateTime(2023, 7, 26),
                  CategoryId = 1,
                  Image = "diplomacy.jpg"
              },
              new Post
              {
                  Id = 3,
                  Title = "Release of New X-Phone Smartphone",
                  Description = "Technology company X-Tech announced the release of its new flagship smartphone, X-Phone, with innovative features.",
                  Text = "X-Tech, a well-known player in the technology market, has announced the release of its new smartphone, named X-Phone. This device impresses with its features: powerful processor, enhanced camera with 8K video recording capability, and support for new communication standards. X-Phone also became the company's first smartphone to use facial recognition technology for maximum user security.",
                  DatePublication = new DateTime(2023, 8, 10),
                  CategoryId = 2,
                  Image = "x-phone.jpg"
              },
              new Post
              {
                  Id = 4,
                  Title = "DurchCloud Breakthrough in Cloud Technologies",
                  Description = "DurchTech has unveiled its new product - DurchCloud, which promises to revolutionize data storage in the cloud.",
                  Text = "DurchTech, an innovative player in cloud technology, has introduced its latest achievement - DurchCloud. This platform allows users to store, process, and secure their data in a cloud environment using advanced encryption algorithms. DurchCloud promises high performance, a user-friendly interface, and guaranteed confidentiality of user information.",
                  DatePublication = new DateTime(2023, 7, 2),
                  CategoryId = 2,
                  Image = "durchcloud.jpg"
              },
              new Post
              {
                  Id = 5,
                  Title = "Discovery of New Planet in Andromeda Galaxy",
                  Description = "Astronomers have announced the discovery of a new Earth-like planet in the Andromeda galaxy using a powerful telescope.",
                  Text = "Using state-of-the-art telescopes, astronomers have identified a new planet located in the Andromeda galaxy, which is over 2 million light-years away from Earth. While the planet has differences, scientists see potential for research on extraterrestrial life due to its similarity to our conditions. This discovery could revolutionize our understanding of the universe and the possibility of the existence of other civilizations.",
                  DatePublication = new DateTime(2023, 8, 5),
                  CategoryId = 3,
                  Image = "andromeda_planet.jpg"
              },
              new Post
              {
                  Id = 6,
                  Title = "New Method to Combat Allergy Flare-ups",
                  Description = "Scientists have developed and tested a new method to treat allergic reactions, reducing flare-ups and improving the quality of life for patients.",
                  Text = "Researchers from the Medical Institute have presented a new method aimed at combating allergy flare-ups. This method is based on immunotherapy and the principle of gradual adaptation of the body to allergens. After successful clinical trials, patients suffering from severe allergic reactions experienced noticeable improvements in health and a reduction in the intensity of allergic symptoms.",
                  DatePublication = new DateTime(2023, 7, 17),
                  CategoryId = 3,
                  Image = "allergy_treatment.jpg"
              }
          );
        }

        public static void SeedDashdoardAccesses(this ModelBuilder model)
        {
            model.Entity<DashboardAccess>().HasData(
                new DashboardAccess()
                {
                    Id = 1,
                    IpAddress = "0.0.0.0",
                }
            );
        }

    }
}
