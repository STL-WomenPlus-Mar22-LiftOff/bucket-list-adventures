using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BucketListAdventures.Migrations
{
    public partial class location_id_nullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LocationId",
                table: "Destinations",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "LocationId",
                keyValue: null,
                column: "LocationId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "LocationId",
                table: "Destinations",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
