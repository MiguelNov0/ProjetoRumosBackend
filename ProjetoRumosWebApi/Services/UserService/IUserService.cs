using ProjetoRumosWebApi.Dtos.User;
using ProjetoRumosWebApi.ServiceResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoRumosWebApi.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<GetUserDto>> GetUserById(int userId);
        Task<ServiceResponse<GetUserDto>> UpdateImage(string email, string imagePath);
        Task<ServiceResponse<GetUserDto>> DeleteUser(int userId);
    }
}
