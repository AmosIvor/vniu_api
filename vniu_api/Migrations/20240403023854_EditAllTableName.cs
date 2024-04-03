using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vniu_api.Migrations
{
    public partial class EditAllTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Carts_CartId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Carts_AspNetUsers_UserId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderLines_Orders_OrderId",
                table: "OrderLines");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Addresses_AddressID",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderStatuses_OrderStatusId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_PaymentMethods_PaymentMethodId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Promotions_PromotionId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ShippingMethods_ShippingMethodId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentMethods_AspNetUsers_UserId",
                table: "PaymentMethods");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentMethods_PaymentTypes_PaymentTypeId",
                table: "PaymentMethods");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewImages_Reviews_ReviewId",
                table: "ReviewImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_UserId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_OrderLines_OrderLineId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAddresses_Addresses_AddressId",
                table: "UserAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAddresses_AspNetUsers_UserId",
                table: "UserAddresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAddresses",
                table: "UserAddresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShippingMethods",
                table: "ShippingMethods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReviewImages",
                table: "ReviewImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Promotions",
                table: "Promotions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentTypes",
                table: "PaymentTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentMethods",
                table: "PaymentMethods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderStatuses",
                table: "OrderStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderLines",
                table: "OrderLines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Carts",
                table: "Carts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartItems",
                table: "CartItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses");

            migrationBuilder.RenameTable(
                name: "UserAddresses",
                newName: "User_Address");

            migrationBuilder.RenameTable(
                name: "ShippingMethods",
                newName: "ShippingMethod");

            migrationBuilder.RenameTable(
                name: "Reviews",
                newName: "Review");

            migrationBuilder.RenameTable(
                name: "ReviewImages",
                newName: "ReviewImage");

            migrationBuilder.RenameTable(
                name: "Promotions",
                newName: "Promotion");

            migrationBuilder.RenameTable(
                name: "PaymentTypes",
                newName: "PaymentType");

            migrationBuilder.RenameTable(
                name: "PaymentMethods",
                newName: "PaymentMethod");

            migrationBuilder.RenameTable(
                name: "OrderStatuses",
                newName: "OrderStatus");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Order");

            migrationBuilder.RenameTable(
                name: "OrderLines",
                newName: "OrderLine");

            migrationBuilder.RenameTable(
                name: "Carts",
                newName: "Cart");

            migrationBuilder.RenameTable(
                name: "CartItems",
                newName: "CartItem");

            migrationBuilder.RenameTable(
                name: "Addresses",
                newName: "Address");

            migrationBuilder.RenameIndex(
                name: "IX_UserAddresses_AddressId",
                table: "User_Address",
                newName: "IX_User_Address_AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_UserId",
                table: "Review",
                newName: "IX_Review_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_OrderLineId",
                table: "Review",
                newName: "IX_Review_OrderLineId");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewImages_ReviewId",
                table: "ReviewImage",
                newName: "IX_ReviewImage_ReviewId");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Promotion",
                newName: "PromotionStartDate");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Promotion",
                newName: "PromotionName");

            migrationBuilder.RenameColumn(
                name: "IsUsed",
                table: "Promotion",
                newName: "PromotionIsUsed");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Promotion",
                newName: "PromotionEndDate");

            migrationBuilder.RenameColumn(
                name: "DiscountRate",
                table: "Promotion",
                newName: "PromotionDiscountRate");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Promotion",
                newName: "PromotionDescription");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Promotion",
                newName: "PromotionId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentMethods_UserId",
                table: "PaymentMethod",
                newName: "IX_PaymentMethod_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentMethods_PaymentTypeId",
                table: "PaymentMethod",
                newName: "IX_PaymentMethod_PaymentTypeId");

            migrationBuilder.RenameColumn(
                name: "AddressID",
                table: "Order",
                newName: "AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_UserId",
                table: "Order",
                newName: "IX_Order_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_ShippingMethodId",
                table: "Order",
                newName: "IX_Order_ShippingMethodId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_PromotionId",
                table: "Order",
                newName: "IX_Order_PromotionId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_PaymentMethodId",
                table: "Order",
                newName: "IX_Order_PaymentMethodId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_OrderStatusId",
                table: "Order",
                newName: "IX_Order_OrderStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_AddressID",
                table: "Order",
                newName: "IX_Order_AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderLines_OrderId",
                table: "OrderLine",
                newName: "IX_OrderLine_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Carts_UserId",
                table: "Cart",
                newName: "IX_Cart_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CartItems_CartId",
                table: "CartItem",
                newName: "IX_CartItem_CartId");

            migrationBuilder.RenameColumn(
                name: "AddressID",
                table: "Address",
                newName: "AddressId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User_Address",
                table: "User_Address",
                columns: new[] { "UserId", "AddressId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShippingMethod",
                table: "ShippingMethod",
                column: "ShippingMethodId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Review",
                table: "Review",
                column: "ReviewId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReviewImage",
                table: "ReviewImage",
                column: "ReviewImageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Promotion",
                table: "Promotion",
                column: "PromotionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentType",
                table: "PaymentType",
                column: "PaymentTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentMethod",
                table: "PaymentMethod",
                column: "PaymentMethodId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderStatus",
                table: "OrderStatus",
                column: "OrderStatusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderLine",
                table: "OrderLine",
                column: "OrderLineId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cart",
                table: "Cart",
                column: "CartId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartItem",
                table: "CartItem",
                column: "CartItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Address",
                table: "Address",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_AspNetUsers_UserId",
                table: "Cart",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Cart_CartId",
                table: "CartItem",
                column: "CartId",
                principalTable: "Cart",
                principalColumn: "CartId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Address_AddressId",
                table: "Order",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AspNetUsers_UserId",
                table: "Order",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_OrderStatus_OrderStatusId",
                table: "Order",
                column: "OrderStatusId",
                principalTable: "OrderStatus",
                principalColumn: "OrderStatusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_PaymentMethod_PaymentMethodId",
                table: "Order",
                column: "PaymentMethodId",
                principalTable: "PaymentMethod",
                principalColumn: "PaymentMethodId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Promotion_PromotionId",
                table: "Order",
                column: "PromotionId",
                principalTable: "Promotion",
                principalColumn: "PromotionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_ShippingMethod_ShippingMethodId",
                table: "Order",
                column: "ShippingMethodId",
                principalTable: "ShippingMethod",
                principalColumn: "ShippingMethodId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLine_Order_OrderId",
                table: "OrderLine",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentMethod_AspNetUsers_UserId",
                table: "PaymentMethod",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentMethod_PaymentType_PaymentTypeId",
                table: "PaymentMethod",
                column: "PaymentTypeId",
                principalTable: "PaymentType",
                principalColumn: "PaymentTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_AspNetUsers_UserId",
                table: "Review",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_OrderLine_OrderLineId",
                table: "Review",
                column: "OrderLineId",
                principalTable: "OrderLine",
                principalColumn: "OrderLineId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewImage_Review_ReviewId",
                table: "ReviewImage",
                column: "ReviewId",
                principalTable: "Review",
                principalColumn: "ReviewId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Address_Address_AddressId",
                table: "User_Address",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Address_AspNetUsers_UserId",
                table: "User_Address",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_AspNetUsers_UserId",
                table: "Cart");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Cart_CartId",
                table: "CartItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Address_AddressId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_AspNetUsers_UserId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_OrderStatus_OrderStatusId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_PaymentMethod_PaymentMethodId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Promotion_PromotionId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_ShippingMethod_ShippingMethodId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderLine_Order_OrderId",
                table: "OrderLine");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentMethod_AspNetUsers_UserId",
                table: "PaymentMethod");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentMethod_PaymentType_PaymentTypeId",
                table: "PaymentMethod");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_AspNetUsers_UserId",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_OrderLine_OrderLineId",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewImage_Review_ReviewId",
                table: "ReviewImage");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Address_Address_AddressId",
                table: "User_Address");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Address_AspNetUsers_UserId",
                table: "User_Address");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User_Address",
                table: "User_Address");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShippingMethod",
                table: "ShippingMethod");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReviewImage",
                table: "ReviewImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Review",
                table: "Review");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Promotion",
                table: "Promotion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentType",
                table: "PaymentType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentMethod",
                table: "PaymentMethod");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderStatus",
                table: "OrderStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderLine",
                table: "OrderLine");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartItem",
                table: "CartItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cart",
                table: "Cart");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Address",
                table: "Address");

            migrationBuilder.RenameTable(
                name: "User_Address",
                newName: "UserAddresses");

            migrationBuilder.RenameTable(
                name: "ShippingMethod",
                newName: "ShippingMethods");

            migrationBuilder.RenameTable(
                name: "ReviewImage",
                newName: "ReviewImages");

            migrationBuilder.RenameTable(
                name: "Review",
                newName: "Reviews");

            migrationBuilder.RenameTable(
                name: "Promotion",
                newName: "Promotions");

            migrationBuilder.RenameTable(
                name: "PaymentType",
                newName: "PaymentTypes");

            migrationBuilder.RenameTable(
                name: "PaymentMethod",
                newName: "PaymentMethods");

            migrationBuilder.RenameTable(
                name: "OrderStatus",
                newName: "OrderStatuses");

            migrationBuilder.RenameTable(
                name: "OrderLine",
                newName: "OrderLines");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Orders");

            migrationBuilder.RenameTable(
                name: "CartItem",
                newName: "CartItems");

            migrationBuilder.RenameTable(
                name: "Cart",
                newName: "Carts");

            migrationBuilder.RenameTable(
                name: "Address",
                newName: "Addresses");

            migrationBuilder.RenameIndex(
                name: "IX_User_Address_AddressId",
                table: "UserAddresses",
                newName: "IX_UserAddresses_AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewImage_ReviewId",
                table: "ReviewImages",
                newName: "IX_ReviewImages_ReviewId");

            migrationBuilder.RenameIndex(
                name: "IX_Review_UserId",
                table: "Reviews",
                newName: "IX_Reviews_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Review_OrderLineId",
                table: "Reviews",
                newName: "IX_Reviews_OrderLineId");

            migrationBuilder.RenameColumn(
                name: "PromotionStartDate",
                table: "Promotions",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "PromotionName",
                table: "Promotions",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "PromotionIsUsed",
                table: "Promotions",
                newName: "IsUsed");

            migrationBuilder.RenameColumn(
                name: "PromotionEndDate",
                table: "Promotions",
                newName: "EndDate");

            migrationBuilder.RenameColumn(
                name: "PromotionDiscountRate",
                table: "Promotions",
                newName: "DiscountRate");

            migrationBuilder.RenameColumn(
                name: "PromotionDescription",
                table: "Promotions",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "PromotionId",
                table: "Promotions",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentMethod_UserId",
                table: "PaymentMethods",
                newName: "IX_PaymentMethods_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentMethod_PaymentTypeId",
                table: "PaymentMethods",
                newName: "IX_PaymentMethods_PaymentTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderLine_OrderId",
                table: "OrderLines",
                newName: "IX_OrderLines_OrderId");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "Orders",
                newName: "AddressID");

            migrationBuilder.RenameIndex(
                name: "IX_Order_UserId",
                table: "Orders",
                newName: "IX_Orders_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_ShippingMethodId",
                table: "Orders",
                newName: "IX_Orders_ShippingMethodId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_PromotionId",
                table: "Orders",
                newName: "IX_Orders_PromotionId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_PaymentMethodId",
                table: "Orders",
                newName: "IX_Orders_PaymentMethodId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_OrderStatusId",
                table: "Orders",
                newName: "IX_Orders_OrderStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_AddressId",
                table: "Orders",
                newName: "IX_Orders_AddressID");

            migrationBuilder.RenameIndex(
                name: "IX_CartItem_CartId",
                table: "CartItems",
                newName: "IX_CartItems_CartId");

            migrationBuilder.RenameIndex(
                name: "IX_Cart_UserId",
                table: "Carts",
                newName: "IX_Carts_UserId");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "Addresses",
                newName: "AddressID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAddresses",
                table: "UserAddresses",
                columns: new[] { "UserId", "AddressId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShippingMethods",
                table: "ShippingMethods",
                column: "ShippingMethodId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReviewImages",
                table: "ReviewImages",
                column: "ReviewImageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                column: "ReviewId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Promotions",
                table: "Promotions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentTypes",
                table: "PaymentTypes",
                column: "PaymentTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentMethods",
                table: "PaymentMethods",
                column: "PaymentMethodId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderStatuses",
                table: "OrderStatuses",
                column: "OrderStatusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderLines",
                table: "OrderLines",
                column: "OrderLineId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartItems",
                table: "CartItems",
                column: "CartItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carts",
                table: "Carts",
                column: "CartId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses",
                column: "AddressID");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Carts_CartId",
                table: "CartItems",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "CartId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_AspNetUsers_UserId",
                table: "Carts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLines_Orders_OrderId",
                table: "OrderLines",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Addresses_AddressID",
                table: "Orders",
                column: "AddressID",
                principalTable: "Addresses",
                principalColumn: "AddressID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderStatuses_OrderStatusId",
                table: "Orders",
                column: "OrderStatusId",
                principalTable: "OrderStatuses",
                principalColumn: "OrderStatusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_PaymentMethods_PaymentMethodId",
                table: "Orders",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "PaymentMethodId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Promotions_PromotionId",
                table: "Orders",
                column: "PromotionId",
                principalTable: "Promotions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ShippingMethods_ShippingMethodId",
                table: "Orders",
                column: "ShippingMethodId",
                principalTable: "ShippingMethods",
                principalColumn: "ShippingMethodId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentMethods_AspNetUsers_UserId",
                table: "PaymentMethods",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentMethods_PaymentTypes_PaymentTypeId",
                table: "PaymentMethods",
                column: "PaymentTypeId",
                principalTable: "PaymentTypes",
                principalColumn: "PaymentTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewImages_Reviews_ReviewId",
                table: "ReviewImages",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "ReviewId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_UserId",
                table: "Reviews",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_OrderLines_OrderLineId",
                table: "Reviews",
                column: "OrderLineId",
                principalTable: "OrderLines",
                principalColumn: "OrderLineId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAddresses_Addresses_AddressId",
                table: "UserAddresses",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "AddressID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAddresses_AspNetUsers_UserId",
                table: "UserAddresses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
