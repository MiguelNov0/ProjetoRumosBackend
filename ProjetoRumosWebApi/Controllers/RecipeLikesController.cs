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
    public class RecipeLikesController : ControllerBase
    {
        private readonly ProjetoRumosContext _context;

        public RecipeLikesController(ProjetoRumosContext context)
        {
            _context = context;
        }
        [HttpGet("GetLikes")]
        public async Task<ServiceResponse<int>> GetLikes(int recipeId)
        {
            var sr = new ServiceResponse<int>();

            try
            {
                List<UserRecipeLike> likes = await _context.UserRecipeLikes.Where(r => r.Recipe.Id==recipeId).ToListAsync();

                int totalLikes = likes.Count;

                sr.Data = totalLikes;

            }
            catch (Exception e)
            {
                sr.Message = e.Message;
                sr.Success = false;
            }
            return sr;
        }

        
        [HttpGet("AddLike")]
        public async Task<ServiceResponse<int>> AddLike(int recipeId, int userId)
        {
            var sr = new ServiceResponse<int>();

            try
            {
                UserRecipeLike like = new UserRecipeLike();
                like.Recipe = await _context.Recipes.FirstAsync(r => r.Id == recipeId);
                like.User = await _context.Users.FirstAsync(u => u.Id == userId);

                await _context.UserRecipeLikes.AddAsync(like);
                await _context.SaveChangesAsync();

                List<UserRecipeLike> likes = await _context.UserRecipeLikes.Where(r => r.Recipe.Id == recipeId).ToListAsync();

                int totalLikes = likes.Count;

                sr.Data = totalLikes;

            }
            catch (Exception e)
            {
                sr.Message = e.Message;
                sr.Success = false;
            }
            return sr;

        }

        [HttpGet("RemoveLike")]
        public async Task<ServiceResponse<int>> RemoveLike(int recipeId, int userId)
        {
            var sr = new ServiceResponse<int>();

            try
            {
                UserRecipeLike like = await _context.UserRecipeLikes.FirstAsync(r => r.Recipe.Id == recipeId && r.User.Id==userId);

                _context.UserRecipeLikes.Remove(like);
                await _context.SaveChangesAsync();

                List<UserRecipeLike> likes = await _context.UserRecipeLikes.Where(r => r.Recipe.Id == recipeId).ToListAsync();

                int totalLikes = likes.Count;

                sr.Data = totalLikes;

            }
            catch (Exception e)
            {
                sr.Message = e.Message;
                sr.Success = false;
            }
            return sr;

        }

        [HttpGet("CheckIfLiked")]
        public async Task<ServiceResponse<bool>> CheckIfLiked(int userId, int recipeId)
        {
            var sr = new ServiceResponse<bool>();

            try
            {
                sr.Data = await _context.UserRecipeLikes.AnyAsync(l => l.Recipe.Id == recipeId && l.User.Id == userId);          

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
