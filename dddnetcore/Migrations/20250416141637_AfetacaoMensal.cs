using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DDDNetCore.Migrations
{
    /// <inheritdoc />
    public partial class AfetacaoMensal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AfetacaoPerfilId",
                table: "AfetacaoMensais",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AfetacaoMensais_AfetacaoPerfilId",
                table: "AfetacaoMensais",
                column: "AfetacaoPerfilId");

            migrationBuilder.AddForeignKey(
                name: "FK_AfetacaoMensais_AfetacaoPerfis_AfetacaoPerfilId",
                table: "AfetacaoMensais",
                column: "AfetacaoPerfilId",
                principalTable: "AfetacaoPerfis",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AfetacaoMensais_AfetacaoPerfis_AfetacaoPerfilId",
                table: "AfetacaoMensais");

            migrationBuilder.DropIndex(
                name: "IX_AfetacaoMensais_AfetacaoPerfilId",
                table: "AfetacaoMensais");

            migrationBuilder.DropColumn(
                name: "AfetacaoPerfilId",
                table: "AfetacaoMensais");
        }
    }
}
