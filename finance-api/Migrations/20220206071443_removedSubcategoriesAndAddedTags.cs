using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace finance_api.Migrations
{
    public partial class removedSubcategoriesAndAddedTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionCategoryOptions_CategoryOptionId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionSubCategories_SubCategoryId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionSubCategoryFirstOptions_SubCategoryFirstOptionId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionSubCategorySecondOptions_SubCategorySecondOptionId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "TransactionCategoryOptions");

            migrationBuilder.DropTable(
                name: "TransactionSubCategories");

            migrationBuilder.DropTable(
                name: "TransactionSubCategoryFirstOptions");

            migrationBuilder.DropTable(
                name: "TransactionSubCategorySecondOptions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_CategoryOptionId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_SubCategoryFirstOptionId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_SubCategoryId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "CategoryOptionId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "SubCategoryFirstOptionId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "SubCategoryId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "SubCategorySecondOptionId",
                table: "Transactions",
                newName: "DirectionId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_SubCategorySecondOptionId",
                table: "Transactions",
                newName: "IX_Transactions_DirectionId");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "DirectionId",
                table: "TransactionCategories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "TransactionDirections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionDirections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionTags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionTags_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionTags_TransactionId",
                table: "TransactionTags",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_TransactionDirections_DirectionId",
                table: "Transactions",
                column: "DirectionId",
                principalTable: "TransactionDirections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionDirections_DirectionId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "TransactionDirections");

            migrationBuilder.DropTable(
                name: "TransactionTags");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "DirectionId",
                table: "TransactionCategories");

            migrationBuilder.RenameColumn(
                name: "DirectionId",
                table: "Transactions",
                newName: "SubCategorySecondOptionId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_DirectionId",
                table: "Transactions",
                newName: "IX_Transactions_SubCategorySecondOptionId");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryOptionId",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SubCategoryFirstOptionId",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SubCategoryId",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "TransactionCategoryOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionCategoryOptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionSubCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryOptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionSubCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionSubCategoryFirstOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionSubCategoryFirstOptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionSubCategorySecondOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubCategoryFirstOptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionSubCategorySecondOptions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CategoryOptionId",
                table: "Transactions",
                column: "CategoryOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_SubCategoryFirstOptionId",
                table: "Transactions",
                column: "SubCategoryFirstOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_SubCategoryId",
                table: "Transactions",
                column: "SubCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_TransactionCategoryOptions_CategoryOptionId",
                table: "Transactions",
                column: "CategoryOptionId",
                principalTable: "TransactionCategoryOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_TransactionSubCategories_SubCategoryId",
                table: "Transactions",
                column: "SubCategoryId",
                principalTable: "TransactionSubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_TransactionSubCategoryFirstOptions_SubCategoryFirstOptionId",
                table: "Transactions",
                column: "SubCategoryFirstOptionId",
                principalTable: "TransactionSubCategoryFirstOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_TransactionSubCategorySecondOptions_SubCategorySecondOptionId",
                table: "Transactions",
                column: "SubCategorySecondOptionId",
                principalTable: "TransactionSubCategorySecondOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
