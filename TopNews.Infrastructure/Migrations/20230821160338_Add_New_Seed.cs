using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TopNews.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_New_Seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatePublication = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Sports" },
                    { 2, "Technology" },
                    { 3, "Entertainment" }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "CategoryId", "DatePublication", "Description", "Image", "Text", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tomorrow, local council elections will take place in the city. Citizens will choose the new composition of local representatives.", "election.jpg", "On Monday, August 15, local council elections will take place in the city. This is an important event for the local community as citizens will determine the composition of regional representatives for the next four years. Several parties have nominated their candidates, and the competition promises to be intense. Citizens are encouraged to actively participate in voting and elections.", "Local Council Elections" },
                    { 2, 1, new DateTime(2023, 7, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Presidents of two countries have signed an agreement on cooperation in trade and cultural relations.", "diplomacy.jpg", "On Tuesday, July 25, the presidents of Alpha and Beta countries signed an important bilateral agreement to expand cooperation. According to the agreement, the countries have committed to promoting trade development between each other, as well as jointly implementing cultural projects and exchanging educational experiences. This is a step towards strengthening international relations and improving economic ties between the countries.", "International Diplomacy" },
                    { 3, 2, new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Technology company X-Tech announced the release of its new flagship smartphone, X-Phone, with innovative features.", "x-phone.jpg", "X-Tech, a well-known player in the technology market, has announced the release of its new smartphone, named X-Phone. This device impresses with its features: powerful processor, enhanced camera with 8K video recording capability, and support for new communication standards. X-Phone also became the company's first smartphone to use facial recognition technology for maximum user security.", "Release of New X-Phone Smartphone" },
                    { 4, 2, new DateTime(2023, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "DurchTech has unveiled its new product - DurchCloud, which promises to revolutionize data storage in the cloud.", "durchcloud.jpg", "DurchTech, an innovative player in cloud technology, has introduced its latest achievement - DurchCloud. This platform allows users to store, process, and secure their data in a cloud environment using advanced encryption algorithms. DurchCloud promises high performance, a user-friendly interface, and guaranteed confidentiality of user information.", "DurchCloud Breakthrough in Cloud Technologies" },
                    { 5, 3, new DateTime(2023, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Astronomers have announced the discovery of a new Earth-like planet in the Andromeda galaxy using a powerful telescope.", "andromeda_planet.jpg", "Using state-of-the-art telescopes, astronomers have identified a new planet located in the Andromeda galaxy, which is over 2 million light-years away from Earth. While the planet has differences, scientists see potential for research on extraterrestrial life due to its similarity to our conditions. This discovery could revolutionize our understanding of the universe and the possibility of the existence of other civilizations.", "Discovery of New Planet in Andromeda Galaxy" },
                    { 6, 3, new DateTime(2023, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Scientists have developed and tested a new method to treat allergic reactions, reducing flare-ups and improving the quality of life for patients.", "allergy_treatment.jpg", "Researchers from the Medical Institute have presented a new method aimed at combating allergy flare-ups. This method is based on immunotherapy and the principle of gradual adaptation of the body to allergens. After successful clinical trials, patients suffering from severe allergic reactions experienced noticeable improvements in health and a reduction in the intensity of allergic symptoms.", "New Method to Combat Allergy Flare-ups" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CategoryId",
                table: "Posts",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
