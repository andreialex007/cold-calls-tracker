using Microsoft.EntityFrameworkCore.Migrations;

namespace ColdCallsTracker.Migrations
{
    public partial class CostingsRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Costing_Quotes_QuoteId",
                table: "Costing");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Costing",
                table: "Costing");

            migrationBuilder.RenameTable(
                name: "Costing",
                newName: "Costings");

            migrationBuilder.RenameIndex(
                name: "IX_Costing_QuoteId",
                table: "Costings",
                newName: "IX_Costings_QuoteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Costings",
                table: "Costings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Costings_Quotes_QuoteId",
                table: "Costings",
                column: "QuoteId",
                principalTable: "Quotes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Costings_Quotes_QuoteId",
                table: "Costings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Costings",
                table: "Costings");

            migrationBuilder.RenameTable(
                name: "Costings",
                newName: "Costing");

            migrationBuilder.RenameIndex(
                name: "IX_Costings_QuoteId",
                table: "Costing",
                newName: "IX_Costing_QuoteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Costing",
                table: "Costing",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Costing_Quotes_QuoteId",
                table: "Costing",
                column: "QuoteId",
                principalTable: "Quotes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
