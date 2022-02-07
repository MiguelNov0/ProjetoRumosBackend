using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoRumosWebApi.Dtos.Comment;
using ProjetoRumosWebApi.ServiceResponse;
using ProjetoRumosWebApi.Services.CommentService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoRumosWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeCommentController : ControllerBase
    {
        private readonly IRecipeCommentService _recipeCommentService;

        public RecipeCommentController (IRecipeCommentService recipeCommentService)
        {
            _recipeCommentService = recipeCommentService;
        }
        [HttpGet]
        [Route("GetAllByRecipeId")]
        public async Task<ActionResult<ServiceResponse<List<GetRecipeCommentDto>>>> GetAllByRecipeId(int recipeId)
        {
            return Ok(await _recipeCommentService.GetAllByRecipeId(recipeId));
        }
        [HttpPost]
        [Route("AddComment")]
        public async Task<ActionResult<ServiceResponse<List<GetRecipeCommentDto>>>> AddComment(string newComment, int userId, int recipeId)
        {
            return Ok(await _recipeCommentService.AddComment(newComment, userId,recipeId));
        }
    }
}
