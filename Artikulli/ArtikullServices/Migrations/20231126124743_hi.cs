using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtikullServices.Migrations
{
    /// <inheritdoc />
    public partial class hi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cases",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emertimi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Njesia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataSkadences = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cmimi = table.Column<double>(type: "float", nullable: false),
                    Lloj = table.Column<int>(type: "int", nullable: false),
                    KaTvsh = table.Column<bool>(type: "bit", nullable: false),
                    Tipi = table.Column<int>(type: "int", nullable: false),
                    BarkodKod = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cases", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cases");
        }
    }
}
