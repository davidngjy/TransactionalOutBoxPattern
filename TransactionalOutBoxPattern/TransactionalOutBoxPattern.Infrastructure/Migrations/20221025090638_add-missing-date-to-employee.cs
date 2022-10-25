using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransactionalOutBoxPattern.Infrastructure.Migrations
{
    public partial class addmissingdatetoemployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "created_on",
                table: "employee",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "modified_on",
                table: "employee",
                type: "timestamp with time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_on",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "modified_on",
                table: "employee");
        }
    }
}
