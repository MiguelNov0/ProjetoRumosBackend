using ProjetoRumosClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoRumosWebApi.Dtos.Category
{
    public class UpdateCategoryDto
    {
        public int Id { get; set; }
        public string categoryName { get; set; }

    }
}
