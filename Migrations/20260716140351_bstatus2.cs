using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoMeal.Migrations
{
    /// <inheritdoc />
    public partial class bstatus2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Businesses_BusinessStatus_BusinessStatusUid",
                table: "Businesses");

            migrationBuilder.DropIndex(
                name: "IX_Businesses_BusinessStatusUid",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "BusinessStatusUid",
                table: "Businesses");

            migrationBuilder.AddColumn<Guid>(
                name: "StatusId",
                table: "Businesses",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("08dee248-76ba-4892-84e2-33ba384fe310"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_StatusId",
                table: "Businesses",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Businesses_BusinessStatus_StatusId",
                table: "Businesses",
                column: "StatusId",
                principalTable: "BusinessStatus",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Businesses_BusinessStatus_StatusId",
                table: "Businesses");

            migrationBuilder.DropIndex(
                name: "IX_Businesses_StatusId",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Businesses");

            migrationBuilder.AddColumn<Guid>(
                name: "BusinessStatusUid",
                table: "Businesses",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_BusinessStatusUid",
                table: "Businesses",
                column: "BusinessStatusUid");

            migrationBuilder.AddForeignKey(
                name: "FK_Businesses_BusinessStatus_BusinessStatusUid",
                table: "Businesses",
                column: "BusinessStatusUid",
                principalTable: "BusinessStatus",
                principalColumn: "Uid");
        }
    }
}
