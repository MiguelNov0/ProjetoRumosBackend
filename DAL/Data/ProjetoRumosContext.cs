using Microsoft.EntityFrameworkCore;
using ProjetoRumosClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public class ProjetoRumosContext : DbContext
    {
        //Definir ligação a base de dados
        public ProjetoRumosContext(DbContextOptions<ProjetoRumosContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Difficulty>().HasData(
                new {Id=1, DifficultyLevel = "Fácil"},
                new { Id = 2, DifficultyLevel = "Média"},
                new { Id = 3, DifficultyLevel = "Díficil"});
            modelBuilder.Entity<Category>().HasData(
                new { Id = 1, CategoryName = "Básicos"},
                new { Id = 2, CategoryName = "Entradas"},
                new { Id = 3, CategoryName = "Sopas"},
                new { Id = 4, CategoryName = "Acompanhamentos"},
                new { Id = 5, CategoryName = "Salgados,Tartes e Pizas"},
                new { Id = 6, CategoryName = "Arroz e Massas"},
                new { Id = 7, CategoryName = "Pratos de Peixe"},
                new { Id = 8, CategoryName = "Pratos de Carne"},
                new { Id = 9, CategoryName = "Pratos Vegetarianos"},
                new { Id = 10, CategoryName = "Pães"},
                new { Id = 11, CategoryName = "Molhos, Temperos e Patês"},
                new { Id = 12, CategoryName = "Geleias, Doces e Compotas"},
                new { Id = 13, CategoryName = "Sobremesas"},
                new { Id = 14, CategoryName = "Bolos e Biscoitos"},
                new { Id = 15, CategoryName = "Bebidas"});
            base.OnModelCreating(modelBuilder);

        }

        // Definir tabelas da base de dados
        public DbSet<Category> Categories { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeComment> RecipeComments { get; set; }
        public DbSet<UserRecipeFavourite> UserRecipeFavourites { get; set; }
        public DbSet<UserRecipeLike> UserRecipeLikes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Difficulty> DifficultyLevels { get; set; }

        //COnvert Enum da propriedade DifficultyType da class Difficulty de int(default "Key") para String(Value)

    }
}
