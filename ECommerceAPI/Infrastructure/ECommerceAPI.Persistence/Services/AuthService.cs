using AutoMapper;
using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Abstractions.Token;
using ECommerceAPI.Application.Dtos;
using ECommerceAPI.Application.Features.AppUser.Commands.Login;
using ECommerceAPI.Domain.Entities.Identity;
using ECommerceAPI.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly ITokenHandler _tokenHandler;
        private readonly IUserService _userService;

        public AuthService(UserManager<User> userManager, IMapper mapper, ITokenHandler tokenHandler, SignInManager<User> signInManager, IUserService userService )
        {
            _userManager = userManager;
            _mapper = mapper;
            _tokenHandler = tokenHandler;
            _signInManager = signInManager;
            _userService = userService;
        }

        public async Task<Token> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new NotFoundException("Kullanıcı adı veya şifre hatalı! Kullanıcı Bulunamadı...");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (result.Succeeded)
            {
                var roles = await _userManager.GetRolesAsync(user);
                Token token = _tokenHandler.CreateAccessToken(user,roles);
                await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration);
                return token;
            }
            else
            {
                throw new UnauthorizedAccessException("Kullanıcı adı veya şifre hatalı!");
            }
        }

        public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
        {
            User? user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if (user != null && user?.RefreshTokenEndDate > DateTime.UtcNow)
            {
                var roles = await _userManager.GetRolesAsync(user);
                Token token = _tokenHandler.CreateAccessToken(user,roles);
                await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration);
                return token;
            }
            else
            {
                throw new NotFoundException("Kullanıcı bulunumadı!");
            }
        }
    }
}
