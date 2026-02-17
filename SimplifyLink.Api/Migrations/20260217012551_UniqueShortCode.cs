using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimplifyLink.Api.Migrations
{
    /// <inheritdoc />
    public partial class UniqueShortCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ShortLinks_ShortCode",
                table: "ShortLinks",
                column: "ShortCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ShortLinks_ShortCode",
                table: "ShortLinks");
        }
    }
}
