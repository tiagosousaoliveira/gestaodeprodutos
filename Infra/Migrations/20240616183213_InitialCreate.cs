using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infra.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    CodigoProduto = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DescricaoProduto = table.Column<string>(type: "text", nullable: false),
                    SituacaoProduto = table.Column<bool>(type: "boolean", nullable: false),
                    DataFabricacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DataValidade = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CodigoFornecedor = table.Column<int>(type: "integer", nullable: false),
                    DescricaoFornecedor = table.Column<string>(type: "text", nullable: false),
                    CnpjFornecedor = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.CodigoProduto);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produtos");
        }
    }
}
