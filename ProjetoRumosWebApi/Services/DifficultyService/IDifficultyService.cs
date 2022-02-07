using ProjetoRumosWebApi.Dtos.Difficulty;
using ProjetoRumosWebApi.ServiceResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoRumosWebApi.Services.DifficultyService
{
    public interface IDifficultyService
    {
        Task<ServiceResponse<List<GetDifficultyDto>>> GetAllDifficulties();
        Task<ServiceResponse<GetDifficultyDto>> GetDifficultyById(int diffultyId);
    }
}
