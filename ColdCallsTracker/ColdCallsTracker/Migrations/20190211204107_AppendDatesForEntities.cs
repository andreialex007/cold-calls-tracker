using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ColdCallsTracker.Migrations
{
    public partial class AppendDatesForEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreate",
                table: "Phones",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModify",
                table: "Phones",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreate",
                table: "Companies",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModify",
                table: "Companies",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreate",
                table: "CallRecords",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModify",
                table: "CallRecords",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreate",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "DateModify",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "DateCreate",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "DateModify",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "DateCreate",
                table: "CallRecords");

            migrationBuilder.DropColumn(
                name: "DateModify",
                table: "CallRecords");
        }
    }
}
