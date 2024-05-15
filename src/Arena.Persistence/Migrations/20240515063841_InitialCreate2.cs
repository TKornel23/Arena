using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arena.Persistence.Migrations
{
    public partial class InitialCreate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ROUNDS",
                schema: "ar",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "nvarchar(36)", nullable: false),
                    ROUND = table.Column<int>(type: "INTEGER", nullable: false),
                    ATTACKER_HEALTH_CHANGE = table.Column<float>(type: "REAL", nullable: false),
                    ATTACKER_ROLE = table.Column<string>(type: "TEXT", nullable: false),
                    ATTACKER_GUID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ATTACKER_HEALTH = table.Column<float>(type: "REAL", nullable: false),
                    ATTACKER_MAX_HEALTH = table.Column<float>(type: "REAL", nullable: false),
                    DEFENDER_HEALTH_CHANGE = table.Column<float>(type: "REAL", nullable: false),
                    DEFENDER_ROLE = table.Column<string>(type: "TEXT", nullable: false),
                    DEFENDER_GUID = table.Column<Guid>(type: "TEXT", nullable: false),
                    DEFENDER_HEALTH = table.Column<float>(type: "REAL", nullable: false),
                    DEFENDER_MAX_HEALTH = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROUNDS", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ROUNDS",
                schema: "ar");
        }
    }
}
