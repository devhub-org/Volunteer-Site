using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class change_tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tables_AspNetUsers_AuthorId1",
                table: "Tables");

            migrationBuilder.DropIndex(
                name: "IX_Tables_AuthorId1",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "AuthorId1",
                table: "Tables");

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "Tables",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tables_AuthorId",
                table: "Tables",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_AspNetUsers_AuthorId",
                table: "Tables",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tables_AspNetUsers_AuthorId",
                table: "Tables");

            migrationBuilder.DropIndex(
                name: "IX_Tables_AuthorId",
                table: "Tables");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "Tables",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthorId1",
                table: "Tables",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tables_AuthorId1",
                table: "Tables",
                column: "AuthorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_AspNetUsers_AuthorId1",
                table: "Tables",
                column: "AuthorId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
