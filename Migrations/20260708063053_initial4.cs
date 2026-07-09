using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoMeal.Migrations
{
    /// <inheritdoc />
    public partial class initial4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Business_BusinessType_BusinessTypeId",
                table: "Business");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Business_BusinessId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Status_StatusId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_User_UserId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderPackage_Order_OrderId",
                table: "OrderPackage");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderPackage_Package_PackageId",
                table: "OrderPackage");

            migrationBuilder.DropForeignKey(
                name: "FK_Package_Business_BusinessId",
                table: "Package");

            migrationBuilder.DropForeignKey(
                name: "FK_Package_PackageType_PackageTypeId",
                table: "Package");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Role_RoleId",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Status",
                table: "Status");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                table: "Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PackageType",
                table: "PackageType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Package",
                table: "Package");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderPackage",
                table: "OrderPackage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BusinessType",
                table: "BusinessType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Business",
                table: "Business");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Status",
                newName: "Statuses");

            migrationBuilder.RenameTable(
                name: "Role",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "PackageType",
                newName: "PackageTypes");

            migrationBuilder.RenameTable(
                name: "Package",
                newName: "Packages");

            migrationBuilder.RenameTable(
                name: "OrderPackage",
                newName: "OrderPackages");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Orders");

            migrationBuilder.RenameTable(
                name: "BusinessType",
                newName: "BusinessTypes");

            migrationBuilder.RenameTable(
                name: "Business",
                newName: "Businesses");

            migrationBuilder.RenameIndex(
                name: "IX_User_RoleId",
                table: "Users",
                newName: "IX_Users_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Package_PackageTypeId",
                table: "Packages",
                newName: "IX_Packages_PackageTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Package_BusinessId",
                table: "Packages",
                newName: "IX_Packages_BusinessId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderPackage_PackageId",
                table: "OrderPackages",
                newName: "IX_OrderPackages_PackageId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_UserId",
                table: "Orders",
                newName: "IX_Orders_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_StatusId",
                table: "Orders",
                newName: "IX_Orders_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_BusinessId",
                table: "Orders",
                newName: "IX_Orders_BusinessId");

            migrationBuilder.RenameIndex(
                name: "IX_Business_BusinessTypeId",
                table: "Businesses",
                newName: "IX_Businesses_BusinessTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Uid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Statuses",
                table: "Statuses",
                column: "Uid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Uid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PackageTypes",
                table: "PackageTypes",
                column: "Uid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Packages",
                table: "Packages",
                column: "Uid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderPackages",
                table: "OrderPackages",
                columns: new[] { "OrderId", "PackageId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Uid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BusinessTypes",
                table: "BusinessTypes",
                column: "Uid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Businesses",
                table: "Businesses",
                column: "Uid");

            migrationBuilder.AddForeignKey(
                name: "FK_Businesses_BusinessTypes_BusinessTypeId",
                table: "Businesses",
                column: "BusinessTypeId",
                principalTable: "BusinessTypes",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderPackages_Orders_OrderId",
                table: "OrderPackages",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderPackages_Packages_PackageId",
                table: "OrderPackages",
                column: "PackageId",
                principalTable: "Packages",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Businesses_BusinessId",
                table: "Orders",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Statuses_StatusId",
                table: "Orders",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_Businesses_BusinessId",
                table: "Packages",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_PackageTypes_PackageTypeId",
                table: "Packages",
                column: "PackageTypeId",
                principalTable: "PackageTypes",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Businesses_BusinessTypes_BusinessTypeId",
                table: "Businesses");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderPackages_Orders_OrderId",
                table: "OrderPackages");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderPackages_Packages_PackageId",
                table: "OrderPackages");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Businesses_BusinessId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Statuses_StatusId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Packages_Businesses_BusinessId",
                table: "Packages");

            migrationBuilder.DropForeignKey(
                name: "FK_Packages_PackageTypes_PackageTypeId",
                table: "Packages");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Statuses",
                table: "Statuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PackageTypes",
                table: "PackageTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Packages",
                table: "Packages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderPackages",
                table: "OrderPackages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BusinessTypes",
                table: "BusinessTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Businesses",
                table: "Businesses");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Statuses",
                newName: "Status");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "Role");

            migrationBuilder.RenameTable(
                name: "PackageTypes",
                newName: "PackageType");

            migrationBuilder.RenameTable(
                name: "Packages",
                newName: "Package");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Order");

            migrationBuilder.RenameTable(
                name: "OrderPackages",
                newName: "OrderPackage");

            migrationBuilder.RenameTable(
                name: "BusinessTypes",
                newName: "BusinessType");

            migrationBuilder.RenameTable(
                name: "Businesses",
                newName: "Business");

            migrationBuilder.RenameIndex(
                name: "IX_Users_RoleId",
                table: "User",
                newName: "IX_User_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Packages_PackageTypeId",
                table: "Package",
                newName: "IX_Package_PackageTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Packages_BusinessId",
                table: "Package",
                newName: "IX_Package_BusinessId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_UserId",
                table: "Order",
                newName: "IX_Order_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_StatusId",
                table: "Order",
                newName: "IX_Order_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_BusinessId",
                table: "Order",
                newName: "IX_Order_BusinessId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderPackages_PackageId",
                table: "OrderPackage",
                newName: "IX_OrderPackage_PackageId");

            migrationBuilder.RenameIndex(
                name: "IX_Businesses_BusinessTypeId",
                table: "Business",
                newName: "IX_Business_BusinessTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Uid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Status",
                table: "Status",
                column: "Uid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                table: "Role",
                column: "Uid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PackageType",
                table: "PackageType",
                column: "Uid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Package",
                table: "Package",
                column: "Uid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "Uid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderPackage",
                table: "OrderPackage",
                columns: new[] { "OrderId", "PackageId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_BusinessType",
                table: "BusinessType",
                column: "Uid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Business",
                table: "Business",
                column: "Uid");

            migrationBuilder.AddForeignKey(
                name: "FK_Business_BusinessType_BusinessTypeId",
                table: "Business",
                column: "BusinessTypeId",
                principalTable: "BusinessType",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Business_BusinessId",
                table: "Order",
                column: "BusinessId",
                principalTable: "Business",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Status_StatusId",
                table: "Order",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_User_UserId",
                table: "Order",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderPackage_Order_OrderId",
                table: "OrderPackage",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderPackage_Package_PackageId",
                table: "OrderPackage",
                column: "PackageId",
                principalTable: "Package",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Package_Business_BusinessId",
                table: "Package",
                column: "BusinessId",
                principalTable: "Business",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Package_PackageType_PackageTypeId",
                table: "Package",
                column: "PackageTypeId",
                principalTable: "PackageType",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role_RoleId",
                table: "User",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
