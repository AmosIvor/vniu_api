using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vniu_api.Migrations
{
    public partial class UpdateOrderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderDate",
                table: "Order",
                newName: "OrderUpdateAt");

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderCreateAt",
                table: "Order",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderCreateAt",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "OrderUpdateAt",
                table: "Order",
                newName: "OrderDate");
        }
    }
}
