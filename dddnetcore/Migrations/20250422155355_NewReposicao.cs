using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DDDNetCore.Migrations
{
    /// <inheritdoc />
    public partial class NewReposicao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AfetacaoPerfis",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DuracaoMes = table.Column<int>(type: "INTEGER", nullable: false),
                    PMsAprovados = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AfetacaoPerfis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CargasMensais",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    JornadaDiaria = table.Column<double>(type: "REAL", nullable: false),
                    DiasUteis = table.Column<double>(type: "REAL", nullable: false),
                    Ausencias = table.Column<double>(type: "REAL", nullable: false),
                    SalarioBase = table.Column<double>(type: "REAL", nullable: false),
                    MesAno = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargasMensais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contratos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Tipo = table.Column<string>(type: "TEXT", nullable: false),
                    Salario = table.Column<double>(type: "REAL", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataFim = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contratos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Families",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Families", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    CategoryId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "Rubricas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rubricas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tarefas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    NomeTarefa = table.Column<string>(type: "TEXT", nullable: false),
                    DescricaoTarefa = table.Column<string>(type: "TEXT", nullable: false),
                    StatusTarefa = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarefas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AfetacaoMensais",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PMs = table.Column<double>(type: "REAL", nullable: false),
                    AfetacaoPerfilId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AfetacaoMensais", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AfetacaoMensais_AfetacaoPerfis_AfetacaoPerfilId",
                        column: x => x.AfetacaoPerfilId,
                        principalTable: "AfetacaoPerfis",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Indicadores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    NomeIndicador = table.Column<string>(type: "TEXT", nullable: true),
                    ValorAtual = table.Column<double>(type: "REAL", nullable: true),
                    ValorMaximo = table.Column<double>(type: "REAL", nullable: true),
                    ProjetoId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Indicadores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Indicadores_Projetos_ProjetoId",
                        column: x => x.ProjetoId,
                        principalTable: "Projetos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Perfil",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PMsTotais = table.Column<int>(type: "INTEGER", nullable: false),
                    DescricaoPerfil = table.Column<string>(type: "TEXT", nullable: false),
                    ProjetoId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perfil", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Perfil_Projetos_ProjetoId",
                        column: x => x.ProjetoId,
                        principalTable: "Projetos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orcamentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    GastoPlaneado = table.Column<double>(type: "REAL", nullable: false),
                    GastoExecutado = table.Column<double>(type: "REAL", nullable: false),
                    RubricaId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orcamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orcamentos_Rubricas_RubricaId",
                        column: x => x.RubricaId,
                        principalTable: "Rubricas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Atividades",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DataFimAtividade = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataInicioAtividade = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DescricaoAtividade = table.Column<string>(type: "TEXT", nullable: false),
                    NomeAtividade = table.Column<string>(type: "TEXT", nullable: false),
                    TarefaId = table.Column<Guid>(type: "TEXT", nullable: false),
                    OrcamentoId = table.Column<Guid>(type: "TEXT", nullable: true),
                    ProjetoId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atividades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Atividades_Orcamentos_OrcamentoId",
                        column: x => x.OrcamentoId,
                        principalTable: "Orcamentos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Atividades_Projetos_ProjetoId",
                        column: x => x.ProjetoId,
                        principalTable: "Projetos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Atividades_Tarefas_TarefaId",
                        column: x => x.TarefaId,
                        principalTable: "Tarefas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AfetacaoMensais_AfetacaoPerfilId",
                table: "AfetacaoMensais",
                column: "AfetacaoPerfilId");

            migrationBuilder.CreateIndex(
                name: "IX_Atividades_OrcamentoId",
                table: "Atividades",
                column: "OrcamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Atividades_ProjetoId",
                table: "Atividades",
                column: "ProjetoId");

            migrationBuilder.CreateIndex(
                name: "IX_Atividades_TarefaId",
                table: "Atividades",
                column: "TarefaId");

            migrationBuilder.CreateIndex(
                name: "IX_Indicadores_ProjetoId",
                table: "Indicadores",
                column: "ProjetoId");

            migrationBuilder.CreateIndex(
                name: "IX_Orcamentos_RubricaId",
                table: "Orcamentos",
                column: "RubricaId");

            migrationBuilder.CreateIndex(
                name: "IX_Perfil_ProjetoId",
                table: "Perfil",
                column: "ProjetoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AfetacaoMensais");

            migrationBuilder.DropTable(
                name: "Atividades");

            migrationBuilder.DropTable(
                name: "CargasMensais");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Contratos");

            migrationBuilder.DropTable(
                name: "Families");

            migrationBuilder.DropTable(
                name: "Indicadores");

            migrationBuilder.DropTable(
                name: "Perfil");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "AfetacaoPerfis");

            migrationBuilder.DropTable(
                name: "Orcamentos");

            migrationBuilder.DropTable(
                name: "Tarefas");

            migrationBuilder.DropTable(
                name: "Projetos");

            migrationBuilder.DropTable(
                name: "Rubricas");
        }
    }
}
