using LanchesMac.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;

namespace LanchesMac.context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Lanche> Lanches { get; set; }
        public DbSet<CarrinhoCompraItem> CarrinhoCompraItens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Categoria>().HasData(

                new Categoria
                {
                    CategoriaId = 1,
                    CategoriaNome = "Normal",
                    Descricao = "Lanche feito com ingredientes normais"
                },

                new Categoria
                {
                    CategoriaId = 2,
                    CategoriaNome = "Natural",
                    Descricao = "Lanche feito com ingredientes integrais e naturais"
                }
            );

            modelBuilder.Entity<Lanche>().HasData(

                new Lanche
                {
                    LancheId = 1,
                    CategoriaId = 1,
                    Nome = "Cheese Salada",
                    DescricaoCurta = "Pão, hambúrger, ovo, presunto, queijo e batata palha",
                    DescricaoDetalhada = "Delicioso pão de hambúrger com ovo frito; presunto e queijo de primeira qualidade acompanhado com batata palha",
                    EmEstoque = true,
                    ImagemThumbnailUrl = "http://www.macoratti.net/Imagens/lanches/cheesesalada1.jpg",
                    ImagemUrl = "http://www.macoratti.net/Imagens/lanches/cheesesalada1.jpg",
                    IsLanchePreferido = false,
                    Preco = 12.50m
                },

                new Lanche
                {
                    LancheId = 2,
                    CategoriaId = 1,
                    Nome = "Misto Quente",
                    DescricaoCurta = "Pão, presunto, mussarela e tomate",
                    DescricaoDetalhada = "Delicioso pão francês quentinho na chapa com presunto e mussarela bem servidos com tomate preparado com carinho.",
                    EmEstoque = true,
                    ImagemThumbnailUrl = "http://www.macoratti.net/Imagens/lanches/mistoquente4.jpg",
                    ImagemUrl = "http://www.macoratti.net/Imagens/lanches/mistoquente4.jpg",
                    IsLanchePreferido = false,
                    Preco = 8.00m
                },

                new Lanche
                {
                    LancheId = 3,
                    CategoriaId = 1,
                    Nome = "Cheese Burger",
                    DescricaoCurta = "Pão, hambúrger, presunto, mussarela e batalha palha",
                    DescricaoDetalhada = "Pão de hambúrger especial com hambúrger de nossa preparação e presunto e mussarela; acompanha batata palha.",
                    EmEstoque = true,
                    ImagemThumbnailUrl = "http://www.macoratti.net/Imagens/lanches/cheeseburger1.jpg",
                    ImagemUrl = "http://www.macoratti.net/Imagens/lanches/cheeseburger1.jpg",
                    IsLanchePreferido = false,
                    Preco = 11.00m
                },

                new Lanche
                {
                    LancheId = 4,
                    CategoriaId = 2,
                    Nome = "Lanche Natural Peito Peru",
                    DescricaoCurta = "Pão Integral, queijo branco, peito de peru, cenoura, alface, iogurte",
                    DescricaoDetalhada = "Pão integral natural com queijo branco, peito de peru e cenoura ralada com alface picado e iorgurte natural.",
                    EmEstoque = true,
                    ImagemThumbnailUrl = "http://www.macoratti.net/Imagens/lanches/lanchenatural.jpg",
                    ImagemUrl = "http://www.macoratti.net/Imagens/lanches/lanchenatural.jpg",
                    IsLanchePreferido = true,
                    Preco = 15.00m
                }
            );
        }
    }
}
