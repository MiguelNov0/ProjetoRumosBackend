using ProjetoRumosClassLibrary;
using ProjetoRumosWebApi.Dtos.Category;
using ProjetoRumosWebApi.Dtos.Difficulty;
using ProjetoRumosWebApi.Dtos.Ingredient;
using ProjetoRumosWebApi.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoRumosWebApi.Dtos.Recipe
{
    public class UpdateRecipeDto
    {
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
        public GetDifficultyDto Difficulty { get; set; }
        public GetCategoryDto Category { get; set; }
        public string Portion { get; set; }
        public string PrepTime { get; set; }
        public GetUserDto User { get; set; }
        public List<AddIngredientDto> Ingredients { get; set; }
    }
}
