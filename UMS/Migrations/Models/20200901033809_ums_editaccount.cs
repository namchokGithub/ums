using Microsoft.EntityFrameworkCore.Migrations;

namespace UMS.Migrations.Models
{
    public partial class ums_editaccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EditAccount",
                columns: table => new
                {
                    acc_Id = table.Column<string>(nullable: false),
                    acc_User = table.Column<string>(nullable: true),
                    acc_Email = table.Column<string>(nullable: true),
                    acc_Firstname = table.Column<string>(nullable: false),
                    acc_Lastname = table.Column<string>(nullable: false),
                    acc_IsActive = table.Column<string>(nullable: false),
                    acc_Rolename = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EditAccount", x => x.acc_Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EditAccount");
        }
    }
}
