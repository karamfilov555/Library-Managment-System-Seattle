using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Data.Migrations
{
    public partial class ManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_HistoryRegistries",
                table: "HistoryRegistries");

            migrationBuilder.DropIndex(
                name: "IX_HistoryRegistries_BookId",
                table: "HistoryRegistries");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "HistoryRegistries");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HistoryRegistries",
                table: "HistoryRegistries",
                columns: new[] { "BookId", "UserId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_HistoryRegistries",
                table: "HistoryRegistries");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "HistoryRegistries",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_HistoryRegistries",
                table: "HistoryRegistries",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryRegistries_BookId",
                table: "HistoryRegistries",
                column: "BookId");
        }
    }
}
