using ProjetoRumosClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoRumosWebApi.Dtos.Ingredient
{
    public class AddIngredientDto
    {        
        public string IngredientName { get; set; }
        public string Quantity { get; set; }
        public string Measurement { get; set; }
        
    }
}
