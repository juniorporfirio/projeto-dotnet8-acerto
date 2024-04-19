using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acerto.Produtos.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_produtos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    nome_produto = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    descricao = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true),
                    preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ativo = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_produtos", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_produtos");
        }
    }
}
