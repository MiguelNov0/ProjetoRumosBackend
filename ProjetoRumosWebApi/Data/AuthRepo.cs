using AutoMapper;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjetoRumosClassLibrary;
using ProjetoRumosWebApi.Dtos.User;
using ProjetoRumosWebApi.ServiceResponse;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoRumosWebApi.Data
{
    public class AuthRepo : IAuthRepo
    {
        private readonly ProjetoRumosContext _context;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly DateTime _expirationDate;

        public AuthRepo(ProjetoRumosContext context, IConfiguration config, IMapper mapper)
        {
            _context = context;
            _config = config; // -----> para buscar o token ao appsettings.config
            _mapper = mapper;
            _expirationDate = DateTime.Now.AddHours(12);
        }
        public async Task<ServiceResponse<GetUserDto>> Login(string email, string password)
        {
            ServiceResponse<GetUserDto> response = new ServiceResponse<GetUserDto>();
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
            if (user == null)
            {
                response.Success = false;
                response.Message = "Este email ainda não se encontra registado!";
            }
            else if(VerifyPassHash(password, user.PasswordHash, user.PasswordSalt) == false)
            {
                response.Success = false;
                response.Message = "Dados de Login Incorretos!";
            }
            else if (user.Active==false)
            {
                response.Success = false;
                response.Message = "Esta Conta foi Eliminada!";
            }
            else
            {
                GetUserDto userDto = _mapper.Map<GetUserDto>(user);
                userDto.Token = CreateToken(user);
                userDto.TokenExpirationDate  = _expirationDate;
                response.Data = userDto;
            }
            return response;
        }
        public async Task<ServiceResponse<GetUserDto>> UpdatePassword(string email, string pw)
        {
            ServiceResponse<GetUserDto> response = new ServiceResponse<GetUserDto>();
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());

            //Validação se o email já se encontra registado
            if (!await UserExists(user.Email))
            {
                response.Success = false;
                response.Message = "Este email não existe, por favor faça registe-se.";
                return response;
            }
            CreatePassHash(pw, out byte[] passHash, out byte[] passSalt);

            user.PasswordHash = passHash;
            user.PasswordSalt = passSalt;
            user.RegisterDate = DateTime.Now;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            GetUserDto userDto = _mapper.Map<GetUserDto>(user);
            response.Data = userDto;
            return response;
        }
        public async Task<ServiceResponse<GetUserDto>> UpdateEmail(string email, string updatedEmail)
        {
            ServiceResponse<GetUserDto> response = new ServiceResponse<GetUserDto>();

            try
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());

                //Validação se o email já se encontra registado
                if (!await UserExists(user.Email))
                {
                    response.Success = false;
                    response.Message = "Este email não existe, por favor faça registe-se.";
                    return response;
                }

                user.Email = updatedEmail;

                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                GetUserDto userDto = _mapper.Map<GetUserDto>(user);
                response.Data = userDto;
            }
            catch (Exception e)
            {

                response.Success = false;
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<GetUserDto>> Register(AddUserDto newUser)
        {
            User user = _mapper.Map<User>(newUser);
            
            ServiceResponse<GetUserDto> response = new ServiceResponse<GetUserDto>();
            //Validação se o email já se encontra registado
            if(await UserExists(user.Email))
            {
                response.Success = false;
                response.Message = "Este email já existe, por favor faça login.";
                return response;
            }
            CreatePassHash(newUser.Password, out byte[] passHash, out byte[] passSalt);

            user.PasswordHash = passHash;
            user.PasswordSalt = passSalt;
            user.RegisterDate = DateTime.Now;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            GetUserDto userDto = _mapper.Map<GetUserDto>(user);
            response.Data = userDto;
            return response;
        }

        public async Task<bool> UserExists(string email)
        {
            //Se houver este email na bd e estiver ativado return true
            if(await _context.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower() && u.Active==true))
            {
                return true;
            }
            return false;
        }

        //método para encriptar a password
        private void CreatePassHash(string password, out byte[] passHash, out byte[] passSalt)
        {
            using( var hmac = new HMACSHA512())
            {
                passSalt = hmac.Key;
                passHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPassHash(string password, byte[] passHash, byte[]passSalt)
        {
            //usando a passSalt
            using (var hmac = new HMACSHA512(passSalt))
            {
                //cria uma nova passHash baseada na pass introduzida
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                //Compara cada byte da passHash guardada na db com cada byte da computedHass
                for(int i = 0; i < computedHash.Length; i++)
                {
                    //se não for igual, pass incorreta
                    if(computedHash[i] != passHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            { 
                //claim para o id do user
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                //claim para o email do user
                new Claim(ClaimTypes.Email, user.Email),
            };
            //Cria uma chave de segurança para o token que está no config
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            //Credenciais
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            
            //obtem a info para criar o token final com as claims e a data de expiração
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                //12h de validade
                Expires = _expirationDate,
                SigningCredentials = credentials
            };

            //Criar o Token
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            
            //retorna o json web token como string
            return tokenHandler.WriteToken(token);
        }
        
    }
    
}
