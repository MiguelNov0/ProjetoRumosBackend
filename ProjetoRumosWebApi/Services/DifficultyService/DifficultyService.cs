using AutoMapper;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using ProjetoRumosClassLibrary;
using ProjetoRumosWebApi.Dtos.Difficulty;
using ProjetoRumosWebApi.ServiceResponse;
using ProjetoRumosWebApi.Services.DifficultyService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoRumosWebApi.Services.DifficultyService
{
    

    public class DifficultyService : IDifficultyService
    {

        private readonly IMapper _mapper;
        private readonly ProjetoRumosContext _context;

        public DifficultyService(IMapper mapper, ProjetoRumosContext context)
        {
            _mapper = mapper;
            _context = context;
        }


       

        public async Task<ServiceResponse<List<GetDifficultyDto>>> GetAllDifficulties()
        {
            var serviceResponse = new ServiceResponse<List<GetDifficultyDto>>();
            var dbDifficultyLevels = await _context.DifficultyLevels.ToListAsync();
            serviceResponse.Data = dbDifficultyLevels.Select(c => _mapper.Map<GetDifficultyDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetDifficultyDto>> GetDifficultyById(int diffultyId)
        {
            var serviceResponse = new ServiceResponse<GetDifficultyDto>();
            var dbDifficultyLevel = await _context.DifficultyLevels.FirstOrDefaultAsync(c => c.Id == diffultyId);
            serviceResponse.Data = _mapper.Map<GetDifficultyDto>(dbDifficultyLevel);
            return serviceResponse;
        }

       
    }
}
