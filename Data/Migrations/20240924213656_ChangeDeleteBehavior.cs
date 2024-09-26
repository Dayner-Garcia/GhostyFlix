using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Series_Genders_GenderPrimaryId",
                table: "Series");

            migrationBuilder.DropForeignKey(
                name: "FK_Series_Genders_GenderSecondaryId",
                table: "Series");

            migrationBuilder.AddForeignKey(
                name: "FK_Series_Genders_GenderPrimaryId",
                table: "Series",
                column: "GenderPrimaryId",
                principalTable: "Genders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Series_Genders_GenderSecondaryId",
                table: "Series",
                column: "GenderSecondaryId",
                principalTable: "Genders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Series_Genders_GenderPrimaryId",
                table: "Series");

            migrationBuilder.DropForeignKey(
                name: "FK_Series_Genders_GenderSecondaryId",
                table: "Series");

            migrationBuilder.AddForeignKey(
                name: "FK_Series_Genders_GenderPrimaryId",
                table: "Series",
                column: "GenderPrimaryId",
                principalTable: "Genders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Series_Genders_GenderSecondaryId",
                table: "Series",
                column: "GenderSecondaryId",
                principalTable: "Genders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
