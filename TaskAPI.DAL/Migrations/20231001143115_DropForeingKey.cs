using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskAPI.DAL.Migrations
{
    /// <inheritdoc />
    public partial class DropForeingKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Persons_CurrentPersonId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_CurrentPersonId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "CurrentPersonId",
                table: "Tasks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentPersonId",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_CurrentPersonId",
                table: "Tasks",
                column: "CurrentPersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Persons_CurrentPersonId",
                table: "Tasks",
                column: "CurrentPersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
