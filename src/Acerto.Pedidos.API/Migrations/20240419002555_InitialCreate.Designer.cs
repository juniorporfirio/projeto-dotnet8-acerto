﻿// <auto-generated />
using System;
using Acerto.Pedidos.API.Infra.Contexto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Acerto.Pedidos.API.Migrations
{
    [DbContext(typeof(PedidoContexto))]
    [Migration("20240419002555_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Acerto.Pedidos.API.Dominio.Entidades.Pedido", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("data_criacao");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("pedido_status");

                    b.Property<decimal>("ValorTotal")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("valor_total");

                    b.HasKey("Id");

                    b.ToTable("tb_pedido", (string)null);
                });

            modelBuilder.Entity("Acerto.Pedidos.API.Dominio.Entidades.Produto", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("PedidoId")
                        .HasColumnType("char(36)")
                        .HasColumnName("pedido_id");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("nome");

                    b.Property<decimal>("Preco")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("preco");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int")
                        .HasColumnName("quantidade");

                    b.HasKey("Id", "PedidoId");

                    b.HasIndex("PedidoId");

                    b.ToTable("tb_produto", (string)null);
                });

            modelBuilder.Entity("Acerto.Pedidos.API.Dominio.Entidades.Produto", b =>
                {
                    b.HasOne("Acerto.Pedidos.API.Dominio.Entidades.Pedido", "Pedido")
                        .WithMany("Produtos")
                        .HasForeignKey("PedidoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pedido");
                });

            modelBuilder.Entity("Acerto.Pedidos.API.Dominio.Entidades.Pedido", b =>
                {
                    b.Navigation("Produtos");
                });
#pragma warning restore 612, 618
        }
    }
}
