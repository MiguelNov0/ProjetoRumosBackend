using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoRumosWebApi.Dtos.RecipeIngredient
{
    public class UpdateRecipeIngredientDto
    {
        public int Id { get; set; }
        public string Quantity { get; set; }
    }
}
