using ECommerceAPI.Application.Features.Customers.Commands.Create;
using ECommerceAPI.Application.Mapping.CustomerMapper;
using ECommerceAPI.Application.Validators.CustomerValidator;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
                    cfg.RegisterServicesFromAssemblyContaining<CreateCustomerCommand>());

            services.AddValidatorsFromAssemblyContaining<CreateCustomerCommandValidator>();

            services.AddAutoMapper(typeof(CustomerProfile).Assembly);

            return services;
        }
    }
}
