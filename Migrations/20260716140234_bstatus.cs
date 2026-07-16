using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoMeal.Migrations
{
    /// <inheritdoc />
    public partial class bstatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BusinessStatusUid",
                table: "Businesses",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Businesses",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("08dee248-76ba-4892-84e2-33ba384fe31d"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "BusinessStatus",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessStatus", x => x.Uid);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_BusinessStatusUid",
                table: "Businesses",
                column: "BusinessStatusUid");

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_OwnerId",
                table: "Businesses",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Businesses_AspNetUsers_OwnerId",
                table: "Businesses",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Businesses_BusinessStatus_BusinessStatusUid",
                table: "Businesses",
                column: "BusinessStatusUid",
                principalTable: "BusinessStatus",
                principalColumn: "Uid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Businesses_AspNetUsers_OwnerId",
                table: "Businesses");

            migrationBuilder.DropForeignKey(
                name: "FK_Businesses_BusinessStatus_BusinessStatusUid",
                table: "Businesses");

            migrationBuilder.DropTable(
                name: "BusinessStatus");

            migrationBuilder.DropIndex(
                name: "IX_Businesses_BusinessStatusUid",
                table: "Businesses");

            migrationBuilder.DropIndex(
                name: "IX_Businesses_OwnerId",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "BusinessStatusUid",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Businesses");
        }
    }
}
