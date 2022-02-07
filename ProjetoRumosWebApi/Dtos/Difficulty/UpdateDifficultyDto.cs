using ProjetoRumosClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoRumosWebApi.Dtos.Difficulty
{
    public class UpdateDifficultyDto
    {
        public int Id { get; set; }
        public string DifficultyLevel { get; set; }
    }
}
