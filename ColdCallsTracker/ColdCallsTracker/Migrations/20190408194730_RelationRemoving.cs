using Microsoft.EntityFrameworkCore.Migrations;

namespace ColdCallsTracker.Migrations
{
    public partial class RelationRemoving : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_States_StateId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_StateId",
                table: "Companies");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Companies_StateId",
                table: "Companies",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_States_StateId",
                table: "Companies",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
