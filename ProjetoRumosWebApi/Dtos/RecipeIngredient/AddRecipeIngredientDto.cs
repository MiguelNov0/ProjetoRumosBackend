using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoRumosWebApi.Dtos.RecipeIngredient
{
    public class AddRecipeIngredientDto
    {
        public string Quantity { get; set; }
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public int MeasurementId { get; set; }
    }
}
