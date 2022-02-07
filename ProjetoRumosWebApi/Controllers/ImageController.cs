using DAL.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoRumosWebApi.Dtos.Image;
using ProjetoRumosWebApi.ServiceResponse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoRumosWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly ProjetoRumosContext _context;

        public ImageController(ProjetoRumosContext context)
        {

            _context = context;
        }

        [HttpGet("GetImages")]
        public async Task<ServiceResponse<List<ImageDto>>> GetImages()
        {
            var sr = new ServiceResponse<List<ImageDto>>();
            var imageFiles = new List<ImageDto>();
            

            try
            {
                List<string> images = await _context.Recipes.Select(r => r.ImagePath).ToListAsync();
                images.AddRange(await _context.Users.Select(r => r.PhotoPath).ToListAsync());
                foreach(var imagePath in images)
                {
                    var imageFile = new ImageDto();
                    //converte a imagem em bytes
                    Byte[] image = await System.IO.File.ReadAllBytesAsync(imagePath);
                    imageFile.file = File(image, "image/jpeg");
                    imageFile.filePath = imagePath;
                    imageFiles.Add(imageFile);

                }
                               

                sr.Data = imageFiles;

            }
            catch (Exception e)
            {
                sr.Message = e.Message;
                sr.Success = false;
            }
            return sr;

        }
        [HttpGet("GetImage")]
        public async Task<ServiceResponse<string>> GetImage(string filePath)
        {

            var sr = new ServiceResponse<string>();

            //converte a imagem em bytes
            Byte[] image = await System.IO.File.ReadAllBytesAsync(filePath);
            string base64 = Convert.ToBase64String(image);
            sr.Data = base64;
            return sr;          

        }

        [HttpPost("Upload")]
        public async Task<ServiceResponse<ImageDto>> Upload([FromForm] IFormFile file)
        {
            var sr = new ServiceResponse<ImageDto>();
            var imageFile = new ImageDto();

            try
            {
                //Cria o diretorio
                string dir = @"Images";
                if (!Directory.Exists(dir))
                {
                    DirectoryInfo di = Directory.CreateDirectory(dir); 
                }

                //guarda a imagen no server
                string fName = file.FileName;
                string path = Path.Combine(@"Images\" + fName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                //converte a imagem em bytes
                Byte[] image = await System.IO.File.ReadAllBytesAsync(path);

                imageFile.file = File(image, "image/jpeg");
                imageFile.filePath = path;

                sr.Data = imageFile;
                
            }
            catch (Exception e)
            {
                sr.Message = e.Message;
                sr.Success = false;
            }
            return sr;

        }
    }
}
