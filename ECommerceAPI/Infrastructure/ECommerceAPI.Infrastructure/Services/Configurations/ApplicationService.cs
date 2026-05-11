using ECommerceAPI.Application.Abstractions.Services.Configurations;
using ECommerceAPI.Application.CustomAttributes;
using ECommerceAPI.Application.Dtos.Configurations;
using ECommerceAPI.Domain.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Infrastructure.Services.Configurations
{
    public class ApplicationService : IApplicationService
    {
        public List<Menu> GetAuthorizeDefinitionEndpoints(Type type)
        {
            Assembly assembly = Assembly.GetAssembly(type);
            var controllers = assembly.GetTypes().Where(t => t.IsAssignableTo(typeof(ControllerBase)));

            List<Menu> menus = new();

            if (controllers != null)
            {
                foreach (var controller in controllers)
                {
                    var actions = controller.GetMethods().Where(m => m.IsDefined(typeof(AuthorizeDefinitionAttribute)));
                    if (actions != null)
                    {
                        foreach (var action in actions)
                        {
                            var attributes = action.GetCustomAttributes(true);
                            if (attributes != null)
                            {
                                Menu menu = null;
                                var authorizeDefinitonAttribute = attributes.FirstOrDefault(a => a.GetType() == typeof(AuthorizeDefinitionAttribute)) as AuthorizeDefinitionAttribute;
                                if(!menus.Any(m => m.Name == authorizeDefinitonAttribute.Menu))
                                {
                                    menu = new() { Name =  authorizeDefinitonAttribute.Menu };
                                    menus.Add(menu);
                                }
                                else
                                {
                                    menu = menus.FirstOrDefault(m => m.Name == authorizeDefinitonAttribute.Menu);
                                }
                                Application.Dtos.Configurations.Action action2 = new()
                                {
                                    ActionType = Enum.GetName(typeof(ActionType), authorizeDefinitonAttribute.ActionType),
                                    Definition = authorizeDefinitonAttribute.Definition,
                                };

                                var httpAttribute = attributes.FirstOrDefault(a => a.GetType().IsAssignableTo(typeof(HttpMethodAttribute))) as HttpMethodAttribute;
                                if (httpAttribute != null)
                                {
                                    action2.HttpType = httpAttribute.HttpMethods.First();
                                }
                                else
                                {
                                    action2.HttpType = HttpMethods.Get;
                                }

                                action2.Code = $"{action2.HttpType}.{action2.ActionType}.{action2.Definition.Replace(" ", "")}";
                                menu.Actions.Add(action2);
                            }      
                        }
                    }
                }
            }
            return menus;
        }
    }
}
