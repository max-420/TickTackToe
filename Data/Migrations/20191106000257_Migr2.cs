using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Migr2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Step_Game_GameId",
                table: "Step");

            migrationBuilder.DropColumn(
                name: "IsFinished",
                table: "Game");

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "Step",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserPlayerType",
                table: "Game",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Winner",
                table: "Game",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Step_Game_GameId",
                table: "Step",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Step_Game_GameId",
                table: "Step");

            migrationBuilder.DropColumn(
                name: "UserPlayerType",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "Winner",
                table: "Game");

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "Step",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<bool>(
                name: "IsFinished",
                table: "Game",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Step_Game_GameId",
                table: "Step",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
