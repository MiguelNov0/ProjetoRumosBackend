using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoRumosWebApi.Dtos.Category;
using ProjetoRumosWebApi.Dtos.Difficulty;
using ProjetoRumosWebApi.Dtos.Recipe;
using ProjetoRumosWebApi.Dtos.User;
using ProjetoRumosWebApi.ServiceResponse;
using ProjetoRumosWebApi.Services.RecipeService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoRumosWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetRecipeDto>>>> GetAll()
        {
            return Ok(await _recipeService.GetAllRecipes());
        }
        [Authorize]
        [HttpGet]
        [Route("GetFromUser")]
        public async Task<ActionResult<ServiceResponse<List<GetRecipeDto>>>> GetFromUserId(int userId)
        {
            return Ok(await _recipeService.GetAllRecipesFromUser(userId));
        }

        [HttpGet]
        [Route("GetByCategory")]
        public async Task<ActionResult<ServiceResponse<List<GetRecipeDto>>>> GetByCategory(string categoryName)
        {
            return Ok(await _recipeService.GetRecipesByCategory(categoryName));
        }
        [HttpGet]
        [Route("Search")]
        public async Task<ActionResult<ServiceResponse<List<GetRecipeDto>>>> SearchRecipes(string searchQuery)
        {
            return Ok(await _recipeService.SearchRecipes(searchQuery));
        }
        [HttpGet]
        [Route("GetFavourites")]
        public async Task<ActionResult<ServiceResponse<List<GetRecipeDto>>>> GetFavourites(int userId)
        {
            return Ok(await _recipeService.GetFavourites(userId));
        }

        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult<ServiceResponse<List<GetRecipeDto>>>> Add(AddRecipeDto recipe)
        {
            return Ok(await _recipeService.AddRecipe(recipe));
        }
        [HttpPut]
        [Route("Update")]
        public async Task<ActionResult<ServiceResponse<List<GetRecipeDto>>>> Update(UpdateRecipeDto recipe)
        {
            return Ok(await _recipeService.UpdateRecipe(recipe));
        }
        [HttpDelete]
        [Route("Delete")]
        public async Task<ActionResult<ServiceResponse<List<GetRecipeDto>>>> Delete(int recipeId)
        {
            return Ok(await _recipeService.DeleteRecipe(recipeId));
        }
    }
}
