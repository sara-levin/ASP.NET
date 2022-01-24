using Microsoft.EntityFrameworkCore.Migrations;

namespace NewStart.Migrations
{
    public partial class InitialDatabaseUpload : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MythologyUserReviews",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Review = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MythologyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MythologyUserReviews", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MythologyUserReviews_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MythologyUserReviews_Mythologies_MythologyID",
                        column: x => x.MythologyID,
                        principalTable: "Mythologies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MythologyUserReviews_MythologyID",
                table: "MythologyUserReviews",
                column: "MythologyID");

            migrationBuilder.CreateIndex(
                name: "IX_MythologyUserReviews_UserID",
                table: "MythologyUserReviews",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MythologyUserReviews");
        }
    }
}
