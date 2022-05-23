using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PIAWebApi.Migrations
{
    public partial class FechaGanadores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaGanadores",
                table: "Participantes",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaGanadores",
                table: "Participantes");
        }
    }
}
