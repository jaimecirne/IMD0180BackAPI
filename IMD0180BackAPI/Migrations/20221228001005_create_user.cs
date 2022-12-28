using Microsoft.EntityFrameworkCore.Migrations;

namespace IMD0180BackAPI.Migrations
{
    public partial class create_user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Users (Id, Login, Password, Role, LastLogin) VALUES (1, 'superuser', 'a329447465d3af6751a371b9bfd77f2794d7ae22ad5d524bcd69af36c229798d', 'Admin' ,'2022-10-07 08:23:19.120')");
            migrationBuilder.Sql("INSERT INTO Users (Id, Login, Password, Role, LastLogin) VALUES (2, 'user', '1964fa683d5830da2b3c3e89f6e8253c5538c1023a95ec86ee349a0f4e9f0d84', 'userbase' ,'2022-10-07 08:23:19.120')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
