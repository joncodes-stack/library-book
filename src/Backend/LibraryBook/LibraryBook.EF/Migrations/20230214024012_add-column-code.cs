using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryBook.EF.Migrations
{
    public partial class addcolumncode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "code",
                table: "tb_users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "code",
                table: "tb_users");
        }
    }
}
