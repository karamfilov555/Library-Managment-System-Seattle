using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Data.Migrations
{
    public partial class reviewAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BookRatingId",
                table: "Books",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "BanRecords",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BookRating",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    BookId = table.Column<string>(nullable: true),
                    Rating = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookRating", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Grade = table.Column<decimal>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    BookRatingId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Review_BookRating_BookRatingId",
                        column: x => x.BookRatingId,
                        principalTable: "BookRating",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Review_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookRatingId",
                table: "Books",
                column: "BookRatingId",
                unique: true,
                filter: "[BookRatingId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Review_BookRatingId",
                table: "Review",
                column: "BookRatingId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_UserId",
                table: "Review",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookRating_BookRatingId",
                table: "Books",
                column: "BookRatingId",
                principalTable: "BookRating",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookRating_BookRatingId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "BookRating");

            migrationBuilder.DropIndex(
                name: "IX_Books_BookRatingId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "BookRatingId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BanRecords");
        }
    }
}
