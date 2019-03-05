using Microsoft.EntityFrameworkCore.Migrations;

namespace ColdCallsTracker.Migrations
{
    public partial class CompanyNullableInQuote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Companies_CompanyId",
                table: "Quotes");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "Quotes",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Companies_CompanyId",
                table: "Quotes",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Companies_CompanyId",
                table: "Quotes");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "Quotes",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Companies_CompanyId",
                table: "Quotes",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
