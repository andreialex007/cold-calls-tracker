using Microsoft.EntityFrameworkCore.Migrations;

namespace ColdCallsTracker.Migrations
{
    public partial class QuoteTemplateDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "QuoteTemplates",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "QuoteTemplates");
        }
    }
}
