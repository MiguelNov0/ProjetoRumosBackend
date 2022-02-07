using AutoMapper;
using ProjetoRumosClassLibrary;
using ProjetoRumosWebApi.Dtos.Category;
using ProjetoRumosWebApi.Dtos.Comment;
using ProjetoRumosWebApi.Dtos.Difficulty;
using ProjetoRumosWebApi.Dtos.Ingredient;
using ProjetoRumosWebApi.Dtos.Measurement;
using ProjetoRumosWebApi.Dtos.Recipe;
using ProjetoRumosWebApi.Dtos.RecipeIngredient;
using ProjetoRumosWebApi.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoRumosWebApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, GetUserDto>();            
            CreateMap<GetUserDto, User>();            
            CreateMap<AddUserDto, User>();

            CreateMap<Category, GetCategoryDto>();
            CreateMap<GetCategoryDto, Category>();
            CreateMap<AddCategoryDto, Category>();
            
            CreateMap<RecipeComment, GetRecipeCommentDto>()
                .ForMember(dest => dest.UserId, act => act.MapFrom(src => src.User));
            CreateMap<AddRecipeCommentDto, RecipeComment>().ForMember(dest =>
            dest.User, act => act.MapFrom(src => src.UserId)).ForMember(dest =>
            dest.Recipe, act => act.MapFrom(src => src.RecipeId));


            CreateMap<Ingredient, GetIngredientDto>();
            CreateMap<AddIngredientDto, Ingredient>();
            CreateMap<Ingredient, AddIngredientDto>();

            //Mapping Nested Dtos
            CreateMap<Recipe, GetRecipeDto>()
                .ForMember(dest => dest.Ingredients, act => act.MapFrom(src => src.Ingredients));
            CreateMap<AddRecipeDto, Recipe>()
                .ForMember(dest => dest.Ingredients, act => act.MapFrom(src => src.Ingredients));
            CreateMap<UpdateRecipeDto, Recipe>();

            CreateMap<Difficulty, GetDifficultyDto>();
            CreateMap<GetDifficultyDto, Difficulty>();
            CreateMap<AddDifficultyDto, Difficulty>();






        }
    }
}
