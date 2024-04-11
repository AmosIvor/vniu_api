using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vniu_api.Migrations
{
    public partial class UpdateReviewTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ReviewCreateAt",
                table: "Review",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ReviewUpdateAt",
                table: "Review",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReviewCreateAt",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "ReviewUpdateAt",
                table: "Review");
        }
    }
}
