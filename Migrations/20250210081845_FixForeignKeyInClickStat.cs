using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrlShortenerApi01.Migrations
{
    /// <inheritdoc />
    public partial class FixForeignKeyInClickStat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClickStat_ShortLinks_ShortLinksId",
                table: "ClickStat");

            migrationBuilder.DropIndex(
                name: "IX_ClickStat_ShortLinksId",
                table: "ClickStat");

            migrationBuilder.DropColumn(
                name: "ShortLinksId",
                table: "ClickStat");

            migrationBuilder.CreateIndex(
                name: "IX_ClickStat_shortLinkId",
                table: "ClickStat",
                column: "shortLinkId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClickStat_ShortLinks_shortLinkId",
                table: "ClickStat",
                column: "shortLinkId",
                principalTable: "ShortLinks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClickStat_ShortLinks_shortLinkId",
                table: "ClickStat");

            migrationBuilder.DropIndex(
                name: "IX_ClickStat_shortLinkId",
                table: "ClickStat");

            migrationBuilder.AddColumn<int>(
                name: "ShortLinksId",
                table: "ClickStat",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ClickStat_ShortLinksId",
                table: "ClickStat",
                column: "ShortLinksId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClickStat_ShortLinks_ShortLinksId",
                table: "ClickStat",
                column: "ShortLinksId",
                principalTable: "ShortLinks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
