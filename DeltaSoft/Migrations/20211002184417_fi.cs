using Microsoft.EntityFrameworkCore.Migrations;

namespace DeltaSoft.Migrations
{
    public partial class fi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserTaskTable");

            migrationBuilder.DropTable(
                name: "UserTasks");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TaskTables",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskTables_UserId",
                table: "TaskTables",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskTables_AspNetUsers_UserId",
                table: "TaskTables",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskTables_AspNetUsers_UserId",
                table: "TaskTables");

            migrationBuilder.DropIndex(
                name: "IX_TaskTables_UserId",
                table: "TaskTables");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TaskTables");

            migrationBuilder.CreateTable(
                name: "ApplicationUserTaskTable",
                columns: table => new
                {
                    ApplicationUsersId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TaskTablesTaskId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserTaskTable", x => new { x.ApplicationUsersId, x.TaskTablesTaskId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserTaskTable_AspNetUsers_ApplicationUsersId",
                        column: x => x.ApplicationUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserTaskTable_TaskTables_TaskTablesTaskId",
                        column: x => x.TaskTablesTaskId,
                        principalTable: "TaskTables",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTasks",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTasks", x => new { x.TaskId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserTasks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTasks_TaskTables_TaskId",
                        column: x => x.TaskId,
                        principalTable: "TaskTables",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserTaskTable_TaskTablesTaskId",
                table: "ApplicationUserTaskTable",
                column: "TaskTablesTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTasks_UserId",
                table: "UserTasks",
                column: "UserId");
        }
    }
}
