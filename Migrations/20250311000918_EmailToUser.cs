using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliotecaMiaRaymundo.Migrations
{
    /// <inheritdoc />
    public partial class EmailToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "PkUsuario",
                keyValue: 1,
                column: "Email",
                value: "123@gmail.com");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Usuarios");
        }
    }
}
