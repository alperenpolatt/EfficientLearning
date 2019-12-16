using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EfLearning.Data.Migrations
{
    public partial class compositeKeyTakenClassroom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TakenClassrooms_GivenClassrooms_GivenClassroomId",
                table: "TakenClassrooms");

            migrationBuilder.DropForeignKey(
                name: "FK_TakenClassrooms_AspNetUsers_UserId",
                table: "TakenClassrooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TakenClassrooms",
                table: "TakenClassrooms");

            migrationBuilder.DropIndex(
                name: "IX_TakenClassrooms_UserId",
                table: "TakenClassrooms");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TakenClassrooms");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "TakenClassrooms",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GivenClassroomId",
                table: "TakenClassrooms",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TakenClassrooms",
                table: "TakenClassrooms",
                columns: new[] { "UserId", "GivenClassroomId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TakenClassrooms_GivenClassrooms_GivenClassroomId",
                table: "TakenClassrooms",
                column: "GivenClassroomId",
                principalTable: "GivenClassrooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TakenClassrooms_AspNetUsers_UserId",
                table: "TakenClassrooms",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TakenClassrooms_GivenClassrooms_GivenClassroomId",
                table: "TakenClassrooms");

            migrationBuilder.DropForeignKey(
                name: "FK_TakenClassrooms_AspNetUsers_UserId",
                table: "TakenClassrooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TakenClassrooms",
                table: "TakenClassrooms");

            migrationBuilder.AlterColumn<int>(
                name: "GivenClassroomId",
                table: "TakenClassrooms",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "TakenClassrooms",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "TakenClassrooms",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TakenClassrooms",
                table: "TakenClassrooms",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TakenClassrooms_UserId",
                table: "TakenClassrooms",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TakenClassrooms_GivenClassrooms_GivenClassroomId",
                table: "TakenClassrooms",
                column: "GivenClassroomId",
                principalTable: "GivenClassrooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TakenClassrooms_AspNetUsers_UserId",
                table: "TakenClassrooms",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
