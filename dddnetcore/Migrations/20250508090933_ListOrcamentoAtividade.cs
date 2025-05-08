using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DDDNetCore.Migrations
{
    /// <inheritdoc />
    public partial class ListOrcamentoAtividade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Atividades_Orcamentos_OrcamentoId",
                table: "Atividades");

            migrationBuilder.DropIndex(
                name: "IX_Atividades_OrcamentoId",
                table: "Atividades");

            migrationBuilder.DropColumn(
                name: "OrcamentoId",
                table: "Atividades");

            migrationBuilder.AddColumn<Guid>(
                name: "AtividadeId",
                table: "Orcamentos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orcamentos_AtividadeId",
                table: "Orcamentos",
                column: "AtividadeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orcamentos_Atividades_AtividadeId",
                table: "Orcamentos",
                column: "AtividadeId",
                principalTable: "Atividades",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orcamentos_Atividades_AtividadeId",
                table: "Orcamentos");

            migrationBuilder.DropIndex(
                name: "IX_Orcamentos_AtividadeId",
                table: "Orcamentos");

            migrationBuilder.DropColumn(
                name: "AtividadeId",
                table: "Orcamentos");

            migrationBuilder.AddColumn<Guid>(
                name: "OrcamentoId",
                table: "Atividades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Atividades_OrcamentoId",
                table: "Atividades",
                column: "OrcamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Atividades_Orcamentos_OrcamentoId",
                table: "Atividades",
                column: "OrcamentoId",
                principalTable: "Orcamentos",
                principalColumn: "Id");
        }
    }
}
