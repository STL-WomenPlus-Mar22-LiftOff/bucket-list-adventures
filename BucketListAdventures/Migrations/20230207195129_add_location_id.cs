using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BucketListAdventures.Migrations
{
    public partial class add_location_id : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LocationId",
                table: "Destinations",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Destinations");
        }
    }
}
