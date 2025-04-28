using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DDDNetCore.Migrations
{
    /// <inheritdoc />
    public partial class ProjetoMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProjetoId",
                table: "Perfil",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProjetoId",
                table: "Indicadores",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProjetoId",
                table: "Atividades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Projetos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    NomeProjeto = table.Column<string>(type: "TEXT", nullable: true),
                    DescricaoProjeto = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projetos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Perfil_ProjetoId",
                table: "Perfil",
                column: "ProjetoId");

            migrationBuilder.CreateIndex(
                name: "IX_Indicadores_ProjetoId",
                table: "Indicadores",
                column: "ProjetoId");

            migrationBuilder.CreateIndex(
                name: "IX_Atividades_ProjetoId",
                table: "Atividades",
                column: "ProjetoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Atividades_Projetos_ProjetoId",
                table: "Atividades",
                column: "ProjetoId",
                principalTable: "Projetos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Indicadores_Projetos_ProjetoId",
                table: "Indicadores",
                column: "ProjetoId",
                principalTable: "Projetos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Perfil_Projetos_ProjetoId",
                table: "Perfil",
                column: "ProjetoId",
                principalTable: "Projetos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Atividades_Projetos_ProjetoId",
                table: "Atividades");

            migrationBuilder.DropForeignKey(
                name: "FK_Indicadores_Projetos_ProjetoId",
                table: "Indicadores");

            migrationBuilder.DropForeignKey(
                name: "FK_Perfil_Projetos_ProjetoId",
                table: "Perfil");

            migrationBuilder.DropTable(
                name: "Projetos");

            migrationBuilder.DropIndex(
                name: "IX_Perfil_ProjetoId",
                table: "Perfil");

            migrationBuilder.DropIndex(
                name: "IX_Indicadores_ProjetoId",
                table: "Indicadores");

            migrationBuilder.DropIndex(
                name: "IX_Atividades_ProjetoId",
                table: "Atividades");

            migrationBuilder.DropColumn(
                name: "ProjetoId",
                table: "Perfil");

            migrationBuilder.DropColumn(
                name: "ProjetoId",
                table: "Indicadores");

            migrationBuilder.DropColumn(
                name: "ProjetoId",
                table: "Atividades");
        }
    }
}
