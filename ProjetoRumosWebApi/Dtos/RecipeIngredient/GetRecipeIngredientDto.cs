using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoRumosWebApi.Dtos.RecipeIngredient
{
    public class GetRecipeIngredientDto
    {
        public string Ingredient { get; set; }
        public string Quantity { get; set; }
        public string Measurment { get; set; }
        public int RecipeId { get; set; }
        
    }
}
