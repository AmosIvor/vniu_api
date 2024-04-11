using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vniu_api.Migrations
{
    public partial class AddVariationForeignKeyOrderLineAndCartItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VariationId",
                table: "OrderLine",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VariationId",
                table: "CartItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderLine_VariationId",
                table: "OrderLine",
                column: "VariationId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_VariationId",
                table: "CartItem",
                column: "VariationId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Variation_VariationId",
                table: "CartItem",
                column: "VariationId",
                principalTable: "Variation",
                principalColumn: "VariationId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLine_Variation_VariationId",
                table: "OrderLine",
                column: "VariationId",
                principalTable: "Variation",
                principalColumn: "VariationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Variation_VariationId",
                table: "CartItem");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderLine_Variation_VariationId",
                table: "OrderLine");

            migrationBuilder.DropIndex(
                name: "IX_OrderLine_VariationId",
                table: "OrderLine");

            migrationBuilder.DropIndex(
                name: "IX_CartItem_VariationId",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "VariationId",
                table: "OrderLine");

            migrationBuilder.DropColumn(
                name: "VariationId",
                table: "CartItem");
        }
    }
}
