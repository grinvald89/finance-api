using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace finance_api.Migrations
{
    public partial class _changeTransactionDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TransactionDate",
                table: "Transactions",
                newName: "Date");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Transactions",
                newName: "TransactionDate");
        }
    }
}
