using MediatR;
using SmartBooking.Application.Responses;
using SmartBooking.Core.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBooking.Core.Features.User.Command.Models
{
    public class UpdateUserProfileCommand : IRequest<Response<UserProfileDto>>
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
