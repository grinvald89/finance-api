using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace finance_api.Migrations
{
    public partial class addDeletedProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "UserRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "UserFullNames",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "UserAuthorizations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "TransactionTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "TransactionSubCategorySecondOptions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "TransactionSubCategoryFirstOptions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "TransactionSubCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "TransactionStatuses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Transactions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "TransactionCategoryOptions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "TransactionCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "PersonalAccounts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "FamilyAccounts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "BusinessAccounts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "UserFullNames");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "UserAuthorizations");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "TransactionTypes");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "TransactionSubCategorySecondOptions");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "TransactionSubCategoryFirstOptions");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "TransactionSubCategories");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "TransactionStatuses");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "TransactionCategoryOptions");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "TransactionCategories");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "PersonalAccounts");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "FamilyAccounts");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "BusinessAccounts");
        }
    }
}
