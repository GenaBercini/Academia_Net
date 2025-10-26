using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAdminHasData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Adress", "DateOfAdmission", "DateOfHire", "Dni", "Email", "JobPosition", "LastName", "Name", "PasswordHash", "Salt", "Status", "StudentNumber", "TypeUser", "UserName" },
                values: new object[] { 1, "Juan Jose Paso 123", null, null, "42789654", "admin@tpi.com", null, "Admin", "Juan", "VIaTwx4tNyNfsVeQma/oipr3HFCgTm3k/TpMR7HYtfM=", "Wqy2R6Fb1uSq6nM9Pc52VlPCsPJYk4SyN85OyjOBTes=", 1, null, 1, "admin" });
        }
    }
}
