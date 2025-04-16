﻿// <auto-generated />
using System;
using DDDSample1.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DDDNetCore.Migrations
{
    [DbContext(typeof(DDDSample1DbContext))]
    [Migration("20250416141637_AfetacaoMensal")]
    partial class AfetacaoMensal
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.3");

            modelBuilder.Entity("DDDSample1.Domain.Categories.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("DDDSample1.Domain.Families.Family", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Families");
                });

            modelBuilder.Entity("DDDSample1.Domain.Products.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("CategoryId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("dddnetcore.Domain.AfetacaoMensais.AfetacaoMensal", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("AfetacaoPerfilId")
                        .HasColumnType("TEXT");

                    b.Property<double>("PMs")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("AfetacaoPerfilId");

                    b.ToTable("AfetacaoMensais");
                });

            modelBuilder.Entity("dddnetcore.Domain.AfetacaoPerfis.AfetacaoPerfil", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("DuracaoMes")
                        .HasColumnType("INTEGER");

                    b.Property<double>("PMsAprovados")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("AfetacaoPerfis");
                });

            modelBuilder.Entity("dddnetcore.Domain.Atividades.Atividade", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataFimAtividade")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataInicioAtividade")
                        .HasColumnType("TEXT");

                    b.Property<string>("DescricaoAtividade")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NomeAtividade")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("OrcamentoId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TarefaId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("OrcamentoId");

                    b.HasIndex("TarefaId");

                    b.ToTable("Atividades");
                });

            modelBuilder.Entity("dddnetcore.Domain.Contratos.Contrato", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Ativo")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DataFim")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("TEXT");

                    b.Property<double>("Salario")
                        .HasColumnType("REAL");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Contratos");
                });

            modelBuilder.Entity("dddnetcore.Domain.Orcamentos.Orcamento", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<double>("GastoExecutado")
                        .HasColumnType("REAL");

                    b.Property<double>("GastoPlaneado")
                        .HasColumnType("REAL");

                    b.Property<Guid>("RubricaId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RubricaId");

                    b.ToTable("Orcamentos");
                });

            modelBuilder.Entity("dddnetcore.Domain.Rubricas.Rubrica", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Rubricas");
                });

            modelBuilder.Entity("dddnetcore.Domain.Tarefas.Tarefa", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("DescricaoTarefa")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NomeTarefa")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("StatusTarefa")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Tarefas");
                });

            modelBuilder.Entity("dddnetcore.Domain.AfetacaoMensais.AfetacaoMensal", b =>
                {
                    b.HasOne("dddnetcore.Domain.AfetacaoPerfis.AfetacaoPerfil", "AfetacaoPerfil")
                        .WithMany()
                        .HasForeignKey("AfetacaoPerfilId");

                    b.Navigation("AfetacaoPerfil");
                });

            modelBuilder.Entity("dddnetcore.Domain.Atividades.Atividade", b =>
                {
                    b.HasOne("dddnetcore.Domain.Orcamentos.Orcamento", "Orcamento")
                        .WithMany()
                        .HasForeignKey("OrcamentoId");

                    b.HasOne("dddnetcore.Domain.Tarefas.Tarefa", "Tarefa")
                        .WithMany()
                        .HasForeignKey("TarefaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Orcamento");

                    b.Navigation("Tarefa");
                });

            modelBuilder.Entity("dddnetcore.Domain.Orcamentos.Orcamento", b =>
                {
                    b.HasOne("dddnetcore.Domain.Rubricas.Rubrica", "Rubrica")
                        .WithMany()
                        .HasForeignKey("RubricaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rubrica");
                });
#pragma warning restore 612, 618
        }
    }
}
