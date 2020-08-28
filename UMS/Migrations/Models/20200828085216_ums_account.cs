using Microsoft.EntityFrameworkCore.Migrations;

namespace UMS.Migrations.Models
{
    public partial class ums_account : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    acc_Id = table.Column<string>(nullable: false),
                    acc_User = table.Column<string>(nullable: true),
                    acc_NormalizedUserName = table.Column<string>(nullable: true),
                    acc_Email = table.Column<string>(nullable: true),
                    acc_NormalizedEmail = table.Column<string>(nullable: true),
                    acc_PasswordHash = table.Column<string>(nullable: true),
                    acc_SecurityStamp = table.Column<string>(nullable: true),
                    acc_ConcurrencyStamp = table.Column<string>(nullable: true),
                    acc_Firstname = table.Column<string>(nullable: true),
                    acc_Lastname = table.Column<string>(nullable: true),
                    acc_IsActive = table.Column<string>(nullable: false),
                    acc_Rolename = table.Column<string>(nullable: true),
                    acc_TypeAccoutname = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.acc_Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account");
        }
    }
}
