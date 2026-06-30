using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReelRating.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveReviewCategoriesAndStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_REVIEW_CATEGORY",
                schema: "MOVIEDB",
                table: "REVIEW");

            migrationBuilder.DropForeignKey(
                name: "FK_REVIEW_STATUS",
                schema: "MOVIEDB",
                table: "REVIEW");

            migrationBuilder.DropIndex(
                name: "IDX_REVIEW_STATUS",
                schema: "MOVIEDB",
                table: "REVIEW");

            migrationBuilder.DropIndex(
                name: "IX_REVIEW_CATEGORIES_ID",
                schema: "MOVIEDB",
                table: "REVIEW");

            migrationBuilder.DropColumn(
                name: "CATEGORIES_ID",
                schema: "MOVIEDB",
                table: "REVIEW");

            migrationBuilder.DropColumn(
                name: "STATUS_ID",
                schema: "MOVIEDB",
                table: "REVIEW");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CATEGORIES_ID",
                schema: "MOVIEDB",
                table: "REVIEW",
                type: "NUMBER(10)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "STATUS_ID",
                schema: "MOVIEDB",
                table: "REVIEW",
                type: "NUMBER(10)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IDX_REVIEW_STATUS",
                schema: "MOVIEDB",
                table: "REVIEW",
                column: "STATUS_ID");

            migrationBuilder.CreateIndex(
                name: "IX_REVIEW_CATEGORIES_ID",
                schema: "MOVIEDB",
                table: "REVIEW",
                column: "CATEGORIES_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_REVIEW_CATEGORY",
                schema: "MOVIEDB",
                table: "REVIEW",
                column: "CATEGORIES_ID",
                principalSchema: "MOVIEDB",
                principalTable: "CATEGORIES",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_REVIEW_STATUS",
                schema: "MOVIEDB",
                table: "REVIEW",
                column: "STATUS_ID",
                principalSchema: "MOVIEDB",
                principalTable: "STATUS",
                principalColumn: "ID");
        }
    }
}
