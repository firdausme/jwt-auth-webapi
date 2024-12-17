using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JwtAuthWebApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    username = table.Column<string>(type: "varchar(100)", nullable: false),
                    password = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_details",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    full_name = table.Column<string>(type: "varchar(100)", nullable: false),
                    date_of_birth = table.Column<DateTime>(type: "date", nullable: false),
                    user_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_details", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_details_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "password", "username" },
                values: new object[,]
                {
                    { 1, "$2a$11$ycHRAsTG3Oj1UZfY4D19g.G4CUrXUpj7fXbqlHZcweU0SjmFZmp/e", "adi" },
                    { 2, "$2a$11$AT3zOO4jNjnYY5DlSeSRW.idzdZ7z.KWyDhhjnjmWQDdTj0wbi4r6", "budi" },
                    { 3, "$2a$11$ukCGM.Gmu0Fv9om5Ee3H6ewK5TMKGFhQlLJuGUY1Ywz/HqPWpVmY.", "caca" }
                });

            migrationBuilder.InsertData(
                table: "user_details",
                columns: new[] { "id", "date_of_birth", "full_name", "user_id" },
                values: new object[,]
                {
                    { 1, new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Adi Wibowo", 1 },
                    { 2, new DateTime(2002, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Budi Darwan", 2 },
                    { 3, new DateTime(1998, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Caca Handika", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "ix_user_details_user_id",
                table: "user_details",
                column: "user_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_details");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
