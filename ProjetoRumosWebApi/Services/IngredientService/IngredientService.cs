using AutoMapper;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using ProjetoRumosClassLibrary;
using ProjetoRumosWebApi.Dtos.Ingredient;
using ProjetoRumosWebApi.ServiceResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoRumosWebApi.Services.IngredientService
{
    public class IngredientService : IIngredientService
    {

        private readonly IMapper _mapper;
        private readonly ProjetoRumosContext _context;

        public IngredientService(IMapper mapper, ProjetoRumosContext context)
        {
            _mapper = mapper;
            _context = context;
        }


        public async Task<ServiceResponse<List<GetIngredientDto>>> AddIngredient(AddIngredientDto newIngredient)
        {
            var serviceResponse = new ServiceResponse<List<GetIngredientDto>>();
            Ingredient ingredient = _mapper.Map<Ingredient>(newIngredient);

            _context.Ingredients.Add(ingredient);
            await _context.SaveChangesAsync();
            serviceResponse.Data = await _context.Ingredients.Select(c => _mapper.Map<GetIngredientDto>(c)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetIngredientDto>>> DeleteIngredient(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetIngredientDto>>();
            try
            {
                Ingredient ingredient = await _context.Ingredients.FirstAsync(c => c.Id == id);
                _context.Ingredients.Remove(ingredient);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _context.Ingredients.Select(c => _mapper.Map<GetIngredientDto>(c)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;

            }
            return serviceResponse;
        }

        public  async Task<ServiceResponse<List<GetIngredientDto>>> GetAllIngredients()
        {
            var serviceResponse = new ServiceResponse<List<GetIngredientDto>>();
            var dbIngredients = await _context.Ingredients.ToListAsync();
            serviceResponse.Data = dbIngredients.Select(c => _mapper.Map<GetIngredientDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetIngredientDto>> GetIngredientById(int id)
        {
            var serviceResponse = new ServiceResponse<GetIngredientDto>();
            var dbIngredient = await _context.Ingredients.FirstOrDefaultAsync(c => c.Id == id);
            serviceResponse.Data = _mapper.Map<GetIngredientDto>(dbIngredient);
            return serviceResponse;
        }

    }
}
