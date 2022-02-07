using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoRumosClassLibrary
{
    public class Recipe // entidade Receita, com  id como chave primaria para EF e listas favoriteRecipe para relação de muitos para muitos entre a tabela receitas e a tabela Users 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }        

        public string Name { get; set; }
        
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public bool isVegan { get; set; }
        public bool isVegetarian { get; set; }
        public bool isLowCarb { get; set; }
        public bool isGlutenFree { get; set; }
        public bool isLactoseFree { get; set; }
        public string ImagePath { get; set; }
        public string Portion { get; set; }
        public string PrepTime { get; set; }
        public bool Active { get; set; } = true;


        public ICollection<RecipeComment> RecipeComments { get; set; }
        public ICollection<UserRecipeLike> UserRecipeLikes { get; set; }
        public ICollection<UserRecipeFavourite> UserRecipeFavourites { get; set; }
        public ICollection<Ingredient> Ingredients { get; set; }
        
        [DisplayName("UserId")]
        public User User { get; set; }
        [DisplayName("CategoryId")]
        public Category Category { get; set; }
        [DisplayName("DifficultyId")]
        public Difficulty Difficulty { get; set; }

    }
}
