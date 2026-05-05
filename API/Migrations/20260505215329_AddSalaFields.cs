using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AddSalaFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SALAS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Uuid = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    MaxPlayers = table.Column<int>(type: "integer", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SALAS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DrawnNumbers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    DrawnAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    SalaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrawnNumbers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DrawnNumbers_SALAS_SalaId",
                        column: x => x.SalaId,
                        principalTable: "SALAS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SALAS_CARTELAS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SalaUuid = table.Column<Guid>(type: "uuid", nullable: false),
                    PlayerUuid = table.Column<Guid>(type: "uuid", nullable: false),
                    CardJson = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    SalaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SALAS_CARTELAS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SALAS_CARTELAS_SALAS_SalaId",
                        column: x => x.SalaId,
                        principalTable: "SALAS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DrawnNumbers_SalaId",
                table: "DrawnNumbers",
                column: "SalaId");

            migrationBuilder.CreateIndex(
                name: "IX_SALAS_Uuid",
                table: "SALAS",
                column: "Uuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SALAS_CARTELAS_SalaId",
                table: "SALAS_CARTELAS",
                column: "SalaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DrawnNumbers");

            migrationBuilder.DropTable(
                name: "SALAS_CARTELAS");

            migrationBuilder.DropTable(
                name: "SALAS");
        }
    }
}
