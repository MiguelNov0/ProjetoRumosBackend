using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoRumosClassLibrary;
using ProjetoRumosWebApi.Dtos.Category;
using ProjetoRumosWebApi.Dtos.Difficulty;
using ProjetoRumosWebApi.Dtos.Recipe;
using ProjetoRumosWebApi.Dtos.User;
using ProjetoRumosWebApi.ServiceResponse;
using ProjetoRumosWebApi.Services.CategoryService;
using ProjetoRumosWebApi.Services.RecipeService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoRumosWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCategoryDto>>>> GetAll()
        {
            return Ok(await _categoryService.GetAllCategories());
        }
        [HttpGet]
        [Route("GetById")]
        public async Task<ActionResult<ServiceResponse<GetCategoryDto>>> GetById(int categoryId)
        {
            return Ok(await _categoryService.GetCategoryById(categoryId));
        }
    }

}
