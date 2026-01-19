using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversalClientBase.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SistemaCobranzaInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.CreateTable(
                name: "Revisiones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Cuenta = table.Column<string>(type: "TEXT", nullable: false),
                    Responsable = table.Column<string>(type: "TEXT", nullable: false),
                    PersonalExterno = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Hallazgo = table.Column<string>(type: "TEXT", nullable: false),
                    Lectura = table.Column<int>(type: "INTEGER", nullable: false),
                    FechaRevision = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ProcesadaEnAutomatizacion = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Revisiones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sanciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Cuenta = table.Column<string>(type: "TEXT", nullable: false),
                    ImporteUMA = table.Column<decimal>(type: "TEXT", nullable: false),
                    UMAS = table.Column<int>(type: "INTEGER", nullable: false),
                    Importe = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Inciso = table.Column<int>(type: "INTEGER", nullable: false),
                    FechaSancion = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FechaVencimiento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Estatus = table.Column<string>(type: "TEXT", nullable: false),
                    Pagada = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sanciones", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Revisiones_Cuenta",
                table: "Revisiones",
                column: "Cuenta");

            migrationBuilder.CreateIndex(
                name: "IX_Sanciones_Cuenta",
                table: "Sanciones",
                column: "Cuenta");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Revisiones");

            migrationBuilder.DropTable(
                name: "Sanciones");

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    Phone = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });
        }
    }
}
