using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TopNews.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dashboardAccess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DashboardAccess",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DashboardAccess", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "DashboardAccess",
                columns: new[] { "Id", "IpAddress" },
                values: new object[] { 1, "0.0.0.0" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DashboardAccess");
        }
    }
}
