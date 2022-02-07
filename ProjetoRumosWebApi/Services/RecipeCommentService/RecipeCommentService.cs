using AutoMapper;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using ProjetoRumosClassLibrary;
using ProjetoRumosWebApi.Dtos.Comment;
using ProjetoRumosWebApi.Dtos.Recipe;
using ProjetoRumosWebApi.ServiceResponse;
using ProjetoRumosWebApi.Services.CommentService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoRumosWebApi.Services.RecipeCommentService
{
    public class RecipeCommentService : IRecipeCommentService
    {
        private readonly IMapper _mapper;
        private readonly ProjetoRumosContext _context;
        public RecipeCommentService(IMapper mapper, ProjetoRumosContext context)
        {
            _mapper = mapper;
            _context = context;
        }

     
        public async Task<ServiceResponse<List<GetRecipeCommentDto>>> AddComment(string newComment, int userId, int recipeId)
        {
            var serviceResponse = new ServiceResponse<List<GetRecipeCommentDto>>();
            try
            {
                RecipeComment comment = new RecipeComment();
                comment.Recipe = await _context.Recipes.FirstAsync(r=> r.Id ==recipeId);
                comment.User = await _context.Users.FirstAsync(u => u.Id == userId);
                comment.Comment = newComment;
                _context.RecipeComments.Add(comment);

                await _context.SaveChangesAsync();

                serviceResponse.Data = await _context.RecipeComments.Select(c => _mapper.Map<GetRecipeCommentDto>(c)).ToListAsync();
            }
            catch (Exception e)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = e.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetRecipeCommentDto>>> GetAllByRecipeId(int recipeId)
        {
            var serviceResponse = new ServiceResponse<List<GetRecipeCommentDto>>();
            try
            {
                var dbComments = await _context.RecipeComments.Include(c=>c.User).Include(c=>c.Recipe).Where(c=>c.Recipe.Id == recipeId).ToListAsync();
                serviceResponse.Data = dbComments.Select(c => _mapper.Map<GetRecipeCommentDto>(c)).ToList();
            }
            catch (Exception e)
            {
                serviceResponse.Success = false;
                serviceResponse.Message=e.Message;
            }
            return serviceResponse;
        }

        
    }
}
