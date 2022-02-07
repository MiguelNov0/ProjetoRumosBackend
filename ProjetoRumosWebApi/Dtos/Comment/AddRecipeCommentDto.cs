using ProjetoRumosWebApi.Dtos.Recipe;
using ProjetoRumosWebApi.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoRumosWebApi.Dtos.Comment
{
    public class AddRecipeCommentDto
    {
        public string Comment { get; set; }
        public GetUserDto UserId { get; set; }
        public GetRecipeDto RecipeId { get; set; }
    }
}
