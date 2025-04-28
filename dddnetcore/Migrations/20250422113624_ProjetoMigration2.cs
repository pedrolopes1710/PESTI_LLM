using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DDDNetCore.Migrations
{
    /// <inheritdoc />
    public partial class ProjetoMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjetoId",
                table: "Perfil",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjetoId",
                table: "Indicadores",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjetoId",
                table: "Atividades",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Atividades_Projetos_ProjetoId",
                table: "Atividades",
                column: "ProjetoId",
                principalTable: "Projetos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Indicadores_Projetos_ProjetoId",
                table: "Indicadores",
                column: "ProjetoId",
                principalTable: "Projetos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Perfil_Projetos_ProjetoId",
                table: "Perfil",
                column: "ProjetoId",
                principalTable: "Projetos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjetoId",
                table: "Perfil",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjetoId",
                table: "Indicadores",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjetoId",
                table: "Atividades",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

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
    }
}
