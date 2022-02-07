using AutoMapper;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using ProjetoRumosClassLibrary;
using ProjetoRumosWebApi.Dtos.User;
using ProjetoRumosWebApi.ServiceResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoRumosWebApi.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly ProjetoRumosContext _context;

        public UserService(IMapper mapper, ProjetoRumosContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<ServiceResponse<GetUserDto>> GetUserById(int userId)
        {
            var response = new ServiceResponse<GetUserDto>();
            try
            {
                User user = await _context.Users.FirstAsync(u => u.Id == userId);
                GetUserDto userdto = _mapper.Map<GetUserDto>(user);
                response.Data = userdto;

            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Success = false;
            }
            return response;
        }

        public async Task<ServiceResponse<GetUserDto>> DeleteUser(int userId)
        {
            var serviceResponse = new ServiceResponse<GetUserDto>();
            try
            {

                User user = await _context.Users.FirstAsync(c => c.Id == userId);
                user.Active = false;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetUserDto>(user);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;

            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetUserDto>> UpdateImage(string email,string imagePath)
        {
            var response = new ServiceResponse<GetUserDto>();
            try
            {
                User user = await _context.Users.FirstAsync(u => u.Email == email);
                user.PhotoPath = imagePath;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                GetUserDto userdto = _mapper.Map<GetUserDto>(user);
                response.Data = userdto;

            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Success = false;
            }
            return response;
        }

    }
}
