using AutoMapper;
using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Dtos.User;
using ECommerceAPI.Application.UnitOfWork;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Domain.Entities.Identity;
using ECommerceAPI.Domain.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ECommerceAPI.Application.Features.AppUser.Commands.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserCommandResponse>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            CreateUser userDto = _mapper.Map<CreateUser>(request);
            CreateUserResponse response = await _userService.CreateAsync(userDto);
            if (response.Succeeded)
            {
                return new CreateUserCommandResponse(true, "Kullanıcı başarıyla kayıt olmuştur.");
            } else
            {
                return new CreateUserCommandResponse(response.Succeeded, response.Message);
            }          
        }
    }
}
