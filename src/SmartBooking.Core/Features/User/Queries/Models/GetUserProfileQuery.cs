using MediatR;
using SmartBooking.Application.Responses;
using SmartBooking.Core.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBooking.Core.Features.User.Queries.Models
{
    public class GetUserProfileQuery : IRequest<Response<UserProfileDto>>
    {
        public string UserId { get; set; }

        public GetUserProfileQuery(string userId)
        {
            UserId = userId;
        }
    }
}
