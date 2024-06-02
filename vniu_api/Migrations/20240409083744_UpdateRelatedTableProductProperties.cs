using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vniu_api.Migrations
{
    public partial class UpdateRelatedTableProductProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductItemId",
                table: "OrderLine",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductItemId",
                table: "CartItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            //migrationBuilder.CreateTable(
            //    name: "Colour",
            //    columns: table => new
            //    {
            //        ColourId = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        ColourName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Colour", x => x.ColourId);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ProductCategory",
            //    columns: table => new
            //    {
            //        ProductCategoryId = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        ProductCategoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            //        ParentCategoryId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ProductCategory", x => x.ProductCategoryId);
            //        table.ForeignKey(
            //            name: "FK_ProductCategory_ProductCategory_ParentCategoryId",
            //            column: x => x.ParentCategoryId,
            //            principalTable: "ProductCategory",
            //            principalColumn: "ProductCategoryId");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "SizeOption",
            //    columns: table => new
            //    {
            //        SizeId = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        SizeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            //        SortOrder = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_SizeOption", x => x.SizeId);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Product",
            //    columns: table => new
            //    {
            //        ProductId = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        ProductName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            //        ProductDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
            //        ProductCategoryId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Product", x => x.ProductId);
            //        table.ForeignKey(
            //            name: "FK_Product_ProductCategory_ProductCategoryId",
            //            column: x => x.ProductCategoryId,
            //            principalTable: "ProductCategory",
            //            principalColumn: "ProductCategoryId",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            migrationBuilder.CreateTable(
                name: "PromotionCategory",
                columns: table => new
                {
                    PromotionId = table.Column<int>(type: "int", nullable: false),
                    ProductCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionCategory", x => new { x.PromotionId, x.ProductCategoryId });
                    table.ForeignKey(
                        name: "FK_PromotionCategory_ProductCategory_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "ProductCategory",
                        principalColumn: "ProductCategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromotionCategory_Promotion_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotion",
                        principalColumn: "PromotionId",
                        onDelete: ReferentialAction.Cascade);
                });

            //migrationBuilder.CreateTable(
            //    name: "ProductItem",
            //    columns: table => new
            //    {
            //        ProductItemId = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        OriginalPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
            //        SalePrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
            //        ProductItemSold = table.Column<int>(type: "int", nullable: false),
            //        ProductItemRating = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
            //        ProductItemCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ProductId = table.Column<int>(type: "int", nullable: false),
            //        ColourId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ProductItem", x => x.ProductItemId);
            //        table.ForeignKey(
            //            name: "FK_ProductItem_Colour_ColourId",
            //            column: x => x.ColourId,
            //            principalTable: "Colour",
            //            principalColumn: "ColourId",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_ProductItem_Product_ProductId",
            //            column: x => x.ProductId,
            //            principalTable: "Product",
            //            principalColumn: "ProductId",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ProductImage",
            //    columns: table => new
            //    {
            //        ProductImageId = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        ProductImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
            //        ProductItemId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ProductImage", x => x.ProductImageId);
            //        table.ForeignKey(
            //            name: "FK_ProductImage_ProductItem_ProductItemId",
            //            column: x => x.ProductItemId,
            //            principalTable: "ProductItem",
            //            principalColumn: "ProductItemId",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Variation",
            //    columns: table => new
            //    {
            //        VariationId = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        QuantityInStock = table.Column<int>(type: "int", nullable: false),
            //        ProductItemId = table.Column<int>(type: "int", nullable: false),
            //        SizeId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Variation", x => x.VariationId);
            //        table.ForeignKey(
            //            name: "FK_Variation_ProductItem_ProductItemId",
            //            column: x => x.ProductItemId,
            //            principalTable: "ProductItem",
            //            principalColumn: "ProductItemId",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_Variation_SizeOption_SizeId",
            //            column: x => x.SizeId,
            //            principalTable: "SizeOption",
            //            principalColumn: "SizeId",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            migrationBuilder.CreateIndex(
                name: "IX_OrderLine_ProductItemId",
                table: "OrderLine",
                column: "ProductItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_ProductItemId",
                table: "CartItem",
                column: "ProductItemId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Product_ProductCategoryId",
            //    table: "Product",
            //    column: "ProductCategoryId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ProductCategory_ParentCategoryId",
            //    table: "ProductCategory",
            //    column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_ProductItemId",
                table: "ProductImage",
                column: "ProductItemId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ProductItem_ColourId",
            //    table: "ProductItem",
            //    column: "ColourId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ProductItem_ProductId",
            //    table: "ProductItem",
            //    column: "ProductId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_PromotionCategory_ProductCategoryId",
            //    table: "PromotionCategory",
            //    column: "ProductCategoryId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Variation_ProductItemId",
            //    table: "Variation",
            //    column: "ProductItemId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Variation_SizeId",
            //    table: "Variation",
            //    column: "SizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_ProductItem_ProductItemId",
                table: "CartItem",
                column: "ProductItemId",
                principalTable: "ProductItem",
                principalColumn: "ProductItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLine_ProductItem_ProductItemId",
                table: "OrderLine",
                column: "ProductItemId",
                principalTable: "ProductItem",
                principalColumn: "ProductItemId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_ProductItem_ProductItemId",
                table: "CartItem");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderLine_ProductItem_ProductItemId",
                table: "OrderLine");

            migrationBuilder.DropTable(
                name: "ProductImage");

            migrationBuilder.DropTable(
                name: "PromotionCategory");

            migrationBuilder.DropTable(
                name: "Variation");

            migrationBuilder.DropTable(
                name: "ProductItem");

            migrationBuilder.DropTable(
                name: "SizeOption");

            migrationBuilder.DropTable(
                name: "Colour");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "ProductCategory");

            migrationBuilder.DropIndex(
                name: "IX_OrderLine_ProductItemId",
                table: "OrderLine");

            migrationBuilder.DropIndex(
                name: "IX_CartItem_ProductItemId",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "ProductItemId",
                table: "OrderLine");

            migrationBuilder.DropColumn(
                name: "ProductItemId",
                table: "CartItem");
        }
    }
}
