using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arena.Persistence.Migrations
{
    public partial class InitialCreate3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ARENA_GUID",
                schema: "ar",
                table: "ROUNDS",
                type: "nvarchar(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ROUNDS_ARENA_GUID",
                schema: "ar",
                table: "ROUNDS",
                column: "ARENA_GUID");

            migrationBuilder.AddForeignKey(
                name: "FK_ROUNDS_Arena_ARENA_GUID",
                schema: "ar",
                table: "ROUNDS",
                column: "ARENA_GUID",
                principalSchema: "ar",
                principalTable: "Arena",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ROUNDS_Arena_ARENA_GUID",
                schema: "ar",
                table: "ROUNDS");

            migrationBuilder.DropIndex(
                name: "IX_ROUNDS_ARENA_GUID",
                schema: "ar",
                table: "ROUNDS");

            migrationBuilder.DropColumn(
                name: "ARENA_GUID",
                schema: "ar",
                table: "ROUNDS");
        }
    }
}
