using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.AppUser.Queries.GetRolesToUser
{
    public class GetRolesToUserQueryRequest : IRequest<GetRolesToUserQueryResponse>
    {
        public int UserId { get; set; }
    }
}
