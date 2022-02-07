using ProjetoRumosWebApi.Dtos.Recipe;
using ProjetoRumosWebApi.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProjetoRumosWebApi.Dtos.Comment
{
    public class GetRecipeCommentDto
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        [JsonPropertyName("User")]
        public GetUserDto UserId { get; set; }
        public int RecipeId { get; set; }
    }
}
