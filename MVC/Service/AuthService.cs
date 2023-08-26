﻿using System.IdentityModel.Tokens.Jwt;
using MVC.Dto;
using MVC.Entity;
using MVC.Repository;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace MVC.Service
{
    public class AuthService
    {
        public readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool Login(LoginDTO loginDto)
        {
            if (loginDto.Email != null)
            {
                var user = _userRepository.GetUserByEmail(loginDto.Email);

                if (user.Result.Password == loginDto.Password)
                {
                    return true;
                }

                return false;
            }

            return false;
        }

        public async Task<User> GetUserName(string email)
        {
            return await _userRepository.GetUserByEmail(email);
        }

        public async Task<List<User>> GetAllUser()
        {
	        return await _userRepository.GetAllUsersAsync();
        }
    }
}
