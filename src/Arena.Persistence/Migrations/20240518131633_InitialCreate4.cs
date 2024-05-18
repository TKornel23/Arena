using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arena.Persistence.Migrations
{
    public partial class InitialCreate4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "STATUS",
                schema: "ar",
                table: "Arena",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "STATUS",
                schema: "ar",
                table: "Arena");
        }
    }
}
