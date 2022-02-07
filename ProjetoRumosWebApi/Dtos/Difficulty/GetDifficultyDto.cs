using ProjetoRumosClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoRumosWebApi.Dtos.Difficulty
{
    public class GetDifficultyDto
    {
        public int Id { get; set; }
        public string DifficultyLevel { get; set; }
    }
}
