using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrlShortenerApi01.Migrations
{
    /// <inheritdoc />
    public partial class makeDeletedAtInShortLinkModelNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "website",
                table: "Profiles",
                newName: "Website");

            migrationBuilder.RenameColumn(
                name: "company",
                table: "Profiles",
                newName: "Company");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedAt",
                table: "ShortLinks",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Website",
                table: "Profiles",
                newName: "website");

            migrationBuilder.RenameColumn(
                name: "Company",
                table: "Profiles",
                newName: "company");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedAt",
                table: "ShortLinks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
