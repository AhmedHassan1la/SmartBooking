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
    public class LoginCommand : IRequest<Response<LoginDto>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
