using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryBook.EF.Migrations
{
    /// <inheritdoc />
    public partial class createdatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_gender",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_gender", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    phoneNumber = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    password = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    profilePic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    code = table.Column<int>(type: "int", nullable: true),
                    refreshToken = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    refreshTokenExpireTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_book",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    isbnNumber = table.Column<long>(type: "bigint", maxLength: 20, nullable: true),
                    auhor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    editor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    synopsis = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    pictureBook = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    id_gender = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id_user = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_book", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_book_tb_gender_id_gender",
                        column: x => x.id_gender,
                        principalTable: "tb_gender",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_book_tb_users_id_user",
                        column: x => x.id_user,
                        principalTable: "tb_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_book_id_gender",
                table: "tb_book",
                column: "id_gender");

            migrationBuilder.CreateIndex(
                name: "IX_tb_book_id_user",
                table: "tb_book",
                column: "id_user");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_book");

            migrationBuilder.DropTable(
                name: "tb_gender");

            migrationBuilder.DropTable(
                name: "tb_users");
        }
    }
}
