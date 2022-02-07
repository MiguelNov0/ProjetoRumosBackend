using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoRumosWebApi.Dtos.Difficulty;
using ProjetoRumosWebApi.ServiceResponse;
using ProjetoRumosWebApi.Services.DifficultyService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoRumosWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DifficultyController : ControllerBase
    {
        private readonly IDifficultyService _difficultyService;

        public DifficultyController(IDifficultyService difficultyService)
        {
            _difficultyService = difficultyService;
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetDifficultyDto>>>> GetAll()
        {
            return Ok(await _difficultyService.GetAllDifficulties());
        }
        [HttpGet]
        [Route("GetByName")]
        public async Task<ActionResult<ServiceResponse<GetDifficultyDto>>> GetByDifficultyId(int difficultyId)
        {
            return Ok(await _difficultyService.GetDifficultyById(difficultyId));
        }
    }
}
