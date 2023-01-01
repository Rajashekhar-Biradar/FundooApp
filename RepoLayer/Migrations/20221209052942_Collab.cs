using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepoLayer.Migrations
{
    public partial class Collab : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CollabTable",
                columns: table => new
                {
                    CollabID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollaEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Updatedata = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserID = table.Column<long>(type: "bigint", nullable: false),
                    Note_ID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollabTable", x => x.CollabID);
                    table.ForeignKey(
                        name: "FK_CollabTable_noteTable_Note_ID",
                        column: x => x.Note_ID,
                        principalTable: "noteTable",
                        principalColumn: "Note_ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CollabTable_userTable_UserID",
                        column: x => x.UserID,
                        principalTable: "userTable",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollabTable_Note_ID",
                table: "CollabTable",
                column: "Note_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CollabTable_UserID",
                table: "CollabTable",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollabTable");
        }
    }
}
