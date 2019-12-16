using Microsoft.EntityFrameworkCore.Migrations;

namespace EfLearning.Data.Migrations
{
    public partial class allowNullMaterialScale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_GivenClassrooms_GivenClassroomId",
                table: "Announcements");

            migrationBuilder.DropIndex(
                name: "IX_Announcements_GivenClassroomId",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "GivenClassroomId",
                table: "Announcements");

            migrationBuilder.AlterColumn<int>(
                name: "MaterialScale",
                table: "Materials",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MaterialScale",
                table: "Materials",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GivenClassroomId",
                table: "Announcements",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_GivenClassroomId",
                table: "Announcements",
                column: "GivenClassroomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_GivenClassrooms_GivenClassroomId",
                table: "Announcements",
                column: "GivenClassroomId",
                principalTable: "GivenClassrooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
