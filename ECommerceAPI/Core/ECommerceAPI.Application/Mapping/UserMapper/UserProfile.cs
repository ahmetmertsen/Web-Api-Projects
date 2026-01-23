using AutoMapper;
using ECommerceAPI.Application.Dtos;
using ECommerceAPI.Application.Dtos.User;
using ECommerceAPI.Application.Features.AppUser.Commands.Create;
using ECommerceAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Mapping.UserMapper
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<CreateUserCommand, User>();
            CreateMap<CreateUserCommand, CreateUser>();
            CreateMap<CreateUser, User>();

            CreateMap<User,UserDto>();

            CreateMap<User, CreateUserCommandResponse>();
            
        }
    }
}
