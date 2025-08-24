using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BACKEND.Migrations
{
    /// <inheritdoc />
    public partial class DodajSlikaUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Analiticari",
                columns: table => new
                {
                    Sifra = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kontakt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StrucnaSprema = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SlikaUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Analiticari", x => x.Sifra);
                });

            migrationBuilder.CreateTable(
                name: "Lokacije",
                columns: table => new
                {
                    Sifra = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MjestoUzorkovanja = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lokacije", x => x.Sifra);
                });

            migrationBuilder.CreateTable(
                name: "operateri",
                columns: table => new
                {
                    Sifra = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lozinka = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_operateri", x => x.Sifra);
                });

            migrationBuilder.CreateTable(
                name: "uzorcitla",
                columns: table => new
                {
                    Sifra = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    masauzorka = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    vrstatla = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    datum = table.Column<DateTime>(type: "datetime2", nullable: true),
                    lokacija = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_uzorcitla", x => x.Sifra);
                    table.ForeignKey(
                        name: "FK_uzorcitla_Lokacije_lokacija",
                        column: x => x.lokacija,
                        principalTable: "Lokacije",
                        principalColumn: "Sifra",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Analize",
                columns: table => new
                {
                    Sifra = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: true),
                    uzoraktla = table.Column<int>(type: "int", nullable: false),
                    analiticar = table.Column<int>(type: "int", nullable: false),
                    pHVrijednost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Fosfor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Kalij = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Magnezij = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Karbonati = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Humus = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Analize", x => x.Sifra);
                    table.ForeignKey(
                        name: "FK_Analize_Analiticari_analiticar",
                        column: x => x.analiticar,
                        principalTable: "Analiticari",
                        principalColumn: "Sifra",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Analize_uzorcitla_uzoraktla",
                        column: x => x.uzoraktla,
                        principalTable: "uzorcitla",
                        principalColumn: "Sifra",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Analize_analiticar",
                table: "Analize",
                column: "analiticar");

            migrationBuilder.CreateIndex(
                name: "IX_Analize_uzoraktla",
                table: "Analize",
                column: "uzoraktla");

            migrationBuilder.CreateIndex(
                name: "IX_uzorcitla_lokacija",
                table: "uzorcitla",
                column: "lokacija");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Analize");

            migrationBuilder.DropTable(
                name: "operateri");

            migrationBuilder.DropTable(
                name: "Analiticari");

            migrationBuilder.DropTable(
                name: "uzorcitla");

            migrationBuilder.DropTable(
                name: "Lokacije");
        }
    }
}
