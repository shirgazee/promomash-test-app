using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PromomashTestApp.Infrastructure.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "countries",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_countries", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "provinces",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    country_id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_provinces", x => x.id);
                    table.ForeignKey(
                        name: "FK_provinces_countries_country_id",
                        column: x => x.country_id,
                        principalTable: "countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "countries",
                columns: new[] { "id", "name" },
                values: new object[] { new Guid("250c8522-ee4d-42fa-ba06-97dc33f7f3af"), "Country 1" });

            migrationBuilder.InsertData(
                table: "countries",
                columns: new[] { "id", "name" },
                values: new object[] { new Guid("ef82605c-046c-403e-a7ec-f40d799f64f7"), "Country 2" });

            migrationBuilder.InsertData(
                table: "provinces",
                columns: new[] { "id", "country_id", "name" },
                values: new object[] { new Guid("09d8c1cf-9f0d-4c66-9d8b-109767076015"), new Guid("250c8522-ee4d-42fa-ba06-97dc33f7f3af"), "Provice 1.1" });

            migrationBuilder.InsertData(
                table: "provinces",
                columns: new[] { "id", "country_id", "name" },
                values: new object[] { new Guid("99f881e9-4207-47cb-8960-edd2361ce212"), new Guid("ef82605c-046c-403e-a7ec-f40d799f64f7"), "Provice 2.2" });

            migrationBuilder.InsertData(
                table: "provinces",
                columns: new[] { "id", "country_id", "name" },
                values: new object[] { new Guid("a35f5eb1-6782-4f47-9ef8-f004fcbe2269"), new Guid("ef82605c-046c-403e-a7ec-f40d799f64f7"), "Provice 2.1" });

            migrationBuilder.InsertData(
                table: "provinces",
                columns: new[] { "id", "country_id", "name" },
                values: new object[] { new Guid("f67558e4-3103-4e2e-ad25-91f2f666edb0"), new Guid("250c8522-ee4d-42fa-ba06-97dc33f7f3af"), "Provice 1.2" });

            migrationBuilder.CreateIndex(
                name: "IX_provinces_country_id",
                table: "provinces",
                column: "country_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "provinces");

            migrationBuilder.DropTable(
                name: "countries");
        }
    }
}
