using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace finance_api.Migrations
{
    public partial class ChangeTypeForTransactionTadIdsOnString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TagIds",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TagIds",
                table: "Transactions");
        }
    }
}
