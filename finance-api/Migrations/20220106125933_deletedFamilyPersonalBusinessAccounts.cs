using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace finance_api.Migrations
{
    public partial class deletedFamilyPersonalBusinessAccounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_FamilyAccounts_FamilyAccountId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_PersonalAccounts_PersonalAccountId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "BusinessAccounts");

            migrationBuilder.DropTable(
                name: "FamilyAccounts");

            migrationBuilder.DropTable(
                name: "PersonalAccounts");

            migrationBuilder.DropIndex(
                name: "IX_Users_FamilyAccountId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PersonalAccountId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FamilyAccountId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PersonalAccountId",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FamilyAccountId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PersonalAccountId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "BusinessAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessAccounts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FamilyAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonalAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalAccounts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_FamilyAccountId",
                table: "Users",
                column: "FamilyAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PersonalAccountId",
                table: "Users",
                column: "PersonalAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessAccounts_UserId",
                table: "BusinessAccounts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_FamilyAccounts_FamilyAccountId",
                table: "Users",
                column: "FamilyAccountId",
                principalTable: "FamilyAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_PersonalAccounts_PersonalAccountId",
                table: "Users",
                column: "PersonalAccountId",
                principalTable: "PersonalAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
