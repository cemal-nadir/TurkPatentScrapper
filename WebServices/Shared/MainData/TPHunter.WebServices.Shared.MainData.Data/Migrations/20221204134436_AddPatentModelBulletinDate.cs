using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TPHunter.WebServices.Shared.MainData.Data.Migrations
{
    public partial class AddPatentModelBulletinDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BulletinDate",
                table: "Patents",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BulletinDate",
                table: "Patents");
        }
    }
}
