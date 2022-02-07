using ProjetoRumosWebApi.Dtos.Ingredient;
using ProjetoRumosWebApi.ServiceResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoRumosWebApi.Services.IngredientService
{
   public interface IIngredientService
    {
        Task<ServiceResponse<List<GetIngredientDto>>> GetAllIngredients();
        Task<ServiceResponse<GetIngredientDto>> GetIngredientById(int id);
        Task<ServiceResponse<List<GetIngredientDto>>> AddIngredient(AddIngredientDto newIngredient);
        Task<ServiceResponse<List<GetIngredientDto>>> DeleteIngredient(int id);
    }
}
