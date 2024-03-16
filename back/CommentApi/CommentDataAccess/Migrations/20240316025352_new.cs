using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommentDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Commentaires",
                columns: table => new
                {
                    IdComment = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdAI = table.Column<int>(type: "int", nullable: false),
                    IdUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreEtoile = table.Column<int>(type: "int", nullable: false),
                    Datepublication = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commentaires", x => x.IdComment);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Commentaires_IdComment",
                table: "Commentaires",
                column: "IdComment");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Commentaires");
        }
    }
}
