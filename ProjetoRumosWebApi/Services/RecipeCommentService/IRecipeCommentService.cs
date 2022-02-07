using ProjetoRumosWebApi.Dtos.Comment;
using ProjetoRumosWebApi.ServiceResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoRumosWebApi.Services.CommentService
{
    public interface IRecipeCommentService
    {
        Task<ServiceResponse<List<GetRecipeCommentDto>>> GetAllByRecipeId(int recipeId);
        Task<ServiceResponse<List<GetRecipeCommentDto>>> AddComment(string newComment, int userId, int recipeId);
    }
}
