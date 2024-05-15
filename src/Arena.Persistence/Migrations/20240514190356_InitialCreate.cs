using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arena.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ar");

            migrationBuilder.CreateTable(
                name: "Arena",
                schema: "ar",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "nvarchar(36)", nullable: false),
                    ROUND_COUNT = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arena", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Arena",
                schema: "ar");
        }
    }
}
