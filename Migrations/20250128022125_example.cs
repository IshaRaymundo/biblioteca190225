using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliotecaGuzmanGeovani.Migrations
{
    /// <inheritdoc />
    public partial class example : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PkUsusario",
                table: "Usuarios",
                newName: "PkUssario");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "PkRol", "Nombre" },
                values: new object[] { 1, "Admin" });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "PkUssario", "FkRol", "Nombre", "Password", "UserName" },
                values: new object[] { 1, 1, "Geo", "123", "Usuario" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "PkUssario",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "PkRol",
                keyValue: 1);

            migrationBuilder.RenameColumn(
                name: "PkUssario",
                table: "Usuarios",
                newName: "PkUsusario");
        }
    }
}
