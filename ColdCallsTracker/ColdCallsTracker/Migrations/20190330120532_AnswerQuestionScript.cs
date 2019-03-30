using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ColdCallsTracker.Migrations
{
    public partial class AnswerQuestionScript : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CallScripts",
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
                    table.PrimaryKey("PK_CallScripts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CallQuestions",
                columns: table => new
                {
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateModify = table.Column<DateTime>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(nullable: true),
                    CallScriptId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CallQuestions_CallScripts_CallScriptId",
                        column: x => x.CallScriptId,
                        principalTable: "CallScripts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CallAnswers",
                columns: table => new
                {
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateModify = table.Column<DateTime>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(nullable: true),
                    FromQuestionId = table.Column<int>(nullable: true),
                    ToQuestionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CallAnswers_CallQuestions_FromQuestionId",
                        column: x => x.FromQuestionId,
                        principalTable: "CallQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CallAnswers_CallQuestions_ToQuestionId",
                        column: x => x.ToQuestionId,
                        principalTable: "CallQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CallAnswers_FromQuestionId",
                table: "CallAnswers",
                column: "FromQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_CallAnswers_ToQuestionId",
                table: "CallAnswers",
                column: "ToQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_CallQuestions_CallScriptId",
                table: "CallQuestions",
                column: "CallScriptId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CallAnswers");

            migrationBuilder.DropTable(
                name: "CallQuestions");

            migrationBuilder.DropTable(
                name: "CallScripts");
        }
    }
}
