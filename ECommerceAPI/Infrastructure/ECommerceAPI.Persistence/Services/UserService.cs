using AutoMapper;
using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Dtos.User;
using ECommerceAPI.Application.Features.AppUser.Commands.Create;
using ECommerceAPI.Application.UnitOfWork;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Domain.Entities.Identity;
using ECommerceAPI.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        
        public UserService(UserManager<User> userManager, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<CreateUserResponse> CreateAsync(CreateUser model)
        {
            var user = _mapper.Map<User>(model);
            user.UserName = model.Email;
            user.Email = model.Email;

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded == true)
            {
                var customer = new Customer()
                {
                    FullName = model.FullName,
                    UserId = user.Id
                };

                await _unitOfWork.CustomerRepository.AddAsync(customer);
                CancellationToken cancellationToken = new();
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new CreateUserResponse
                {
                    Succeeded = true,
                    Message = "Kullanıcı başarıyla kayıt olmuştur."
                };
            }
            else
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new CreateUserFailedException($"Kayıt sırasında hata oluştu. {errors}");
            }
        }

        public async Task UpdateRefreshToken(string refreshToken, User user, DateTime accesTokenDate)
        {
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = DateTime.UtcNow.AddDays(7);
                await _userManager.UpdateAsync(user);
            } else
            {
                throw new NotFoundException("Kullanıcı bulunamadı!");
            }    
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var response = _mapper.Map<List<UserDto>>(users);

            return response;
        }

        public async Task AssignRoleToUserAsync(int userId, string[] roles)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, userRoles);

                await _userManager.AddToRolesAsync(user, roles);
            }
        }

        public async Task<string[]> GetRolesToUserAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new NotFoundException("Kullanıcı bulunamadı.");
            }
            var userRoles = await _userManager.GetRolesAsync(user);
            return userRoles.ToArray();
        }
    }
}
