using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MessagingData.Migrations
{
    /// <inheritdoc />
    public partial class updatedPasswordTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Passwords",
                newName: "HashedValue");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HashedValue",
                table: "Passwords",
                newName: "Value");
        }
    }
}
