using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransactionalOutBoxPattern.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ReinitializeMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "department",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    departmenttype = table.Column<string>(name: "department_type", type: "text", nullable: false),
                    totalsalary = table.Column<decimal>(name: "total_salary", type: "numeric", nullable: false),
                    createdon = table.Column<DateTimeOffset>(name: "created_on", type: "timestamp with time zone", nullable: false),
                    modifiedon = table.Column<DateTimeOffset>(name: "modified_on", type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_department", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "outbox_message",
                columns: table => new
                {
                    eventid = table.Column<Guid>(name: "event_id", type: "uuid", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    occurredon = table.Column<DateTimeOffset>(name: "occurred_on", type: "timestamp with time zone", nullable: false),
                    processedon = table.Column<DateTimeOffset>(name: "processed_on", type: "timestamp with time zone", nullable: true),
                    error = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_outbox_message", x => x.eventid);
                });

            migrationBuilder.CreateTable(
                name: "employee",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    firstname = table.Column<string>(name: "first_name", type: "text", nullable: false),
                    lastname = table.Column<string>(name: "last_name", type: "text", nullable: false),
                    role = table.Column<string>(type: "text", nullable: false),
                    departmentid = table.Column<Guid>(name: "department_id", type: "uuid", nullable: false),
                    salaryamount = table.Column<decimal>(name: "salary_amount", type: "numeric", nullable: false),
                    createdon = table.Column<DateTimeOffset>(name: "created_on", type: "timestamp with time zone", nullable: false),
                    modifiedon = table.Column<DateTimeOffset>(name: "modified_on", type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee", x => x.id);
                    table.ForeignKey(
                        name: "FK_employee_department_department_id",
                        column: x => x.departmentid,
                        principalTable: "department",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "task",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    iscompleted = table.Column<bool>(name: "is_completed", type: "boolean", nullable: false),
                    employeeid = table.Column<Guid>(name: "employee_id", type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_task", x => x.id);
                    table.ForeignKey(
                        name: "FK_task_employee_employee_id",
                        column: x => x.employeeid,
                        principalTable: "employee",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_employee_department_id",
                table: "employee",
                column: "department_id");

            migrationBuilder.CreateIndex(
                name: "IX_task_employee_id",
                table: "task",
                column: "employee_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "outbox_message");

            migrationBuilder.DropTable(
                name: "task");

            migrationBuilder.DropTable(
                name: "employee");

            migrationBuilder.DropTable(
                name: "department");
        }
    }
}
