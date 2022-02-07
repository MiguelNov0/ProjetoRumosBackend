using ProjetoRumosClassLibrary;
using ProjetoRumosWebApi.Dtos.Category;
using ProjetoRumosWebApi.Dtos.Difficulty;
using ProjetoRumosWebApi.Dtos.Recipe;
using ProjetoRumosWebApi.Dtos.User;
using ProjetoRumosWebApi.ServiceResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoRumosWebApi.Services.RecipeService
{
    public interface IRecipeService
    {
        Task<ServiceResponse<List<GetRecipeDto>>> GetAllRecipes();
        Task<ServiceResponse<List<GetRecipeDto>>> SearchRecipes(string searchQuery);
        Task<ServiceResponse<List<GetRecipeDto>>> GetAllRecipesFromUser(int userId);
        Task<ServiceResponse<List<GetRecipeDto>>> GetRecipesByCategory(string categoryName);
        Task<ServiceResponse<List<GetRecipeDto>>> GetFavourites(int userId);
        Task<ServiceResponse<List<GetRecipeDto>>> AddRecipe(AddRecipeDto newRecipe);
        Task<ServiceResponse<GetRecipeDto>> UpdateRecipe(UpdateRecipeDto updatedRecipe);
        Task<ServiceResponse<List<GetRecipeDto>>> DeleteRecipe(int id);
    }
}
