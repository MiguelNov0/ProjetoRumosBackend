using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoRumosClassLibrary
{
    public class Difficulty
    {
        [Key]
        public int Id { get; set; }
        public string DifficultyLevel { get; set; }
        ICollection<Recipe> Recipes { get; set; }
    }
}
