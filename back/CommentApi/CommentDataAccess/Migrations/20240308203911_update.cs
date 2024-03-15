using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommentDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "nombreEtoile",
                table: "Commentaires",
                newName: "NombreEtoile");

            migrationBuilder.RenameColumn(
                name: "idUser",
                table: "Commentaires",
                newName: "IdUser");

            migrationBuilder.RenameColumn(
                name: "idComment",
                table: "Commentaires",
                newName: "IdComment");

            migrationBuilder.RenameIndex(
                name: "IX_Commentaires_idComment",
                table: "Commentaires",
                newName: "IX_Commentaires_IdComment");

            migrationBuilder.AddColumn<Guid>(
                name: "IdAI",
                table: "Commentaires",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdAI",
                table: "Commentaires");

            migrationBuilder.RenameColumn(
                name: "NombreEtoile",
                table: "Commentaires",
                newName: "nombreEtoile");

            migrationBuilder.RenameColumn(
                name: "IdUser",
                table: "Commentaires",
                newName: "idUser");

            migrationBuilder.RenameColumn(
                name: "IdComment",
                table: "Commentaires",
                newName: "idComment");

            migrationBuilder.RenameIndex(
                name: "IX_Commentaires_IdComment",
                table: "Commentaires",
                newName: "IX_Commentaires_idComment");
        }
    }
}
