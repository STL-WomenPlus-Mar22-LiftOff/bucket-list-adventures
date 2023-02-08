using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BucketListAdventures.Migrations
{
    public partial class airplane_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AirLineCode",
                table: "UserProfiles",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AirLineCode",
                table: "UserProfiles");
        }
    }
}
