using AutoMapper;
using ProjetoRumosWebApi.Dtos.Category;
using ProjetoRumosWebApi.ServiceResponse;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using ProjetoRumosClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoRumosWebApi.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly ProjetoRumosContext _context;
        public CategoryService(IMapper mapper, ProjetoRumosContext context)
        {
            _mapper = mapper;
            _context = context;
        }


        public async Task<ServiceResponse<List<GetCategoryDto>>> AddCategory(AddCategoryDto newCategory)
        {
            var serviceResponse = new ServiceResponse<List<GetCategoryDto>>();
            Category category = _mapper.Map<Category>(newCategory);

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            serviceResponse.Data = await _context.Categories.Select(c => _mapper.Map<GetCategoryDto>(c)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCategoryDto>>> DeleteCategory(string categoryName)
        {
            var serviceResponse = new ServiceResponse<List<GetCategoryDto>>();
            try
            {
                Category category = await _context.Categories.FirstAsync(c => c.CategoryName == categoryName);
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _context.Categories.Select(c => _mapper.Map<GetCategoryDto>(c)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCategoryDto>>> GetAllCategories()
        {
            var serviceResponse = new ServiceResponse<List<GetCategoryDto>>();
            var dbCategories = await _context.Categories.ToListAsync();
            serviceResponse.Data = dbCategories.Select(c => _mapper.Map<GetCategoryDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCategoryDto>> GetCategoryById(int categoryId)
        {
            var serviceResponse = new ServiceResponse<GetCategoryDto>();
            var dbCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
            serviceResponse.Data = _mapper.Map<GetCategoryDto>(dbCategory);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCategoryDto>> UpdateCategory(UpdateCategoryDto updatedCategory)
        {
            var serviceResponse = new ServiceResponse<GetCategoryDto>();
            try
            {
                Category category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryName == updatedCategory.categoryName);

                category.CategoryName = updatedCategory.categoryName;


                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetCategoryDto>(category);
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
