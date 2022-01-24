using Microsoft.EntityFrameworkCore.Migrations;

namespace NewStart.Migrations
{
    public partial class InitialDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GodUserReviews",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Review = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GodID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GodUserReviews", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GodUserReviews_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GodUserReviews_Gods_GodID",
                        column: x => x.GodID,
                        principalTable: "Gods",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnitUserReviews",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Review = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UnitID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitUserReviews", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UnitUserReviews_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UnitUserReviews_Units_UnitID",
                        column: x => x.UnitID,
                        principalTable: "Units",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GodUserReviews_GodID",
                table: "GodUserReviews",
                column: "GodID");

            migrationBuilder.CreateIndex(
                name: "IX_GodUserReviews_UserID",
                table: "GodUserReviews",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_UnitUserReviews_UnitID",
                table: "UnitUserReviews",
                column: "UnitID");

            migrationBuilder.CreateIndex(
                name: "IX_UnitUserReviews_UserID",
                table: "UnitUserReviews",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GodUserReviews");

            migrationBuilder.DropTable(
                name: "UnitUserReviews");
        }
    }
}
