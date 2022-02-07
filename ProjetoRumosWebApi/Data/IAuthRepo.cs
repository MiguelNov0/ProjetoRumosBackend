using ProjetoRumosClassLibrary;
using ProjetoRumosWebApi.Dtos.User;
using ProjetoRumosWebApi.ServiceResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoRumosWebApi.Data
{
    public interface IAuthRepo
    {
        Task<ServiceResponse<GetUserDto>> Register(AddUserDto user);
        Task<ServiceResponse<GetUserDto>> UpdateEmail(string email, string pw);
        Task<ServiceResponse<GetUserDto>> UpdatePassword(string email, string updatedEmail);
        Task<ServiceResponse<GetUserDto>> Login(string email, string password);
        Task<bool> UserExists(string email);
    }
}
