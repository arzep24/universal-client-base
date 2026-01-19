using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversalClientBase.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSeguimientoDinamico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaVencimiento",
                table: "Sanciones");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaUltimoEstatus",
                table: "Sanciones",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaUltimoEstatus",
                table: "Sanciones");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaVencimiento",
                table: "Sanciones",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
