using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoRumosWebApi.Dtos.Image
{
    public class ImageDto
    {
        public string filePath { get; set; }
        public FileContentResult file { get; set; }
    }
}
