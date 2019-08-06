using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Panda2.Data.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Statuses",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Statuses",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Receipts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Receipts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Packages",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Packages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Packages");
        }
    }
}
