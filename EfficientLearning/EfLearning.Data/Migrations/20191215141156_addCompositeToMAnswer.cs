using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EfLearning.Data.Migrations
{
    public partial class addCompositeToMAnswer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaterialAnswers_AspNetUsers_UserId",
                table: "MaterialAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MaterialAnswers",
                table: "MaterialAnswers");

            migrationBuilder.DropIndex(
                name: "IX_MaterialAnswers_UserId",
                table: "MaterialAnswers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "MaterialAnswers");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "MaterialAnswers",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaterialAnswers",
                table: "MaterialAnswers",
                columns: new[] { "UserId", "MaterialId" });

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialAnswers_AspNetUsers_UserId",
                table: "MaterialAnswers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaterialAnswers_AspNetUsers_UserId",
                table: "MaterialAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MaterialAnswers",
                table: "MaterialAnswers");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "MaterialAnswers",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "MaterialAnswers",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaterialAnswers",
                table: "MaterialAnswers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialAnswers_UserId",
                table: "MaterialAnswers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialAnswers_AspNetUsers_UserId",
                table: "MaterialAnswers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
