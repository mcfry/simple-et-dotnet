using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExerciseTimer.Migrations
{
    /// <inheritdoc />
    public partial class AddUidToSetRecords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Uid",
                table: "SetRecords",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Uid",
                table: "SetRecords");
        }
    }
}
