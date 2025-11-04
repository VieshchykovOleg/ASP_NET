using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyPortal.Migrations
{
    /// <inheritdoc />
    public partial class ChangedRatingToDecimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Surveys",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Surveys");
        }
    }
}
