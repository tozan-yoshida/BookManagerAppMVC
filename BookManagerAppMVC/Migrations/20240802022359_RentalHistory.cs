using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookManagerAppMVC.Migrations
{
    /// <inheritdoc />
    public partial class RentalHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RentalHistoryId",
                table: "Book",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RentalHistory",
                columns: table => new
                {
                    RentalHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RentDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RentUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalHistory", x => x.RentalHistoryId);
                    table.ForeignKey(
                        name: "FK_RentalHistory_Book_BookId",
                        column: x => x.BookId,
                        principalTable: "Book",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RentalHistory_BookId",
                table: "RentalHistory",
                column: "BookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RentalHistory");

            migrationBuilder.DropColumn(
                name: "RentalHistoryId",
                table: "Book");
        }
    }
}
