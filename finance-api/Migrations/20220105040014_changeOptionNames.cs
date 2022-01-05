using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace finance_api.Migrations
{
    public partial class changeOptionNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionCategoryOptions_OptionId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionSubCategoryFirstOptions_FirstOptionId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionSubCategorySecondOptions_SecondOptionId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "SecondOptionId",
                table: "Transactions",
                newName: "SubCategorySecondOptionId");

            migrationBuilder.RenameColumn(
                name: "OptionId",
                table: "Transactions",
                newName: "SubCategoryFirstOptionId");

            migrationBuilder.RenameColumn(
                name: "FirstOptionId",
                table: "Transactions",
                newName: "CategoryOptionId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_SecondOptionId",
                table: "Transactions",
                newName: "IX_Transactions_SubCategorySecondOptionId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_OptionId",
                table: "Transactions",
                newName: "IX_Transactions_SubCategoryFirstOptionId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_FirstOptionId",
                table: "Transactions",
                newName: "IX_Transactions_CategoryOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_TransactionCategoryOptions_CategoryOptionId",
                table: "Transactions",
                column: "CategoryOptionId",
                principalTable: "TransactionCategoryOptions",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionCategoryOptions_CategoryOptionId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionSubCategoryFirstOptions_SubCategoryFirstOptionId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionSubCategorySecondOptions_SubCategorySecondOptionId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "SubCategorySecondOptionId",
                table: "Transactions",
                newName: "SecondOptionId");

            migrationBuilder.RenameColumn(
                name: "SubCategoryFirstOptionId",
                table: "Transactions",
                newName: "OptionId");

            migrationBuilder.RenameColumn(
                name: "CategoryOptionId",
                table: "Transactions",
                newName: "FirstOptionId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_SubCategorySecondOptionId",
                table: "Transactions",
                newName: "IX_Transactions_SecondOptionId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_SubCategoryFirstOptionId",
                table: "Transactions",
                newName: "IX_Transactions_OptionId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_CategoryOptionId",
                table: "Transactions",
                newName: "IX_Transactions_FirstOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_TransactionCategoryOptions_OptionId",
                table: "Transactions",
                column: "OptionId",
                principalTable: "TransactionCategoryOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_TransactionSubCategoryFirstOptions_FirstOptionId",
                table: "Transactions",
                column: "FirstOptionId",
                principalTable: "TransactionSubCategoryFirstOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_TransactionSubCategorySecondOptions_SecondOptionId",
                table: "Transactions",
                column: "SecondOptionId",
                principalTable: "TransactionSubCategorySecondOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
