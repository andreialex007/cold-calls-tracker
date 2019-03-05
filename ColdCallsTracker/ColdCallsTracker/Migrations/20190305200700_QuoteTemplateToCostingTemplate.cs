using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ColdCallsTracker.Migrations
{
    public partial class QuoteTemplateToCostingTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuoteTemplates",
                columns: table => new
                {
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateModify = table.Column<DateTime>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuoteTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuoteTemplateCostingTemplate",
                columns: table => new
                {
                    CostingTemplateId = table.Column<int>(nullable: false),
                    QuoteTemplateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuoteTemplateCostingTemplate", x => new { x.CostingTemplateId, x.QuoteTemplateId });
                    table.ForeignKey(
                        name: "FK_QuoteTemplateCostingTemplate_CostingTemplates_CostingTemplateId",
                        column: x => x.CostingTemplateId,
                        principalTable: "CostingTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuoteTemplateCostingTemplate_QuoteTemplates_QuoteTemplateId",
                        column: x => x.QuoteTemplateId,
                        principalTable: "QuoteTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuoteTemplateCostingTemplate_QuoteTemplateId",
                table: "QuoteTemplateCostingTemplate",
                column: "QuoteTemplateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuoteTemplateCostingTemplate");

            migrationBuilder.DropTable(
                name: "QuoteTemplates");
        }
    }
}
