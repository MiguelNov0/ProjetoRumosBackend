using DAL.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoRumosClassLibrary;
using ProjetoRumosWebApi.ServiceResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoRumosWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouriteController : ControllerBase
    {
        private readonly ProjetoRumosContext _context;

        public FavouriteController(ProjetoRumosContext context)
        {
            _context = context;
        }
        

        [HttpGet("AddFavourite")]
        public async Task<ServiceResponse<int>> AddFavourite(int recipeId, int userId)
        {
            var sr = new ServiceResponse<int>();

            try
            {
                UserRecipeFavourite favourite = new UserRecipeFavourite();
                favourite.Recipe = await _context.Recipes.FirstAsync(r => r.Id == recipeId);
                favourite.User = await _context.Users.FirstAsync(u => u.Id == userId);

                await _context.UserRecipeFavourites.AddAsync(favourite);
                await _context.SaveChangesAsync();

                List<UserRecipeFavourite> favourites = await _context.UserRecipeFavourites.Where(r => r.Recipe.Id == recipeId).ToListAsync();

                int totalLikes = favourites.Count;

                sr.Data = totalLikes;

            }
            catch (Exception e)
            {
                sr.Message = e.Message;
                sr.Success = false;
            }
            return sr;

        }

        [HttpGet("RemoveFavourite")]
        public async Task<ServiceResponse<int>> RemoveFavourite(int recipeId, int userId)
        {
            var sr = new ServiceResponse<int>();

            try
            {
                UserRecipeFavourite favourite = await _context.UserRecipeFavourites.FirstAsync(r => r.Recipe.Id == recipeId && r.User.Id == userId);

                _context.UserRecipeFavourites.Remove(favourite);
                await _context.SaveChangesAsync();

                List<UserRecipeFavourite> favourites = await _context.UserRecipeFavourites.Where(r => r.Recipe.Id == recipeId).ToListAsync();

                int totalLikes = favourites.Count;

                sr.Data = totalLikes;

            }
            catch (Exception e)
            {
                sr.Message = e.Message;
                sr.Success = false;
            }
            return sr;

        }

        [HttpGet("CheckIfFavourited")]
        public async Task<ServiceResponse<bool>> CheckIfFavourited(int userId, int recipeId)
        {
            var sr = new ServiceResponse<bool>();

            try
            {
                sr.Data = await _context.UserRecipeFavourites.AnyAsync(l => l.Recipe.Id == recipeId && l.User.Id == userId);

            }
            catch (Exception e)
            {
                sr.Message = e.Message;
                sr.Success = false;
            }
            return sr;

        }
    }
}
