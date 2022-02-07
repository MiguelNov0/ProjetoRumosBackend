using Newtonsoft.Json;
using ProjetoRumosClassLibrary;
using ProjetoRumosWebApi.Dtos.Category;
using ProjetoRumosWebApi.Dtos.Difficulty;
using ProjetoRumosWebApi.Dtos.Ingredient;
using ProjetoRumosWebApi.Dtos.User;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProjetoRumosWebApi.Dtos.Recipe
{
    public class GetRecipeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsVegan { get; set; }
        public bool IsVegetarian { get; set; }
        public bool IsLowCarb { get; set; }
        public bool IsGlutenFree { get; set; }
        public bool IsLactoseFree { get; set; }
        public string ImagePath { get; set; }
        public int DifficultyId { get; set; }
        public int CategoryId { get; set; }
        public string Portion { get; set; }
        public string PrepTime { get; set; }
        public int UserId { get; set; }
        public ICollection<GetIngredientDto> Ingredients { get; set; }
    }
}

