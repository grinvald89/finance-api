using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace finance_api.Migrations
{
    public partial class changeSecondOption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubCategoryFirstOptionId",
                table: "TransactionSubCategoryFirstOptions");

            migrationBuilder.AddColumn<Guid>(
                name: "SubCategoryFirstOptionId",
                table: "TransactionSubCategorySecondOptions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubCategoryFirstOptionId",
                table: "TransactionSubCategorySecondOptions");

            migrationBuilder.AddColumn<Guid>(
                name: "SubCategoryFirstOptionId",
                table: "TransactionSubCategoryFirstOptions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
