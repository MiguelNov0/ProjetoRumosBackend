using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoRumosWebApi.Data;
using ProjetoRumosWebApi.Dtos.User;
using ProjetoRumosWebApi.ServiceResponse;
using ProjetoRumosWebApi.Services.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoRumosWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthRepo _authRepo;
        private readonly IUserService _userService;

        public UserController(IAuthRepo authRepo, IUserService userService)
        {
            _authRepo = authRepo;
            _userService = userService;
        }
        [HttpPost]
        [Route("UpdateEmail")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> UpdateEmail(string email, string updatedEmail)
        {
            return Ok(await _authRepo.UpdateEmail(email, updatedEmail));
        }

        [HttpPost]
        [Route("UpdatePassword")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> UpdatePassword(string email, string pw)
        {
            return Ok(await _authRepo.UpdatePassword(email, pw));
        }

        [HttpPost]
        [Route("UpdateImage")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> UpdateImage(string email, string imagePath)
        {
            return Ok(await _userService.UpdateImage(email, imagePath));
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> Register(AddUserDto newUser)
        {
            return Ok(await _authRepo.Register(newUser));
        }
        [HttpGet]
        [Route("login")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> Login(string email, string password)
        {
            return Ok(await _authRepo.Login(email,password));
        }

        [HttpGet]
        [Route("GetByUserId")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> GetByUserId(int userId)
        {
            return Ok(await _userService.GetUserById(userId));
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> Delete(int userId)
        {
            return Ok(await _userService.DeleteUser(userId));
        }
    }
}
