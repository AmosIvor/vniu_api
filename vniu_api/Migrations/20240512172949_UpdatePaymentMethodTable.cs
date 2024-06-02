using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vniu_api.Migrations
{
    public partial class UpdatePaymentMethodTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentMethod_AspNetUsers_UserId",
                table: "PaymentMethod");

            migrationBuilder.DropIndex(
                name: "IX_PaymentMethod_UserId",
                table: "PaymentMethod");

            migrationBuilder.DropColumn(
                name: "AccountNumber",
                table: "PaymentMethod");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PaymentMethod");

            migrationBuilder.RenameColumn(
                name: "Provider",
                table: "PaymentMethod",
                newName: "PaymentProvider");

            migrationBuilder.RenameColumn(
                name: "ExpiryDate",
                table: "PaymentMethod",
                newName: "PaymentDate");

            migrationBuilder.AddColumn<string>(
                name: "PaymentCartType",
                table: "PaymentMethod",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentDescription",
                table: "PaymentMethod",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentStatus",
                table: "PaymentMethod",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentCartType",
                table: "PaymentMethod");

            migrationBuilder.DropColumn(
                name: "PaymentDescription",
                table: "PaymentMethod");

            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "PaymentMethod");

            migrationBuilder.RenameColumn(
                name: "PaymentProvider",
                table: "PaymentMethod",
                newName: "Provider");

            migrationBuilder.RenameColumn(
                name: "PaymentDate",
                table: "PaymentMethod",
                newName: "ExpiryDate");

            migrationBuilder.AddColumn<string>(
                name: "AccountNumber",
                table: "PaymentMethod",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "PaymentMethod",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethod_UserId",
                table: "PaymentMethod",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentMethod_AspNetUsers_UserId",
                table: "PaymentMethod",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
