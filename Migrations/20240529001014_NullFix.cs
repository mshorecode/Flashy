using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flashy.Migrations
{
    /// <inheritdoc />
    public partial class NullFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flashcards_Sets_SetId",
                table: "Flashcards");

            migrationBuilder.AlterColumn<int>(
                name: "SetId",
                table: "Flashcards",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Flashcards_Sets_SetId",
                table: "Flashcards",
                column: "SetId",
                principalTable: "Sets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flashcards_Sets_SetId",
                table: "Flashcards");

            migrationBuilder.AlterColumn<int>(
                name: "SetId",
                table: "Flashcards",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Flashcards_Sets_SetId",
                table: "Flashcards",
                column: "SetId",
                principalTable: "Sets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
