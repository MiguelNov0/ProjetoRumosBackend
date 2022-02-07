using AutoMapper;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using ProjetoRumosClassLibrary;
using ProjetoRumosWebApi.Dtos.Category;
using ProjetoRumosWebApi.Dtos.Difficulty;
using ProjetoRumosWebApi.Dtos.Ingredient;
using ProjetoRumosWebApi.Dtos.Recipe;
using ProjetoRumosWebApi.Dtos.User;
using ProjetoRumosWebApi.ServiceResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoRumosWebApi.Services.RecipeService
{
    public class RecipeService : IRecipeService
    {

        private readonly IMapper _mapper;
        private readonly ProjetoRumosContext _context;

        public RecipeService(IMapper mapper, ProjetoRumosContext context)
        {
            _mapper = mapper;
            _context = context;
        }


        public async Task<ServiceResponse<List<GetRecipeDto>>> AddRecipe(AddRecipeDto newRecipe)

        {
            var serviceResponse = new ServiceResponse<List<GetRecipeDto>>();
            try
            {
                User user = await _context.Users.FirstAsync(u => u.Id == newRecipe.UserId);
                Difficulty difficulty = await _context.DifficultyLevels.FirstAsync(d => d.Id == newRecipe.DifficultyId);
                Category category = await _context.Categories.FirstAsync(c => c.Id == newRecipe.CategoryId);
                Recipe recipe = _mapper.Map<Recipe>(newRecipe);
                recipe.Category = category;
                recipe.Difficulty = difficulty;
                recipe.User = user;
             
                _context.Recipes.Add(recipe);
                await _context.SaveChangesAsync();

                var dbRecipes = await _context.Recipes.Where(r => r.Active == true).Include(r => r.Ingredients).Include(r => r.User).Include(r => r.Difficulty).Include(r => r.Category).OrderBy(r => r.Id).ToListAsync();

                serviceResponse.Data = dbRecipes.Select(c => _mapper.Map<GetRecipeDto>(c)).ToList();
                return serviceResponse;
            }
            catch (Exception ex)
            {

                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;

        }

        public async Task<ServiceResponse<List<GetRecipeDto>>> DeleteRecipe(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetRecipeDto>>();
            try
            {
                
                Recipe recipe = await _context.Recipes.FirstAsync(c => c.Id == id);
                recipe.Active = false;
                _context.Recipes.Update(recipe);
                await _context.SaveChangesAsync();

                serviceResponse.Data = _context.Recipes.Select(c => _mapper.Map<GetRecipeDto>(c)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;

            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetRecipeDto>>> GetAllRecipes()
        {
            var serviceResponse = new ServiceResponse<List<GetRecipeDto>>();
            var dbRecipes = await _context.Recipes.Where(r=> r.Active==true).Include(r => r.Ingredients).Include(r=>r.User).Include(r=>r.Difficulty).Include(r=>r.Category).OrderBy(r=>r.Id).ToListAsync();
            
            serviceResponse.Data = dbRecipes.Select(c => _mapper.Map<GetRecipeDto>(c)).ToList();
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<GetRecipeDto>>> SearchRecipes(string searchQuery)
        {
            var serviceResponse = new ServiceResponse<List<GetRecipeDto>>();
            var dbRecipes = await _context.Recipes.Where(r => r.Name.Contains(searchQuery)||r.User.FirstName.Contains(searchQuery)|| r.User.LastName.Contains(searchQuery)|| r.User.Email.Contains(searchQuery)).Where(r => r.Active == true).Include(r => r.Ingredients).Include(r => r.User).Include(r => r.Difficulty).Include(r => r.Category).OrderBy(r => r.Id).ToListAsync();
            
            
            serviceResponse.Data = dbRecipes.Select(c => _mapper.Map<GetRecipeDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetRecipeDto>>> GetFavourites(int userId)
        {
            var serviceResponse = new ServiceResponse<List<GetRecipeDto>>();
            List<UserRecipeFavourite> favourites =await _context.UserRecipeFavourites.Where(f => f.User.Id == userId).Include(f=>f.Recipe).Include(f=>f.User).ToListAsync();
            List<Recipe> recipes = new List<Recipe>();
            foreach(var fav in favourites)
            {
                var recipe = await _context.Recipes.FirstAsync(r => r.Id == fav.Recipe.Id);
                recipes.Add(recipe);
            }
            

            serviceResponse.Data = recipes.Select(c => _mapper.Map<GetRecipeDto>(c)).ToList();
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<GetRecipeDto>>> GetAllRecipesFromUser(int userId)
        {
            var serviceResponse = new ServiceResponse<List<GetRecipeDto>>();
            var dbRecipes = await _context.Recipes.Where(r=>r.User.Id == userId).Where(r => r.Active == true).Include(r => r.User).Include(r => r.Category).Include(r => r.Difficulty).Include(r => r.Ingredients).OrderBy(r => r.Id).ToListAsync();
            serviceResponse.Data = dbRecipes.Select(c => _mapper.Map<GetRecipeDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetRecipeDto>>> GetRecipesByCategory(string categoryName)
        {
            var serviceResponse = new ServiceResponse<List<GetRecipeDto>>();
            var dbRecipes = await _context.Recipes.Where(r => r.Category.CategoryName == categoryName).Where(r => r.Active == true).Include(r => r.User).Include(r => r.Category).Include(r => r.Difficulty).Include(r => r.Ingredients).OrderBy(r => r.Id).ToListAsync();
            serviceResponse.Data = dbRecipes.Select(c => _mapper.Map<GetRecipeDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetRecipeDto>> UpdateRecipe(UpdateRecipeDto updatedRecipe)
        {
            var serviceResponse = new ServiceResponse<GetRecipeDto>();
            try
            {
                List<Ingredient> ingredients = _mapper.Map<List<Ingredient>>(updatedRecipe.Ingredients);
                Category category = _mapper.Map<Category>(updatedRecipe.Category);
                Difficulty difficulty = _mapper.Map<Difficulty>(updatedRecipe.Difficulty);
                User user = _mapper.Map<User>(updatedRecipe.User);
                
                Recipe recipe =new Recipe();
                recipe.Id = updatedRecipe.Id;
                recipe.User = user;
                recipe.Ingredients = ingredients;
                recipe.Name = updatedRecipe.Name;
                recipe.Category = category;
                recipe.CreationDate = updatedRecipe.CreationDate;
                recipe.Difficulty = difficulty;
                recipe.Description = updatedRecipe.Description;
                recipe.ImagePath = updatedRecipe.ImagePath;
                recipe.isGlutenFree = updatedRecipe.isGlutenFree;
                recipe.isLactoseFree = updatedRecipe.isLactoseFree;
                recipe.isLowCarb = updatedRecipe.isLowCarb;
                recipe.isVegan = updatedRecipe.isVegan;
                recipe.isVegetarian = updatedRecipe.isVegetarian;
                recipe.Portion = updatedRecipe.Portion;
                recipe.PrepTime = updatedRecipe.PrepTime;
                
                

                //Remove os antigos ingredientes desta receita
                List<Ingredient> outdatedIgredients = await _context.Ingredients.Where(i => i.Recipe.Id == updatedRecipe.Id).ToListAsync();
                _context.Ingredients.RemoveRange(outdatedIgredients);

                //Recipe outedatedRecipe = await _context.Recipes.FirstAsync(r => r.Id == updatedRecipe.Id);
                //_context.Recipes.Remove(outedatedRecipe);
                _context.Recipes.Update(recipe);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetRecipeDto>(recipe);
                return serviceResponse;

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;

            }
            return serviceResponse;
        }
    }
}
