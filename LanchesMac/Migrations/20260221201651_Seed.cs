using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LanchesMac.Migrations
{
    public partial class Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "CategoriaId", "CategoriaNome", "Descricao" },
                values: new object[,]
                {
                    { 1, "Normal", "Lanche feito com ingredientes normais" },
                    { 2, "Natural", "Lanche feito com ingredientes integrais e naturais" }
                });

            migrationBuilder.InsertData(
                table: "Lanches",
                columns: new[] { "LancheId", "CategoriaId", "DescricaoCurta", "DescricaoDetalhada", "EmEstoque", "ImagemThumbnailUrl", "ImagemUrl", "IsLanchePreferido", "Nome", "Preco" },
                values: new object[,]
                {
                    { 1, 1, "Pão, hambúrger, ovo, presunto, queijo e batata palha", "Delicioso pão de hambúrger com ovo frito; presunto e queijo de primeira qualidade acompanhado com batata palha", true, "http://www.macoratti.net/Imagens/lanches/cheesesalada1.jpg", "http://www.macoratti.net/Imagens/lanches/cheesesalada1.jpg", false, "Cheese Salada", 12.50m },
                    { 2, 1, "Pão, presunto, mussarela e tomate", "Delicioso pão francês quentinho na chapa com presunto e mussarela bem servidos com tomate preparado com carinho.", true, "http://www.macoratti.net/Imagens/lanches/mistoquente4.jpg", "http://www.macoratti.net/Imagens/lanches/mistoquente4.jpg", false, "Misto Quente", 8.00m },
                    { 3, 1, "Pão, hambúrger, presunto, mussarela e batalha palha", "Pão de hambúrger especial com hambúrger de nossa preparação e presunto e mussarela; acompanha batata palha.", true, "http://www.macoratti.net/Imagens/lanches/cheeseburger1.jpg", "http://www.macoratti.net/Imagens/lanches/cheeseburger1.jpg", false, "Cheese Burger", 11.00m },
                    { 4, 2, "Pão Integral, queijo branco, peito de peru, cenoura, alface, iogurte", "Pão integral natural com queijo branco, peito de peru e cenoura ralada com alface picado e iorgurte natural.", true, "http://www.macoratti.net/Imagens/lanches/lanchenatural.jpg", "http://www.macoratti.net/Imagens/lanches/lanchenatural.jpg", true, "Lanche Natural Peito Peru", 15.00m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Lanches",
                keyColumn: "LancheId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Lanches",
                keyColumn: "LancheId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Lanches",
                keyColumn: "LancheId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Lanches",
                keyColumn: "LancheId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValue: 2);
        }
    }
}
