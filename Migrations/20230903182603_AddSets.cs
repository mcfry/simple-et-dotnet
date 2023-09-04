using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExerciseTimer.Migrations
{
    /// <inheritdoc />
    public partial class AddSets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Toppings_SetRecords_SetRecordId",
                table: "Toppings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Toppings",
                table: "Toppings");

            migrationBuilder.RenameTable(
                name: "Toppings",
                newName: "Sets");

            migrationBuilder.RenameIndex(
                name: "IX_Toppings_SetRecordId",
                table: "Sets",
                newName: "IX_Sets_SetRecordId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sets",
                table: "Sets",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sets_SetRecords_SetRecordId",
                table: "Sets",
                column: "SetRecordId",
                principalTable: "SetRecords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sets_SetRecords_SetRecordId",
                table: "Sets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sets",
                table: "Sets");

            migrationBuilder.RenameTable(
                name: "Sets",
                newName: "Toppings");

            migrationBuilder.RenameIndex(
                name: "IX_Sets_SetRecordId",
                table: "Toppings",
                newName: "IX_Toppings_SetRecordId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Toppings",
                table: "Toppings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Toppings_SetRecords_SetRecordId",
                table: "Toppings",
                column: "SetRecordId",
                principalTable: "SetRecords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
