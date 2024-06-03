using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flashy.Migrations
{
    /// <inheritdoc />
    public partial class FixModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "dateCreated",
                table: "Sets",
                newName: "DateCreated");

            migrationBuilder.CreateIndex(
                name: "IX_Flashcards_SetId",
                table: "Flashcards",
                column: "SetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flashcards_Sets_SetId",
                table: "Flashcards",
                column: "SetId",
                principalTable: "Sets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flashcards_Sets_SetId",
                table: "Flashcards");

            migrationBuilder.DropIndex(
                name: "IX_Flashcards_SetId",
                table: "Flashcards");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "Sets",
                newName: "dateCreated");
        }
    }
}
