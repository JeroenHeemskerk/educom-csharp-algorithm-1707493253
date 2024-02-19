using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BornToMove.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RatingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MoveRating",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Moveid = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    Vote = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoveRating", x => x.id);
                    table.ForeignKey(
                        name: "FK_MoveRating_Move_Moveid",
                        column: x => x.Moveid,
                        principalTable: "Move",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MoveRating_Moveid",
                table: "MoveRating",
                column: "Moveid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MoveRating");
        }
    }
}
