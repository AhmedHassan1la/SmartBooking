using MediatR;
using SmartBooking.Application.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBooking.Core.Features.User.Command.Models
{
    public class RegisterCommand:IRequest<Response<string>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string DisplayName { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string ImageUrl { get; set; }
    }
}
