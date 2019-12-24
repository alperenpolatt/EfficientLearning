using Microsoft.EntityFrameworkCore.Migrations;

namespace EfLearning.Data.Migrations
{
    public partial class GivenPMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DonePractices_GivenPracticeId",
                table: "DonePractices");

            migrationBuilder.CreateIndex(
                name: "IX_DonePractices_GivenPracticeId",
                table: "DonePractices",
                column: "GivenPracticeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DonePractices_GivenPracticeId",
                table: "DonePractices");

            migrationBuilder.CreateIndex(
                name: "IX_DonePractices_GivenPracticeId",
                table: "DonePractices",
                column: "GivenPracticeId",
                unique: true);
        }
    }
}
