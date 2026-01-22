using ECommerceAPI.Application.Abstractions.Token;
using ECommerceAPI.Application.Dtos;
using ECommerceAPI.Domain.Entities.Identity;
using ECommerceAPI.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.AppUser.Commands.Login
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserCommandResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenHandler _tokenHandler;

        public LoginUserCommandHandler(UserManager<User> userManager, SignInManager<User> signInManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user =  await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new NotFoundException("Kullanıcı adı veya şifre hatalı! Kullanıcı Bulunamadı...");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (result.Succeeded)
            {
                Token token = _tokenHandler.CreateAccessToken();
                return new LoginUserCommandResponse
                {
                    Succeeded = true,
                    Message = "Giriş işlemi başarılı.",
                    Token = token
                };
            } else
            {
                throw new UnauthorizedAccessException("Kullanıcı adı veya şifre hatalı!");
            }
            
        }
    }
}
