using Microsoft.EntityFrameworkCore.Migrations;

namespace UMS.Migrations
{
    public partial class umsacc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EditProfile",
                columns: table => new
                {
                    acc_Id = table.Column<string>(nullable: false),
                    acc_Firstname = table.Column<string>(nullable: true),
                    acc_Lastname = table.Column<string>(nullable: true),
                    acc_PasswordHash = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EditProfile", x => x.acc_Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EditProfile");
        }
    }
}
